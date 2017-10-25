using INFC20BackendFinal.Models;
using INFC20BackendFinal.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace INFC20BackendFinal.DataAccessLayer
{
    public class TagDAL
    {
        private static readonly Type type = new Tag().GetType();
        private static Dictionary<string, object> parameters;
        private static string procedure;
        private static string[] exceptionParams;

        public static Tag GetTag(string tagId)
        {
            procedure = TagProcedure.GET_TAG.ToString();
            parameters = new Dictionary<string, object>();
            parameters.Add("TagId", tagId);

            return Utils.Get(type, procedure, parameters).FirstOrDefault() as Tag;
        }

        public static List<object> GetListingsTags(int listingId)
        {
            procedure = TagProcedure.GET_TAGS_FOR_LISTING.ToString();
            parameters = new Dictionary<string, object>();
            parameters.Add("ListingId", listingId);

            return Utils.Get(type, procedure, parameters);
        }

        public static void AddTag(Tag tag)
        {
            procedure = TagProcedure.ADD_TAG.ToString();
            Utils.InsertEntity(tag, procedure, exceptionParams);
        }

        public static void AddTagToListing(Listing listing, Tag tag)
        {
            procedure = TagProcedure.ADD_TAG_TO_LISTING.ToString();
            parameters = new Dictionary<string, object>();

            if (listing != null && tag != null)
            {
                parameters.Add("ListingId", listing.Id);
                parameters.Add("TagId", tag.TagId);

                Utils.Insert(procedure, parameters);
            }
        }

        public static void AddTagToListing(int listingId, string tagId)
        {
            procedure = TagProcedure.ADD_TAG_TO_LISTING.ToString();
            parameters = new Dictionary<string, object>();

            if (listingId > 0 && tagId != null)
            {
                parameters.Add("ListingId", listingId);
                parameters.Add("TagId", tagId);

                Utils.Insert(procedure, parameters);
            }
        }

        public static void RemoveTagFromListing(Listing listing, Tag tag)
        {
            if (listing != null && tag != null)
                RemoveTagFromListing(listing.Id, tag.TagId);
        }

        public static void RemoveTagFromListing(int listingId, string tagId)
        {
            procedure = TagProcedure.REMOVE_LISTING_TAG.ToString();
            parameters = new Dictionary<string, object>();

            if (tagId != null && listingId > 0)
            {
                parameters.Add("ListingId", listingId);
                parameters.Add("TagId", tagId);

                Utils.Insert(procedure, parameters);
            }
        }

        public static void RemoveTag(Tag tag)
        {
            procedure = TagProcedure.REMOVE_TAG.ToString();
            Utils.InsertEntity(tag, procedure, exceptionParams);
        }

        public static void RemoveTag(string tagId)
        {
            procedure = TagProcedure.REMOVE_TAG.ToString();
            parameters = new Dictionary<string, object>();
            parameters.Add("TagId", tagId);

            Utils.Insert(procedure, parameters);
        }

        public static List<object> GetAllTags()
        {
            procedure = TagProcedure.GET_ALL_TAGS.ToString();
            return Utils.Get(type, procedure, parameters);
        }



    }
}