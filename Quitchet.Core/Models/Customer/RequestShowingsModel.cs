using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quitchet.Core.Models.Customer
{
    public class AllColumns
    {
        public string PK_RequestShowingID { get; set; }
        public string FK_UserID { get; set; }
        public string AgentPKUserID { get; set; }
        public string Agent_Email { get; set; }
        public string AgentID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AOOfcName { get; set; }
        public string Phone2Nbr { get; set; }
        public string AGENT_IMAGE { get; set; }
        public string MLSID { get; set; }
        public string PRP_ADDRESS { get; set; }
        public string PRP_PRICE { get; set; }
        public string BEDROOMS { get; set; }
        public string BATHROOMS { get; set; }
        public string SQFT { get; set; }
        public string PHOTO_COUNT { get; set; }
        public string PRP_IMAGES { get; set; }
        public string Class { get; set; }
        public string Status { get; set; }
        public string GeoLat { get; set; }
        public string GeoLong { get; set; }
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
        public string AgentScheduleDate { get; set; }
        public string ScheduleFromTime { get; set; }
        public string ScheduleToTime { get; set; }
        public string SetAppointment_From { get; set; }
        public string SetAppointment_To { get; set; }
        public string Have_BuyerFeedback { get; set; }
        public string BuyerFeedback_PostDate { get; set; }
        public string BuyerFeedback_PostTime { get; set; }
        public string PK_UserShowingFeedbackID { get; set; }
    }
    public class RequestShowingsModel
    {
        public List<AllColumns> AllListt;
        public string UserChoice1 { get; set; }
        public string UserChoice2 { get; set; }
        public string pk { get; set; }
        public string RequestStatus { get; set; }

    }
    public class EditRequestModel
    {
        public string PK_RequestShowingID { get; set; }
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
    }
}
