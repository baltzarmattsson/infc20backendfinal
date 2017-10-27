using INFC20BackendFinal.Models;
using INFC20BackendFinal.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

        public static List<object> GetBidsForListing(int listingId)
        {
            procedure = BidProcedure.USP_GET_BIDS_FOR_LISTING.ToString();

            parameters = new Dictionary<string, object>();
            parameters.Add("ListingId", listingId);

            return Utils.Get(type, procedure, parameters, exceptionParams);
        }

        public static bool IsBidOK(Bid bid)
        {
            if (bid != null)
            {
                Listing listing = ListingDAL.GetListing(bid.ListingId);
                if (DateTime.Now <= listing.EndTime)
                {
                    double highestBid = GetHighestBidForListing(bid.ListingId);
                    if (bid.Amount > highestBid)
                        return true;
                }
            }
            return false;
        }
        private static double GetHighestBidForListing(int listingId)
        {
            procedure = BidProcedure.USP_GET_HIGHEST_BID_FOR_LISTING.ToString();
            
            using (SqlConnection con = Connector.Connect())
            {
                using (SqlCommand cmd = new SqlCommand(procedure, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("ListingId", listingId);

                    using (SqlDataReader dataReader = cmd.ExecuteReader())
                    {
                        while (dataReader.Read() && dataReader.HasRows)
                        {
                            double highestBid = Convert.ToDouble(dataReader["Amount"]);
                            return highestBid;
                        }
                    }
                }
            }
            return 0.0;
        }

    }
}