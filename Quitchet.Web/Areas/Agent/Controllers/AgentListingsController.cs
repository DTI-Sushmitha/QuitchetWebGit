using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Quitchet.Core.Models.Agent;
using Quitchet.Core.Models.Customer;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Quitchet.Web.Areas.Agent.Controllers
{
    public class AgentListingsController : Controller
    {
        // GET: Agent/AgentListings
        public ActionResult Index()
        {
            return View();
        }
        public class resultClassModel
        {
            public string STATUS { get; set; }
            public string Status { get; set; }
        }
        public async Task<JsonResult> GetAgentActiveListingdetails(List<string> ActiveData)
        {
            string UserID = "";
            string Class = "";
            string Status = "";
            var Posted_FromDate = "";
            var Posted_ToDate = "";
            string Seller = "";
            var AgentID = "";
            if (Request.Cookies["UseridCookie"] != null)
            {
                UserID = Request.Cookies["UseridCookie"].Value;
            }
            if (ActiveData == null)
            {
                Class = "";
                Status = "";
                Posted_FromDate = "";
                Posted_ToDate = "";
                Seller = "";
            }
            else
            {
                Class = ActiveData[2];
                Status = ActiveData[1];
                Posted_FromDate = ActiveData[3];
                Posted_ToDate = ActiveData[4];
                Seller = ActiveData[5];
                AgentID = ActiveData[0];
            }
            try
            {
                string Baseurl = ConfigurationManager.AppSettings["WebApiURL"];
                List<AgentListingModel> PropDetails = new List<AgentListingModel>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string Method = "/MyActiveListingsGet?AgentID=" + AgentID + "&Class=" + Class + "&Status=" + Status + "&Posted_FromDate=" + Posted_FromDate + "&Posted_ToDate=" + Posted_ToDate + "&Seller=" + Seller;
                    HttpResponseMessage Res = await client.GetAsync(Method);
                    if (Res.IsSuccessStatusCode)
                    {
                        var DataByagent = Res.Content.ReadAsStringAsync().Result;
                        PropDetails = JsonConvert.DeserializeObject<List<AgentListingModel>>(DataByagent);

                    }
                    return Json(PropDetails, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception EX)
            {
                return Json(EX, JsonRequestBehavior.AllowGet);
            }
        }
        public async Task<JsonResult> GetAgentScheduleShowingsdetails(List<string> ActiveData)
        {
            string UserID = "";
            string Class = "";
            string Status = "";
            var Posted_FromDate = "";
            var Posted_ToDate = "";
            string Seller = "";
            var AgentID = "";
            if (Request.Cookies["UseridCookie"] != null)
            {
                UserID = Request.Cookies["UseridCookie"].Value;
            }
            if (ActiveData == null)
            {
                Class = "";
                Status = "";
                Posted_FromDate = "";
                Posted_ToDate = "";
                Seller = "";
            }
            else
            {
                Class = ActiveData[2];
                Status = ActiveData[1];
                Posted_FromDate = ActiveData[3];
                Posted_ToDate = ActiveData[4];
                Seller = ActiveData[5];
                AgentID = ActiveData[0];
            }
            try
            {
                string Baseurl = ConfigurationManager.AppSettings["WebApiURL"];
                List<AgentListingModel> PropDetails = new List<AgentListingModel>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string Method = "/AgentActiveListingsScheduleShowingsGetWeb?AgentID=" + AgentID + "&Class=" + Class + "&Status=" + Status + "&Posted_FromDate=" + Posted_FromDate + "&Posted_ToDate=" + Posted_ToDate + "&Seller=" + Seller;
                    HttpResponseMessage Res = await client.GetAsync(Method);
                    if (Res.IsSuccessStatusCode)
                    {
                        var DataByagent = Res.Content.ReadAsStringAsync().Result;
                        PropDetails = JsonConvert.DeserializeObject<List<AgentListingModel>>(DataByagent);

                    }
                    return Json(PropDetails, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception EX)
            {
                return Json(EX, JsonRequestBehavior.AllowGet);
            }
        }
        public async Task<JsonResult> GetMyListingPropdetails(string MLSID)
        {
            try
            {
                string Baseurl = ConfigurationManager.AppSettings["WebApiURL"];
                List<AgentListingModel> PropDetails = new List<AgentListingModel>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string Method = "/SellerWithMLSIDGet?MLSID=" + MLSID;
                    HttpResponseMessage Res = await client.GetAsync(Method);
                    if (Res.IsSuccessStatusCode)
                    {
                        var DataByagent = Res.Content.ReadAsStringAsync().Result;
                        PropDetails = JsonConvert.DeserializeObject<List<AgentListingModel>>(DataByagent);

                    }
                    return Json(PropDetails, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception EX)
            {
                return Json(EX, JsonRequestBehavior.AllowGet);
            }
        }
        public async Task<JsonResult> GetMyListingOpenHousedetails(string MLSID, string Agentid)
        {
            try
            {
                string Baseurl = ConfigurationManager.AppSettings["WebApiURL"];
                List<AgentListingModel> PropDetails = new List<AgentListingModel>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string Method = "/MyListingsOpenHousesGet?AgentID=" + Agentid + "&MLSID=" + MLSID;
                    HttpResponseMessage Res = await client.GetAsync(Method);
                    if (Res.IsSuccessStatusCode)
                    {
                        var DataByagent = Res.Content.ReadAsStringAsync().Result;
                        PropDetails = JsonConvert.DeserializeObject<List<AgentListingModel>>(DataByagent);

                    }
                    return Json(PropDetails, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception EX)
            {
                return Json(EX, JsonRequestBehavior.AllowGet);
            }
        }
        public async Task<JsonResult> GetMyListingScheduleShowingsdetails(string MLSID, string Agentid)
        {
            try
            {
                string Baseurl = ConfigurationManager.AppSettings["WebApiURL"];
                List<AgentListingModel> PropDetails = new List<AgentListingModel>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string Method = "/ListingWiseScheduleShowingsGet?AgentID=" + Agentid + "&MLSID=" + MLSID;
                    HttpResponseMessage Res = await client.GetAsync(Method);
                    if (Res.IsSuccessStatusCode)
                    {
                        var DataByagent = Res.Content.ReadAsStringAsync().Result;
                        PropDetails = JsonConvert.DeserializeObject<List<AgentListingModel>>(DataByagent);

                    }
                    return Json(PropDetails, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception EX)
            {
                return Json(EX, JsonRequestBehavior.AllowGet);
            }
        }
        public async Task<JsonResult> GetMyListingCompletedShowingsdetails(string MLSID, string Agentid)
        {
            try
            {
                string Baseurl = ConfigurationManager.AppSettings["WebApiURL"];
                List<AgentListingModel> PropDetails = new List<AgentListingModel>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string Method = "/MyListingsCompletedShowingsGet?AgentID=" + Agentid + "&MLSID=" + MLSID;
                    HttpResponseMessage Res = await client.GetAsync(Method);
                    if (Res.IsSuccessStatusCode)
                    {
                        var DataByagent = Res.Content.ReadAsStringAsync().Result;
                        PropDetails = JsonConvert.DeserializeObject<List<AgentListingModel>>(DataByagent);

                    }
                    return Json(PropDetails, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception EX)
            {
                return Json(EX, JsonRequestBehavior.AllowGet);
            }
        }
        public async Task<JsonResult> GetSellerslist(string SellerCode, string AgentID)
        {
            try
            {
                string Baseurl = ConfigurationManager.AppSettings["WebApiURL"];
                List<AgentListingModel> AgentDetails = new List<AgentListingModel>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string Method = "/AllMySellersGet?AgentID=" + AgentID;
                    HttpResponseMessage Res = await client.GetAsync(Method);
                    if (Res.IsSuccessStatusCode)
                    {
                        var DataByVIEWID = Res.Content.ReadAsStringAsync().Result;
                        AgentDetails = JsonConvert.DeserializeObject<List<AgentListingModel>>(DataByVIEWID);
                    }
                    return Json(AgentDetails, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception EX)
            {
                return Json(EX, JsonRequestBehavior.AllowGet);
            }
        }
        public async Task<JsonResult> GetHostlist(string HostName)
        {
            try
            {
                string Baseurl = ConfigurationManager.AppSettings["WebApiURL"];
                List<AgentListingModel> AgentDetails = new List<AgentListingModel>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string Method = "/SearchAgentsListForHostGet?Name=" + HostName;
                    HttpResponseMessage Res = await client.GetAsync(Method);
                    if (Res.IsSuccessStatusCode)
                    {
                        var DataByVIEWID = Res.Content.ReadAsStringAsync().Result;
                        AgentDetails = JsonConvert.DeserializeObject<List<AgentListingModel>>(DataByVIEWID);
                    }
                    return Json(AgentDetails, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception EX)
            {
                return Json(EX, JsonRequestBehavior.AllowGet);
            }
        }
        public async Task<JsonResult> GetAgentDetails(string AgentID)
        {
            try
            {
                string Baseurl = ConfigurationManager.AppSettings["WebApiURL"];
                List<AgentListingModel> AgentDetails = new List<AgentListingModel>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string Method = "/AgentGet?AgentID=" + AgentID;
                    HttpResponseMessage Res = await client.GetAsync(Method);
                    if (Res.IsSuccessStatusCode)
                    {
                        var DataByVIEWID = Res.Content.ReadAsStringAsync().Result;
                        AgentDetails = JsonConvert.DeserializeObject<List<AgentListingModel>>(DataByVIEWID);
                    }
                    return Json(AgentDetails, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception EX)
            {
                return Json(EX, JsonRequestBehavior.AllowGet);
            }
        }
        public async Task<JsonResult> GetMyManagePropertyDetails(string PKID, string AgentID)
        {
            try
            {
                string Baseurl = ConfigurationManager.AppSettings["WebApiURL"];
                List<AgentListingModel> AgentDetails = new List<AgentListingModel>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string Method = "/GetOpenHousesByPK?PK_OpenHouseID=" + PKID + "&AgentID=" + AgentID;
                    HttpResponseMessage Res = await client.GetAsync(Method);
                    if (Res.IsSuccessStatusCode)
                    {
                        var DataByVIEWID = Res.Content.ReadAsStringAsync().Result;
                        AgentDetails = JsonConvert.DeserializeObject<List<AgentListingModel>>(DataByVIEWID);
                    }
                    return Json(AgentDetails, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception EX)
            {
                return Json(EX, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult PostAgentOpenHouse(List<string> ActiveData)
        {
            var AgentID = ActiveData[0];
            var MLSID = ActiveData[1];
            var Title = ActiveData[2];
            var Date = ActiveData[3];
            var Location = ActiveData[6];
            var Notes = ActiveData[7];
            var Reminder = ActiveData[8];
            var HostAgentID = ActiveData[9];
            var OpenHouseType = ActiveData[10];
            var FromTime = Convert.ToDateTime(ActiveData[4]).ToString("HH:mm:ss");
            var ToTime = Convert.ToDateTime(ActiveData[5]).ToString("HH:mm:ss");
            var Invitees = ActiveData[11];
            string ResponceData = "";
            try
            {
                string urlAddress = ConfigurationManager.AppSettings["WebApiURL"]+"/OpenHousePost?";
                using (WebClient client = new WebClient())
                {
                    NameValueCollection postData = new NameValueCollection()
                    {
                      { "OpenHouseType",    OpenHouseType },
                      { "AgentID",    AgentID },
                      { "MLSID",   MLSID },
                      { "Title" ,Title},
                      { "Date",Date},
                      { "FromTime",FromTime.Replace('.',':')},
                      { "ToTime",ToTime.Replace('.',':')},
                      { "Location",Location },
                      { "Notes",Notes},
                      { "Reminder",Reminder},
                      { "HostAgentID", HostAgentID},
                       { "Invitees", Invitees},
                    };
                    ResponceData = Encoding.UTF8.GetString(client.UploadValues(urlAddress, postData));
                    var response = JsonConvert.DeserializeObject(ResponceData);
                    if (response.ToString() == "Successfully Updated.")
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
        public JsonResult SendEditFeedback(string PK_ShowingFeedbackID, string Comments, string AllowFeedbackToSeller)
        {
            string ResponceData = "";
            try
            {
                string urlAddress = ConfigurationManager.AppSettings["WebApiURL"]+"/EditShowingsFeedbackPost?";
                using (WebClient client = new WebClient())
                {
                    NameValueCollection postData = new NameValueCollection()
                    {
                      { "PK_ShowingFeedbackID",    PK_ShowingFeedbackID },
                      { "Comments",    Comments },
                      { "AllowFeedbackToSeller",   AllowFeedbackToSeller },
                    };
                    ResponceData = Encoding.UTF8.GetString(client.UploadValues(urlAddress, postData));
                    var response = JsonConvert.DeserializeObject(ResponceData);
                    if (response.ToString() == "Successfully Updated.")
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
        public JsonResult SendAgentEditFeedback(string PK_AgentShowingFeedbackID, string Comments, string AllowFeedbackToSeller)
        {
            string ResponceData = "";
            try
            {
                string urlAddress = ConfigurationManager.AppSettings["WebApiURL"]+"/EditAgentShowingsFeedbackPost?";
                using (WebClient client = new WebClient())
                {
                    NameValueCollection postData = new NameValueCollection()
                    {
                      { "PK_AgentShowingFeedbackID",    PK_AgentShowingFeedbackID },
                      { "Comments",    Comments },
                      { "AllowFeedbackToSeller",   AllowFeedbackToSeller },
                    };
                    ResponceData = Encoding.UTF8.GetString(client.UploadValues(urlAddress, postData));
                    var response = JsonConvert.DeserializeObject(ResponceData);
                    if (response.ToString() == "Successfully Updated.")
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
        public JsonResult DeleteOpenHouse(string OpenHouseID)
        {
            string ResponceData = "";
            try
            {
                string urlAddress = ConfigurationManager.AppSettings["WebApiURL"]+"/DeleteOpenHousePost?";
                using (WebClient client = new WebClient())
                {
                    NameValueCollection postData = new NameValueCollection()
                    {
                      { "PK_OpenHouseID",    OpenHouseID },
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
        public JsonResult EditPublicOpenHouse(List<string> ActiveData)
        {
            var OpenHouseID = ActiveData[0];
            var OpenHouseType = ActiveData[1];
            var AgentID = ActiveData[2];
            var MLSID = ActiveData[3];
            var Title = ActiveData[4];
            var Date = ActiveData[5];
            var FromTime = Convert.ToDateTime(ActiveData[6]).ToString("HH:mm:ss");
            var ToTime = Convert.ToDateTime(ActiveData[7]).ToString("HH:mm:ss");
            var Location = ActiveData[8];
            var Notes = ActiveData[9];
            var Reminder = ActiveData[10];
            var HostAgentID = ActiveData[11];
            var Invitees = ActiveData[12];
            string ResponceData = "";
            try
            {
                string urlAddress = ConfigurationManager.AppSettings["WebApiURL"]+"/EditOpenHousePost?";
                using (WebClient client = new WebClient())
                {
                    NameValueCollection postData = new NameValueCollection()
                    {
                      { "PK_OpenHouseID", OpenHouseID },
                      { "OpenHouseType", OpenHouseType },
                      { "AgentID",   AgentID },
                      { "MLSID" ,MLSID},
                      { "Title",Title},
                      { "Date",Date},
                      { "FromTime",FromTime},
                      { "ToTime",ToTime },
                      { "Location",Location},
                      { "Notes",Notes},
                      { "Reminder", Reminder},
                       { "HostAgentID", HostAgentID},
                       { "Invitees", Invitees},
                    };
                    ResponceData = Encoding.UTF8.GetString(client.UploadValues(urlAddress, postData));
                    var response = JsonConvert.DeserializeObject(ResponceData);
                    if (response.ToString() == "Successfully Updated.")
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
        public async Task<JsonResult> GetAgentInviteesDetails(string OpenHouseID)
        {
            try
            {
                string Baseurl = ConfigurationManager.AppSettings["WebApiURL"];
                List<AgentListingModel> AgentDetails = new List<AgentListingModel>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string Method = "/GetOpenHousesInvitees?PK_OpenHouseID=" + OpenHouseID;
                    HttpResponseMessage Res = await client.GetAsync(Method);
                    if (Res.IsSuccessStatusCode)
                    {
                        var DataByVIEWID = Res.Content.ReadAsStringAsync().Result;
                        AgentDetails = JsonConvert.DeserializeObject<List<AgentListingModel>>(DataByVIEWID);
                    }
                    return Json(AgentDetails, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception EX)
            {
                return Json(EX, JsonRequestBehavior.AllowGet);
            }
        }
        public async Task<JsonResult> GetAgentFeedback(string AgentID)
        {
            try
            {
                string Baseurl = ConfigurationManager.AppSettings["WebApiURL"];
                List<AgentListingModel> AgentDetails = new List<AgentListingModel>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string Method = "/AgentShowingsFeedbackGetPK?PK_AgentShowingFeedbackID=" + AgentID;
                    HttpResponseMessage Res = await client.GetAsync(Method);
                    if (Res.IsSuccessStatusCode)
                    {
                        var DataByVIEWID = Res.Content.ReadAsStringAsync().Result;
                        AgentDetails = JsonConvert.DeserializeObject<List<AgentListingModel>>(DataByVIEWID);
                    }
                    return Json(AgentDetails, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception EX)
            {
                return Json(EX, JsonRequestBehavior.AllowGet);
            }
        }
        public async Task<JsonResult> GetEditAlertsByMlsID(string MLSId)
        {
            try
            {
                string Baseurl = ConfigurationManager.AppSettings["WebApiURL"];
                List<AgentListingModel> AgentDetails = new List<AgentListingModel>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string Method = "/EditListingAlertGet?MLSID=" + MLSId;
                    HttpResponseMessage Res = await client.GetAsync(Method);
                    if (Res.IsSuccessStatusCode)
                    {
                        var DataByVIEWID = Res.Content.ReadAsStringAsync().Result;
                        AgentDetails = JsonConvert.DeserializeObject<List<AgentListingModel>>(DataByVIEWID);
                    }
                    return Json(AgentDetails, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception EX)
            {
                return Json(EX, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult DeleteContactEditAlert(string PKeyContactID)
        {
            string ResponceData = "";
            try
            {
                string urlAddress = ConfigurationManager.AppSettings["WebApiURL"]+"/RemoveListingAlertContactPost?";
                using (WebClient client = new WebClient())
                {
                    NameValueCollection postData = new NameValueCollection()
                    {
                      { "PK_ListingContactID",    PKeyContactID },
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
        public async Task<JsonResult> GetBuyerFeedBack(string BuyerID)
        {
            try
            {
                string Baseurl = ConfigurationManager.AppSettings["WebApiURL"];
                List<AgentListingModel> AgentDetails = new List<AgentListingModel>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string Method = "/UserShowingsFeedbackGetPK?PK_UserShowingFeedbackID=" + BuyerID;
                    HttpResponseMessage Res = await client.GetAsync(Method);
                    if (Res.IsSuccessStatusCode)
                    {
                        var DataByVIEWID = Res.Content.ReadAsStringAsync().Result;
                        AgentDetails = JsonConvert.DeserializeObject<List<AgentListingModel>>(DataByVIEWID);
                    }
                    return Json(AgentDetails, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception EX)
            {
                return Json(EX, JsonRequestBehavior.AllowGet);
            }
        }
        public async Task<JsonResult> GetListingContacts(string FkeyListignAlertID)
        {
            try
            {
                string Baseurl = ConfigurationManager.AppSettings["WebApiURL"];
                List<AgentListingModel> AgentDetails = new List<AgentListingModel>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string Method = "/EditListingAlertContactsGet?FK_ListingAlertID=" + FkeyListignAlertID;
                    HttpResponseMessage Res = await client.GetAsync(Method);
                    if (Res.IsSuccessStatusCode)
                    {
                        var DataByVIEWID = Res.Content.ReadAsStringAsync().Result;
                        AgentDetails = JsonConvert.DeserializeObject<List<AgentListingModel>>(DataByVIEWID);
                    }
                    return Json(AgentDetails, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception EX)
            {
                return Json(EX, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult SendMyListingEditAlert(string AgentID, string MLSID, string ReadBeforeSendingToSeller, string NofiticationToSellerIn_CheckIN, string NofiticationToSellerIn_CheckOUT, string ContactToNotify)
        {
            string ResponceData = "";
            try
            {
                string urlAddress = ConfigurationManager.AppSettings["WebApiURL"]+"/MyListingEditAlertsPost?";
                using (WebClient client = new WebClient())
                {
                    NameValueCollection postData = new NameValueCollection()
                    {
                      { "AgentID",    AgentID },
                      { "MLSID",    MLSID },
                      { "ReadBeforeSendingToSeller",   ReadBeforeSendingToSeller },
                       { "NofiticationToSellerIn_CheckIN",    NofiticationToSellerIn_CheckIN },
                      { "NofiticationToSellerIn_CheckOUT",    NofiticationToSellerIn_CheckOUT },
                      { "ContactToNotify",   ContactToNotify },
                    };
                    ResponceData = Encoding.UTF8.GetString(client.UploadValues(urlAddress, postData));
                    var response = JsonConvert.DeserializeObject(ResponceData);
                    if (response.ToString() == "Successfully Updated.")
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

        //for Check in Hit


        public JsonResult ForCheckInService(string AgentID, string MLSID, string FK_OpenHouseID)
        {
            string ResponceData = "";
            try
            {
                string urlAddress = ConfigurationManager.AppSettings["WebApiURL"]+"/AgentOpenHouseCheckInOutPost?";
                using (WebClient client = new WebClient())
                {
                    NameValueCollection postData = new NameValueCollection()
                    {
                      { "AgentID",    AgentID },
                      { "MLSID",    MLSID },
                      { "FK_OpenHouseID",   FK_OpenHouseID },
                    };
                    // ResponceData = Encoding.UTF8.GetString(client.UploadValues(urlAddress, postData));
                    //var response = JsonConvert.DeserializeObject(ResponceData);
                    //return Json(response.ToString(), JsonRequestBehavior.AllowGet);
                    ResponceData = Encoding.UTF8.GetString(client.UploadValues(urlAddress, postData));
                    var proplist = JsonConvert.DeserializeObject<List<AgentListingModel>>(ResponceData);
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


        public JsonResult ForCheckInAddTimeService(string PK_OpenHouse_CheckInPK, string Time)
        {
            string ResponceData = "";
            try
            {
                string urlAddress = ConfigurationManager.AppSettings["WebApiURL"]+"/AgentOpenHouseAddTimerPost?";
                using (WebClient client = new WebClient())
                {
                    NameValueCollection postData = new NameValueCollection()
                    {
                      { "PK_OpenHouse_CheckInPK",    PK_OpenHouse_CheckInPK },
                      { "Time",    Time },
                    };
                    // ResponceData = Encoding.UTF8.GetString(client.UploadValues(urlAddress, postData));
                    //var response = JsonConvert.DeserializeObject(ResponceData);
                    //return Json(response.ToString(), JsonRequestBehavior.AllowGet);
                    ResponceData = Encoding.UTF8.GetString(client.UploadValues(urlAddress, postData));
                    var proplist = JsonConvert.DeserializeObject<List<AgentListingModel>>(ResponceData);
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

        public JsonResult PostEmergencyCall(string AgentID, string PK_OpenHouseID)
        {
            string ResponceData = "";
            try
            {
                string urlAddress = ConfigurationManager.AppSettings["WebApiURL"]+ "/AgentOpenHouseEmergencyPost?";
                using (WebClient client = new WebClient())
                {
                    NameValueCollection postData = new NameValueCollection()
                    {
                      { "AgentID",    AgentID },
                      { "PK_OpenHouseID",    PK_OpenHouseID },
                    };
                    ResponceData = Encoding.UTF8.GetString(client.UploadValues(urlAddress, postData));
                     var proplist = JsonConvert.DeserializeObject<List<resultClassModel>>(ResponceData);

                    if (proplist[0].Status.ToString() == "Emergency" || proplist[0].STATUS.ToString() == "Emergency")
                    {
                        return Json("Success", JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json("Fail", JsonRequestBehavior.AllowGet);
                    }

                    //ResponceData = Encoding.UTF8.GetString(client.UploadValues(urlAddress, postData));
                    //var proplist = JsonConvert.DeserializeObject<List<AgentListingModel>>(ResponceData);
                    //var jsonResult = Json(proplist, JsonRequestBehavior.AllowGet);
                    //jsonResult.MaxJsonLength = int.MaxValue;
                    //return jsonResult;
                }
            }

            catch (Exception EX)
            {
                return Json(EX, JsonRequestBehavior.AllowGet);
            }
        }

    }
}