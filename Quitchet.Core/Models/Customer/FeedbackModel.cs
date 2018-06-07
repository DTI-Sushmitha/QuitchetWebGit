using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quitchet.Core.Models.Customer
{
    public class FeedbackModel
    {
    }
        public class OH_FeedbackModel
    {
        public string PK_OpenHouseFeedbackID { get; set; }
        public string FK_OpenHouseID { get; set; }
        public string MLSID { get; set; }
        public string Comment { get; set; }
        public string Reating { get; set; }
        public bool Top3 { get; set; }
        public string PriceCompare { get; set; }
        public string Exterior { get; set; }
        public string Interr { get; set; }
        public string Landscapping { get; set; }
        public string ConsidiringOffer { get; set; }
        public string FeatureToImprove { get; set; }
        public string FEEDBACK_DATE { get; set; }
        public string FEEDBACK_TIME { get; set; }
        public string Is_Viewed { get; set; }
        public string Class { get; set; }
        public string Status { get; set; }
        public string PRP_ADDRESS { get; set; }
        public string PRP_PRICE { get; set; }
        public string BEDROOMS { get; set; }
        public string BATHROOMS { get; set; }
        public string SQFT { get; set; }
        public string PHOTO_COUNT { get; set; }
        public string PRP_IMAGES { get; set; }
    }
}
