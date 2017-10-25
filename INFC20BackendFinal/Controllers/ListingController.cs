﻿using INFC20BackendFinal.DataAccessLayer;
using INFC20BackendFinal.Models;
using System;
using System.Collections.Generic;
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
        public IHttpActionResult GetListings()
        {
            var allListings = ListingDAL.GetAllListings();
            return Ok(allListings);
        }

        // GET: api/Listing/5
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var listing = ListingDAL.GetListing(id);

            var bidsForListing = BidDAL.GetBidsForListing(id).Cast<Bid>();
            listing.Bids = bidsForListing.ToList();

            return Ok(listing);
        }

        // POST: api/Listing
        [HttpPost]
        public IHttpActionResult Post([FromBody]Listing listing)
        {
            if (listing != null)
            {
                int newId = ListingDAL.AddListing(listing);
                listing.Id = newId;
                return Ok(listing);
            } 
            else
            {
                return BadRequest();
            }

        }

        [HttpPost]
        [Route("api/Listing/UploadImageForListingId/{listingId}")]
        public IHttpActionResult UploadImageForListingId(int listingId)
        {

            Listing listing = ListingDAL.GetListing(listingId);

            if (listing == null)
            {
                return NotFound();
            }

            var httpRequest = HttpContext.Current.Request;

            if (httpRequest.Files.Count == 0)
            {
                return BadRequest();
            }

            var postedImage = httpRequest.Files[0];

            string saveDir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\infc20images\\";

            Directory.CreateDirectory(saveDir);
            var localFilePath = saveDir + postedImage.FileName;
            postedImage.SaveAs(localFilePath);

            listing.ImgUrl = localFilePath;

            ListingDAL.UpdateListing(listing);

            return Ok(listing);
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
