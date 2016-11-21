using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebGIS.Controllers
{
    public class BathroomController : ApiController
    {
        // GET: api/Bathroom
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Bathroom/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Bathroom
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Bathroom/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Bathroom/5
        public void Delete(int id)
        {
        }
    }
}
