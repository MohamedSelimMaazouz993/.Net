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
    public partial class RolesmenuDto : IMapFrom<Rolesmenu>
    {
        public int RmRoleid { get; set; }
        public int RmMenuid { get; set; }
        public string RmName { get; set; }
        public bool? RmAjout { get; set; }
        public bool? RmSelection { get; set; }
        public bool? RmModif { get; set; }
        public bool? RmSupprime { get; set; }
        public bool? RmActive { get; set; }
        public long? RmCreatedbyapp { get; set; }
        public DateTime? RmCreatedondate { get; set; }
        public long? RmUpdatedbyapp { get; set; }
        public DateTime? RmUpdatedondate { get; set; }

        public virtual MenuDto RmMenu { get; set; }
        public virtual RoleDto RmRole { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Rolesmenu, RolesmenuDto>().ReverseMap();

        }
    }
}
