using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quitchet.Core.Models.Customer
{
    public class AgentListingsModel
    {
        public string MLSId { get; set; }
        public string GeoLatitude { get; set; }
        public string GeoLongitude { get; set; }
        public string City { get; set; }
        public string Class { get; set; }
        public string Sale_Rent { get; set; }
        public string ListingPrice { get; set; }
        public string SoldPrice { get; set; }
        public string ADDRESKKS { get; set; }
        public string Bedrooms { get; set; }
        public string BathRooms { get; set; }
        public string PhotoCount { get; set; }
        public string Photolocation { get; set; }
        public string ApproxAcres { get; set; }
        public string Is_Favorite { get; set; }
        public string Is_DriveBy { get; set; }
        public string Is_Viewd { get; set; }
    }
}
