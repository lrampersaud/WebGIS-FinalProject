using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebGIS.Controllers
{
    public class VoteController : ApiController
    {

        // PUT: api/Vote/5
        /// <summary>
        /// call this to up or down vote a bathroom
        /// </summary>
        /// <param name="id">id of the bathroom</param>
        /// <param name="upVote">parameter name: upVote. true or false. true to upvote and false to downvote</param>
        /// <returns></returns>
        public bool Put(int id, [FromBody] bool upVote)
        {
            DataProvider provider = new DataProvider();
            try
            {
                provider.Vote(id, upVote);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
