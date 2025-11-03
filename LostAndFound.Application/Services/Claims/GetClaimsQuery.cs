using AutoMapper;
using AutoMapper.QueryableExtensions;
using LostAndFound.Application.Common.Interfaces;
using LostAndFound.Application.Services.Claims.Models;
using LostAndFound.Application.Services.Items.Models;
using LostAndFound.Application.Services.Users.Models;
using LostAndFound.Domain;
using LostAndFound.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostAndFound.Application.Services.Claims
{
    public class GetClaimsQuery : IRequest<List<ClaimDto>>
    {
        public string? UserEmail { get; set; }
    }

    public class GetClaimsQueryHandler : IRequestHandler<GetClaimsQuery, List<ClaimDto>>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetClaimsQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<ClaimDto>> Handle(GetClaimsQuery request, CancellationToken cancellationToken)
        {
            var user = new User();

            if (!string.IsNullOrEmpty(request.UserEmail))
            {
                // Get User the Item will be linked to
                user = await _dbContext.Users.FirstOrDefaultAsync(e => e.Email.ToLower().Equals(request.UserEmail.ToLower()), cancellationToken);
            }
            else
            {
                user = null;
            }

            // Get all claims
            var entities = await _dbContext.Claims
                 .ProjectTo<ClaimDto>(_mapper.ConfigurationProvider)
                 .ToListAsync(cancellationToken);

            if (user != null)
            {
                entities = entities.Where(x => x.UserId == user.UserId).ToList();
            }

            // Order the users in descending order by UserId
            entities = entities.OrderByDescending(x => x.ClaimId).ToList();

            return entities;
        }
    }
}
