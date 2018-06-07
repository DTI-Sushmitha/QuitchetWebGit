using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Quitchet.Core.Models.Customer;
using System.Management;
using System.Collections.Specialized;
using System.Text;
using System.Web.Script.Serialization;
using System.Threading;
using System.Configuration;

namespace Quitchet.Web.Areas.Customer.Controllers
{
    public class SavedSearchController : Controller
    {
        // GET: /Customer/SavedSearch/
        public ActionResult Index(int? SSID, string SSName)
        {
            if (SSID != null)
            {
                Session["SaveSearchName"] = SSName;
                Session["SaveSearchID"] = SSID.ToString();
            }
            else
            {
                Session["SaveSearchName"] = 0;
                Session["SaveSearchID"] = 0;
            }
            return View();
        }
        //Methor To Remove Saved Search
        public JsonResult DeleteSavedSearch(string searchID)
        {
            string ResponceData = "";
            try
            {
                string urlAddress = ConfigurationManager.AppSettings["WebApiURL"]+"/SavedSearchesRemove?";
                using (WebClient client = new WebClient())
                {
                    NameValueCollection postData = new NameValueCollection()
                    {
                      { "PK_SavedSearcheID", searchID },

                    };
                    ResponceData = Encoding.UTF8.GetString(client.UploadValues(urlAddress, postData));
                    var response = JsonConvert.DeserializeObject(ResponceData);
                    if (response.ToString() == "Successfully Deleted.")
                    {
                        return Json("Sucess", JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json("Fail", JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception EX)
            {
                return Json(EX, JsonRequestBehavior.AllowGet);
            }
        }
        // Method To Get Saved Search Filter Data based on PKey_SearchID
        public async Task<JsonResult> GetFilterDetails(int searchID)
        {
            try
            {
                string Baseurl = ConfigurationManager.AppSettings["WebApiURL"];
                List<SavedSearchDeatails> currentFilterData = new List<SavedSearchDeatails>();

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string Method = "/SavedSearchesGetByPK?PK_SaveSearcheID=" + searchID;
                    HttpResponseMessage Res = await client.GetAsync(Method);
                    if (Res.IsSuccessStatusCode)
                    {
                        var DataByMLSID = Res.Content.ReadAsStringAsync().Result;
                        currentFilterData = JsonConvert.DeserializeObject<List<SavedSearchDeatails>>(DataByMLSID);
                    }
                    var jsonResult = Json(currentFilterData, JsonRequestBehavior.AllowGet);
                    jsonResult.MaxJsonLength = int.MaxValue;
                    return jsonResult;
                    //return Json(currentFilterData, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception EX)
            {
                return Json(EX, JsonRequestBehavior.AllowGet);
            }
        }
        //Method to get properties based on Home FIlters
        public JsonResult SearchFilterProperties(List<String> obj)
        {
             string UserID = "0", res = "";
            if (Request.Cookies["UseridCookie"] != null)
            {
                UserID = Request.Cookies["UseridCookie"].Value;
            }
            List<CurrentCityPropartyModel> dataBasedonLotsFilter = new List<CurrentCityPropartyModel>();
            string pagesource = "";
            try
            {
                string urlAddress = ConfigurationManager.AppSettings["WebApiURL"]+"/SearchFiltersProp?";
                using (WebClient client = new WebClient())
                {
                    NameValueCollection postData = new NameValueCollection()
                    {
                        {"FilterTypeID",obj[0]},
                        {"ClassType",obj[1]},
                        {"DrivingDistance",obj[2]},
                        {"DrivingLocation",obj[3]},
                        {"PropartyType",obj[4]},
                        {"From_Price",obj[5].Split('-')[0].ToString()},
                        {"To_Price",obj[5].Split('-')[1].ToString()},
                        {"BathRooms",obj[6]},
                        {"BedRooms",obj[7]},
                        {"Sqft_from",obj[8].Split('-')[0].ToString()},
                        {"Sqft_to",obj[8].Split('-')[1].ToString()},
                        {"Elementary",obj[9]},
                        {"Garage_Type",obj[10]},
                        {"Master_Bedroom",obj[11]},
                        {"Lot_Acreage_From",obj[12].Split('-')[0].ToString()},
                        {"Lot_Acreage_To",obj[12].Split('-')[1].ToString()},
                        {"Stories",obj[13]},
                        {"SubDivision",obj[14]},
                        {"Garage_Capacity",obj[15]},
                        {"Age",obj[16]},
                        {"Lot_Features",obj[17]},
                        {"Appliances",obj[18]},
                        {"FoundationType",obj[19]},
                        {"Master_Features",obj[20]},
                        {"Exterior_Features",obj[21]},
                        {"LandaryRoom",obj[22]},
                        {"Interior_Features",obj[23]},
                        {"FirePlace",obj[24]},
                        {"FlooringType",obj[25]},
                        {"CommunityAmenities",obj[26]},
                        {"HeatingSystem",obj[27]},
                        {"WaterType",obj[28]},
                        {"WaterHeater",obj[29]},
                        {"SewerType",obj[30]},
                        {"ParkingType",""},
                        //{"CitiesList",obj[32] },
                        //{"ResultCount",obj[33]},
                        {"SortBy",obj[36]},
                        {"Middle",obj[37]},
                        {"High",obj[38]},
                        {"PolygonPath",obj[39] },
                         {"MapBounds",obj[40] },
                         {"ZoomLevel",obj[41] },
                        {"FK_UserID",UserID},
                    };
                    pagesource = Encoding.UTF8.GetString(client.UploadValues(urlAddress, postData));
                    var proplist = JsonConvert.DeserializeObject<List<CurrentCityPropartyModel>>(pagesource);
                    var jsonResult = Json(proplist, JsonRequestBehavior.AllowGet);
                    jsonResult.MaxJsonLength = int.MaxValue;
                    return jsonResult;
                }
            }
            catch (Exception ex)
            {
                res = ex.Message.ToString();
                return Json(res, JsonRequestBehavior.AllowGet);
            }
            //return Json("", JsonRequestBehavior.AllowGet);
        }
        //Method to get Saved Rental |Properties based on Rental Filters 
        public JsonResult SearchRentalFilterProperties(List<String> obj)
        {
            string UserID = "0", res = "";
            if (Request.Cookies["UseridCookie"] != null)
            {
                UserID = Request.Cookies["UseridCookie"].Value;
            }
            List<CurrentCityPropartyModel> dataBasedonLotsFilter = new List<CurrentCityPropartyModel>();
            string pagesource = "";
            try
            {
                string urlAddress = ConfigurationManager.AppSettings["WebApiURL"]+"/SearchFiltersProp?";
                using (WebClient client = new WebClient())
                {
                    NameValueCollection postData = new NameValueCollection()
                    {
                        { "FilterTypeID",obj[0]},
                        {"ClassType",obj[1]},
                        {"DrivingDistance",obj[2]},
                        {"DrivingLocation",obj[3]},
                        {"PropartyType",obj[4]},
                        {"From_Price",obj[5].Split('-')[0].ToString()},
                        {"To_Price",obj[5].Split('-')[1].ToString()},
                        {"BathRooms",obj[6]},
                        {"BedRooms",obj[7]},
                        {"Sqft_from",obj[8].Split('-')[0].ToString()},
                        {"Sqft_to",obj[8].Split('-')[1].ToString()},
                        {"Elementary",obj[9]},
                        {"Garage_Type","Any"},
                        { "Master_Bedroom","Any"},
                        { "Lot_Acreage_From","0"},
                        {"Lot_Acreage_To","10000"},
                        {"Stories","Any"},
                        {"SubDivision","Any"},
                        {"Garage_Capacity",obj[11]},
                        {"Age",obj[12]},
                        {"Lot_Features","Any"},
                        { "Appliances","Any"},
                        {"FoundationType","Any"},
                        { "Master_Features","Any"},
                        {"Exterior_Features","Any"},
                        {"LandaryRoom","Any"},
                        {"Interior_Features","Any"},
                        {"FirePlace","Any"},
                        {"FlooringType","Any"},
                        {"CommunityAmenities","Any"},
                        {"HeatingSystem","Any"},
                        {"WaterType","Any"},
                        {"WaterHeater","Any"},
                        {"SewerType","Any"},
                        {"ParkingType",obj[10]},
                        {"CitiesList",obj[14] },
                        {"ResultCount",obj[15]},
                        {"SortBy",obj[18]},
                        {"Middle",obj[19]},
                        {"High",obj[20]},
                        {"PloygonPath",obj[21] },
                        {"MapBounds",obj[22] },
                        {"ZoomLevel",obj[23] },
                        {"FK_UserID",UserID},
                    };
                    pagesource = Encoding.UTF8.GetString(client.UploadValues(urlAddress, postData));
                    var proplist = JsonConvert.DeserializeObject<List<CurrentCityPropartyModel>>(pagesource);
                    var jsonResult = Json(proplist, JsonRequestBehavior.AllowGet);
                    jsonResult.MaxJsonLength = int.MaxValue;
                    return jsonResult;
                }
            }
            catch (Exception ex)
            {
                res = ex.Message.ToString();
                return Json(res, JsonRequestBehavior.AllowGet);
            }
            //return Json("", JsonRequestBehavior.AllowGet);
        }
        //Mehto to get Saved LOts/Land Data based on Lot/Lands Filters
        public JsonResult SearchLotsFilterProperties(List<String> obj)
        {
            string UserID = "0", res = "";
            if (Request.Cookies["UseridCookie"] != null)
            {
                UserID = Request.Cookies["UseridCookie"].Value;
            }
            List<CurrentCityPropartyModel> dataBasedonLotsFilter = new List<CurrentCityPropartyModel>();
            string pagesource = "";
            try
            {
                string urlAddress = ConfigurationManager.AppSettings["WebApiURL"]+"/SearchFiltersProp?";
                using (WebClient client = new WebClient())
                {
                    NameValueCollection postData = new NameValueCollection()
                    {
                        {"FilterTypeID",obj[0]},
                        {"ClassType",obj[1]},
                        {"DrivingDistance",obj[2]},
                        {"DrivingLocation",obj[3]},
                        {"PropartyType",obj[4]},
                        {"From_Price",obj[5].Split('-')[0].ToString()},
                        {"To_Price",obj[5].Split('-')[1].ToString()},
                        {"BathRooms","Any"},
                        {"BedRooms","Any"},
                        {"Sqft_from","0"},
                        {"Sqft_to","10000"},
                        {"Elementary",obj[7]},
                        {"Garage_Type","Any"},
                        { "Master_Bedroom","Any"},
                        { "Lot_Acreage_From",obj[6].Split('-')[0].ToString()},
                        {"Lot_Acreage_To",obj[6].Split('-')[1].ToString()},
                        {"Stories","Any"},
                        {"SubDivision",obj[8]},
                        {"Garage_Capacity","Any"},
                        {"Age","Any"},
                        {"Lot_Features",obj[9]},
                        { "Appliances","Any"},
                        {"FoundationType","Any"},
                        { "Master_Features","Any"},
                        {"Exterior_Features","Any"},
                        {"LandaryRoom","Any"},
                        {"Interior_Features","Any"},
                        {"FirePlace","Any"},
                        {"FlooringType","Any"},
                        {"CommunityAmenities","Any"},
                        {"HeatingSystem","Any"},
                        {"WaterType",obj[10]},
                        {"WaterHeater","Any"},
                        {"SewerType",obj[11]},
                        {"ParkingType","Any"},
                        {"CitiesList",obj[13] },
                        {"ResultCount",obj[14]},
                        {"SortBy",obj[17]},
                        {"Middle",obj[18]},
                        {"High",obj[19]},
                        {"PloygonPath",obj[20] },
                        {"MapBounds",obj[21] },
                        {"ZoomLevel",obj[22] },
                        {"FK_UserID",UserID},
                    };
                    pagesource = Encoding.UTF8.GetString(client.UploadValues(urlAddress, postData));
                    var proplist = JsonConvert.DeserializeObject<List<CurrentCityPropartyModel>>(pagesource);
                    var jsonResult = Json(proplist, JsonRequestBehavior.AllowGet);
                    jsonResult.MaxJsonLength = int.MaxValue;
                    return jsonResult;
                }
            }
            catch (Exception ex)
            {
                res = ex.Message.ToString();
                return Json(res, JsonRequestBehavior.AllowGet);
            }
            //return Json("", JsonRequestBehavior.AllowGet);
        }

        // Method to update Edited Home filter data
        public JsonResult UpdateHomeSearchFilters(List<String> obj)
        {
            string UserID = "", macAdress = "", res = "";
            if (Request.Cookies["UseridCookie"] != null)
            {
                UserID = Request.Cookies["UseridCookie"].Value;
            }
            ManagementClass objMgmtCls = new ManagementClass("Win32_NetworkAdapterConfiguration");
            foreach (ManagementObject objMgmt in objMgmtCls.GetInstances())  //-----------To Get Mac Adress of Client Machine-----------------
            {
                if (!(bool)objMgmt["ipEnabled"])
                    continue;
                macAdress = objMgmt["MACAddress"].ToString();
            }
            string pagesource = "";
            try
            {
                string urlAddress = ConfigurationManager.AppSettings["WebApiURL"]+"/UpdateSaveSearches?";
                using (WebClient client = new WebClient())
                {
                    NameValueCollection postData = new NameValueCollection()
                    {
                        {"NotificationFrequncy",obj[33] },
                        {"PK_SaveSearchID",obj[32] },
                        {"SearchName",obj[31]},
                        {"FilterType",obj[0]},
                        {"ClassType",obj[1]},
                        {"DrivingDistMinuis",obj[2]},
                        {"DrivinDistLocation",obj[3]},
                        {"PropartyType",obj[4]},
                        {"FromPrice",obj[5].Split('-')[0].ToString()},
                        {"ToPrice",obj[5].Split('-')[1].ToString()},
                        {"Bathrooms",obj[6]},
                        {"Bedrooms",obj[7]},
                        {"SqftFrom",obj[8].Split('-')[0].ToString()},
                        {"SqftTo",obj[8].Split('-')[1].ToString()},
                        {"Elementary",obj[9]},
                        {"GarageType",obj[10]},
                        {"MasterBedroom",obj[11]},
                        {"LotAcreageFrom",obj[12].Split('-')[0].ToString()},
                        {"LotAcreageTo",obj[12].Split('-')[1].ToString()},
                        {"Stores",obj[13]},
                        {"SubDivison",obj[14]},
                        {"GarageCapacity",obj[15]},
                        {"Age",obj[16]},
                        {"LotFeatures",obj[17]},
                        {"Appliances",obj[18]},
                        {"FoundationType",obj[19]},
                        {"MasterFeatures",obj[20]},
                        {"ExteriorFeatures",obj[21]},
                        {"LaundryRoom",obj[22]},
                        {"interiorFeatures",obj[23]},
                        {"FirePlace",obj[24]},
                        {"FlooringType",obj[25]},
                        {"CommunityAmenities",obj[26]},
                        {"HeatingSyatem",obj[27]},
                        {"WaterType",obj[28]},
                        {"WaterHeater",obj[29]},
                        {"SEWER_TYPE",obj[30]},
                        {"ParkingType",""},
                        {"FK_UserID",UserID},
                        {"User_DeviceID",macAdress},
                        {"OS_Type_ID","1" },
                          {"CitiesList",obj[34] },
                        {"ResultCount",obj[35] },
                        {"Middle",obj[36] },
                        {"High",obj[37] },
                        {"PloygonPath",obj[39] },
                         {"MapBounds",obj[40] },
                         {"ZoomLevel",obj[41] },
                    };
                    pagesource = Encoding.UTF8.GetString(client.UploadValues(urlAddress, postData));
                    var response = JsonConvert.DeserializeObject(pagesource);
                    if (response.ToString() == "Successfully Created.")
                    {
                        res = "Success";
                        return Json(res, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        res = "Fail";
                        return Json("res", JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception ex)
            {
                res = ex.Message.ToString();
                return Json(res, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult UpdateRentalSearchFilters(List<String> obj)
        {
            string UserID = "", macAdress = "", res = "";
            if (Request.Cookies["UseridCookie"] != null)
            {
                UserID = Request.Cookies["UseridCookie"].Value;
            }
            ManagementClass objMgmtCls = new ManagementClass("Win32_NetworkAdapterConfiguration");
            foreach (ManagementObject objMgmt in objMgmtCls.GetInstances()) //-----------To Get Mac Adress of Client Machine-----------------  
            {
                if (!(bool)objMgmt["ipEnabled"])
                    continue;
                macAdress = objMgmt["MACAddress"].ToString();
            }
            string pagesource = "";
            try
            {
                string urlAddress = ConfigurationManager.AppSettings["WebApiURL"]+"/UpdateSaveSearches?";
                using (WebClient client = new WebClient())
                {
                    NameValueCollection postData = new NameValueCollection()
                    {
                        {"NotificationFrequncy",obj[15] },
                        {"PK_SaveSearchID",obj[14] },
                        {"SearchName",obj[13]},
                        {"FilterType",obj[0]},
                        {"ClassType",obj[1]},
                        {"DrivingDistMinuis",obj[2]},
                        {"DrivinDistLocation",obj[3]},
                        {"PropartyType",obj[4]},
                        {"FromPrice",obj[5].Split('-')[0].ToString()},
                        {"ToPrice",obj[5].Split('-')[1].ToString()},
                        {"Bathrooms",obj[6]},
                        {"Bedrooms",obj[7]},
                        {"SqftFrom",obj[8].Split('-')[0].ToString()},
                        {"SqftTo",obj[8].Split('-')[1].ToString()},
                        {"Elementary",obj[9]},
                        {"GarageType",""},
                        {"MasterBedroom",""},
                        {"LotAcreageFrom",""},
                        {"LotAcreageTo",""},
                        {"Stores",""},
                        {"SubDivison",""},
                        {"GarageCapacity",obj[11]},
                        {"Age",obj[12]},
                        {"LotFeatures",""},
                        {"Appliances",""},
                        {"FoundationType",""},
                        {"MasterFeatures",""},
                        {"ExteriorFeatures",""},
                        {"LaundryRoom",""},
                        {"interiorFeatures",""},
                        {"FirePlace",""},
                        {"FlooringType",""},
                        {"CommunityAmenities",""},
                        {"HeatingSyatem",""},
                        {"WaterType",""},
                        {"WaterHeater",""},
                        {"SEWER_TYPE",""},
                        {"ParkingType",obj[10]},
                        {"FK_UserID",UserID},
                        {"User_DeviceID",macAdress},
                        {"OS_Type_ID","1" },
                        {"CitiesList",obj[16] },
                        {"ResultCount",obj[17] },
                        {"Middle",obj[18] },
                        {"High",obj[19] },
                        {"PloygonPath",obj[21] },
                        {"MapBounds",obj[22] },
                        {"ZoomLevel",obj[23] },
                    };
                    pagesource = Encoding.UTF8.GetString(client.UploadValues(urlAddress, postData));
                    var response = JsonConvert.DeserializeObject(pagesource);
                    if (response.ToString() == "Successfully Created.")
                    {
                        res = "Success";
                        return Json(res, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        res = "Fail";
                        return Json("res", JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception ex)
            {
                res = ex.Message.ToString();
                return Json(res, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult UpdateLotsSearchFilters(List<String> obj)
        {
            string UserID = "", macAdress = "", res = "";
            if (Request.Cookies["UseridCookie"] != null)
            {
                UserID = Request.Cookies["UseridCookie"].Value;
            }
            ManagementClass objMgmtCls = new ManagementClass("Win32_NetworkAdapterConfiguration");
            foreach (ManagementObject objMgmt in objMgmtCls.GetInstances())   //-----------To Get Mac Adress of Client Machine-----------------  
            {
                if (!(bool)objMgmt["ipEnabled"])
                    continue;
                macAdress = objMgmt["MACAddress"].ToString();
            }
            string pagesource = "";
            try
            {
                string urlAddress = ConfigurationManager.AppSettings["WebApiURL"]+"/UpdateSaveSearches?";
                using (WebClient client = new WebClient())
                {
                    NameValueCollection postData = new NameValueCollection()
                    {
                         {"NotificationFrequncy",obj[14] },
                        {"PK_SaveSearchID",obj[13] },
                        {"SearchName",obj[12]},
                        {"FilterType",obj[0]},
                        {"ClassType",obj[1]},
                        {"DrivingDistMinuis",obj[2]},
                        {"DrivinDistLocation",obj[3]},
                        {"PropartyType",obj[4]},
                        {"FromPrice",obj[5].Split('-')[0].ToString()},
                        {"ToPrice",obj[5].Split('-')[1].ToString()},
                        {"Bathrooms",""},
                        {"Bedrooms",""},
                        {"SqftFrom",""},
                        {"SqftTo",""},
                        {"Elementary",obj[7]},
                        {"GarageType",""},
                        {"MasterBedroom",""},
                        {"LotAcreageFrom",obj[6].Split('-')[0].ToString()},
                        {"LotAcreageTo",obj[6].Split('-')[1].ToString()},
                        {"Stores",""},
                        {"SubDivison",obj[8]},
                        {"GarageCapacity",""},
                        {"Age",""},
                        {"LotFeatures",obj[9]},
                        {"Appliances",""},
                        {"FoundationType",""},
                        {"MasterFeatures",""},
                        {"ExteriorFeatures",""},
                        {"LaundryRoom",""},
                        {"interiorFeatures",""},
                        {"FirePlace",""},
                        {"FlooringType",""},
                        {"CommunityAmenities",""},
                        {"HeatingSyatem",""},
                        {"WaterType",obj[10]},
                        {"WaterHeater",""},
                        {"SEWER_TYPE",obj[11]},
                        {"ParkingType",""},
                        {"FK_UserID",UserID},
                        {"User_DeviceID",macAdress},
                        {"OS_Type_ID","1" },
                          {"CitiesList",obj[15] },
                        {"ResultCount",obj[16] },
                        {"Middle",obj[17] },
                        {"High",obj[18] },
                        {"PloygonPath",obj[20] },
                          {"MapBounds",obj[21] },
                         {"ZoomLevel",obj[22] },
                    };
                    pagesource = Encoding.UTF8.GetString(client.UploadValues(urlAddress, postData));
                    var response = JsonConvert.DeserializeObject(pagesource);

                    if (response.ToString() == "Successfully Created.")
                    {
                        res = "Success";
                        return Json(res, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        res = "Fail";
                        return Json("res", JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception ex)
            {
                res = ex.Message.ToString();
                return Json(res, JsonRequestBehavior.AllowGet);
            }
        }

        //////////////////////////////
    }
}
/* 
  public async Task<JsonResult> SearchFilterProperties(List<String> obj)
        {
            string res = "", UserID = "";
            if (Request.Cookies["UseridCookie"] != null)
            {
                UserID = Request.Cookies["UseridCookie"].Value;
            }
            try
            {
                var ptype = "Any";
                string Baseurl = ConfigurationManager.AppSettings["WebApiURL"];
                List<CurrentCityPropartyModel> dataBasedonHomeFilter = new List<CurrentCityPropartyModel>();
                using (var client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromMilliseconds(150000);
                    var cts = new CancellationTokenSource();
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string Method = "/SearchFiltersPropGet?FilterTypeID=" + obj[0] + "&ClassType=" + obj[1] + "&DrivingDistance=" + obj[2] + "&DrivingLocation=" + obj[3] + "&PropartyType=" + obj[4] + "&From_Price=" + obj[5].Split('-')[0].ToString() + "&To_Price=" + obj[5].Split('-')[1].ToString();
                    Method = Method + "&BedRooms=" + obj[7] + "&BathRooms=" + obj[6] + "&Sqft_from=" + obj[8].Split('-')[0].ToString() + "&Sqft_to=" + obj[8].Split('-')[1].ToString() + "&Master_Bedroom=" + obj[11] + "&Stories=" + obj[13] + "&Garage_Capacity=" + obj[15] + "&Garage_Type=" + obj[10];
                    Method = Method + "&Lot_Acreage_From=" + obj[12].Split('-')[0].ToString() + "&Lot_Acreage_To=" + obj[12].Split('-')[1].ToString() + "&SubDivision=" + obj[14] + "&Age=" + obj[16] + "&Lot_Features=" + obj[17] + "&FoundationType=" + obj[19] + "&Exterior_Features=" + obj[21];
                    Method = Method + "&Interior_Features=" + obj[23] + "&FlooringType=" + obj[25] + "&Appliances=" + obj[18] + "&Master_Features=" + obj[20] + "&LandaryRoom=" + obj[22] + "&FirePlace=" + obj[24];
                    Method = Method + "&CommunityAmenities=" + obj[26] + "&HeatingSystem=" + obj[27] + "&WaterHeater=" + obj[29] + "&WaterType=" + obj[28] + "&SewerType=" + obj[30] + "&Elementary=" + obj[9] + "&ParkingType=" + ptype + "&CitiesList=" + obj[32] + "&ResultCount=" + obj[33] + "&SortBy=" + obj[36] + "&Fk_UserID=" + UserID + "&Middle=" + obj[37] + "&High=" + obj[38];
                    Method = Method + "&PolygonPath=" + obj[39] + "&MapBounds=" + obj[40] + "&ZoomLevel=" + obj[41];
                    HttpResponseMessage Res = await client.GetAsync(Method);

                    if (Res.IsSuccessStatusCode)
                    {
                        var DataByMLSID = Res.Content.ReadAsStringAsync().Result;
                        dataBasedonHomeFilter = JsonConvert.DeserializeObject<List<CurrentCityPropartyModel>>(DataByMLSID);
                    }
                    var jsonResult = Json(dataBasedonHomeFilter, JsonRequestBehavior.AllowGet);
                    jsonResult.MaxJsonLength = int.MaxValue;
                    return jsonResult;
                }
            }
            catch (Exception ex)
            {
                res = ex.Message.ToString();
                return Json(res, JsonRequestBehavior.AllowGet);
            }
        }
        ////////////
         public async Task<JsonResult> SearchRentalFilterProperties(List<String> obj)
        {
            string res = "", UserID = "";
            if (Request.Cookies["UseridCookie"] != null)
            {
                UserID = Request.Cookies["UseridCookie"].Value;
            }
            try
            {
                string Baseurl = ConfigurationManager.AppSettings["WebApiURL"];
                List<CurrentCityPropartyModel> dataBasedonRentalFilter = new List<CurrentCityPropartyModel>();
                using (var client = new HttpClient())
                {

                    client.Timeout = TimeSpan.FromMilliseconds(150000);
                    var cts = new CancellationTokenSource();
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string Method = "/SearchFiltersPropGet?FilterTypeID=" + obj[0] + "&ClassType=" + obj[1] + "&DrivingDistance=" + obj[2] + "&DrivingLocation=" + obj[3] + "&PropartyType=" + obj[4] + "&From_Price=" + obj[5].Split('-')[0].ToString() + "&To_Price=" + obj[5].Split('-')[1].ToString();
                    Method = Method + "&BedRooms=" + obj[7] + "&BathRooms=" + obj[6] + "&Sqft_from=" + obj[8].Split('-')[0].ToString() + "&Sqft_to=" + obj[8].Split('-')[1].ToString() + "&Master_Bedroom=Any&Stories=Any&Garage_Capacity=" + obj[11] + "&Garage_Type=Any";
                    Method = Method + "&Lot_Acreage_From=0&Lot_Acreage_To=10000&SubDivision=Any&Age=" + obj[12] + "&Lot_Features=Any&FoundationType=Any&Exterior_Features=Any";
                    Method = Method + "&Interior_Features=Any&FlooringType=Any&Appliances=Any&Master_Features=Any&LandaryRoom=Any&FirePlace=Any";
                    Method = Method + "&CommunityAmenities=Any&HeatingSystem=Any&WaterHeater=Any&WaterType=Any&SewerType=Any&Elementary=" + obj[9] + "&ParkingType=" + obj[10] + "&Fk_UserID=" + UserID + "&CitiesList=" + obj[14] + "&ResultCount=" + obj[15] + "&SortBy=" + obj[18] + "&Middle=" + obj[19] + "&High=" + obj[20];
                    Method = Method + "&PolygonPath=" + obj[21] + "&MapBounds=" + obj[22] + "&ZoomLevel=" + obj[23];
                    HttpResponseMessage Res = await client.GetAsync(Method);
                    if (Res.IsSuccessStatusCode)
                    {
                        var DataByMLSID = Res.Content.ReadAsStringAsync().Result;
                        dataBasedonRentalFilter = JsonConvert.DeserializeObject<List<CurrentCityPropartyModel>>(DataByMLSID);
                    }
                    var jsonResult = Json(dataBasedonRentalFilter, JsonRequestBehavior.AllowGet);
                    jsonResult.MaxJsonLength = int.MaxValue;
                    return jsonResult;
                }
            }
            catch (Exception ex)
            {
                res = ex.Message.ToString();
                return Json(res, JsonRequestBehavior.AllowGet);
            }
        }
        /////////////////
          public async Task<JsonResult> SearchLotsFilterProperties(List<String> obj)
        {
            string res = "", UserID = "";
            if (Request.Cookies["UseridCookie"] != null)
            {
                UserID = Request.Cookies["UseridCookie"].Value;
            }
            try
            {
                string Baseurl = ConfigurationManager.AppSettings["WebApiURL"];
                List<CurrentCityPropartyModel> dataBasedonLotsFilter = new List<CurrentCityPropartyModel>();
                using (var client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromMilliseconds(150000);
                    var cts = new CancellationTokenSource();
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string Method = "/SearchFiltersPropGet?FilterTypeID=" + obj[0] + "&ClassType=" + obj[1] + "&DrivingDistance=" + obj[2] + "&DrivingLocation=" + obj[3] + "&PropartyType=" + obj[4] + "&From_Price=" + obj[5].Split('-')[0].ToString() + "&To_Price=" + obj[5].Split('-')[1].ToString();
                    Method = Method + "&BedRooms=Any&BathRooms=Any&Sqft_from=0&Sqft_to=10000&Master_Bedroom=Any&Stories=Any&Garage_Capacity=Any&Garage_Type=Any";
                    Method = Method + "&Lot_Acreage_From=" + obj[6].Split('-')[0].ToString() + "&Lot_Acreage_To=" + obj[6].Split('-')[1].ToString() + "&SubDivision=" + obj[8] + "&Age=Any&Lot_Features=" + obj[9] + "&FoundationType=Any&Exterior_Features=Any";
                    Method = Method + "&Interior_Features=Any&FlooringType=Any&Appliances=Any&Master_Features=Any&LandaryRoom=Any&FirePlace=Any";
                    Method = Method + "&CommunityAmenities=Any&HeatingSystem=Any&WaterHeater=Any&WaterType=" + obj[10] + "&SewerType=" + obj[11] + "&Elementary=" + obj[7] + "&ParkingType=Any&Fk_UserID=" + UserID + "&CitiesList=" + obj[13] + "&ResultCount=" + obj[14] + "&SortBy=" + obj[17] + "&Middle=" + obj[18] + "&High=" + obj[19];
                    Method = Method + "&PolygonPath=" + obj[20] + "&MapBounds=" + obj[21] + "&ZoomLevel=" + obj[22];
                    HttpResponseMessage Res = await client.GetAsync(Method);
                    if (Res.IsSuccessStatusCode)
                    {
                        var DataByMLSID = Res.Content.ReadAsStringAsync().Result;
                        dataBasedonLotsFilter = JsonConvert.DeserializeObject<List<CurrentCityPropartyModel>>(DataByMLSID);
                    }
                    var jsonResult = Json(dataBasedonLotsFilter, JsonRequestBehavior.AllowGet);
                    jsonResult.MaxJsonLength = int.MaxValue;
                    return jsonResult;
                }
            }
            catch (Exception ex)
            {
                res = ex.Message.ToString();
                return Json(res, JsonRequestBehavior.AllowGet);
            }
        }
 */
