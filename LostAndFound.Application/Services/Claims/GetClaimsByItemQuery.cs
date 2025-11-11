using AutoMapper;
using AutoMapper.QueryableExtensions;
using LostAndFound.Application.Common.Interfaces;
using LostAndFound.Application.Services.Claims.Models;
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
    public class GetClaimsByItemQuery : IRequest<List<ClaimDto>>
    {
        public int ItemId { get; set; }
    }

    public class GetClaimsByItemQueryHandler : IRequestHandler<GetClaimsByItemQuery, List<ClaimDto>>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetClaimsByItemQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<ClaimDto>> Handle(GetClaimsByItemQuery request, CancellationToken cancellationToken)
        {
            // Get User the Item will be linked to
            var currentItem = await _dbContext.Items
                .FirstOrDefaultAsync(e => e.ItemId == request.ItemId, cancellationToken);

            var allLinkedClaims = await _dbContext.Claims
                .Where(e => e.ItemId == currentItem.ItemId)
                .Include(e => e.User)
                .Include(e => e.Item)
                 .ProjectTo<ClaimDto>(_mapper.ConfigurationProvider)
                 .ToListAsync(cancellationToken);

            return allLinkedClaims;
        }
    }
}
