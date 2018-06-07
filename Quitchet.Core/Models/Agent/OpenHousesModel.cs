using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quitchet.Core.Models.Agent
{
    public class OpenHousesModel
    {
        public string PK_OpenHouseID { get; set; }
        public string OpenHouseType { get; set; }
        public string AgentID { get; set; }
        public string MLSID { get; set; }
        public string Title { get; set; }
        public string OpenHouseDate { get; set; }
        public string OpenHouse_FromTime { get; set; }
        public string OpenHouse_ToTime { get; set; }
        public string DATE { get; set; }
        public string FromTime { get; set; }
        public string ToTime { get; set; }
        public string Location { get; set; }
        public string Notes { get; set; }
        public string Reminder { get; set; }
        public string HostAgentID { get; set; }
        public string HostFirstName { get; set; }
        public string HostLastName { get; set; }
        public string HostImage { get; set; }
        public string HostCompany { get; set; }
        public string MLSID1 { get; set; }
        public string Class { get; set; }
        public string Status { get; set; }
        public string GeoLat { get; set; }
        public string GeoLong { get; set; }
        public string BEDROOMS { get; set; }
        public string BATHROOMS { get; set; }
        public string SQFT { get; set; }
        public string PRP_ADDRESS { get; set; }
        public string PRP_PRICE { get; set; }
        public string PRP_IMAGES { get; set; }
        public string PK_UserID { get; set; }
        //Feedback 
        public string PK_OpenHouseFeedbackID { get; set; }
        public string FK_OpenHouseID { get; set; }
        public string Comment { get; set; }
        public string Reating { get; set; }
        public string Top3 { get; set; }
        public string PriceCompare { get; set; }
        public string Exterior { get; set; }
        public string Interr { get; set; }
        public string Landscapping { get; set; }
        public string ConsidiringOffer { get; set; }
        public string FeatureToImprove { get; set; }
        public string FEEDBACK_DATE { get; set; }
        public string FEEDBACK_TIME { get; set; }
        public string Is_Viewed { get; set; }
        //------------Checkin 
        public string PK_OpenHouseSafetySettingID { get; set; }
        public string OH__SafetyTimerStatue { get; set; }
        public string OH_EmergencyContact { get; set; }
        public string OH_EmergencyContactName { get; set; }
        public string OH_EmergencyContactImage { get; set; }
        public string OH_SafetyTimerLimit { get; set; }
        public string OH_TimerExtensionAgent { get; set; }
        public string OH_TimerAuthCode { get; set; }
        public string OH_EmergencyCode { get; set; }
        public string OH_Call_TimerExpire { get; set; }
        public string OH_TextEmergContact_TimerExpire { get; set; }
        public string OH_Message_TimerExpire { get; set; }
        public string OH_Call_CodeEntered { get; set; }
        public string OH_TextEmergContact_CodeEntered { get; set; }
        public string OH_Message_CodeEntered { get; set; }
        public string Created_Date { get; set; }
        public string DurationInMinute { get; set; }
    }
}
