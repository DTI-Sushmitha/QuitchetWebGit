using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quitchet.Core.Models.Customer
{
    public class MyBasicNeedsAndWishListModel
    {
        public string Filter_TypeID { get; set; }
        public string Distance_Minutes { get; set; }
        public string Distance_Location { get; set; }
        public string Property_Type { get; set; }
        public string From_Price { get; set; }
        public string To_Price { get; set; }
        public string Bedrooms { get; set; }
        public string Bathrooms { get; set; }
        public string Master_Bedroom { get; set; }
        public string HandicapAccess { get; set; }
        public string From_LotAcreage { get; set; }
        public string To_LotAcreage { get; set; }
    }
}
