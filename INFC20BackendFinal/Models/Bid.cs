using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace INFC20BackendFinal.Models
{
    public class Bid
    {
        public string Email { get; set; }
        public int ListingId { get; set; }
        public double Amount { get; set; }

        public DateTime? TimeStamp { get; set; }

        public Bid() { }

        public Bid(string email, int listingId, double amount)
        {
            this.Email = email;
            this.ListingId = listingId;
            this.Amount = amount;
        }
    }
}