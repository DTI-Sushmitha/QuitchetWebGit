using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quitchet.Core.Models.Customer
{
    public class SellerToolsModel
    {
        public string AgentID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone1Ctry { get; set; }
        public string AGENT_ADDRESS { get; set; }
        public string OFFICE_NAME { get; set; }
        public string AGENT_IMAGE { get; set; }
        public string MLSNO { get; set; }
        public string Price { get; set; }
        public string Address { get; set; }
        public string Prop_Image { get; set; }
        public string Choice2ToTime2 { get; set; }
        public string RequestStatus { get; set; }
       public string UnViewedCount { get; set; }
        public string Status { get; set; }
        public string STATUS { get; set; }
        public string Phone1Nbr { get; set; }
        public string PK_UserID { get; set; }
        public string Primary_Color { get; set; }
        public string Secondary_Color { get; set; }

    }
    public class SellerTools_showings_Model
    {
        public string PK_RequestShowingID { get; set; }
        public string MLSNO { get; set; }
        public string MlsAddress { get; set; }
        public string PRP_PRICE { get; set; }
        public string SQFT { get; set; }
        public string MlsImage { get; set; }
        public string SHOWING_DATE { get; set; }
        public string SHOWING_TIME { get; set; }
        public string SHOWING_TIME_TO { get; set; }
        public string ExportDate { get; set; }
        public string ExportFromTime { get; set; }
        public string AlertTime { get; set; }
    

    }
}
