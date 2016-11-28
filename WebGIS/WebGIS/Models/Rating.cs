using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebGIS.Models
{
    public class Rating
    {
        public int id { get; set; }
        public int location_id { get; set; }
        public string description { get; set; }
        public int starRating { get; set; }
    }
}