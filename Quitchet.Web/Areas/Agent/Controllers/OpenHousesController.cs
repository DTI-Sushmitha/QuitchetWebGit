using Newtonsoft.Json;
using Quitchet.Core.Models.Agent;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Quitchet.Web.Areas.Agent.Controllers
{
    public class OpenHousesController : Controller
    {
        // GET: Agent/OpenHouses
        public ActionResult Index()
        {
            return View();
        }
        public async Task<JsonResult> GetAgentScheduledListingdetails(List<string> ActiveData)
        {
            string UserID = "";
            var AgentID = "";
            AgentID = ActiveData[0];
            if (Request.Cookies["UseridCookie"] != null)
            {
                UserID = Request.Cookies["UseridCookie"].Value;
            }
            try
            {
                string Baseurl = ConfigurationManager.AppSettings["WebApiURL"];
                List<OpenHousesModel> PropDetails = new List<OpenHousesModel>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string Method = "/AgentScheduledOpenHouseGet?AgentID=" + AgentID;
                    HttpResponseMessage Res = await client.GetAsync(Method);
                    if (Res.IsSuccessStatusCode)
                    {
                        var DataByagent = Res.Content.ReadAsStringAsync().Result;
                        PropDetails = JsonConvert.DeserializeObject<List<OpenHousesModel>>(DataByagent);

                    }
                    return Json(PropDetails, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception EX)
            {
                return Json(EX, JsonRequestBehavior.AllowGet);
            }
        }

        public async Task<JsonResult> GetAgentCompletedListingdetails(List<string> ActiveData)
        {
            string UserID = "";
            var AgentID = "";
            AgentID = ActiveData[0];
            if (Request.Cookies["UseridCookie"] != null)
            {
                UserID = Request.Cookies["UseridCookie"].Value;
            }
            try
            {
                string Baseurl = ConfigurationManager.AppSettings["WebApiURL"];
                List<OpenHousesModel> PropDetails = new List<OpenHousesModel>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string Method = "/AgentCompletedOpenHouseGet?AgentID=" + AgentID;
                    HttpResponseMessage Res = await client.GetAsync(Method);
                    if (Res.IsSuccessStatusCode)
                    {
                        var DataByagent = Res.Content.ReadAsStringAsync().Result;
                        PropDetails = JsonConvert.DeserializeObject<List<OpenHousesModel>>(DataByagent);

                    }
                    return Json(PropDetails, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception EX)
            {
                return Json(EX, JsonRequestBehavior.AllowGet);
            }
        }

        public async Task<JsonResult> GetOpenHousePropertyDetails(string PKID, string AgentID)
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

        public async Task<JsonResult> GetAllAgents()
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
                    string Method = "/GetAllAgents";
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

        public async Task<JsonResult> GetActiveListings(string Listing)
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
                    string Method = "/AgentSearchAddressGet?Address=" + Listing;
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

        public async Task<JsonResult> GetSeller(string ID)
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
                    string Method = "/SellerWithMLSIDGet?MLSID=" + ID;
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
        //PostFeedbackRequestToInvitee
        public JsonResult PostFeedbackRequestToInvitee(string AgentID, string HostID, string FK_OpenHouseID, string OpenHouseMLSID, string Invitees)
        {
            string ResponceData = "";
            try
            {
                string urlAddress = ConfigurationManager.AppSettings["WebApiURL"]+ "/OpenHouseFeedbackRequestPost?";
                using (WebClient client = new WebClient())
                {
                    NameValueCollection postData = new NameValueCollection()
                    {

                      { "AgentID",    AgentID },
                       { "HostID", HostID},
                      { "FK_OpenHouseID",   FK_OpenHouseID },
                      { "OpenHouseMLSID",OpenHouseMLSID},
                       { "Invitees", Invitees},
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


        public async Task<JsonResult> GetFeedbacksByOpenHouseID(string PKID)
        {
            try
            {
                string Baseurl = ConfigurationManager.AppSettings["WebApiURL"];
                List<OpenHousesModel> AgentDetails = new List<OpenHousesModel>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string Method = "/OpenHouseFeedbacksGet?FK_OpenHouseID=" + PKID;
                    HttpResponseMessage Res = await client.GetAsync(Method);
                    if (Res.IsSuccessStatusCode)
                    {
                        var DataByVIEWID = Res.Content.ReadAsStringAsync().Result;
                        AgentDetails = JsonConvert.DeserializeObject<List<OpenHousesModel>>(DataByVIEWID);
                    }
                    return Json(AgentDetails, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception EX)
            {
                return Json(EX, JsonRequestBehavior.AllowGet);
            }
        }

        public async Task<JsonResult> GetFeedbackDetailsByPkeyFeedbackID(string PKID)
        {
            try
            {
                string Baseurl = ConfigurationManager.AppSettings["WebApiURL"];
                List<OpenHousesModel> AgentDetails = new List<OpenHousesModel>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string Method = "/OpenHouseFeedbacksByPKGet?PK_OpenHouseFeedbackID=" + PKID;
                    HttpResponseMessage Res = await client.GetAsync(Method);
                    if (Res.IsSuccessStatusCode)
                    {
                        var DataByVIEWID = Res.Content.ReadAsStringAsync().Result;
                        AgentDetails = JsonConvert.DeserializeObject<List<OpenHousesModel>>(DataByVIEWID);
                    }
                    return Json(AgentDetails, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception EX)
            {
                return Json(EX, JsonRequestBehavior.AllowGet);
            }
        }

        public async Task<JsonResult> GetAgentSecurityDetails(string AgentID)
        {
            try
            {
                string Baseurl = ConfigurationManager.AppSettings["WebApiURL"];
                List<OpenHousesModel> AgentDetails = new List<OpenHousesModel>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string Method = "/AgentOpenHouseSettingsGet?AgentID=" + AgentID;
                    HttpResponseMessage Res = await client.GetAsync(Method);
                    if (Res.IsSuccessStatusCode)
                    {
                        var DataByVIEWID = Res.Content.ReadAsStringAsync().Result;
                        AgentDetails = JsonConvert.DeserializeObject<List<OpenHousesModel>>(DataByVIEWID);
                    }
                    return Json(AgentDetails, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception EX)
            {
                return Json(EX, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult SendEditFeedback(string PK_OpenHouseFeedbackID, string Comments, string AllowFeedbackToSeller)
        {
            string ResponceData = "";
            try
            {
                string urlAddress = ConfigurationManager.AppSettings["WebApiURL"]+"/EditOpenHouseFeedbackPost?";
                using (WebClient client = new WebClient())
                {
                    NameValueCollection postData = new NameValueCollection()
                    {
                      { "PK_OpenHouseFeedbackID",    PK_OpenHouseFeedbackID },
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
    }
}