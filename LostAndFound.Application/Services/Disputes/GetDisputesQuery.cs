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

namespace LostAndFound.Application.Services.Disputes
{
    public class GetDisputesQuery : IRequest<List<ClaimDto>>
    {
        public int UserId { get; set; }
    }

    public class GetDisputesQueryHandler : IRequestHandler<GetDisputesQuery, List<ClaimDto>>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetDisputesQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<ClaimDto>> Handle(GetDisputesQuery request, CancellationToken cancellationToken)
        {
            // Get all users
            var entities = await _dbContext.Claims
                 .ProjectTo<ClaimDto>(_mapper.ConfigurationProvider)
                 .ToListAsync(cancellationToken);

            // Order the users in descending order by UserId
            entities = entities.OrderByDescending(x => x.ClaimId).ToList();

            return entities;
        }
    }
}
