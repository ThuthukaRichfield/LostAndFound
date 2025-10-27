using AutoMapper;
using AutoMapper.QueryableExtensions;
using LostAndFound.Application.Common.Interfaces;
using LostAndFound.Application.Services.Claims.Models;
using LostAndFound.Application.Services.Disputes.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostAndFound.Application.Services.Disputes
{
    public class GetDisputeByIdQuery : IRequest<DisputeDto>
    {
        public int DisputeId { get; set; }
    }

    public class GetDisputeByIdQueryHandler : IRequestHandler<GetDisputeByIdQuery, DisputeDto>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetDisputeByIdQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<DisputeDto> Handle(GetDisputeByIdQuery request, CancellationToken cancellationToken)
        {
            // Get all users
            var entity = await _dbContext.Disputes
                .ProjectTo<DisputeDto>(_mapper.ConfigurationProvider)
                .FirstAsync(e => e.DisputeId == request.DisputeId, cancellationToken);

            return entity;
        }
    }
}
