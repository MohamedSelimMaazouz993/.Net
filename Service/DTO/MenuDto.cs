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
    public partial class MenuDto: IMapFrom<Menu>
    {
        public int MenId { get; set; }
        public string MenTitre { get; set; }
        public string MenDescription { get; set; }
        public string MemRouterlink { get; set; }
        public string MemHref { get; set; }
        public string MemIcon { get; set; }
        public string MemTarget { get; set; }
        public bool? MenHassubmenu { get; set; }
        public int? MenParentid { get; set; }
        public int? MenModuleid { get; set; }

        public virtual ModuleDto MenModule { get; set; }
        public virtual MenuDto MenParent { get; set; }
        public virtual ICollection<MenuDto> InverseMenParent { get; set; }
        public virtual ICollection<RolesmenuDto> Rolesmenus { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Menu, MenuDto>().ReverseMap();

        }
    }
}
