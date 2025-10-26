using AutoMapper;
using AutoMapper.QueryableExtensions;
using LostAndFound.Application.Common.Interfaces;
using LostAndFound.Application.Services.Claims.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostAndFound.Application.Services.Claims
{
    public class GetClaimByIdQuery : IRequest<ClaimDto>
    {
        public int ClaimId { get; set; }
    }

    public class GetClaimByIdQueryHandler : IRequestHandler<GetClaimByIdQuery, ClaimDto>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetClaimByIdQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ClaimDto> Handle(GetClaimByIdQuery request, CancellationToken cancellationToken)
        {
            // Get all users
            var entity = await _dbContext.Claims
                .ProjectTo<ClaimDto>(_mapper.ConfigurationProvider)
                .FirstAsync(e => e.ClaimId == request.ClaimId, cancellationToken);

            return entity;
        }
    }
}
