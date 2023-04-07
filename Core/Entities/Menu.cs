using System;
using System.Collections.Generic;

#nullable disable

namespace Core.Entities
{
    public partial class Menu
    {
        public Menu()
        {
            InverseMenParent = new HashSet<Menu>();
            Rolesmenus = new HashSet<Rolesmenu>();
        }

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

        public virtual Module MenModule { get; set; }
        public virtual Menu MenParent { get; set; }
        public virtual ICollection<Menu> InverseMenParent { get; set; }
        public virtual ICollection<Rolesmenu> Rolesmenus { get; set; }
    }
}
