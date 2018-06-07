using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quitchet.Core.Models.Agent
{
    //These 2 classer are used to get REquested listings group by Date(Array with child array type from JSON)
    public class AllColumns
    {
        public string PK_RequestShowingID { get; set; }
        public string FK_UserID { get; set; }
        public string AgentID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AOOfcName { get; set; }
        public string Phone2Nbr { get; set; }
        public string AGENT_IMAGE { get; set; }
        public string MLSID { get; set; }
        public string Class { get; set; }
        public string Status { get; set; }
        public string GeoLat { get; set; }
        public string GeoLong { get; set; }
        public string Is_Viewd { get; set; }
        public string PRP_ADDRESS { get; set; }
        public string PRP_PRICE { get; set; }
        public string BEDROOMS { get; set; }
        public string BATHROOMS { get; set; }
        public string SQFT { get; set; }
        public string PHOTO_COUNT { get; set; }
        public string PRP_IMAGES { get; set; }
        public string UserChoice1 { get; set; }
        public string Choice1FromTime1 { get; set; }
        public string Choice1ToTime1 { get; set; }
        public string Choice1FromTime2 { get; set; }
        public string Choice1ToTime2 { get; set; }
        public string UserChoice2 { get; set; }
        public string Choice2FromTime1 { get; set; }
        public string Choice2ToTime1 { get; set; }
        public string Choice2FromTime2 { get; set; }
        public string Choice2ToTime2 { get; set; }
        public string RequestStatus { get; set; }
        public string Choice1 { get; set; }
        public string Choice2 { get; set; }
        public string Is_CallMade { get; set; }
        public string Is_Confirmed { get; set; }
        public string SetAppointment_From { get; set; }
        public string SetAppointment_To { get; set; }
        public string Have_AgentFeedback { get; set; }
        public string AgentFeedback_PostDate { get; set; }
        public string AgentFeedback_PostTime { get; set; }
        public string PK_AgentShowingFeedbackID { get; set; }
        public string Have_BuyerFeedback { get; set; }
        public string BuyerFeedback_PostDate { get; set; }
        public string BuyerFeedback_PostTime { get; set; }
        public string PK_UserShowingFeedbackID { get; set; }
    }
    public class AgentShowingsModel
    {
        public List<AllColumns> AllListt;
        public string pk { get; set; }
        public string UserChoice1 { get; set; }
        public string UserChoice2 { get; set; }
        public string PRP_ADDRESS { get; set; }
        public string RequestStatus { get; set; }
        public string AgentScheduleDate { get; set; }
        public string SaveStage { get; set; }
        public string DATES { get; set; }
    }

    // This Class used to get Listings based on PKeys to show them in Schedule Popup(Step-1)
    public class ListingDetailsForPopupModel
    {
        public string PK_RequestShowingID { get; set; }
        public string MLSID { get; set; }
        public string Class { get; set; }
        public string Status { get; set; }
        public string PRP_ADDRESS { get; set; }
        public string PRP_PRICE { get; set; }
        public string BEDROOMS { get; set; }
        public string BATHROOMS { get; set; }
        public string SQFT { get; set; }
        public string PHOTO_COUNT { get; set; }
        public string PRP_IMAGES { get; set; }
        public string BuyerFirstName { get; set; }
        public string BuyerLastName { get; set; }
        public string BuyerPhoneNo { get; set; }
        public string BuyerImage { get; set; }

    }
    //Searched MLSID Details Class
    public class SearchedListingModel
    {
        public string MLSNO { get; set; }
        public string ADDRESS { get; set; }

    }

    //Class For Get Agent Details, Shceduled Time and Date in Confirm Map & Schedul;e Popup(Step-2)
    public class DetailColumns
    {
        public string MLSID { get; set; }
        public string Class { get; set; }
        public string Status { get; set; }
        public string GeoLat { get; set; }
        public string GeoLong { get; set; }
        public string PRP_ADDRESS { get; set; }
        public string PRP_PRICE { get; set; }
        public string PRP_IMAGES { get; set; }
        public string ArrivalTime { get; set; }
        public string IsCallMade { get; set; }
        public string IsConform { get; set; }
        public string SetAppointment_From { get; set; }
        public string SetAppointment_To { get; set; }
        public string Instructions { get; set; }
        public string WhoToContact { get; set; }
    }
    public class ScheduledListings_inPopup_Modal
    {
        public List<DetailColumns> AllListt;
        public string PK_REQUESTSHOW { get; set; }
        public string BuyerID { get; set; }
        public string BuyerFirstName { get; set; }
        public string BuyerLastName { get; set; }
        public string BuyerPhoneNo { get; set; }
        public string BuyerImage { get; set; }
        public string Schedule_Date { get; set; }
        public string Schedule_FromTime { get; set; }
        public string Schedule_ToTime { get; set; }
        public string AgentDwellTime { get; set; }
        public string SaveStage { get; set; }
        public string StartPoint { get; set; }
       
    }
    //Model Calss for Get Feedback in Showings
    public class ShowingsFeedbackModel
    {
        public string PK_AgentShowingFeedbackID { get; set; }
        public string MLSID { get; set; }
        public string PRP_ADDRESS { get; set; }
        public string PHOTO_COUNT { get; set; }
        public string PRP_IMAGES { get; set; }
        public string Comment { get; set; }
        public string Reating { get; set; }
        public string Top3 { get; set; }
        public string PriceCompare { get; set; }
        public string Exterior { get; set; }
        public string Interr { get; set; }
        public string Landscapping { get; set; }
        public string ConsidiringOffer { get; set; }
        public string FeatureToImprove { get; set; }
        public string WouldPreferToSave { get; set; }
        public string Feedback_PostDate { get; set; }
        public string Feedback_PostTime { get; set; }
     
    }
    public class RoutesModel
    {
        public string a { get; set; }
    }
    public class LegsModel
    {
        public string a { get; set; }
    }
}
