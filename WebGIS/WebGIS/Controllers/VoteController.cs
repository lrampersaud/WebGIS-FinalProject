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
    public class VoteController : ApiController
    {

        // POST: api/Vote/5
        /// <summary>
        /// call this to up or down vote a bathroom
        /// </summary>
        /// <param name="id">id of the bathroom</param>
        /// <param name="upVote">parameter name: upVote. true or false. true to upvote and false to downvote</param>
        /// <returns></returns>
        [HttpPost]
        [ApiExplorerSettings(IgnoreApi = true)]
        public bool Post([FromBody] VoteModel upvote)
        {
            DataProvider provider = new DataProvider();
            try
            {
                provider.Vote(upvote.id, upvote.upvote);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // PUT: api/Vote/5
        /// <summary>
        /// call this to up or down vote a bathroom
        /// </summary>
        /// <param name="upvote"></param>
        /// <returns></returns>
        [HttpPut]
        public bool Put([FromBody] VoteModel upvote)
        {
            DataProvider provider = new DataProvider();
            try
            {
                provider.Vote(upvote.id, upvote.upvote);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
