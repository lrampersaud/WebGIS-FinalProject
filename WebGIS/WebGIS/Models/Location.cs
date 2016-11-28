using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebGIS.Models
{
    public class Location
    {
        public int id { get; set; }
        public string name { get; set; }
        public string location { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public string image { get; set; }
        public double distance { get; set; }

        public string borough { get; set; }
        public bool openYearRound { get; set; }
        public bool handicap { get; set; }

        public List<Rating> ratingList { get; set; }

        public double rating { get; set; }

        public int upVotes { get; set; }
        public int downVotes { get; set; }

        public Location()
        {
            ratingList=new List<Rating>();
        }
    }
}