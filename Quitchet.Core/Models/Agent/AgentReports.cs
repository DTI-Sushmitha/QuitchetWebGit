using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quitchet.Core.Models.Agent
{
    public class AgentReports
    {
    }
    public class MyAgentsModel
    {
        public string PK_UserID { get; set; }
        public string AgentID { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
    }//Class used to get Agents
    public class BuyersofMyAgentsModel
    { 
        public string FK_UserID { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
    }//Class used to get Buyers of Agents
    public class BuyersMetricsReportModel
    {
        public string BUYERS_COUNT { get; set; }
        public string TOTAL_CLOSED { get; set; }
        public string SHOWINGS_COUNT { get; set; }
        public string UNDER_CONTRACT { get; set; }
        public decimal AVG_BUYER_SALE { get; set; }
        public decimal AVG_SHOWING_SALE { get; set; }
        public string PERCENTAGE { get; set; }
    }//Class used to get MAin Mteric Report for Buyer Metric Reports
    public class BM_Listings_Model
    {
        public string MLSID { get; set; }
        public string Class { get; set; }
        public string Status { get; set; }
        public string Bathrooms { get; set; }
        public string Bedrooms { get; set; }
        public string GeoLat { get; set; }
        public string GeoLong { get; set; }
        public string SQFT { get; set; }
        public string PRP_ADDRESS { get; set; }
        public string PRP_PRICE { get; set; }
        public string SOLD_PRICE { get; set; }
        public string PRP_IMAGES { get; set; }
        public string Agent_FirstName { get; set; }
        public string Agent_LastName { get; set; }
        public string Office_Name { get; set; }
        public string Agent_Image { get; set; }
        public string Primary_Color { get; set; }
        public string Secondary_Color { get; set; }
    }//Class Used to get Listing details for Buyer Metrics
    public class ListingMetricsReportModel
    {
        public string TOTAL_LISTINGS { get; set; }
        public string TOTAL_CLOSED { get; set; }
        public string TOTAL_SHOWINGS { get; set; }
        public string TOTAL_OPENHOUSE { get; set; }
        public string TOTAL_SOLD_PRICE { get; set; }
        public string AVG_LIST_PRICE { get; set; }
        public string AVG_SALE_PRIC { get; set; }
        public string AVG_SHOWING_PER_SALE { get; set; }
        public string AVG_OPENHOUSE_PER_SALE { get; set; }
        public string AVG_DAYS_ON_MARKET { get; set; }
    }//class used to get to main report for Listing Metrics
    public class ListingDetails_ListingMetrics
    {
        public string MLSID { get; set; }
        public string Class { get; set; }
        public string Status { get; set; }
        public string BEDROOMS { get; set; }
        public string BATHROOMS { get; set; }
        public string GeoLat { get; set; }
        public string GeoLong { get; set; }
        public string SQFT { get; set; }
        public string PRP_ADDRESS { get; set; }
        public string PRP_PRICE { get; set; }
        public string SOLD_PRICE { get; set; }
        public string PRP_IMAGES { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AOOfcName { get; set; }
        public string PhotoLocation { get; set; }
        public string Total_OpenHouses { get; set; }
        public string Total_Showings { get; set; }
        public string Primary_Color { get; set; }
        public string Secondary_Color { get; set; }
    }
    public class BD_BuyerDetailsReportModel
    {
        public string Buyer_PkUserID { get; set; }
        public string Buyer_FirstName { get; set; }
        public string Buyer_LastName { get; set; }
        public string Buyer_MobilNo { get; set; }
        public string Buyer_Image { get; set; }
        public string AgentPkUserID { get; set; }
        public string AgentID { get; set; }
        public string Agent_FirstName { get; set; }
        public string Agent_LastName { get; set; }
        public string Agent_Office { get; set; }
        public string Agent_Type { get; set; }
        public string Agent_Image { get; set; }
        public string Days { get; set; }
        public string DaysFrom { get; set; }
        public string DaysTo { get; set; }
        public string Showings { get; set; }
        public string Closer { get; set; }
        public string Minimum { get; set; }
        public string Maximum { get; set; }
        public string Buyer_Note { get; set; }
        public string Primary_Color { get; set; }
        public string Secondary_Color { get; set; }
    }
    public class LD_ListingDetailsReportModel
    {
        public string MLSID { get; set; }
        public string ADDRESS { get; set; }
        public string MLSNo { get; set; }
        public string Class { get; set; }
        public string Status { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string PHOTOS { get; set; }
        public string OrigPrice { get; set; }
        public string ListPrice { get; set; }
        public string SoldPrice { get; set; }
        public string LstDate { get; set; }
        public string CloseDate { get; set; }
        public string DaysInQuitchet { get; set; }
        public string Showings_Count { get; set; }
        public string OpenHouse_Count { get; set; }
        public string Seller_PKUserID { get; set; }
        public string Seller_FirstName { get; set; }
        public string Seller_LastName { get; set; }
        public string Seller_MobileNo { get; set; }
        public string Seller_Image { get; set; }
        public string ListingAgent_AgentID { get; set; }
        public string ListingAgent_Email { get; set; }
        public string ListingAgent_MobileNo { get; set; }
        public string ListingAgent_PkUserID { get; set; }
        public string ListingAgent_FirstName { get; set; }
        public string ListingAgent_LastName { get; set; }
        public string ListingAgent_Office { get; set; }
        public string ListingAgent_Primary { get; set; }
        public string ListingAgent_Secondary { get; set; }
        public string ListingAgent_Image { get; set; }
    }
}
