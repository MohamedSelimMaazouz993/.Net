using System;
using System.Collections.Generic;

#nullable disable

namespace Core.Entities
{
    public partial class Module
    {
        public Module()
        {
            Menus = new HashSet<Menu>();
        }

        public int ModId { get; set; }
        public string ModName { get; set; }
        public string ModDescription { get; set; }
        public bool? ModActive { get; set; }
        public int? ModIdMenu { get; set; }
        public long? ModCreatedbyuser { get; set; }
        public DateTime? ModCreatedondate { get; set; }
        public long? ModUpdatedbyuser { get; set; }
        public DateTime? ModUpdatedondate { get; set; }

        public virtual Role ModIdMenuNavigation { get; set; }
        public virtual ICollection<Menu> Menus { get; set; }
    }
}
