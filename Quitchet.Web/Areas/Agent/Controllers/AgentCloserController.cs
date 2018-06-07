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
    public class AgentCloserController : Controller
    {
        // GET: Agent/AgentCloser
        public ActionResult Index()
        {
            return View();
        }
        //Get Buying Listings to Agent assigned by himself and other Agents
        public async Task<JsonResult> GetMyBuyingListings(string AgentId)
        {
            try
            {
                string Baseurl = ConfigurationManager.AppSettings["WebApiURL"];
                List<CloserListingsModel> PropDetails = new List<CloserListingsModel>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string Method = "/AgentBuyersClosingsGet?AgentID=" + AgentId;
                    HttpResponseMessage Res = await client.GetAsync(Method);
                    if (Res.IsSuccessStatusCode)
                    {
                        var DataByVIEWID = Res.Content.ReadAsStringAsync().Result;
                        PropDetails = JsonConvert.DeserializeObject<List<CloserListingsModel>>(DataByVIEWID);
                    }
                    return Json(PropDetails, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception EX)
            {
                return Json(EX, JsonRequestBehavior.AllowGet);
            }
        }
        //Function to Add Listing to My Buyer from Add NEw CLosing - Buying Popup
        public JsonResult AddClosingtoBuying(string AgetID, string BuyerId, string Mlsid)
        {
            string res = "";
            string pagesource = "";
            try
            {
                List<AgentRequests_BuyersModel> dataBasedonLotsFilter = new List<AgentRequests_BuyersModel>();
                string urlAddress = ConfigurationManager.AppSettings["WebApiURL"]+ "/MyBuyerMoveListingToClosingPost?";
                using (WebClient client = new WebClient())
                {
                    NameValueCollection postData = new NameValueCollection()
                    {
                        {"AgentID",AgetID},
                        {"FK_BuyerID",BuyerId},
                        {"MLSID",Mlsid},
                    };
                    pagesource = Encoding.UTF8.GetString(client.UploadValues(urlAddress, postData));
                    var proplist = JsonConvert.DeserializeObject<List<AgentRequests_BuyersModel>>(pagesource);
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
        }
        public JsonResult AddMyListingforSelling(string AgetID, string SellingAgentId, string Mlsid)
        {
            string res = "";
            string pagesource = "", urlAddress = "";
            try
            {
                List<AgentRequests_BuyersModel> dataBasedonLotsFilter = new List<AgentRequests_BuyersModel>();
                urlAddress = ConfigurationManager.AppSettings["WebApiURL"]+"/AddClosingToSellingPost?";
                using (WebClient client = new WebClient())
                {
                    NameValueCollection postData = new NameValueCollection()
                    {
                        {"AgentID",AgetID},
                        {"SellingAgentID",SellingAgentId},
                         {"MLSID",Mlsid},
                    };
                    pagesource = Encoding.UTF8.GetString(client.UploadValues(urlAddress, postData));
                    var response = JsonConvert.DeserializeObject(pagesource);
                    var proplist = JsonConvert.DeserializeObject<List<AgentRequests_BuyersModel>>(pagesource);
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
        //Function to get Agent(MY) Active Listings to add inSellng Popup
        public async Task<JsonResult> SearchMyActiveListings(string AgentID,string SearchEle)
        {
            try
            {
                string Baseurl = ConfigurationManager.AppSettings["WebApiURL"];
                List<AgentsListingSearchModel> PropDetails = new List<AgentsListingSearchModel>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var Method = "/AgentActiveListingsSearchGet?AgentID="+ AgentID + "&MLSIDSearch=" + SearchEle;
                    HttpResponseMessage Res = await client.GetAsync(Method);
                    if (Res.IsSuccessStatusCode)
                    {
                        var DataByVIEWID = Res.Content.ReadAsStringAsync().Result;
                        PropDetails = JsonConvert.DeserializeObject<List<AgentsListingSearchModel>>(DataByVIEWID);
                    }
                    return Json(PropDetails, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception EX)
            {
                return Json(EX, JsonRequestBehavior.AllowGet);
            }
        }
        //Function to get Agents to assign My(Agent)Active listing to Sell in Selling Popup
        public async Task<JsonResult> SearchAgentsForAddListing(string AgentID, string SearchEle)
        {
            try
            {
                string Baseurl = ConfigurationManager.AppSettings["WebApiURL"];
                List<QuitchetAgentsSearchModel> PropDetails = new List<QuitchetAgentsSearchModel>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                 client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var Method = "/AllNonQuitchAgentsGet?AgentID=" + AgentID + "&AgentSearchName=" + SearchEle;
                    HttpResponseMessage Res = await client.GetAsync(Method);
                    if (Res.IsSuccessStatusCode)
                    {
                        var DataByVIEWID = Res.Content.ReadAsStringAsync().Result;
                        PropDetails = JsonConvert.DeserializeObject<List<QuitchetAgentsSearchModel>>(DataByVIEWID);
                    }
                    return Json(PropDetails, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception EX)
            {
                return Json(EX, JsonRequestBehavior.AllowGet);
            }
        }
        //Get Selling Listings which are are assignend to an RETS Agents
        public async Task<JsonResult> GetMySellingListings(string AgentId)
        {
            try
            {
                string Baseurl = ConfigurationManager.AppSettings["WebApiURL"];
                List<CloserListingsModel> PropDetails = new List<CloserListingsModel>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string Method = "/AgentSellerClosingsGet?AgentID=" + AgentId;
                    HttpResponseMessage Res = await client.GetAsync(Method);
                    if (Res.IsSuccessStatusCode)
                    {
                        var DataByVIEWID = Res.Content.ReadAsStringAsync().Result;
                        PropDetails = JsonConvert.DeserializeObject<List<CloserListingsModel>>(DataByVIEWID);
                    }
                    return Json(PropDetails, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception EX)
            {
                return Json(EX, JsonRequestBehavior.AllowGet);
            }
        }
        //Function to Cancel Closing 
        public JsonResult CancelClosing(string AgentID, string PK_ClosingID, string CancelComment)
        {
            string res = "";
            string pagesource = "";
            try
            {
                string urlAddress = ConfigurationManager.AppSettings["WebApiURL"]+ "/CancelClosingPost?";
                using (WebClient client = new WebClient())
                {
                    NameValueCollection postData = new NameValueCollection()
                    {
                        {"AgentID",AgentID},
                        {"PK_ClosingID",PK_ClosingID},
                        {"CancelComment",CancelComment},
                    };
                    pagesource = Encoding.UTF8.GetString(client.UploadValues(urlAddress, postData));
                    var response = JsonConvert.DeserializeObject(pagesource);
                    if (response.ToString() == "Successfully Canceled.")
                    {
                        res = "Success";
                        return Json(res, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        res = "Fail";
                        return Json(res, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception ex)
            {
                res = ex.Message.ToString();
                return Json(res, JsonRequestBehavior.AllowGet);
            }
        }

        //Function to get Closing List Details to show When Seleceted Manage in Tab View
        //Get Selling Listings which are are assignend to an RETS Agents
        public async Task<JsonResult> GetClosingListDetails(string PK_ClosingID)
        {
            try
            {
                string Baseurl = ConfigurationManager.AppSettings["WebApiURL"];
                List<ClosingListDetailsModel> PropDetails = new List<ClosingListDetailsModel>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string Method = "/GetClosingByPK?PK_ClosingID=" + PK_ClosingID;
                    HttpResponseMessage Res = await client.GetAsync(Method);
                    if (Res.IsSuccessStatusCode)
                    {
                        var DataByVIEWID = Res.Content.ReadAsStringAsync().Result;
                        PropDetails = JsonConvert.DeserializeObject<List<ClosingListDetailsModel>>(DataByVIEWID);
                    }
                    return Json(PropDetails, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception EX)
            {
                return Json(EX, JsonRequestBehavior.AllowGet);
            }
        }
        //Funciton to Add Buyer to the Closing assigned to me by other buyer in Bying Tab Closing Manage
        public JsonResult AddbuyertoClosing(string FK_ClosingID, string BuyerId)
        {
            string res = "";
            string pagesource = "";
            try
            {
                string urlAddress = ConfigurationManager.AppSettings["WebApiURL"]+"/AddBuyerToClosingPost";
                using (WebClient client = new WebClient())
                {
                    NameValueCollection postData = new NameValueCollection()
                    {
                        {"FK_ClosingID",FK_ClosingID},
                        {"BuyerID",BuyerId},
                    };
                    pagesource = Encoding.UTF8.GetString(client.UploadValues(urlAddress, postData));
                    var response = JsonConvert.DeserializeObject(pagesource);
                    if (response.ToString() == "Successfully Added.")
                    {
                        res = "Success";
                        return Json(res, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        res = "Fail";
                        return Json(res, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception ex)
            {
                res = ex.Message.ToString();
                return Json(res, JsonRequestBehavior.AllowGet);
            }
        }
        //Common function to get Closing MArties based on selected tAb in Manage for both Buyind and Selling TAb
        public async Task<JsonResult> GetClosingPartiesinManage(string FK_ClosingID, string AssignedID)
        {
            try
            {
                string Baseurl = ConfigurationManager.AppSettings["WebApiURL"];
                List<ClosingParties> PropDetails = new List<ClosingParties>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var Method = "/CloserPartysGet?FK_ClosingID=" + FK_ClosingID + "&AssignedID=" + AssignedID;
                    HttpResponseMessage Res = await client.GetAsync(Method);
                    if (Res.IsSuccessStatusCode)
                    {
                        var DataByVIEWID = Res.Content.ReadAsStringAsync().Result;
                        PropDetails = JsonConvert.DeserializeObject<List<ClosingParties>>(DataByVIEWID);
                    }
                    return Json(PropDetails, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception EX)
            {
                return Json(EX, JsonRequestBehavior.AllowGet);
            }
        }
        //Get Closig Party, Closer Listing and Buyer/Seller and Message while Schedule
        public async Task<JsonResult> GetClosingListCloserPartyDetailsMessage(string FK_CloserPartyID,string UserID,string Whos)
        {
            try
            {
                string Baseurl = ConfigurationManager.AppSettings["WebApiURL"];
                List<ClosingCloserMessageModel> PropDetails = new List<ClosingCloserMessageModel>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string Method = "/CloserPartyScheduleMessageGet?FK_CloserPartyID=" + FK_CloserPartyID+ "&UserID="+ UserID + "&Whos="+ Whos;
                    HttpResponseMessage Res = await client.GetAsync(Method);
                    if (Res.IsSuccessStatusCode)
                    {
                        var DataByVIEWID = Res.Content.ReadAsStringAsync().Result;
                        PropDetails = JsonConvert.DeserializeObject<List<ClosingCloserMessageModel>>(DataByVIEWID);
                    }
                    return Json(PropDetails, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception EX)
            {
                return Json(EX, JsonRequestBehavior.AllowGet);
            }
        }
        //Funciton to Save Schedule Timings and Send Message to the Closer Party
        public JsonResult SaveScheduleforClosingParty(List<string> obj)
        {
            string res = "";
            string pagesource = "";
            try
            {
                string urlAddress = ConfigurationManager.AppSettings["WebApiURL"]+"/SaveCloserPartyScheduleMessagePost?";
                using (WebClient client = new WebClient())
                {
                    NameValueCollection postData = new NameValueCollection()
                    {
                        {"FK_CloserPartyD",obj[0]},
                        {"ScheduleDate",obj[1]},
                        {"ScheduleTime",Convert.ToDateTime(obj[2]).ToString("HH:mm:ss")},
                        {"Message",obj[3]},
                          {"Attachment",obj[4]},
                    };
                    pagesource = Encoding.UTF8.GetString(client.UploadValues(urlAddress, postData));
                    var response = JsonConvert.DeserializeObject(pagesource);
                    if (response.ToString() == "Successfully Added.")
                    {
                        res = "Success";
                        return Json(res, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        res = "Fail";
                        return Json(res, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception ex)
            {
                res = ex.Message.ToString();
                return Json(res, JsonRequestBehavior.AllowGet);
            }
        }
        //Funciton to Update the Closing Party Satus which assigned to Agent in First Section
        public JsonResult UpdateCloserListingStatus(string PK_CloserPartyID, string Status)
        {
            string res = "";
            string pagesource = "";
            try
            {
                string UserID = "";
                if (Request.Cookies["UseridCookie"] != null)
                {
                    UserID = Request.Cookies["UseridCookie"].Value;
                }
                string urlAddress = ConfigurationManager.AppSettings["WebApiURL"]+ "/CloserStatusChangePost?";
                using (WebClient client = new WebClient())
                {
                    NameValueCollection postData = new NameValueCollection()
                    {
                        {"PK_CloserPartyID",PK_CloserPartyID},
                        {"Status",Status},
                         {"FK_UserID",UserID }
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
                        return Json(res, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception ex)
            {
                res = ex.Message.ToString();
                return Json(res, JsonRequestBehavior.AllowGet);
            }
        }
        //Funciton to Update the Closing Party Satus which assigned to Agent in First Section
    }
}