using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quitchet.Core.Models.Agent
{
    class MyCustomersModel
    {
    }
   public class BuyerFavoritesModel
    {
        public string FK_UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string Image { get; set; }
        public string MLSId { get; set; }
        public string GeoLatitude { get; set; }
        public string GeoLongitude { get; set; }
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
        public string Is_ComparedHome { get; set; }
        public string Is_RequestShowing { get; set; }
        public string Rating { get; set; }
        public string Have_Agent { get; set; }
        public string AgentID { get; set; }
    }
    public class ListingsModel
    {
        public string MLSNO { get; set; }
        public string Class { get; set; }
        public string Status { get; set; }
        public string PRP_ADDRESS { get; set; }
        public string PRP_PRICE { get; set; }
        public string BEDROOMS { get; set; }
        public string BATHROOMS { get; set; }
        public string SQFT { get; set; }
        public string PHOTO_COUNT { get; set; }
        public string PRP_IMAGES { get; set; }
      public string FeedbackCount { get; set; }
    }
    public class MeSeller_Listings_Model
    {
        public List<ListingsModel> Listings;
        public string AgentID { get; set; }
        public string FK_UserID { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string Image { get; set; }
        public string Note { get; set; }
    }
}
