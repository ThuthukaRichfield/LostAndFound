using AutoMapper;
using AutoMapper.QueryableExtensions;
using LostAndFound.Application.Common.Interfaces;
using LostAndFound.Application.Services.Claims.Models;
using LostAndFound.Application.Services.Disputes.Models;
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

namespace LostAndFound.Application.Services.Disputes
{
    public class GetDisputesQuery : IRequest<List<DisputeDto>>
    {
    }

    public class GetDisputesQueryHandler : IRequestHandler<GetDisputesQuery, List<DisputeDto>>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetDisputesQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<DisputeDto>> Handle(GetDisputesQuery request, CancellationToken cancellationToken)
        {
            // Get all disputes
            var entities = await _dbContext.Disputes
                 .ProjectTo<DisputeDto>(_mapper.ConfigurationProvider)
                 .ToListAsync(cancellationToken);

            // Order the disputes in descending order by DisputeId
            entities = entities.OrderByDescending(x => x.DisputeId).ToList();

            return entities;
        }
    }
}
