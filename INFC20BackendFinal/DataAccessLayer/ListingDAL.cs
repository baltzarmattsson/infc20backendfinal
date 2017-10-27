using INFC20BackendFinal.Models;
using INFC20BackendFinal.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace INFC20BackendFinal.DataAccessLayer
{
    public class ListingDAL
    {
        private static readonly Type type = new Listing().GetType();
        private static Dictionary<string, object> parameters;
        private static string procedure;
        private readonly static string[] exceptionParams = new string[] { "Id", "Published" };

        public static Listing GetListing(int id)
        {
            procedure = ListProcedure.USP_GET_LISTING.ToString();
            parameters = new Dictionary<string, object>();
            parameters.Add("Id", id);

            return Utils.Get(type, procedure, parameters, new string[] { "Bids" }).FirstOrDefault() as Listing;
        }

        public static int AddListing(Listing listing)
        {

            procedure = ListProcedure.USP_ADD_LISTING.ToString();

            object newId = Utils.InsertEntity(listing, procedure, new string[] { "Id", "Bids", "Published" });
            return Convert.ToInt32(newId.ToString());
        }

        public static void UpdateListing(Listing listing)
        {
            procedure = ListProcedure.USP_UPDATE_LISTING.ToString();
            Utils.UpdateEntity(listing, procedure, new string[] { "Title", "UserEmail", "Amount", "Bids", "Published" });
        }
        
        public static void RemoveListing(Listing listing)
        {
            procedure = ListProcedure.USP_REMOVE_LISTING.ToString();
            Utils.InsertEntity(listing, procedure, exceptionParams);
        }

        public static void RemoveListing(int id)
        {
            procedure = ListProcedure.USP_REMOVE_LISTING.ToString();
            parameters = new Dictionary<string, object>();
            parameters.Add("Id", id);

            Utils.Insert(procedure, parameters);
        }

        public static List<object> GetAllListings(bool filter)
        {
            procedure = ListProcedure.USP_GET_ALL_LISTINGS_DESC.ToString();
            var listings = new List<object>();
            var tmp = Utils.Get(type, procedure, null, null);

            if (filter)
            {
                foreach (Listing a in tmp)
                {
                    if (a.EndTime > DateTime.Now)
                        listings.Add(a);
                }
                return listings;
            } else
            {
                return tmp;
            }
        }
    }
}