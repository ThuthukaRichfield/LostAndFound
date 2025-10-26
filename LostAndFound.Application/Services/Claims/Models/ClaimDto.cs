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
        }

        public int ClaimId { get; set; }
        public int UserId { get; set; }
        public int ItemId { get; set; }

        public static ClaimDto Create(
            int claimId,
            int userId,
            int itemId)
        {
            return new ClaimDto
            {
                ClaimId = claimId,
                UserId = userId,
                ItemId = itemId,
            };
        }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Claim, ClaimDto>();
        }
    }
}
