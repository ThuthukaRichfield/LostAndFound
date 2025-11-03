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

namespace LostAndFound.Application.Services.Users.Models
{
    public class UserDto : IMapFrom<User>
    {
        public UserDto()
        {
            Email = null!;
        }

        public int UserId { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public UserRole Role { get; set; }

        public static UserDto Create(
            int userId,
            string email,
            UserRole role)
        {
            return new UserDto
            {
                UserId = userId,
                Email = email,
                Role = role,
            };
        }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<User, UserDto>();
        }
    }
}
