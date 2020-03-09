using System;
using System.Collections.Generic;

namespace LodgeDogDB.Models
{
    public partial class Notes
    {
        public DateTime TimeStamp { get; set; }
        public string Name { get; set; }
        public string Note { get; set; }
        public string Reason { get; set; }
        public int? Number { get; set; }

        public virtual Owners NumberNavigation { get; set; }
    }
}
