using AutoMapper;
using AutoMapper.QueryableExtensions;
using LostAndFound.Application.Common.Interfaces;
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

namespace LostAndFound.Application.Services.Items
{
    public class GetItemsQuery : IRequest<List<ItemDto>>
    {
        public ItemStatus? Status { get; set; }
        public string? SearchTerm { get; set; }
        public int UserId { get; set; }
    }

    public class GetItemsQueryHandler : IRequestHandler<GetItemsQuery, List<ItemDto>>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetItemsQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<ItemDto>> Handle(GetItemsQuery request, CancellationToken cancellationToken)
        {
            // Get all users
            var entities = await _dbContext.Items
                 .ProjectTo<ItemDto>(_mapper.ConfigurationProvider)
                 .ToListAsync(cancellationToken);

            // If we have passed a status, only get those users
            if (!string.IsNullOrEmpty(request.SearchTerm))
            {
                // Use equivalent of LIKE query on Title and Category
                entities = entities.Where(x => x.Title.Contains(request.SearchTerm) ||
                    x.Category.Contains(request.SearchTerm))
                    .ToList();
            }

            // If we have passed a status, only get those users
            if (request.Status.HasValue)
            {
                // Handles My Items case
                if (request.Status.Value == ItemStatus.Claimed)
                {
                    //entities = entities.Where(x => x.UserId == request.UserId).ToList();
                }
                else
                {
                    entities = entities.Where(x => x.Status.Equals(request.Status.Value)).ToList();
                }

            }

            // Order the users in descending order by UserId
            entities = entities.OrderByDescending(x => x.ItemId).ToList();

            return entities;
        }
    }
}
