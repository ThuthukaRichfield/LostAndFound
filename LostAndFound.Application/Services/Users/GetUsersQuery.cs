using AutoMapper;
using AutoMapper.QueryableExtensions;
using LostAndFound.Application.Common.Interfaces;
using LostAndFound.Application.Services.Users.Models;
using LostAndFound.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostAndFound.Application.Services.Users
{
    public class GetUsersQuery : IRequest<List<UserDto>>
    {
        public UserRole? Role { get; set; }
    }

    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, List<UserDto>>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public GetUsersQueryHandler(IApplicationDbContext dbContext, IMapper mapper, ICurrentUserService currentUserService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            // Get all users
           var entities = await _dbContext.Users
                .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            // If we have passed a status, only get those users
            if (request.Role.HasValue)
            {
                entities = entities.Where(x => x.Role.Equals(request.Role.Value)).ToList();
            }

            // Order the users in descending order by UserId
            entities = entities.OrderByDescending(x => x.UserId).ToList();

            return entities;
        }
    }
}
