﻿using INFC20BackendFinal.DataAccessLayer;
using INFC20BackendFinal.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace INFC20BackendFinal.Controllers
{
    public class ListingController : ApiController
    {

        [HttpGet]
        public async Task<HttpResponseMessage> GetListings()
        {
            try
            {
                var allListings = ListingDAL.GetAllListings(true);
                return Request.CreateResponse(HttpStatusCode.OK, allListings);
            }
            catch (SqlException sqle)
            {

                string message = ExceptionHandler.HandleSqlException(sqle);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, message);
            }
        }

        // GET: api/Listing/5
        [HttpGet]
        [Route("api/Listing/Get/{id}")]
        public async Task<HttpResponseMessage> Get(int id)
        {

            try
            {
                var listing = ListingDAL.GetListing(id);
                var bidsForListing = BidDAL.GetBidsForListing(id).Cast<Bid>();
                listing.Bids = bidsForListing.ToList();
                return Request.CreateResponse(HttpStatusCode.OK, listing);
            }
            catch (SqlException sqle)
            {
                string message = ExceptionHandler.HandleSqlException(sqle);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, message);
            }
        }

        // GET: api/Listing/GetListingsByEmail
        [HttpPost]
        [Route("api/Listing/GetListingsByEmail")]
        public async Task<HttpResponseMessage> GetListingsByEmail([FromBody]string email)
        {
            if (email != null)
            {
                try
                {
                    var allListings = ListingDAL.GetAllListings(false).Cast<Listing>();
                    var filtered = allListings.Where(listing => listing.UserEmail == email).ToList();
                    return Request.CreateResponse(HttpStatusCode.OK, filtered);
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
        
        // POST: api/Listing
        [HttpPost]
        public async Task<HttpResponseMessage> Post([FromBody]Listing listing)
        {
            if (listing != null)
            {
                try
                {
                    int newId = ListingDAL.AddListing(listing);
                    listing.Id = newId;
                    return Request.CreateResponse(HttpStatusCode.OK, listing);
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

        [HttpPost]
        [Route("api/Listing/UploadImageForListingId/{listingId}")]
        public async Task<HttpResponseMessage> UploadImageForListingId(int listingId)
        {

            try
            {
                Listing listing = ListingDAL.GetListing(listingId);

                if (listing == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "");
                }

                var httpRequest = HttpContext.Current.Request;

                if (httpRequest.Files.Count == 0)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "");
                }

                var postedImage = httpRequest.Files[0];

                string referencablePathByFrontEnd = "assets\\mock-data\\listing-images\\" + listingId + "\\";
                // desktop - string saveDir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Systemvetare\\INFC20Project\\buysellapp\\src\\" + referencablePathByFrontEnd;
                string saveDir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Systemvetare\\INFC20Frontend\\src\\" + referencablePathByFrontEnd;

                Directory.CreateDirectory(saveDir);
                var localFilePath = saveDir + postedImage.FileName;
                postedImage.SaveAs(localFilePath);

                listing.ImgUrl = referencablePathByFrontEnd + postedImage.FileName;

                ListingDAL.UpdateListing(listing);

                return Request.CreateResponse(HttpStatusCode.OK, listing);
            }
            catch (SqlException sqle)
            {

                string message = ExceptionHandler.HandleSqlException(sqle);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, message);
            }
        }

        // PUT: api/Listing/5
        public async Task<HttpResponseMessage> Put([FromBody]Listing listing)
        {
            if (listing != null)
            {
                try
                {
                    ListingDAL.UpdateListing(listing);
                    return Request.CreateResponse(HttpStatusCode.OK, listing);
                }
                catch (SqlException sqle)
                {

                    string message = ExceptionHandler.HandleSqlException(sqle);
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, message);
                }
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "");
            }
        }

    }
}
