using System;
using System.Collections.Generic;

namespace LodgeDogDB.Models
{
    public partial class Bookings
    {
        public DateTime TimeStamp { get; set; }
        public string Source { get; set; }
        public string Primaryguestname { get; set; }
        public int? Numberofoccupants { get; set; }
        public string Property { get; set; }
        public string Unittype { get; set; }
        public DateTime? Datebookingmade { get; set; }
        public DateTime? Checkin { get; set; }
        public DateTime? Checkout { get; set; }
        public int Number { get; set; }
        public double? Baserateofpay { get; set; }
        public string Rci { get; set; }
        public double? Tri { get; set; }
        public int? Pointsused { get; set; }
        public string Reservationpassesneeded { get; set; }
        public string Reservationpassespurchased { get; set; }
        public string Guestpassesadded { get; set; }
        public string Guestpassespurchased { get; set; }
        public string Wyndhamconfirmationnumber { get; set; }

        public virtual Owners NumberNavigation { get; set; }
    }
}
