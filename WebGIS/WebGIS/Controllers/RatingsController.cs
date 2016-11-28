using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.WebSockets;
using WebGIS.Models;

namespace WebGIS.Controllers
{
    public class RatingsController : ApiController
    {
        // GET: api/Ratings
        /// <summary>
        /// get al ratings for bathroom
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public IEnumerable<Rating> Get(int id)
        {
            List<Rating> ratings = new List<Rating>();
            return ratings;
        }

        // GET: api/Ratings/5
        /// <summary>
        /// Get a specific rating
        /// </summary>
        /// <param name="id"></param>
        /// <param name="bathroom_id"></param>
        /// <returns></returns>
        public Rating Get(int id, int bathroom_id)
        {
            Rating rating = new Rating();
            return rating;
        }

        // POST: api/Ratings
        /// <summary>
        /// Update a rating
        /// </summary>
        /// <param name="rating"></param>
        public void Post([FromBody]Rating rating)
        {
        }

        // PUT: api/Ratings/5
        /// <summary>
        /// insert a rating
        /// </summary>
        /// <param name="id"></param>
        /// <param name="rating"></param>
        public void Put([FromBody]Rating rating)
        {
        }

        // DELETE: api/Ratings/5
        [ApiExplorerSettings(IgnoreApi = true)]
        public void Delete(int id)
        {
        }
    }
}
