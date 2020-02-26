using System;
using System.Collections.Generic;

namespace LodgeDogDB.Models
{
    public partial class Owners
    {
        public Owners()
        {
            Bookings = new HashSet<Bookings>();
        }

        public int Number { get; set; }
        public DateTime? TimeStamp { get; set; }
        public string Fullservice { get; set; }
        public DateTime? Signupdate { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Primarycontact { get; set; }
        public string Phonenumber { get; set; }
        public string Alternatephonenumber { get; set; }
        public string Alternatephonenumber2 { get; set; }
        public string Email { get; set; }
        public string Alternateemail { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Membernumber { get; set; }
        public string Viplevel { get; set; }
        public int? Points { get; set; }
        public string Expiration { get; set; }
        public string Yearofuse { get; set; }
        public string Rcimembernumber { get; set; }
        public string Rcipoints { get; set; }
        public string Rciyearofuse { get; set; }
        public double? Rateofpay { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Rci1 { get; set; }
        public string Rci2 { get; set; }
        public short? Guestpasses { get; set; }
        public short? Reservationpasses { get; set; }
        public string Wynresemail { get; set; }

        public virtual ICollection<Bookings> Bookings { get; set; }
    }
}
