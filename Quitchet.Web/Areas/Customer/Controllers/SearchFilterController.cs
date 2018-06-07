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
using System.Configuration;

namespace Quitchet.Web.Areas.Customer.Controllers
{
    public class SearchFilterController : Controller
    {
        //
        // GET: /Customer/SearchFilter/
        public ActionResult Index()
        {
            return View();
        }
        public class AllSchoolsAndSubDivisionsDetails
        {
            public string Schools { get; set; }
            public string SubDivision { get; set; }
            public string ELEMENTARY_SCHOOLS { get; set; }
            public string MIDDLE_SCHOOLS { get; set; }
            public string HIGH_SCHOOLS { get; set; }


        }//Class members for holding Service returned data
        //Getting Data By Current City
        public async Task<JsonResult> GetData(string city, string SortBy)
        {
            string UserID = "0", macAdress = "", sortby = SortBy;
            if (Request.Cookies["UseridCookie"] != null)
            {
                UserID = Request.Cookies["UseridCookie"].Value;
            }

            if (city == null || city == "")
            {
                city = "Hyderabad";
            }
            try
            {
                ManagementClass objMgmtCls = new ManagementClass("Win32_NetworkAdapterConfiguration");
                foreach (ManagementObject objMgmt in objMgmtCls.GetInstances())
                {
                    if (!(bool)objMgmt["ipEnabled"])
                        continue;
                    macAdress = objMgmt["MACAddress"].ToString();
                }
                string Baseurl = ConfigurationManager.AppSettings["WebApiURL"];
                List<CurrentCityPropartyModel> CurrentCity = new List<CurrentCityPropartyModel>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string Method = "/CurrentCityProparty?CityName=" + city + "&UserID=" + UserID + "&DeviceID=" + macAdress + "&SortBy=" + sortby;
                    HttpResponseMessage Res = await client.GetAsync(Method);
                    if (Res.IsSuccessStatusCode)
                    {
                        var CurrentCityData = Res.Content.ReadAsStringAsync().Result;
                        CurrentCity = JsonConvert.DeserializeObject<List<CurrentCityPropartyModel>>(CurrentCityData);
                    }
                    var jsonResult = Json(CurrentCity, JsonRequestBehavior.AllowGet);
                    jsonResult.MaxJsonLength = int.MaxValue;
                    return jsonResult;
                    // return Json(CurrentCity, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception EX)
            {
                return Json(EX.ToString(), JsonRequestBehavior.AllowGet);
            }
        }
        //Getting Data Based on Search Element-Schools,City,MLSID and used when dragged and zoom changed depends on boundaries 
        public async Task<JsonResult> GetListingsbySearchZoomDragonBoundaries(string searchele, string SortBy, string zoomlevel, string mapbounds, string PrvCount)
        {
            string UserID = "0", macAdress = "", sortby = SortBy;
            if (Request.Cookies["UseridCookie"] != null)
            {
                UserID = Request.Cookies["UseridCookie"].Value;
            }
            try
            {
                ManagementClass objMgmtCls = new ManagementClass("Win32_NetworkAdapterConfiguration");
                foreach (ManagementObject objMgmt in objMgmtCls.GetInstances())
                {
                    if (!(bool)objMgmt["ipEnabled"])
                        continue;
                    macAdress = objMgmt["MACAddress"].ToString();
                }
                string Baseurl = ConfigurationManager.AppSettings["WebApiURL"];
                List<CurrentCityPropartyModel> result = new List<CurrentCityPropartyModel>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string Method = "/SearchMlsidCityProparty?Search=" + searchele + "&UserID=" + UserID + "&DeviceID=" + macAdress + "&SortBy=" + sortby + "&ZoomLevel=" + zoomlevel + "&MapBounds=" + mapbounds + "&PrvCount=" + PrvCount;
                    HttpResponseMessage Res = await client.GetAsync(Method);
                    if (Res.IsSuccessStatusCode)
                    {
                        var resultData = Res.Content.ReadAsStringAsync().Result;
                        result = JsonConvert.DeserializeObject<List<CurrentCityPropartyModel>>(resultData);
                     }
                    var jsonResult = Json(result, JsonRequestBehavior.AllowGet);
                   jsonResult.MaxJsonLength = int.MaxValue;
                    return jsonResult;
                }
            }
            catch (Exception EX)
            {
                return Json(EX, JsonRequestBehavior.AllowGet);
            }
        }

        
        public async Task<JsonResult> GetPropBySearchElementAndroid(string searchele, string SortBy, string zoomlevel, string mapbounds)
        {
            string UserID = "0", macAdress = "", sortby = SortBy;
            if (Request.Cookies["UseridCookie"] != null)
            {
                UserID = Request.Cookies["UseridCookie"].Value;
            }
            try
            {
                ManagementClass objMgmtCls = new ManagementClass("Win32_NetworkAdapterConfiguration");
                foreach (ManagementObject objMgmt in objMgmtCls.GetInstances())
                {
                    if (!(bool)objMgmt["ipEnabled"])
                        continue;
                    macAdress = objMgmt["MACAddress"].ToString();
                }
                string Baseurl = ConfigurationManager.AppSettings["WebApiURL"];
                List<CurrentCityPropartyModel> result = new List<CurrentCityPropartyModel>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string Method = "/SearchMlsidCityPropartyAndroid?Search=" + searchele + "&UserID=" + UserID + "&DeviceID=" + macAdress + "&SortBy=" + sortby + "&ZoomLevel=" + zoomlevel + "&MapBounds=" + mapbounds;
                    //string Method = "/SearchMlsidCityProparty?Search=" + searchele + "&UserID=" + UserID_;
                    HttpResponseMessage Res = await client.GetAsync(Method);
                    if (Res.IsSuccessStatusCode)
                    {
                        var resultData = Res.Content.ReadAsStringAsync().Result;
                        result = JsonConvert.DeserializeObject<List<CurrentCityPropartyModel>>(resultData);
                    }
                    var jsonResult = Json(result, JsonRequestBehavior.AllowGet);
                    jsonResult.MaxJsonLength = int.MaxValue;
                    return jsonResult;
                    //return Json(result, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception EX)
            {
                return Json(EX, JsonRequestBehavior.AllowGet);
            }
        }
        public async Task<JsonResult> GetPropFromLanding(string searchele, string class_type, string SortBy,string MapBounds)
        {
            string UserID = "0", macAdress = "", sortby = SortBy;
            if (Request.Cookies["UseridCookie"] != null)
            {
                UserID = Request.Cookies["UseridCookie"].Value;
            }
            try
            {
                ManagementClass objMgmtCls = new ManagementClass("Win32_NetworkAdapterConfiguration");
                foreach (ManagementObject objMgmt in objMgmtCls.GetInstances())
                {
                    if (!(bool)objMgmt["ipEnabled"])
                        continue;
                    macAdress = objMgmt["MACAddress"].ToString();
                }
                string Baseurl = ConfigurationManager.AppSettings["WebApiURL"];
                List<CurrentCityPropartyModel> result = new List<CurrentCityPropartyModel>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string Method = "/SearchMlsidCityPropartyWeb?Search=" + searchele + "&ClassID=" + class_type + "&UserID=" + UserID + "&DeviceID=" + macAdress + "&SortBy=" + sortby+"&MapBounds="+MapBounds;
                    //string Method = "/SearchMlsidCityProparty?Search=" + searchele + "&UserID=" + UserID_;
                    HttpResponseMessage Res = await client.GetAsync(Method);
                    if (Res.IsSuccessStatusCode)
                    {
                        var resultData = Res.Content.ReadAsStringAsync().Result;
                        result = JsonConvert.DeserializeObject<List<CurrentCityPropartyModel>>(resultData);
                    }
                    var jsonResult = Json(result, JsonRequestBehavior.AllowGet);
                    jsonResult.MaxJsonLength = int.MaxValue;
                    return jsonResult;
                    //return Json(result, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception EX)
            {
                return Json(EX, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult SaveHomeSearchFilters(List<String> obj)
        {
            //-----------To Get Mac Adress of Client Machine-----------------  
            string UserID = "", macAdress = "", res = "";
            if (Request.Cookies["UseridCookie"] != null)
            {
                UserID = Request.Cookies["UseridCookie"].Value;
            }
            ManagementClass objMgmtCls = new ManagementClass("Win32_NetworkAdapterConfiguration");
            foreach (ManagementObject objMgmt in objMgmtCls.GetInstances())
            {
                if (!(bool)objMgmt["ipEnabled"])
                    continue;
                macAdress = objMgmt["MACAddress"].ToString();
            }
            string pagesource = "";
            try
            {
                if (obj[4] == null) { obj[4] = ""; }
                if (obj[16] == null) { obj[16] = ""; }
                if (obj[17] == null) { obj[17] = ""; }
                if (obj[18] == null) { obj[18] = ""; }
                if (obj[21] == null) { obj[21] = ""; }
                if (obj[23] == null) { obj[23] = ""; }
                string urlAddress = ConfigurationManager.AppSettings["WebApiURL"]+"/SaveSearches?";

                using (WebClient client = new WebClient())
                {
                    NameValueCollection postData = new NameValueCollection()
                    {
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
            //return Json("", JsonRequestBehavior.AllowGet);
        }
        public JsonResult SaveRentalSearchFilters(List<String> obj)
        {
            //-----------To Get Mac Adress of Client Machine-----------------  
            string UserID = "", macAdress = "", res = "";
            if (Request.Cookies["UseridCookie"] != null)
            {
                UserID = Request.Cookies["UseridCookie"].Value;
            }
            ManagementClass objMgmtCls = new ManagementClass("Win32_NetworkAdapterConfiguration");
            foreach (ManagementObject objMgmt in objMgmtCls.GetInstances())
            {
                if (!(bool)objMgmt["ipEnabled"])
                    continue;
                macAdress = objMgmt["MACAddress"].ToString();
            }
            string pagesource = "";
            try
            {
                if (obj[4] == null) { obj[4] = ""; }
                if (obj[12] == null) { obj[16] = ""; }

                string urlAddress = ConfigurationManager.AppSettings["WebApiURL"]+"/SaveSearches?";

                using (WebClient client = new WebClient())
                {
                    NameValueCollection postData = new NameValueCollection()
                    {
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
            //return Json("", JsonRequestBehavior.AllowGet);
        }
        public JsonResult SaveLotsSearchFilters(List<String> obj)
        {
            //-----------To Get Mac Adress of Client Machine-----------------  
            string UserID = "", macAdress = "", res = "";
            if (Request.Cookies["UseridCookie"] != null)
            {
                UserID = Request.Cookies["UseridCookie"].Value;
            }
            ManagementClass objMgmtCls = new ManagementClass("Win32_NetworkAdapterConfiguration");
            foreach (ManagementObject objMgmt in objMgmtCls.GetInstances())
            {
                if (!(bool)objMgmt["ipEnabled"])
                    continue;
                macAdress = objMgmt["MACAddress"].ToString();
            }
            string pagesource = "";
            try
            {
                if (obj[4] == null) { obj[4] = ""; }
                if (obj[9] == null) { obj[16] = ""; }

                string urlAddress = ConfigurationManager.AppSettings["WebApiURL"]+"/SaveSearches?";

                using (WebClient client = new WebClient())
                {
                    NameValueCollection postData = new NameValueCollection()
                    {
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
            //return Json("", JsonRequestBehavior.AllowGet);
        }
        public JsonResult FavrouriteChanges(string MLSId, string Fav)
        {
            var UserID = "0";
            string macAdress = "";
            string Is_Fav = "true";
            string ResponceData = "";
            if (Fav == "0")
            {
                Is_Fav = "false";
            }
            try
            {
                if (Request.Cookies["UseridCookie"] != null)
                {
                    UserID = Request.Cookies["UseridCookie"].Value;
                }
                ManagementClass objMgmtCls = new ManagementClass("Win32_NetworkAdapterConfiguration");
                foreach (ManagementObject objMgmt in objMgmtCls.GetInstances())
                {
                    if (!(bool)objMgmt["ipEnabled"])
                        continue;
                    macAdress = objMgmt["MACAddress"].ToString();
                }
                string urlAddress = ConfigurationManager.AppSettings["WebApiURL"]+"/UserFavoritesPost?";
                using (WebClient client = new WebClient())
                {
                    NameValueCollection postData = new NameValueCollection()
                    {
                      { "UserID", UserID },
                      { "MLSID", MLSId },
                      { "Is_Favorite",Is_Fav},
                      { "Is_DriveBy","false"}
                    };
                    ResponceData = Encoding.UTF8.GetString(client.UploadValues(urlAddress, postData));
                    var response = JsonConvert.DeserializeObject(ResponceData);
                    if (response.ToString() == "Successfully Created.")
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
        public JsonResult SaveMLSIDAsViewedByUser(string MLSId)
        {
            string UserID = "0", macAdress = "";
            string ResponceData = "";
            try
            {
                ManagementClass objMgmtCls = new ManagementClass("Win32_NetworkAdapterConfiguration");
                foreach (ManagementObject objMgmt in objMgmtCls.GetInstances())
                {
                    if (!(bool)objMgmt["ipEnabled"])
                        continue;
                    macAdress = objMgmt["MACAddress"].ToString();
                }
                if (Request.Cookies["UseridCookie"] != null)
                {
                    UserID = Request.Cookies["UseridCookie"].Value;
                }
                string urlAddress = ConfigurationManager.AppSettings["WebApiURL"]+"/UserViewedMlsPost?";
                using (WebClient client = new WebClient())
                {
                    NameValueCollection postData = new NameValueCollection()
                    {
                      { "UserID", UserID },
                      { "DeviceID", macAdress },
                      { "MLSID", MLSId },
                      { "OS_TypeID","1"}
                    };
                    ResponceData = Encoding.UTF8.GetString(client.UploadValues(urlAddress, postData));
                    var response = JsonConvert.DeserializeObject(ResponceData);
                    if (response.ToString() == "Successfully Created.")
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
        public JsonResult UserNoteUpdate(string MLSID, string Usernote)
        {
            var UserID = "0";

            string ResponceData = "";
            try
            {
                if (Request.Cookies["UseridCookie"] != null)
                {
                    UserID = Request.Cookies["UseridCookie"].Value;
                }
                string urlAddress = ConfigurationManager.AppSettings["WebApiURL"]+"/UserNotePost?";
                using (WebClient client = new WebClient())
                {
                    NameValueCollection postData = new NameValueCollection()
                    {
                      { "UserID", UserID },
                      { "DeviceID", "1" },
                      { "MLSID", MLSID },
                      { "Note",Usernote},
                    };
                    ResponceData = Encoding.UTF8.GetString(client.UploadValues(urlAddress, postData));
                    var response = JsonConvert.DeserializeObject(ResponceData);
                    if (response.ToString() == "Successfully Created.")
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
        public async Task<JsonResult> GetUserNoteData(String MLSID)
        {
            string UserID = "";
            if (Request.Cookies["UseridCookie"] != null)
            {
                UserID = Request.Cookies["UseridCookie"].Value;
            }
            try
            {
                string Baseurl = ConfigurationManager.AppSettings["WebApiURL"];
                List<CurrentCityPropartyModel> UserNote = new List<CurrentCityPropartyModel>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string Method = "/UserNoteGet?FK_UserID=" + UserID + "&MLSID=" + MLSID;
                    HttpResponseMessage Res = await client.GetAsync(Method);
                    if (Res.IsSuccessStatusCode)
                    {
                        var UserNoteData = Res.Content.ReadAsStringAsync().Result;
                        UserNote = JsonConvert.DeserializeObject<List<CurrentCityPropartyModel>>(UserNoteData);
                    }
                    return Json(UserNote, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception EX)
            {
                return Json(EX, JsonRequestBehavior.AllowGet);
            }
        }
        public async Task<JsonResult> GetFullDetails(int MLSID)
        {
            var UserID = "0";
            try
            {
                if (Request.Cookies["UseridCookie"] != null)
                {
                    UserID = Request.Cookies["UseridCookie"].Value;
                }
                string Baseurl = ConfigurationManager.AppSettings["WebApiURL"];
                List<PropertyDetailsModel> CurrentCity = new List<PropertyDetailsModel>();

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string Method = "/PropartyByMLSID?MLSID=" + MLSID + "&UserID=" + UserID;
                    HttpResponseMessage Res = await client.GetAsync(Method);
                    if (Res.IsSuccessStatusCode)
                    {
                        var DataByMLSID = Res.Content.ReadAsStringAsync().Result;
                        CurrentCity = JsonConvert.DeserializeObject<List<PropertyDetailsModel>>(DataByMLSID);
                    }
                    var jsonResult = Json(CurrentCity, JsonRequestBehavior.AllowGet);
                    jsonResult.MaxJsonLength = int.MaxValue;
                    return jsonResult;
                    // return Json(CurrentCity, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception EX)
            {
                return Json(EX, JsonRequestBehavior.AllowGet);
            }
        }
        //Method to Get The Saved SEarch Filtes Names to bind to DropDown List
        public async Task<JsonResult> GetSearchNames()
        {
            string UserID = "", macAdress = "";
            if (Request.Cookies["UseridCookie"] != null)
            {
                UserID = Request.Cookies["UseridCookie"].Value;
            }


            try
            {
                ManagementClass objMgmtCls = new ManagementClass("Win32_NetworkAdapterConfiguration");
                foreach (ManagementObject objMgmt in objMgmtCls.GetInstances())
                {
                    if (!(bool)objMgmt["ipEnabled"])
                        continue;
                    macAdress = objMgmt["MACAddress"].ToString();
                }
                string Baseurl = ConfigurationManager.AppSettings["WebApiURL"];
                List<SavedSearchDeatails> savedSearchNames = new List<SavedSearchDeatails>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string Method = "/SavedSearchesGet?FK_UserID=" + UserID;
                    HttpResponseMessage Res = await client.GetAsync(Method);
                    if (Res.IsSuccessStatusCode)
                    {
                        var savedSearchNamesData = Res.Content.ReadAsStringAsync().Result;
                        savedSearchNames = JsonConvert.DeserializeObject<List<SavedSearchDeatails>>(savedSearchNamesData);
                    }
                    var jsonResult = Json(savedSearchNames, JsonRequestBehavior.AllowGet);
                    jsonResult.MaxJsonLength = int.MaxValue;
                    return jsonResult;
                    //return Json(savedSearchNames, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception EX)
            {
                return Json(EX, JsonRequestBehavior.AllowGet);
            }
        }
        //Method to Get Property List based on Multiple MLSIDs which are in Polygon on Map
        public async Task<JsonResult> GetListByMultipleMLSIDs(string MLSIds, string Sortby)
        {
            string UserID = "0", macAdress = "", sortby = Sortby;
            if (Request.Cookies["UseridCookie"] != null)
            {
                UserID = Request.Cookies["UseridCookie"].Value;
            }
            if (MLSIds == "null" || MLSIds == null)
                MLSIds = "";
            try
            {
                ManagementClass objMgmtCls = new ManagementClass("Win32_NetworkAdapterConfiguration");
                foreach (ManagementObject objMgmt in objMgmtCls.GetInstances())
                {
                    if (!(bool)objMgmt["ipEnabled"])
                        continue;
                    macAdress = objMgmt["MACAddress"].ToString();
                }
                string Baseurl = ConfigurationManager.AppSettings["WebApiURL"];
                List<CurrentCityPropartyModel> result = new List<CurrentCityPropartyModel>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string Method = "/MultiMLSIDSProperty?MLSIDS=" + MLSIds + "&UserID=" + UserID + "&DeviceID=" + macAdress + "&SortBy=" + sortby;
                    //string Method = "/SearchMlsidCityProparty?Search=" + searchele + "&UserID=" + UserID_;
                    HttpResponseMessage Res = await client.GetAsync(Method);
                    if (Res.IsSuccessStatusCode)
                    {
                        var resultData = Res.Content.ReadAsStringAsync().Result;
                        result = JsonConvert.DeserializeObject<List<CurrentCityPropartyModel>>(resultData);
                    }
                    var jsonResult = Json(result, JsonRequestBehavior.AllowGet);
                    jsonResult.MaxJsonLength = int.MaxValue;
                    return jsonResult;
                    //return Json(result, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception EX)
            {
                return Json(EX, JsonRequestBehavior.AllowGet);
            }
        }
        //Method to get all searches history of user
        public async Task<JsonResult> getSavedSearchElements()
        {
            var UserID = "0";
            try
            {
                if (Request.Cookies["UseridCookie"] != null)
                {
                    UserID = Request.Cookies["UseridCookie"].Value;
                }

                string Baseurl = ConfigurationManager.AppSettings["WebApiURL"];
                List<UserSearchedelementsModal> searchdelements = new List<UserSearchedelementsModal>();

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string Method = "/UserSearchesGet?FK_UserID=" + UserID;
                    HttpResponseMessage Res = await client.GetAsync(Method);
                    if (Res.IsSuccessStatusCode)
                    {
                        var searchdelementsData = Res.Content.ReadAsStringAsync().Result;
                        searchdelements = JsonConvert.DeserializeObject<List<UserSearchedelementsModal>>(searchdelementsData);
                    }
                    var jsonResult = Json(searchdelements, JsonRequestBehavior.AllowGet);
                    jsonResult.MaxJsonLength = int.MaxValue;
                    return jsonResult;
                    // return Json(searchdelements, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception EX)
            {
                return Json(EX, JsonRequestBehavior.AllowGet);
            }
        }
        //Method to Get Autocomplete School Names From API
        public async Task<JsonResult> GetAllSchoolNames()
        {
            try
            {
                string Baseurl = ConfigurationManager.AppSettings["WebApiURL"];
                List<AllSchoolsAndSubDivisionsDetails> AllSchoolNames = new List<AllSchoolsAndSubDivisionsDetails>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string Method = "/SchoolsGet";
                    HttpResponseMessage Res = await client.GetAsync(Method);
                    if (Res.IsSuccessStatusCode)
                    {
                        var AllSchoolNamesData = Res.Content.ReadAsStringAsync().Result;
                        AllSchoolNames = JsonConvert.DeserializeObject<List<AllSchoolsAndSubDivisionsDetails>>(AllSchoolNamesData);
                    }
                    var jsonResult = Json(AllSchoolNames, JsonRequestBehavior.AllowGet);
                    jsonResult.MaxJsonLength = int.MaxValue;
                    return jsonResult;
                    //return Json(AllSchoolNames, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception EX)
            {
                return Json(EX, JsonRequestBehavior.AllowGet);
            }
        }
        //Method to Get Autocomplete Subdivisions Names From API
        public async Task<JsonResult> GetAllSubDivisionsNames()
        {
            try
            {
                string Baseurl = ConfigurationManager.AppSettings["WebApiURL"];
                List<AllSchoolsAndSubDivisionsDetails> AllSubDivisionNames = new List<AllSchoolsAndSubDivisionsDetails>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string Method = "/SubDivisionGet";
                    HttpResponseMessage Res = await client.GetAsync(Method);
                    if (Res.IsSuccessStatusCode)
                    {
                        var SubDivisionData = Res.Content.ReadAsStringAsync().Result;
                        AllSubDivisionNames = JsonConvert.DeserializeObject<List<AllSchoolsAndSubDivisionsDetails>>(SubDivisionData);
                    }
                    var jsonResult = Json(AllSubDivisionNames, JsonRequestBehavior.AllowGet);
                    jsonResult.MaxJsonLength = int.MaxValue;
                    return jsonResult;
                    // return Json(AllSubDivisionNames, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception EX)
            {
                return Json(EX, JsonRequestBehavior.AllowGet);
            }
        }
        //Get Elemantary school names
        public async Task<JsonResult> GetAllElementarySchoolNames()
        {
            try
            {
                string Baseurl = ConfigurationManager.AppSettings["WebApiURL"];
                List<AllSchoolsAndSubDivisionsDetails> AllSchoolNames = new List<AllSchoolsAndSubDivisionsDetails>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string Method = "/ElementarySchoolsGet";
                    HttpResponseMessage Res = await client.GetAsync(Method);
                    if (Res.IsSuccessStatusCode)
                    {
                        var AllSchoolNamesData = Res.Content.ReadAsStringAsync().Result;
                        AllSchoolNames = JsonConvert.DeserializeObject<List<AllSchoolsAndSubDivisionsDetails>>(AllSchoolNamesData);
                    }
                    var jsonResult = Json(AllSchoolNames, JsonRequestBehavior.AllowGet);
                    jsonResult.MaxJsonLength = int.MaxValue;
                    return jsonResult;
                }
            }
            catch (Exception EX)
            {
                return Json(EX, JsonRequestBehavior.AllowGet);
            }
        }
        //Get Middle school names
        public async Task<JsonResult> GetAllMiddleSchoolNames()
        {
            try
            {
                string Baseurl = ConfigurationManager.AppSettings["WebApiURL"];
                List<AllSchoolsAndSubDivisionsDetails> AllSchoolNames = new List<AllSchoolsAndSubDivisionsDetails>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string Method = "/MiddleSchoolsGet";
                    HttpResponseMessage Res = await client.GetAsync(Method);
                    if (Res.IsSuccessStatusCode)
                    {
                        var AllSchoolNamesData = Res.Content.ReadAsStringAsync().Result;
                        AllSchoolNames = JsonConvert.DeserializeObject<List<AllSchoolsAndSubDivisionsDetails>>(AllSchoolNamesData);
                    }
                    var jsonResult = Json(AllSchoolNames, JsonRequestBehavior.AllowGet);
                    jsonResult.MaxJsonLength = int.MaxValue;
                    return jsonResult;
                }
            }
            catch (Exception EX)
            {
                return Json(EX, JsonRequestBehavior.AllowGet);
            }
        }
        //Get High school names
        public async Task<JsonResult> GetAllHighSchoolNames()
        {
            try
            {
                string Baseurl = ConfigurationManager.AppSettings["WebApiURL"];
                List<AllSchoolsAndSubDivisionsDetails> AllSchoolNames = new List<AllSchoolsAndSubDivisionsDetails>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string Method = "/HighSchoolsGet";
                    HttpResponseMessage Res = await client.GetAsync(Method);
                    if (Res.IsSuccessStatusCode)
                    {
                        var AllSchoolNamesData = Res.Content.ReadAsStringAsync().Result;
                        AllSchoolNames = JsonConvert.DeserializeObject<List<AllSchoolsAndSubDivisionsDetails>>(AllSchoolNamesData);
                    }
                    var jsonResult = Json(AllSchoolNames, JsonRequestBehavior.AllowGet);
                    jsonResult.MaxJsonLength = int.MaxValue;
                    return jsonResult;
                }
            }
            catch (Exception EX)
            {
                return Json(EX, JsonRequestBehavior.AllowGet);
            }
        }
        //Method to getListing for Filters in Serch Filter module for 3 types of classes
        public JsonResult GetListingsbyFilters(List<String> obj)
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
                        {"From_Price",obj[5]},
                        {"To_Price",obj[6]},
                        { "BedRooms",obj[7]},
                        {"BathRooms",obj[8]},
                        {"Sqft_from",obj[9]},
                        {"Sqft_to",obj[10]},
                        {"Master_Bedroom",obj[11]},
                        {"Stories",obj[12]},
                        {"Garage_Capacity",obj[13]},
                        {"Garage_Type",obj[14]},
                        {"Lot_Acreage_From",obj[15]},
                        {"Lot_Acreage_To",obj[16]},
                        {"SubDivision",obj[17]},
                        {"Age",obj[18]},
                        {"Lot_Features",obj[18]},
                        {"FoundationType",obj[20]},
                        {"Exterior_Features",obj[21]},
                        {"Interior_Features",obj[22]},
                        {"FlooringType",obj[23]},
                        {"Appliances",obj[24]},
                        { "Master_Features",obj[25]},
                        {"LandaryRoom",obj[26]},
                        {"FirePlace",obj[27]},
                        {"CommunityAmenities",obj[28]},
                        {"HeatingSystem",obj[29]},
                        {"WaterHeater",obj[30]},
                        {"WaterType",obj[31]},
                        {"SewerType",obj[32]},
                        {"Elementary",obj[33]},
                        {"Middle",obj[34]},
                        {"High",obj[35]},
                        {"ParkingType",obj[36]},
                        {"FK_UserID",UserID},
                        {"SortBy",obj[37]},
                        {"PolygonPath",obj[38] },
                        {"MapBounds",obj[39] },
                        {"ZoomLevel",obj[40] },
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
        //Method to save searched filters for 3 types of classes
        public JsonResult SaveFilters(List<String> obj)
        {
            //-----------To Get Mac Adress of Client Machine-----------------  
            string UserID = "", macAdress = "", res = "";
            if (Request.Cookies["UseridCookie"] != null)
            {
                UserID = Request.Cookies["UseridCookie"].Value;
            }
            ManagementClass objMgmtCls = new ManagementClass("Win32_NetworkAdapterConfiguration");
            foreach (ManagementObject objMgmt in objMgmtCls.GetInstances())
            {
                if (!(bool)objMgmt["ipEnabled"])
                    continue;
                macAdress = objMgmt["MACAddress"].ToString();
            }
            string pagesource = "";
            try
            {
                string urlAddress = ConfigurationManager.AppSettings["WebApiURL"]+"/SaveSearches?";
                using (WebClient client = new WebClient())
                {
                    NameValueCollection postData = new NameValueCollection()
                    {

                        {"FilterType",obj[0]},
                        {"ClassType",obj[1]},
                        {"DrivingDistMinuis",obj[2]},
                        {"DrivinDistLocation",obj[3]},
                        {"PropartyType",obj[4]},
                        {"FromPrice",obj[5]},
                        {"ToPrice",obj[6]},
                        {"Bathrooms",obj[7]},
                        {"Bedrooms",obj[8]},
                        {"SqftFrom",obj[9]},
                        {"SqftTo",obj[10]},
                        {"MasterBedroom",obj[11]},
                        {"Stores",obj[12]},
                        {"GarageCapacity",obj[13]},
                        {"GarageType",obj[14]},
                        {"LotAcreageFrom",obj[15]},
                        {"LotAcreageTo",obj[16]},
                        {"SubDivison",obj[17]},
                        {"Age",obj[18]},
                        {"LotFeatures",obj[19]},
                        {"FoundationType",obj[20]},
                        {"ExteriorFeatures",obj[21]},
                        {"interiorFeatures",obj[22]},
                        {"FlooringType",obj[23]},
                        {"Appliances",obj[24]},
                        { "MasterFeatures",obj[25]},
                        {"LaundryRoom",obj[26]},
                        {"FirePlace",obj[27]},
                        {"CommunityAmenities",obj[28]},
                        {"HeatingSyatem",obj[29]},
                        {"WaterHeater",obj[30]},
                        {"WaterType",obj[31]},
                        {"SEWER_TYPE",obj[32]},
                        {"Elementary",obj[33]},
                        {"Middle",obj[34] },
                        {"High",obj[35] },
                        {"ParkingType",obj[36]},
                        {"FK_UserID",UserID},
                        {"PloygonPath",obj[38] },
                        {"MapBounds",obj[39] },
                        {"ZoomLevel",obj[40] },
                        {"SearchName",obj[41]},
                        {"CitiesList",obj[42] },
                        {"ResultCount",obj[43] },
                        {"User_DeviceID",macAdress},
                        {"OS_Type_ID","1" },

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
            //return Json("", JsonRequestBehavior.AllowGet);
        }
    }
}