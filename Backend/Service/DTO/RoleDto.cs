using Core.Entities;
using Service.Common.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTO
{
    public partial class RoleDto : IMapFrom<Role>
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
        public long RoleCreatedbyuser { get; set; }
        public DateTime RoleCreatedondate { get; set; }
        public long RoleUpdatedbyuser { get; set; }
        public DateTime RoleUpdatedondate { get; set; }
        public bool RoleActive { get; set; }
        public int? RoleParenteid { get; set; }

        public virtual RoleDto RoleParente { get; set; }
        public virtual ICollection<RoleDto> InverseRoleParente { get; set; }
        public virtual ICollection<ModuleDto> Modules { get; set; }
        public virtual ICollection<RolesmenuDto> Rolesmenus { get; set; }
        public virtual ICollection<UsersroleDto> Usersroles { get; set; }
    }
}
