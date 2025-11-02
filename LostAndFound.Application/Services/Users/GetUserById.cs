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
        public string UserEmail { get; set; }
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
            // 1. Fetch user data using the Identity Service abstraction
            var userDto = await _identityService.GetUserIdAsync(request.UserEmail);

            return userDto;
        }
    }
}
