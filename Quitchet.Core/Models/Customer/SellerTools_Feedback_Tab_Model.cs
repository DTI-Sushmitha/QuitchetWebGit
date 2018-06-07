using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quitchet.Core.Models.Customer
{
    public class SellerTools_Feedback_Tab_Model
    {
        /////////////////////////////////////////for feedback tab-----------------
        public string PK_ShowingFeedbackID { get; set; }
        public string Comment { get; set; }
        public string Reating { get; set; }
        public string Top3 { get; set; }
        public string PriceCompare { get; set; }
        public string Exterior { get; set; }
        public string Interr { get; set; }
        public string Landscapping { get; set; }
        public string ConsidiringOffer { get; set; }
        public string FeatureToImprove { get; set; }
        public string MLSID { get; set; }
        public string FeedbackType { get; set; }
        public string FK_AgentID { get; set; }
        public string FK_UserID { get; set; }
        public string MLSNO { get; set; }
        public string PRP_ADDRESS { get; set; }
        public string PRP_PRICE { get; set; }
        public string SQFT { get; set; }
        public string PHOTO_COUNT { get; set; }
        public string PRP_IMAGES { get; set; }
        public string SHOWING_DATE { get; set; }
        public string SHOWING_TIME { get; set; }
        public string FEEDBACK_DATE { get; set; }
        public string FEEDBACK_TIME { get; set; }
        public string Agent_FeedbackID { get; set; }
        public string Buyer_FeednackID { get; set; }
        public string OpenHouse_FeedBackID { get; set; }
    }
}
