using System;
using System.Collections.Generic;

#nullable disable

namespace Core.Entities
{
    public partial class Role
    {
        public Role()
        {
            InverseRoleParente = new HashSet<Role>();
            Modules = new HashSet<Module>();
            Rolesmenus = new HashSet<Rolesmenu>();
            Usersroles = new HashSet<Usersrole>();
        }

        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
        public long RoleCreatedbyuser { get; set; }
        public DateTime RoleCreatedondate { get; set; }
        public long RoleUpdatedbyuser { get; set; }
        public DateTime RoleUpdatedondate { get; set; }
        public bool RoleActive { get; set; }
        public int? RoleParenteid { get; set; }

        public virtual Role RoleParente { get; set; }
        public virtual ICollection<Role> InverseRoleParente { get; set; }
        public virtual ICollection<Module> Modules { get; set; }
        public virtual ICollection<Rolesmenu> Rolesmenus { get; set; }
        public virtual ICollection<Usersrole> Usersroles { get; set; }
    }
}
