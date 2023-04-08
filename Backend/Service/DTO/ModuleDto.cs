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
    public partial class ModuleDto : IMapFrom<Module>
    {
        public int ModId { get; set; }
        public string ModName { get; set; }
        public string ModDescription { get; set; }
        public bool? ModActive { get; set; }
        public int? ModIdMenu { get; set; }
        public long? ModCreatedbyuser { get; set; }
        public DateTime? ModCreatedondate { get; set; }
        public long? ModUpdatedbyuser { get; set; }
        public DateTime? ModUpdatedondate { get; set; }

        public virtual RoleDto ModIdMenuNavigation { get; set; }
        public virtual ICollection<MenuDto> Menus { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Module, ModuleDto>().ReverseMap();

        }
    }
}
