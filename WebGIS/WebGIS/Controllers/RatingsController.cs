using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
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
        /// get all ratings for bathroom
        /// </summary>
        /// <param name="Id">id of the bathroom</param>
        /// <returns></returns>
        public IEnumerable<Rating> Get(int id)
        {
            DataProvider provider=new DataProvider();
            return provider.GetAllRatingsForBathroom(id);
        }

        // GET: api/Ratings/5
        /// <summary>
        /// Get a specific rating. 2 parameters because i can't use 1 again
        /// </summary>
        /// <param name="id">rating id</param>
        /// <param name="bathroom_id">bathroom id</param>
        /// <returns></returns>
        public Rating Get(int id, int bathroom_id)
        {
            DataProvider provider = new DataProvider();
            return provider.GetRating(id);
        }

        // POST: api/Ratings
        /// <summary>
        /// Update a rating
        /// </summary>
        /// <param name="rating">rating information</param>
         [ApiExplorerSettings(IgnoreApi = true)]
        public void Post([FromBody]Rating rating)
        {
        }

        // PUT: api/Ratings/5
        /// <summary>
        /// insert a rating
        /// </summary>
        /// <param name="id">id of the bathroo</param>
        /// <param name="rating">rating information</param>
        public bool Put([FromBody]Rating rating)
        {
            rating.description = HttpUtility.HtmlEncode(rating.description);
            DataProvider provider = new DataProvider();
            return provider.InsertRating(rating);
        }

        // DELETE: api/Ratings/5
        [ApiExplorerSettings(IgnoreApi = true)]
        public void Delete(int id)
        {
        }
    }
}
