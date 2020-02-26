using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LodgeDogDB.Models
{
    public class PointsGroup
    {
        public int Number { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Membernumber { get; set; }
        public string Viplevel { get; set; }
        public int Points { get; set; }
        public double Rateofpay { get; set; }
        public int Guestpasses { get; set; }
        public int Reservationpasses { get; set; }
        public float Compatibility { get; set; }
    }
}
