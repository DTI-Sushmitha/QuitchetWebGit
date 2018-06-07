using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quitchet.Core.Models.Agent
{
    public class AgentCloserModel
    {
    }
    public class CloserListingsModel{
        public string PK_ClosingID { get; set; }
        public string MLSID { get; set; }
        public string FK_BuyerID { get; set; }
        public string Class { get; set; }
        public string Status { get; set; }
        public string GeoLat { get; set; }
        public string GeoLong { get; set; }
        public string PRP_ADDRESS { get; set; }
        public string PRP_PRICE { get; set; }
        public string BEDROOMS { get; set; }
        public string BATHROOMS { get; set; }
        public string SQFT { get; set; }
        public string PHOTO_COUNT { get; set; }
        public string PRP_IMAGES { get; set; }
    }//TO get All Closing Listing details based on type n Tab View
    public class AgentsListingSearchModel
    {
        public string MLSId { get; set; }
        public string ADDRESKKS { get; set; }
    }//to get Agent's active listings
    public class QuitchetAgentsSearchModel
    {
        public string AgentID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string AOOfcName { get; set; }
        public string PhotoPath { get; set; }
    }//to Get Agents when Serched in Add closing in Selling  TAb
    public class ClosingListDetailsModel {
        public string PK_ClosingID { get; set; }
        public string MLSID { get; set; }
        public string FK_BuyerID { get; set; }
        public string Class { get; set; }
        public string Status { get; set; }
        public string GeoLat { get; set; }
        public string GeoLong { get; set; }
        public string PRP_ADDRESS { get; set; }
        public string PRP_PRICE { get; set; }
        public string BEDROOMS { get; set; }
        public string BATHROOMS { get; set; }
        public string SQFT { get; set; }
        public string PHOTO_COUNT { get; set; }
        public string PRP_IMAGES { get; set; }
        public string SellerID { get; set; }
        public string Seller_FirstName { get; set; }
        public string Seller_LastName { get; set; }
        public string Seller_MobileNo { get; set; }
        public string Seller_Image { get; set; }
        public string ListingAgent_PkUserID { get; set; }
        public string ListingAgent_FirstName { get; set; }
        public string ListingAgent_LastName { get; set; }
        public string ListingAgentID { get; set; }
        public string ListingAgent_Primary_Color { get; set; }
        public string ListingAgent_Secondary_Color { get; set; }
        public string ListingAgent_MobileNo { get; set; }
        public string ListingAgent_Office { get; set; }
        public string ListingAgent_Image { get; set; }
        public string BuyerAgent_PKUserID { get; set; }
        public string BuyerAgent_Primary_Color { get; set; }
        public string BuyerAgent_Secondary_Color { get; set; }
        public string BuyerAgentID { get; set; }
        public string BuyerAgent_FirstName { get; set; }
        public string BuyerAgent_LastName { get; set; }
        public string BuyerAgent_MobileNo { get; set; }
        public string BuyerAgent_Office { get; set; }
        public string BuyerAgent_Image { get; set; }
        public string BuyerID { get; set; }
        public string Buyer_FirstName { get; set; }
        public string Buyer_LastName { get; set; }
        public string Buyer_Email { get; set; }
        public string Buyer_MobileNo { get; set; }
        public string Buyer_Image { get; set; }
        public string Closing_TypeID { get; set; }
    }//To get Closing List details in Manage View
    public class ClosingParties
    {
        public string PK_CloserPartyID { get; set; }
        public string Fk_ClosingID { get; set; }
        public string AssignedID { get; set; }
        public string MLSID { get; set; }
        public string Name { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string CloserPartyType { get; set; }
        public string CompanyName { get; set; }
        public string CloserImage_Path { get; set; }
        public string Status { get; set; }
        public string Created_Date { get; set; }
        public string ScheduleDate { get; set; }
        public string ScheduleTime { get; set; }
        public string Messages { get; set; }

    }//Class used to get Closing Parties in Manage View

    public class ClosingCloserMessageModel
    {
        public string Name { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string CompanyName { get; set; }
        public string CloserImage_Path { get; set; }
        public string CloserPartyType { get; set; }
        public string PK_ClosingID { get; set; }
        public string Closing_TypeID { get; set; }
        public string MLSID { get; set; }
        public string PRP_ADDRESS { get; set; }
        public string PHOTO_COUNT { get; set; }
        public string PRP_IMAGES { get; set; }
        public string AssignedID { get; set; }
        public string AssignedFirstName { get; set; }
        public string AssignedLastName { get; set; }
        public string AssignedEmail { get; set; }
        public string AssignedMobileNo { get; set; }
        public string AssignedImage { get; set; }
        public string ScheduleDate { get; set; }
        public string ScheduleTime { get; set; }
        public string MESSAGE { get; set; }
    }
}
