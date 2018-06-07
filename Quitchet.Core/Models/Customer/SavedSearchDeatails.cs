using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quitchet.Core.Models.Customer
{
    public class SavedSearchDeatails
    {
      
        public string Search_Name { get; set; }
        public int PK_SaveSearchesID { get; set; }
        public string Filter_Type { set; get; }
        public string Class_Type { get; set; }
        public string DrivingDistMinutes { get; set; }
        public string DrivingDistLocation { get; set; }
        public string Property_Type { get; set; }
        public string Price_From { get; set; }
        public string Price_To { get; set; }
        public string Bedrooms { get; set; }
        public string Bathrooms { get; set; }
        public string Sqft_From { get; set; }
        public string Sqft_To { get; set; }
        public string Master_Bedroom { get; set; }
        public string Garage_Capacity { get; set; }
        public string Stories { get; set; }
        public string Garage_Type { get; set; }
        public string Lot_Acreage_From { get; set; }
        public string Lot_Acreage_To { get; set; }
        public string Subdivison { get; set; }
        public string Age { get; set; }
        public string Lot_Features { get; set; }
        public string Foundation_Type { get; set; }
        public string Exterior_Features { get; set; }
        public string Interior_Features { get; set; }
        public string Flooring_Type { get; set; }
        public string Appliances { get; set; }
        public string Master_Features { get; set; }
        public string Laundry_Room { get; set; }
        public string FirePlace { get; set; }
        public string Community_Amenities { get; set; }
        public string Heating_System { get; set; }
        public string Water_Heater { get; set; }
        public string Water_Type { get; set; }
        public string Sewer_Type { get; set; }
        public string Schools { get; set; }
        public string Parking_Type { get; set; }
        public string FK_UserID { get; set; }
        public string User_DeviceID { get; set; }
        public string Created_Date { get; set; }
        public string CitiesList { get; set; }
        public string ResultCount { get; set; }
        public string PolygonPath { get; set; }
        public string MapBounds { get; set; }
        public string ZoomLevel { get; set; }

        public string NotificationFrequency { get; set; }

    }
}
