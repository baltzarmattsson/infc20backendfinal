using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace INFC20BackendFinal.Models
{
    public class Listing
    {
        public int Id { get; set; } // auto incremental
        public DateTime? Published { get; set; } //auto incremental, set nullable

        public DateTime EndTime { get; set; }
        public string Title { get; set; }
        public string ImgUrl { get; set; }
        public string Description { get; set; }
        public string UserEmail { get; set; }

        public Listing() { }

        public Listing(DateTime endTime, string title, string imgUrl, string description, string userEmail)
        {
            this.EndTime = endTime;
            this.Title = title;
            this.ImgUrl = imgUrl;
            this.Description = description;
            this.UserEmail = userEmail;
        }
    }
}