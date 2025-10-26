using AutoMapper;
using AutoMapper.QueryableExtensions;
using LostAndFound.Application.Common.Interfaces;
using LostAndFound.Application.Services.Claims;
using LostAndFound.Application.Services.Claims.Models;
using LostAndFound.Application.Services.Items.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostAndFound.Application.Services.Items
{
    public class GetItemByIdQuery : IRequest<ItemDto>
    {
        public int ItemId { get; set; }
    }

    public class GetItemByIdQueryHandler : IRequestHandler<GetItemByIdQuery, ItemDto>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetItemByIdQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ItemDto> Handle(GetItemByIdQuery request, CancellationToken cancellationToken)
        {
            // Get all users
            var entity = await _dbContext.Items
                .ProjectTo<ItemDto>(_mapper.ConfigurationProvider)
                .FirstAsync(e => e.ItemId == request.ItemId, cancellationToken);

            return entity;
        }
    }
}
