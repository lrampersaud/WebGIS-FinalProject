using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebGIS.Models
{
    public class BathroomsInBoundingBox
    {
        /// <summary>
        /// top Y
        /// </summary>
        public double northEastBoundLatitude { get; set; }

        /// <summary>
        /// top X
        /// </summary>
        public double northEastBoundLongitude { get; set; }

        /// <summary>
        /// bottom Y
        /// </summary>
        public double southWestBoundLatitude { get; set; }

        /// <summary>
        /// bottom X
        /// </summary>
        public double southWestBoundLongitude { get; set; }

        /// <summary>
        /// Number of bathrooms
        /// </summary>
        public int amountBathrooms { get; set; }
    }
}