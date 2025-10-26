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

namespace LostAndFound.Application.Services.Disputes.Models
{
    public class DisputeDto : IMapFrom<Dispute>
    {
        public DisputeDto()
        {
            DisputeId = 0;
            Reason = string.Empty;
            Status = DisputeStatus.Open;
            ClaimId = 0;
        }

        public int DisputeId { get; set; }
        public string Reason { get; set; }
        public DisputeStatus Status { get; set; }
        public int ClaimId { get; set; }

        public static DisputeDto Create(
            int disputeId,
            string reason,
            DisputeStatus status,
            int claimId)
        {
            return new DisputeDto
            {
                DisputeId = disputeId,
                Reason = reason,
                Status = status,
                ClaimId = claimId,
            };
        }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Dispute, DisputeDto>();
        }
    }
}
