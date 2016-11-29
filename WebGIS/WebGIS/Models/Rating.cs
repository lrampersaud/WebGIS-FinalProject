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

        /// <summary>
        /// rating description
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// star rating value 1-5
        /// </summary>
        public int starRating { get; set; }
    }
}