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
    public class GetUserById : IRequest<UserDto>
    {
        public int UserId { get; set; }
    }

    public class GetUserByIdHandler : IRequestHandler<GetUserById, UserDto>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public GetUserByIdHandler(IApplicationDbContext dbContext, IMapper mapper, ICurrentUserService currentUserService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<UserDto> Handle(GetUserById request, CancellationToken cancellationToken)
        {
            // Get all users
            var entity = await _dbContext.Users
                 .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                 .FirstAsync(e => e.UserId == request.UserId);

            return entity;
        }
    }
}
