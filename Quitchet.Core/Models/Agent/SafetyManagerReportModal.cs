using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quitchet.Core.Models.Agent
{
    public class SafetyManagerReportModal
    {
       
        public string AgentID { get; set; }
        public string SAFTYON { get; set; }
        public string PK_ID { get; set; }
        public string SCHEDULE_TIME { get; set; }
        public string CHECK_IN { get; set; }
        public string CHECK_OUT { get; set; }
        public string TIMEOUTON { get; set; }
        public string SafetyStatus { get; set; }
        public string Agent_PkUserID { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string MobileNo { get; set; }
        public string Primary_Color { get; set; }
        public string Secondary_Color { get; set; }
        public string PhotoPath { get; set; }
        public string MLSID { get; set; }
        public string Class { get; set; }
        public string Status { get; set; }
        public string GeoLat { get; set; }
        public string GeoLong { get; set; }
        public string BEDROOMS { get; set; }
        public string BATHROOMS { get; set; }
        public string PRP_ADDRESS { get; set; }
        public string PRP_PRICE { get; set; }
        public string PRP_IMAGES { get; set; }

        public string BuyerID { get; set; }
        public string BuyerFistName { get; set; }
        public string BuyerLastName { get; set; }
        public string BuyerMobileNo { get; set; }
        public string BuyerImage { get; set; }
    }
    public class OHDetailsModel
    {
        public string DATE { get; set; }
        public string FromTime { get; set; }
        public string ToTime { get; set; }
        public string PRP_ADDRESS { get; set; }
        public string OpenHouseType { get; set; }
        public string Notes { get; set; }
      
    }
}
