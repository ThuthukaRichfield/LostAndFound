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

namespace LostAndFound.Application.Services.Claims.Models
{
    public class ClaimDto : IMapFrom<Claim>
    {
        public ClaimDto()
        {
            ClaimId = 0;
            UserId = 0;
            ItemId = 0;
            CreatedBy = string.Empty;
        }

        public int ClaimId { get; set; }
        public int UserId { get; set; }
        public int ItemId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Location { get; set; }
        public DateTime DateLost { get; set; }
        public string Reason { get; set; }

        public static ClaimDto Create(
            int claimId,
            int userId,
            int itemId,
            string createdBy)
        {
            return new ClaimDto
            {
                ClaimId = claimId,
                UserId = userId,
                ItemId = itemId,
                CreatedBy = createdBy
            };
        }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Claim, ClaimDto>()
                .ForMember(d => d.CreatedBy, opts => opts.MapFrom(s => s.User.Email))
                .ForMember(d => d.Location, opts => opts.MapFrom(s => s.Item.Location))
                .ForMember(d => d.DateLost, opts => opts.MapFrom(s => s.Item.DateLost))
                .ForMember(d => d.Reason, opts => opts.MapFrom(s => s.FoundDescription));
        }
    }
}
