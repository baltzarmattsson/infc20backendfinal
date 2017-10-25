using INFC20BackendFinal.DataAccessLayer;
using INFC20BackendFinal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace INFC20BackendFinal.Controllers
{
    public class BidController : ApiController
    {
        // GET: api/Bid
        [HttpGet]
        [Route("api/Bid/GetBidsForListing/{listingId}")]
        public async Task<IHttpActionResult> GetBidsForListing(int listingId)
        {
            return Ok(BidDAL.GetBidsForListing(listingId));
        }
        

        // POST: api/Bid
        [HttpOptions]
        [HttpPost]
        public async Task<IHttpActionResult> Post([FromBody]Bid bid)
        {
            //return Ok("temp? " + temp);

             if (bid != null)
            {
                BidDAL.AddBid(bid);
                return Ok();
            }
            else
            {
                return BadRequest();
            }


        }

        // PUT: api/Bid/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Bid/5
        public void Delete(int id)
        {
        }
    }
}
