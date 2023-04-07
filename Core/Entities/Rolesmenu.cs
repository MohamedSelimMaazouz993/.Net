using System;
using System.Collections.Generic;

#nullable disable

namespace Core.Entities
{
    public partial class Rolesmenu
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

        public virtual Menu RmMenu { get; set; }
        public virtual Role RmRole { get; set; }
    }
}
