using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quitchet.Core.Models.Agent
{
    public class AgentListingModel
    {
        public string Photolocation { get; set; }
        public string MLSId { get; set; }
        public string ListingPrice { get; set; }
        public string SoldPrice { get; set; }
        public string ADDRESKKS { get; set; }
        public string Is_Viewd { get; set; }
        public string PostedDate { get; set; }
        public string Have_Seller { get; set; }
        public string ExpireDate { get; set; }
        public string Sale_Rent { get; set; }
        public string ADDRESS { get; set; }
        public string MLSNO { get; set; }
        public string Name { get; set; }
        public string AgentID { get; set; }
        public string PhotoPath { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IMAGE { get; set; }

        public string SellerFirstName { get; set; }
        public string SellerLastName { get; set; }
        public string SellerImage { get; set; }
        public string MLSID { get; set; }
        public string Class { get; set; }
        public string Status { get; set; }
        public string GeoLat { get; set; }
        public string GeoLong { get; set; }
        public string PRP_ADDRESS { get; set; }
        public string PRP_IMAGES { get; set; }
        public string PRP_PRICE { get; set; }

        public string HostAgentID { get; set; }
        public string DATE { get; set; }
        public string FromTime { get; set; }
        public string ToTime { get; set; }
        public string HostFirstName { get; set; }
        public string HostLastName { get; set; }
        public string HostImage { get; set; }
        public string HostCompany { get; set; }
        public string PK_OpenHouseID { get; set; }
        public string OpenHouseType { get; set; }
        public string Notes { get; set; }
        public string Location { get; set; }

        public string RETS_AgentID { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Photo_Path { get; set; }
        public string PK_RequestShowingID { get; set; }
        public string FK_UserID { get; set; }
        public string AgentFirstName { get; set; }
        public string AgentLastName { get; set; }
        public string AgentOfficeName { get; set; }
        public string AgentImage { get; set; }
        public string Schedule_Date { get; set; }
        public string Schedule_From_Time { get; set; }
        public string Schedule_To_Time { get; set; }
        public string SetAppointment_From { get; set; }
        public string SetAppointment_To { get; set; }

        public string GeoLatitude { get; set; }
        public string GeoLongitude { get; set; }
        public string City { get; set; }
        public string Bedrooms { get; set; }
        public string BathRooms { get; set; }
        public string PhotoCount { get; set; }
        public string ApproxAcres { get; set; }
        public string ShowingAgentID { get; set; }
        public string ScheduleDate { get; set; }
        public string ScheduleFromTime { get; set; }
        public string ScheduleToTime { get; set; }

        public string For_AgentFeedbackPKID { get; set; }
        public string AgentMobileNo { get; set; }
        public string AgentEmail { get; set; }
        public string AgentBrokerage { get; set; }
        public string CompletedDate { get; set; }
        public string CompletedFromTime { get; set; }
        public string CompletedToTime { get; set; }
        public string For_BuyerFeedbackPKID { get; set; }

        //For Showing Feedback//
        public string Feedback_PostDate { get; set; }
        public string Feedback_PostTime { get; set; }
        public string Reating { get; set; }
        public string Comment { get; set; }
        public string Top3 { get; set; }
        public string PriceCompare { get; set; }
        public string Exterior { get; set; }
        public string Interr { get; set; }
        public string Landscapping { get; set; }
        public string ConsidiringOffer { get; set; }
        public string FeatureToImprove { get; set; }
        public string WouldPreferToSave { get; set; }
        public string Is_Viewed { get; set; }
        public string PK_ShowingFeedbackID { get; set; }
        public string Reminder { get; set; }
        //edit alerts
        public string PK_MyListingAlertsID { get; set; }
        public string ReadBeforeSendingToSeller { get; set; }
        public string NofiticationToSellerInCheckIN { get; set; }
        public string NofiticationToSellerInCheckOUT { get; set; }
        public string ContactToNotify { get; set; }
        public string Created_Date { get; set; }
        //edit alert contact
        public string PK_ListingContactsID { get; set; }
        public string FK_ListingAlertID { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public string Image { get; set; }
        public string UserID { get; set; }
        public string IsQuitchetUser { get; set; }
        public string DurationInMinute { get; set; }
        public string CheckInStatus { get; set; }
        public string Running_Time_Duration { get; set; }
        public string Running_Time { get; set; }
        public string PK_OpenHouse_CheckInPK { get; set; }
        public string Time { get; set; }
        public string PK_UserID { get; set; }
        public string Host_PkUserID { get; set; }
        public string Agent_PkUserID { get; set; }

        public string OpenHouseDate { get; set; }
        public string Primary_Color { get; set; }
        public string Secondary_Color { get; set; }
        public string HostAgent_Primary_Color { get; set; }
        public string HostAgent_Secondary_Color { get; set; }
    }
}
