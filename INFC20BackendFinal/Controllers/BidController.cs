using INFC20BackendFinal.DataAccessLayer;
using INFC20BackendFinal.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
        public HttpResponseMessage GetBidsForListing(int listingId)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, BidDAL.GetBidsForListing(listingId));
            }
            catch (SqlException sqle)
            {

                string message = ExceptionHandler.HandleSqlException(sqle);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, message);
            }
       }
        

        // POST: api/Bid
        [HttpPost]
        public HttpResponseMessage Post([FromBody]Bid bid)
        {
            if (bid != null)
            {
                try
                {
                    if (BidDAL.IsBidOK(bid))
                    {
                        BidDAL.AddBid(bid);
                        return Request.CreateResponse(HttpStatusCode.OK, bid);
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Bid too low, plez try again");
                    }
                }
                catch (SqlException sqle)
                {

                    string message = ExceptionHandler.HandleSqlException(sqle);
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, message);
                }
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "");
            }
        }

        // DELETE: api/Bid/5
        public void Delete(int id)
        {
        }
        
    }
}
