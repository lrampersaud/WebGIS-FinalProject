using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebGIS.Models;

namespace WebGIS.Controllers
{
    public class BathroomController : ApiController
    {


        // GET: api/Bathroom
        /// <summary>
        /// Send latitude and longitude to  obtain public restrooms
        /// </summary>
        /// <param name="Latitude">your current latutide</param>
        /// <param name="Longitude">your current longitude</param>
        /// <param name="AmountBathrooms">max number of bathrooms to return</param>
        /// <returns></returns>
        public IEnumerable<Location> Get(double latitude, double longitude, int amountBathrooms=30)
        {
            DataProvider provider = new DataProvider();
            return provider.GetCloseLocations(latitude, longitude, amountBathrooms);

        }
        // GET: api/Bathroom/5
        public Location Get(int id)
        {
            DataProvider provider = new DataProvider();
            return provider.GetBathroom(id);
        }

        // POST: api/Bathroom
        [ApiExplorerSettings(IgnoreApi = true)]
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Bathroom/5
        [ApiExplorerSettings(IgnoreApi = true)]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Bathroom/5
        [ApiExplorerSettings(IgnoreApi = true)]
        public void Delete(int id)
        {
        }
    }
}
