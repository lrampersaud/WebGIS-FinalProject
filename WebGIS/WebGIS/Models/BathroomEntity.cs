using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Web;

namespace WebGIS.Models
{
    public class BathroomEntity
    {
        public string Name { get; set; }
        public  string Address { get; set; }
        public string Borough { get; set; }
        public string Location { get; set; }
        public bool OpenYearRound { get; set; }
        public bool HandicapAccessible { get; set; }

        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public DbGeometry Geom { get; set; }

    }
}