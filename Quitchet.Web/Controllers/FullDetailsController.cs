using Newtonsoft.Json;
using Quitchet.Core.Models.Agent;
using Quitchet.Core.Models.Customer;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Quitchet.Web.Controllers
{
    public class FullDetailsController : Controller
    {
        //
        // GET: /FullDetails/
        public class resultClassModel
        {
            public string Status { get; set; }
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ExpandedView()
        {
            return View();
        }
        public ActionResult CustomerExpandedView()
        {
            return View();
        }
        public async Task<JsonResult> GetFullDetailsByMLSIFD(string AgentID, string MlsID)
        {
            try
            {
                string Baseurl = ConfigurationManager.AppSettings["WebApiURL"];
                List<AgentlistPopUPFullDetails> FullDetails = new List<AgentlistPopUPFullDetails>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string Method = "/PropertySearchByMLSIDWithAgentGet?MLSID=" + MlsID + "&AgentID=" + AgentID;
                    HttpResponseMessage Res = await client.GetAsync(Method);
                    if (Res.IsSuccessStatusCode)
                    {
                        var AgentFullDetails = Res.Content.ReadAsStringAsync().Result;
                        FullDetails = JsonConvert.DeserializeObject<List<AgentlistPopUPFullDetails>>(AgentFullDetails);
                    }
                    return Json(FullDetails, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception EX)
            {
                return Json(EX, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult PostAGLMultiUploadImages(string AgentID, string MLSID, string PhotoBase)
        {
            string ResponceData = "";
            try
            {
                string urlAddress = ConfigurationManager.AppSettings["WebApiURL"]+"/UserAddedPropertyPhotosPost?";
                using (WebClient client = new WebClient())
                {
                    NameValueCollection postData = new NameValueCollection()
                    {
                      { "MLSID",MLSID },
                      { "UserID",AgentID },
                      { "Os_TypeID","1" },
                      { "Photo",PhotoBase },
                    };
                    ResponceData = Encoding.UTF8.GetString(client.UploadValues(urlAddress, postData));
                    var response = JsonConvert.DeserializeObject(ResponceData);
                    if (response.ToString() == "Successfully Saved.")
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
        public JsonResult FavrouriteChanges(string MLSId, string Fav)
        {
            var UserID = "0";
            string macAdress = " ";
            string ResponceData = "";
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
                      { "DeviceID", macAdress },
                      { "MLSID", MLSId },
                      { "Is_Favorite",Fav},
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

            //return Json("", JsonRequestBehavior.AllowGet);
        }

        public JsonResult ShowingCheckINOUT(string AgentID, string MLSID, string FK_RequestShowingsID)
        {
            string ResponceData = "";
            try
            {
                string urlAddress = ConfigurationManager.AppSettings["WebApiURL"]+"/AgentShowingsCheckInOutPost?";
                using (WebClient client = new WebClient())
                {
                    NameValueCollection postData = new NameValueCollection()
                    {
                      { "AgentID",AgentID},
                      { "MLSID", MLSID },
                      { "FK_RequestShowingsID",FK_RequestShowingsID}
                    };
                    ResponceData = Encoding.UTF8.GetString(client.UploadValues(urlAddress, postData));
                    var proplist = JsonConvert.DeserializeObject<List<AgentlistPopUPFullDetails>>(ResponceData);
                    var jsonResult = Json(proplist, JsonRequestBehavior.AllowGet);
                    jsonResult.MaxJsonLength = int.MaxValue;
                    return jsonResult;
                }
            }
            catch (Exception EX)
            {
                return Json(EX, JsonRequestBehavior.AllowGet);
            }

        }
        public async Task<JsonResult> GetShowingSecurityDetails(string AgentID)
        {
            try
            {
                string Baseurl = ConfigurationManager.AppSettings["WebApiURL"];
                List<AgentlistPopUPFullDetails> AgentDetails = new List<AgentlistPopUPFullDetails>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string Method = "/AgentShowingSettingsGet?AgentID=" + AgentID;
                    HttpResponseMessage Res = await client.GetAsync(Method);
                    if (Res.IsSuccessStatusCode)
                    {
                        var DataByVIEWID = Res.Content.ReadAsStringAsync().Result;
                        AgentDetails = JsonConvert.DeserializeObject<List<AgentlistPopUPFullDetails>>(DataByVIEWID);
                    }
                    return Json(AgentDetails, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception EX)
            {
                return Json(EX, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult FullDetailsRequestTime(string FK_RequestShowingsID, string Time, string MLSID)
        {
            string ResponceData = "";
            try
            {
                string urlAddress = ConfigurationManager.AppSettings["WebApiURL"]+"/AgentShowingsAddTimerPost?";
                using (WebClient client = new WebClient())
                {
                    NameValueCollection postData = new NameValueCollection()
                    {
                      { "FK_RequestShowingsID",    FK_RequestShowingsID },
                      { "Time",    Time },
                      { "MLSID",    MLSID },
                    };
                    // ResponceData = Encoding.UTF8.GetString(client.UploadValues(urlAddress, postData));
                    //var response = JsonConvert.DeserializeObject(ResponceData);
                    //return Json(response.ToString(), JsonRequestBehavior.AllowGet);
                    ResponceData = Encoding.UTF8.GetString(client.UploadValues(urlAddress, postData));
                    var proplist = JsonConvert.DeserializeObject<List<AgentlistPopUPFullDetails>>(ResponceData);
                    var jsonResult = Json(proplist, JsonRequestBehavior.AllowGet);
                    jsonResult.MaxJsonLength = int.MaxValue;
                    return jsonResult;
                }
            }

            catch (Exception EX)
            {
                return Json(EX, JsonRequestBehavior.AllowGet);
            }
        }

        //------------------Get Full Details of Customer Listing View
        public async Task<JsonResult> GetCustomerListingDetails(String MLSID,string UserID)
        {
            
            try
            {
                if (UserID == null)
                {
                    UserID = "0";
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
                }
            }
            catch (Exception EX)
            {
                return Json(EX, JsonRequestBehavior.AllowGet);
            }
        }
        //----------------------Method for Emergency Check out for Showings---------------//
        public JsonResult PostEmergencyCallforShowing(string AgentID, string PK_RequestShowingID, string MLSID)
        {
            string ResponceData = "";
            try
            {
                string urlAddress = ConfigurationManager.AppSettings["WebApiURL"]+"/AgentShowingsEmergencyPost?";
                using (WebClient client = new WebClient())
                {
                    NameValueCollection postData = new NameValueCollection()
                    {
                      { "AgentID",    AgentID },
                      { "PK_RequestShowingID",    PK_RequestShowingID },
                      { "MLSID",    MLSID },
                    };
                    ResponceData = Encoding.UTF8.GetString(client.UploadValues(urlAddress, postData));
                    var proplist = JsonConvert.DeserializeObject<List<resultClassModel>>(ResponceData);

                    if (proplist[0].Status.ToString() == "Emergency")
                    {
                        return Json("Success", JsonRequestBehavior.AllowGet);
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

    }
}