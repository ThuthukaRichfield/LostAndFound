using AutoMapper;
using Intent.RoslynWeaver.Attributes;
using LostAndFound.Application.Common.Mappings;
using LostAndFound.Domain;
using LostAndFound.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostAndFound.Application.Services.Items.Models
{
    public class ItemDto : IMapFrom<Item>
    {
        public ItemDto()
        {
            Title = null!;
            Category = null!;
            CreatedDate = new DateTime();
            CreatedBy = null!;
            LastModifiedDate = new DateTime();
            LastModifiedBy = null!;
            Status = ItemStatus.Lost;
        }

        public int ItemId { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public string? LastModifiedBy { get; set; }
        public ItemStatus Status { get; set; }

        public static ItemDto Create(
            int itemId,
            string title,
            string category,
            DateTime createdDate,
            string createdBy,
            DateTime? lastModifiedDate,
            string? lastModifiedBy,
            ItemStatus status)
        {
            return new ItemDto
            {
                ItemId = itemId,
                Title = title,
                Category = category,
                CreatedDate = createdDate,
                CreatedBy = createdBy,
                LastModifiedDate = lastModifiedDate,
                LastModifiedBy = lastModifiedBy,
                Status = status,
            };
        }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Item, ItemDto>();
        }
    }
}
