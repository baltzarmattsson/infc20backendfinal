using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace INFC20BackendFinal.Utilities
{
    enum ListProcedure
    {
        ADD_LISTING,
        GET_LISTING,
        UPDATE_LISTING,
        REMOVE_LISTING,
        GET_ALL_LISTINGS_DESC
    }

    enum UserProcedure
    {
        ADD_USER,
        GET_USER,
        UPDATE_USER,
        REMOVE_USER
    }

    enum TagProcedure
    {
        ADD_TAG,
        ADD_TAG_TO_LISTING,
        REMOVE_LISTING_TAG,
        REMOVE_TAG,
        GET_TAG,
        GET_TAGS_FOR_LISTING,
        GET_ALL_TAGS
    }

    enum BidProcedure
    {
        ADD_BID,
        UPDATE_BID,
        GET_HIGHEST_BID_FOR_LISTING,
        GET_BIDS_FOR_LISTING
    }

    enum ReviewProcedure
    {
        ADD_REVIEW,
        GET_REVIEW,
        GET_REVIEWS_FOR_USER
    }
}