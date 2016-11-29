using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebGIS.Models
{
    public class VoteModel
    {
        /// <summary>
        /// true to upvote
        /// </summary>
        [Required]
        public bool upvote { get; set; }

        /// <summary>
        /// bathroom id
        /// </summary>
        [Required]
        public int id { get; set; }
    }
}