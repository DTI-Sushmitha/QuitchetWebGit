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
    public class AgentShowingsController : Controller
    {
        // GET: Agent/AgentShowings
        public ActionResult Index()
        {
            return View();
        }
        //Get Agent Buyers List based on sent Status and AgentID(Current Agent)
        public async Task<JsonResult> GetAgentBuyers_Requested(string AgentID,string Status)
        {
            string Method = "";
            try
            {
                string Baseurl = ConfigurationManager.AppSettings["WebApiURL"];
                List<AgentRequests_BuyersModel> PropDetails = new List<AgentRequests_BuyersModel>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    if(Status=="Requested")
                    {
                         Method = "/AgentBuyersRequested?AgentID=" + AgentID;
                    }
                        
                    else if (Status == "Pending")
                    {
                         Method = "/AgentBuyersPending?AgentID=" + AgentID;
                    }
                    else if (Status == "Scheduled")
                    {
                         Method = "/AgentBuyersScheduled?AgentID=" + AgentID;

                    }
                    else
                    {
                         Method = "/AgentBuyersCompleted?AgentID=" + AgentID;
                    }
                        

                    HttpResponseMessage Res = await client.GetAsync(Method);
                    if (Res.IsSuccessStatusCode)
                    {
                        var DataByVIEWID = Res.Content.ReadAsStringAsync().Result;
                        PropDetails = JsonConvert.DeserializeObject<List<AgentRequests_BuyersModel>>(DataByVIEWID);
                    }
                    return Json(PropDetails, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception EX)
            {
                return Json(EX, JsonRequestBehavior.AllowGet);
            }
        }
        //Get List  based on given USer ID and Agent Id
        public async Task<JsonResult> GetAgentListings_byStatus(string AgentID,string RequestedUserID,string Status)
        {
            //string UserID = "";
            //if (Request.Cookies["UseridCookie"] != null)
            //{
            //    UserID = Request.Cookies["UseridCookie"].Value;
            //}
            string Method = "";
            try
            {
                string Baseurl = ConfigurationManager.AppSettings["WebApiURL"];
                List<AgentShowingsModel> PropDetails = new List<AgentShowingsModel>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    if (Status == "Requested")
                    {
                        Method = "/AgentShowingsRequestedGet?AgentID=" + AgentID + "&FK_UserID=" + RequestedUserID;
                    }

                    else if (Status == "Pending")
                    {
                        Method = "/AgentShowingsPendingGet?AgentID=" + AgentID + "&FK_UserID=" + RequestedUserID;
                    }
                    else if (Status == "Scheduled")
                    {
                        Method = "/AgentShowingsScheduledGet?AgentID=" + AgentID + "&FK_UserID=" + RequestedUserID;
                    }
                    else if (Status == "Completed")
                    {
                        Method = "/AgentShowingsCompletedGet?AgentID=" + AgentID + "&FK_UserID=" + RequestedUserID;
                    }
                    HttpResponseMessage Res = await client.GetAsync(Method);
                    if (Res.IsSuccessStatusCode)
                    {
                        var DataByVIEWID = Res.Content.ReadAsStringAsync().Result;
                        PropDetails = JsonConvert.DeserializeObject<List<AgentShowingsModel>>(DataByVIEWID);
                    }
                    return Json(PropDetails, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception EX)
            {
                return Json(EX, JsonRequestBehavior.AllowGet);
            }
        }

        //Get Scheduled Timings based on given PKey IDs List
        public async Task<JsonResult> GeTimingsbyPKeyList(string PK_RequestShowingIDs)
        {
            try
            {
                string Baseurl = ConfigurationManager.AppSettings["WebApiURL"];
                List<AgentShowingsModel> PropDetails = new List<AgentShowingsModel>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string Method = "/AgentSelectedListingsDatesGet?PK_RequestShowingIDs=" + PK_RequestShowingIDs;
                    HttpResponseMessage Res = await client.GetAsync(Method);
                    if (Res.IsSuccessStatusCode)
                    {
                        var DataByVIEWID = Res.Content.ReadAsStringAsync().Result;
                        PropDetails = JsonConvert.DeserializeObject<List<AgentShowingsModel>>(DataByVIEWID);
                    }
                    return Json(PropDetails, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception EX)
            {
                return Json(EX, JsonRequestBehavior.AllowGet);
            }
        }
        //Get Listings  based on given PKey IDs List by
        public async Task<JsonResult> GeListignsbyPKeyList(string PK_RequestShowingIDs)
        {
            try
            {
                string Baseurl = ConfigurationManager.AppSettings["WebApiURL"];
                List<ListingDetailsForPopupModel> PropDetails = new List<ListingDetailsForPopupModel>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string Method = "/AgentSelectedRequestedMLSIDGet?PK_RequestShowingIDs=" + PK_RequestShowingIDs;
                    HttpResponseMessage Res = await client.GetAsync(Method);
                    if (Res.IsSuccessStatusCode)
                    {
                        var DataByVIEWID = Res.Content.ReadAsStringAsync().Result;
                        PropDetails = JsonConvert.DeserializeObject<List<ListingDetailsForPopupModel>>(DataByVIEWID);
                    }
                    return Json(PropDetails, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception EX)
            {
                return Json(EX, JsonRequestBehavior.AllowGet);
            }
        }
        //Get Listing to add additionally by searching In Scheduled popup
        public async Task<JsonResult> AddListingBySearch(string SearchEle,string Method)
        {
            var M = "";
            try
            {
                M = Method + SearchEle;
                if (SearchEle.Substring(0, 1) == "#")
                {
                    SearchEle = SearchEle.Remove(0, 1);

                   // Method = "AgentSearchMLSIDGet?MLSID=" + SearchEle;
                }
                //else
                //{
                //    Method = "AgentSearchAddressGet?Address=" + SearchEle;
                //}
                 M = Method + SearchEle;
                string Baseurl = ConfigurationManager.AppSettings["WebApiURL"]+"/";
                List<SearchedListingModel> PropDetails = new List<SearchedListingModel>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage Res = await client.GetAsync(M);
                    if (Res.IsSuccessStatusCode)
                    {
                        var DataByVIEWID = Res.Content.ReadAsStringAsync().Result;
                        PropDetails = JsonConvert.DeserializeObject<List<SearchedListingModel>>(DataByVIEWID);
                    }
                    return Json(PropDetails, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception EX)
            {
                return Json(EX, JsonRequestBehavior.AllowGet);
            }
        }

        //Function to SAve Scheduled Timings For a request Step-1
        public JsonResult ShceduleTimingsForRequest(List<String> obj)
        {
            string res = "";
            string pagesource = "";
            try
            {
                string urlAddress = ConfigurationManager.AppSettings["WebApiURL"]+"/SaveRequestToPendingPost?";
                using (WebClient client = new WebClient())
                {
                    NameValueCollection postData = new NameValueCollection()
                    {
                        {"PK_RequestShowingID",obj[0]},
                        {"RemoveMLSID",obj[1]},
                        {"AddMLSID",obj[2]},
                        {"ScheduleDate",obj[3]},
                        {"Schedule_From_Time",Convert.ToDateTime(obj[4]).ToString("HH:mm:ss").Replace('.',':')},
                        {"Schedule_To_Time",Convert.ToDateTime(obj[5]).ToString("HH:mm:ss").Replace('.',':')},
                        {"AgentDwellTime",obj[6]},
                        
                    };
                    pagesource = Encoding.UTF8.GetString(client.UploadValues(urlAddress, postData));
                    var response = JsonConvert.DeserializeObject(pagesource);
                    if (response.ToString() == "Successfully Updated.")
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
        //Get Scheduled Listings In Confirm Map & Schedule Popup(Stage-2)
        public async Task<JsonResult> GetInProcessListings_Popup2(string PkeyRequestId)
        {
            try
            {
                string Baseurl = ConfigurationManager.AppSettings["WebApiURL"];
                List<ScheduledListings_inPopup_Modal> PropDetails = new List<ScheduledListings_inPopup_Modal>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    var Method = "/ShowingsByPKGet?PK_RequestShowingID=" + PkeyRequestId;
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage Res = await client.GetAsync(Method);
                    if (Res.IsSuccessStatusCode)
                    {
                        var DataByVIEWID = Res.Content.ReadAsStringAsync().Result;
                        PropDetails = JsonConvert.DeserializeObject<List<ScheduledListings_inPopup_Modal>>(DataByVIEWID);
                    }
                    return Json(PropDetails, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception EX)
            {
                return Json(EX, JsonRequestBehavior.AllowGet);
            }
        }
        //Function to Save Start point and Exit in Confrim popup (Step-2)
        public JsonResult SaveStartPoint_ConfirmPopup(string PK_RequestShowingID,string StartingPoint)
        {
            string res = "";
            string pagesource = "";
            try
            {
                string urlAddress = ConfigurationManager.AppSettings["WebApiURL"]+"/SaveShowingsStartingPointPost?";
                using (WebClient client = new WebClient())
                {
                    NameValueCollection postData = new NameValueCollection()
                    {
                        {"PK_RequestShowingID",PK_RequestShowingID},
                        {"StartingPoint",StartingPoint},
                    };
                    pagesource = Encoding.UTF8.GetString(client.UploadValues(urlAddress, postData));
                    var response = JsonConvert.DeserializeObject(pagesource);
                    if (response.ToString() == "Successfully Updated.")
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
        //Function to Save Schedule Timings in Routing Popup (Save & Exit)Button
        public JsonResult SaveListingstoSchedule(List<string> obj)
        {
            string res = "";
            string pagesource = "", urlAddress="";
            try
            {
                if (obj[8] == "save_exit")
                {
                     urlAddress = ConfigurationManager.AppSettings["WebApiURL"]+"/SaveShowingsToScheduledPost?";
                }
                else  if (obj[8] == "save_notify")
                {
                     urlAddress = ConfigurationManager.AppSettings["WebApiURL"]+ "/AgentShowingsSaveAndNotifyPost?";
                }
                
                using (WebClient client = new WebClient())
                {
                    NameValueCollection postData = new NameValueCollection()
                    {
                        {"PK_RequestShowingID",obj[0]},
                        {"MLSID",obj[1]},
                         {"ArrivalTime",Convert.ToDateTime(obj[2]).ToString("HH:mm:ss").Replace('.',':')},
                        {"SetAppointment_FromTime",Convert.ToDateTime(obj[3]).ToString("HH:mm:ss").Replace('.',':')},
                         {"SetAppointment_ToTime",Convert.ToDateTime(obj[4]).ToString("HH:mm:ss").Replace('.',':')},
                        {"IsCallMade",obj[5]},
                         {"IsConform",obj[6]},
                        {"RemoveMLSID",obj[7]},
                    };
                    pagesource = Encoding.UTF8.GetString(client.UploadValues(urlAddress, postData));
                    var response = JsonConvert.DeserializeObject(pagesource);
                    if (response.ToString() == "Successfully Saved.")
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
        public async Task<JsonResult> GetDirectionssss(string origin,string dest,string waypoints)
        {
            try
            {
                String url = "http://maps.googleapis.com/maps/api/directions/json";
               
                List<ScheduledListings_inPopup_Modal> PropDetails = new List<ScheduledListings_inPopup_Modal>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    client.DefaultRequestHeaders.Clear();
                    //  var Method = "ShowingsByPKGet?PK_RequestShowingID=" + PkeyRequestId;
                    var Method = "?origin=" + origin + "&destination=" + dest + "&waypoints=optimize:true|"+waypoints+"&sensor=false";
                   // client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage Res = await client.GetAsync(Method);
                    if (Res.IsSuccessStatusCode)
                    {
                        var DataByVIEWID = Res.Content.ReadAsStringAsync().Result;
                        return Json(DataByVIEWID, JsonRequestBehavior.AllowGet);
                        // PropDetails = JsonConvert.DeserializeObject<List<ScheduledListings_inPopup_Modal>>(DataByVIEWID);
                    }
                    return Json("", JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception EX)
            {
                return Json(EX, JsonRequestBehavior.AllowGet);
            }
        }
        //Function to post feedback by Agent 
        public JsonResult AgentPostFeedback(List<String> obj)
        {
            string UserID = "", res = "";
            if (Request.Cookies["UseridCookie"] != null)
            {
                UserID = Request.Cookies["UseridCookie"].Value;
            }
            string pagesource = "";
            try
            {
                string urlAddress = ConfigurationManager.AppSettings["WebApiURL"]+ "/AgentShowingFeedbackPost?";
                using (WebClient client = new WebClient())
                {
                    NameValueCollection postData = new NameValueCollection()
                    {
                        {"AgentID",obj[0]},
                        {"PK_RequestShowingID",obj[1]},
                        {"MLSID",obj[2]},
                        {"Comments",obj[3]},
                        {"Reating",obj[4]},
                        {"Top3",obj[5]},
                        {"PriceCompare",obj[6]},
                        {"Exterior",obj[7]},
                        {"Interior",obj[8]},
                        {"Landscaping",obj[9]},
                        {"Consideringoffer",obj[10]},
                        {"FeatureToImprove",obj[11]},
                        {"WouldPreferToSave",obj[12]},
                    };
                    pagesource = Encoding.UTF8.GetString(client.UploadValues(urlAddress, postData));
                    var response = JsonConvert.DeserializeObject(pagesource);
                    if (response.ToString() == "Successfully Changed.")
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
        //Function to get Feedback
        public async Task<JsonResult> AgentShowings_GetFeedback(string PK_AgentShowingFeedbackID)
            
        {
            string UserID = "";
            if (Request.Cookies["UseridCookie"] != null)
            {
                UserID = Request.Cookies["UseridCookie"].Value;
            }
            try
            {
                string Baseurl = ConfigurationManager.AppSettings["WebApiURL"];
                List<ShowingsFeedbackModel> PropDetails = new List<ShowingsFeedbackModel>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string Method = "/AgentShowingsFeedbackGetPK?PK_AgentShowingFeedbackID=" + PK_AgentShowingFeedbackID;
                    HttpResponseMessage Res = await client.GetAsync(Method);
                    if (Res.IsSuccessStatusCode)
                    {
                        var DataByagent = Res.Content.ReadAsStringAsync().Result;
                        PropDetails = JsonConvert.DeserializeObject<List<ShowingsFeedbackModel>>(DataByagent);
                    }
                    return Json(PropDetails, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception EX)
            {
                return Json(EX, JsonRequestBehavior.AllowGet);
            }
        }
        public async Task<JsonResult> BuyerShowings_GetFeedback(string PK_UserShowingFeedbackID)

        {
            string UserID = "";
            if (Request.Cookies["UseridCookie"] != null)
            {
                UserID = Request.Cookies["UseridCookie"].Value;
            }
            try
            {
                string Baseurl = ConfigurationManager.AppSettings["WebApiURL"];
                List<ShowingsFeedbackModel> PropDetails = new List<ShowingsFeedbackModel>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string Method = "/UserShowingsFeedbackGetPK?PK_UserShowingFeedbackID=" + PK_UserShowingFeedbackID;
                    HttpResponseMessage Res = await client.GetAsync(Method);
                    if (Res.IsSuccessStatusCode)
                    {
                        var DataByagent = Res.Content.ReadAsStringAsync().Result;
                        PropDetails = JsonConvert.DeserializeObject<List<ShowingsFeedbackModel>>(DataByagent);
                    }
                    return Json(PropDetails, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception EX)
            {
                return Json(EX, JsonRequestBehavior.AllowGet);
            }
        }
        //Function to Save Buyer Comments Edited by Agent
        public JsonResult AgentEdit_SaveBuyerComments(string buyer_FBPkey,string edited_Comments)
        {
            string res = "";
            string pagesource = "", urlAddress = "";
            try
            {
                    urlAddress = ConfigurationManager.AppSettings["WebApiURL"]+"/EditShowingsFeedbackPost?";
                using (WebClient client = new WebClient())
                {
                    NameValueCollection postData = new NameValueCollection()
                    {
                        {"PK_ShowingFeedbackID",buyer_FBPkey},
                        {"Comments",edited_Comments},
                         {"AllowFeedbackToSeller","true"},
                    };
                    pagesource = Encoding.UTF8.GetString(client.UploadValues(urlAddress, postData));
                    var response = JsonConvert.DeserializeObject(pagesource);
                    if (response.ToString() == "Successfully Updated.")
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