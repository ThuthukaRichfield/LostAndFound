using AutoMapper;
using AutoMapper.QueryableExtensions;
using LostAndFound.Application.Common.Interfaces;
using LostAndFound.Application.Services.Claims.Models;
using LostAndFound.Application.Services.Items.Models;
using LostAndFound.Application.Services.Users.Models;
using LostAndFound.Domain;
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
        public int? UserId { get; set; }
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
            // Get all users
            var entities = await _dbContext.Claims
                 .ProjectTo<ClaimDto>(_mapper.ConfigurationProvider)
                 .ToListAsync(cancellationToken);

            if (request.UserId.HasValue)
            {
                entities = entities.Where(x => x.UserId == request.UserId.Value).ToList();
            }

            // Order the users in descending order by UserId
            entities = entities.OrderByDescending(x => x.ClaimId).ToList();

            return entities;
        }
    }
}
