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
        public string northEastBoundLatitude { get; set; }

        /// <summary>
        /// top X
        /// </summary>
        public string northEastBoundLongitude { get; set; }

        /// <summary>
        /// bottom Y
        /// </summary>
        public string southWestBoundLatitude { get; set; }

        /// <summary>
        /// bottom X
        /// </summary>
        public string southWestBoundLongitude { get; set; }
        public int amountBathrooms { get; set; }
    }
}