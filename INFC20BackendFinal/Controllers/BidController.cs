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
        public IHttpActionResult GetBidsForListing(int listingId)
        {
            return Ok(BidDAL.GetBidsForListing(listingId));
        }
        

        // POST: api/Bid
        [HttpPost]
        public IHttpActionResult Post([FromBody]Bid bid)
        {
            if (bid != null)
            {
                BidDAL.AddBid(bid);
                return Ok(bid);
            }
            else
            {
                return BadRequest();
            }
        }

        // DELETE: api/Bid/5
        public void Delete(int id)
        {
        }
    }
}
