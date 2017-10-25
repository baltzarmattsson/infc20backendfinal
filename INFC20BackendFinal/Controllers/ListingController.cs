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
    public class ListingController : ApiController
    {

        [HttpGet]
        public async Task<IHttpActionResult> GetListings()
        {
            var allListings = ListingDAL.GetAllListings();
            return Ok(allListings);
        }

        // GET: api/Listing/5
        [HttpGet]
        public async Task<IHttpActionResult> Get(int id)
        {
            var listing = ListingDAL.GetListing(id);

            var bidsForListing = BidDAL.GetBidsForListing(id).Cast<Bid>();
            listing.Bids = bidsForListing.ToList();

            return Ok(listing);
        }

        // POST: api/Listing
        [HttpPost]
        public async Task<IHttpActionResult> Post([FromBody]Listing listing)
        {
            if (listing != null)
            {
                ListingDAL.AddListing(listing);
            }

            return Ok();
        }

        // PUT: api/Listing/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Listing/5
        public void Delete(int id)
        {
        }
    }
}
