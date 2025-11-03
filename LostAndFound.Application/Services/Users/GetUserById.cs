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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
        private readonly IIdentityService _identityService;

        public GetUserByIdHandler(IApplicationDbContext dbContext, IMapper mapper, ICurrentUserService currentUserService, IIdentityService identityService)
        {
            _dbContext = dbContext;
            _mapper = mapper; 
            _identityService = identityService;
        }

        public async Task<UserDto> Handle(GetUserById request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users
                .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(u => u.UserId == request.UserId, cancellationToken);

            if (user != null)
            {
                // Fetch user data using the Identity Service abstraction
                var identityUser = await _identityService.GetUserIdAsync(user.Email);

                if (identityUser == null)
                {
                    user = null;
                }
                else
                {
                    user.Email = identityUser.Email;
                }
            }

            return user;
        }
    }
}
