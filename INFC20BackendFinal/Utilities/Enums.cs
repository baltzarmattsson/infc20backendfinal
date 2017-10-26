using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace INFC20BackendFinal.Utilities
{
    enum ListProcedure
    {
        USP_ADD_LISTING,
        USP_GET_LISTING,
        USP_UPDATE_LISTING,
        USP_REMOVE_LISTING,
        USP_GET_ALL_LISTINGS_DESC
    }

    enum UserProcedure
    {
        USP_ADD_USER,
        USP_GET_USER,
        USP_UPDATE_USER,
        USP_REMOVE_USER
    }

    enum TagProcedure
    {
        USP_ADD_TAG,
        USP_ADD_TAG_TO_LISTING,
        USP_REMOVE_LISTING_TAG,
        USP_REMOVE_TAG,
        USP_GET_TAG,
        USP_GET_TAGS_FOR_LISTING,
        USP_GET_ALL_TAGS
    }

    enum BidProcedure
    {
        USP_ADD_BID,
        USP_UPDATE_BID,
        USP_GET_HIGHEST_BID_FOR_LISTING,
        USP_GET_BIDS_FOR_LISTING
    }

    enum ReviewProcedure
    {
        USP_ADD_REVIEW,
        USP_GET_REVIEW,
        USP_GET_REVIEWS_FOR_USER
    }
}