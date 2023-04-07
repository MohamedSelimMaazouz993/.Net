using System;
using System.Collections.Generic;

#nullable disable

namespace Core.Entities
{
    public partial class Usersrole
    {
        public int UrUserid { get; set; }
        public int UrRoleid { get; set; }
        public bool? UrActive { get; set; }
        public long? UrCreatedbyuser { get; set; }
        public DateTime? UrCreatedondate { get; set; }
        public long? UrUpdatedbyuser { get; set; }
        public DateTime? UrUpdatedondate { get; set; }

        public virtual Role UrRole { get; set; }
        public virtual User UrUser { get; set; }
    }
}
