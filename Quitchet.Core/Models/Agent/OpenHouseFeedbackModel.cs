using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quitchet.Core.Models.Agent
{
    public class OpenHouseFeedbackModel
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
    }
}
