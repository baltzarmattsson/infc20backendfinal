using INFC20BackendFinal.Models;
using INFC20BackendFinal.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace INFC20BackendFinal.DataAccessLayer
{
    public class BidDAL
    {
        private static readonly Type type = new Bid().GetType();
        private static Dictionary<string, object> parameters;
        private static string procedure;
        private readonly static string[] exceptionParams = new string[] { "TimeStamp" };

        public static void AddBid(Bid bid)
        {
            procedure = BidProcedure.USP_ADD_BID.ToString();
            Utils.InsertEntity(bid, procedure, exceptionParams, false);
        }

        public static void AddBid(User user, Listing listing, double amount)
        {
            if (user != null && listing != null)
                AddBid(new Bid(user.Email, listing.Id, amount));
        }

        public static List<object> GetBidsForListing(int listingId)
        {
            procedure = BidProcedure.USP_GET_BIDS_FOR_LISTING.ToString();

            parameters = new Dictionary<string, object>();
            parameters.Add("ListingId", listingId);

            return Utils.Get(type, procedure, parameters, exceptionParams);
        }
    }
}