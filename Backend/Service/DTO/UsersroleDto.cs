using AutoMapper;
using Core.Entities;
using Service.Common.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTO
{
    public partial class UsersroleDto:IMapFrom<Usersrole>
    {
        public int UrUserid { get; set; }
        public int UrRoleid { get; set; }
        public bool? UrActive { get; set; }
        public long? UrCreatedbyuser { get; set; }
        public DateTime? UrCreatedondate { get; set; }
        public long? UrUpdatedbyuser { get; set; }
        public DateTime? UrUpdatedondate { get; set; }

        public virtual RoleDto UrRole { get; set; }
        public virtual UserDto UrUser { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Usersrole, UsersroleDto>().ReverseMap();

        }
    }
}
