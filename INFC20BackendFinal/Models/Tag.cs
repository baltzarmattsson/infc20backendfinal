using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace INFC20BackendFinal.Models
{
    public class Tag
    {
        public string TagId { get; set; }

        public Tag() { }
        public Tag(string tag)
        {
            this.TagId = tag;
        }
    }
}