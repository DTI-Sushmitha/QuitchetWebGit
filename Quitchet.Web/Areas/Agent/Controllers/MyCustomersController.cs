using Newtonsoft.Json;
using Quitchet.Core.Models.Agent;
using Quitchet.Core.Models.Customer;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Quitchet.Web.Areas.Agent.Controllers
{
    public class MyCustomersController : Controller
    {
        // GET: Agent/MyCustomers
        public ActionResult Index()
        {
            return View();
        }
        //Get Agent's Pending Requests Count
        public async Task<JsonResult> GetAgentRequestsCount(string AgentID)
        {
            try
            {
                string Baseurl = ConfigurationManager.AppSettings["WebApiURL"];
                List<AgentRequests_BuyersModel> PropDetails = new List<AgentRequests_BuyersModel>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string Method = "/MyPendingRequestsCountGet?AgentID=" + AgentID;
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
        //Get Agent Buyers List
        public async Task<JsonResult> GetBuyers(string AgentID)
        {
            try
            {
                string Baseurl = ConfigurationManager.AppSettings["WebApiURL"];
                List<AgentRequests_BuyersModel> PropDetails = new List<AgentRequests_BuyersModel>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string Method = "/MyBuyersGet?AgentID=" + AgentID;
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
        //Get Agent Pending Requests
        public async Task<JsonResult> GetPendingRequests(string AgentID)
        {
            try
            {
                string Baseurl = ConfigurationManager.AppSettings["WebApiURL"];
                List<AgentRequests_BuyersModel> PropDetails = new List<AgentRequests_BuyersModel>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string Method = "/MyPendingRequestsGet?AgentID=" + AgentID ;
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
        //Accept or Deny Pending request from User
        public async Task<JsonResult> Accept_DenyUserRequest(string PK_UserAgentsID,string Status,string AgentId)
        {
            string res = "";
            try
            {
                string Baseurl = ConfigurationManager.AppSettings["WebApiURL"];
                List<AgentRequests_BuyersModel> dataBasedonHomeFilter = new List<AgentRequests_BuyersModel>();
                using (var client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromMilliseconds(150000);
                    var cts = new CancellationTokenSource();
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string Method = "/AgentPendingRequestsAllowDenyGet?PK_UserAgentsID=" + PK_UserAgentsID + "&Status=" + Status + "&AgentId=" + AgentId;
                       HttpResponseMessage Res = await client.GetAsync(Method);
                    if (Res.IsSuccessStatusCode)
                    {
                        var DataByMLSID = Res.Content.ReadAsStringAsync().Result;
                        dataBasedonHomeFilter = JsonConvert.DeserializeObject<List<AgentRequests_BuyersModel>>(DataByMLSID);
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
        //Fucntion to InActive buyer from My Buyers Tab
        public JsonResult InActive_Buyer(string FK_BuyerID, string AgentID)
        {
            string res = "";
            string pagesource = "";
            try
            {
                List<AgentRequests_BuyersModel> dataBasedonLotsFilter = new List<AgentRequests_BuyersModel>();
                string urlAddress = ConfigurationManager.AppSettings["WebApiURL"]+ "/MyBuyerRemovePost?";
                using (WebClient client = new WebClient())
                {
                    NameValueCollection postData = new NameValueCollection()
                    {
                        {"FK_BuyerID",FK_BuyerID},
                        {"AgentID",AgentID},
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
            //return Json("", JsonRequestBehavior.AllowGet);
        }
        //Function To SAve Note for the Buyer
        public JsonResult Save_MyNote(string FK_BuyerID, string AgentID,string Note)
        {
            string res = "";
            string pagesource = "";
            try
            {
                string urlAddress = ConfigurationManager.AppSettings["WebApiURL"]+"/MyBuyerNotePost?";
                using (WebClient client = new WebClient())
                {
                    NameValueCollection postData = new NameValueCollection()
                    {
                        {"FK_BuyerID",FK_BuyerID},
                        {"AgentID",AgentID},
                        {"Note",Note},
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
        //Function to get Selected Buyer Favorites
        public async Task<JsonResult> GetMyBuyerFavorites(string FK_BuyerID)
        {
            try
            {
                string Baseurl = ConfigurationManager.AppSettings["WebApiURL"];
                List<BuyerFavoritesModel> PropDetails = new List<BuyerFavoritesModel>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string Method = "/MyBuyerFavoritesListingsGet?FK_BuyerID=" + FK_BuyerID;
                    HttpResponseMessage Res = await client.GetAsync(Method);
                    if (Res.IsSuccessStatusCode)
                    {
                        var DataByVIEWID = Res.Content.ReadAsStringAsync().Result;
                        PropDetails = JsonConvert.DeserializeObject<List<BuyerFavoritesModel>>(DataByVIEWID);
                    }
                    var jsonResult = Json(PropDetails, JsonRequestBehavior.AllowGet);
                    jsonResult.MaxJsonLength = int.MaxValue;
                    return jsonResult;
                  //  return Json(PropDetails, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception EX)
            {
                return Json(EX, JsonRequestBehavior.AllowGet);
            }
        }
        //Function to Post Requests by Agent Himslef From Buyer Favorites Tab
        public JsonResult PostRequestsBy_Agent(List<String> obj)
        {
            string res = "";
            string pagesource = "";
            try
            {
                obj[4] = Convert.ToDateTime(obj[4]).ToString("HH:mm:ss");
                obj[5] = Convert.ToDateTime(obj[5]).ToString("HH:mm:ss");
                obj[6] = Convert.ToDateTime(obj[6]).ToString("HH:mm:ss");
                obj[7] = Convert.ToDateTime(obj[7]).ToString("HH:mm:ss");
                obj[9] = Convert.ToDateTime(obj[9]).ToString("HH:mm:ss");
                obj[10] = Convert.ToDateTime(obj[10]).ToString("HH:mm:ss");
                obj[11] = Convert.ToDateTime(obj[11]).ToString("HH:mm:ss");
                obj[12] = Convert.ToDateTime(obj[12]).ToString("HH:mm:ss");
                string urlAddress = ConfigurationManager.AppSettings["WebApiURL"]+"/MyBuyerRequestShowingPost?";
                using (WebClient client = new WebClient())
                {
                    NameValueCollection postData = new NameValueCollection()
                    {
                         {"FK_BuyerID",obj[0]},
                        {"MLSID",obj[1]},
                        {"AGENTID",obj[2]},
                        {"Choice1Date",obj[3]},
                        {"Choice1FromTime1",obj[4].Replace('.',':')},
                        {"Choice1ToTime1",obj[5].Replace('.',':')},
                        {"Choice1FromTime2",obj[6].Replace('.',':')},
                        {"Choice1ToTime2",obj[7].Replace('.',':')},
                        {"Choice2Date",obj[8]},
                        {"Choice2FromTime1",obj[9].Replace('.',':')},
                        {"Choice2ToTime1",obj[10].Replace('.',':')},
                        {"Choice2FromTime2",obj[11].Replace('.',':')},
                        {"Choice2ToTime2",obj[12].Replace('.',':')},

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
        //Function To Move Listing to The Closing Party from Favorites
        public JsonResult MoveListing_to_Closing(string AgentID, string FK_BuyerID, string MLSID)
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
                        {"AgentID",AgentID},
                        {"FK_BuyerID",FK_BuyerID},
                        {"MLSID",MLSID},
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
        //Function to get Buyer Basic Needs from ny buyer tab
        public async Task<JsonResult> GetBuyerBasicNeeds(string FK_UserID,int type)
        {
            try
            {
                string Baseurl = ConfigurationManager.AppSettings["WebApiURL"];
                List<MyBasicNeedsAndWishListModel> PropDetails = new List<MyBasicNeedsAndWishListModel>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string Method = "/MyBuyerBasicNeedsGet?FK_BuyerID=" + FK_UserID;
                    HttpResponseMessage Res = await client.GetAsync(Method);
                    if (Res.IsSuccessStatusCode)
                    {
                        var DataByVIEWID = Res.Content.ReadAsStringAsync().Result;
                        PropDetails = JsonConvert.DeserializeObject<List<MyBasicNeedsAndWishListModel>>(DataByVIEWID);
                    }
                    return Json(PropDetails, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception EX)
            {
                return Json(EX, JsonRequestBehavior.AllowGet);
            }
        }
        //Funtion to get buyer WishList from My buyer Tab
        public async Task<JsonResult> GetBuyerWishList(string FK_UserID, int type)
        {
            try
            {
                string Baseurl = ConfigurationManager.AppSettings["WebApiURL"];
                List<MyWishListModel> PropDetails = new List<MyWishListModel>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string Method = "/MyBuyerWishListGet?FK_BuyerID=" + FK_UserID;
                    HttpResponseMessage Res = await client.GetAsync(Method);
                    if (Res.IsSuccessStatusCode)
                    {
                        var DataByVIEWID = Res.Content.ReadAsStringAsync().Result;
                        PropDetails = JsonConvert.DeserializeObject<List<MyWishListModel>>(DataByVIEWID);
                    }
                    return Json(PropDetails, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception EX)
            {
                return Json(EX, JsonRequestBehavior.AllowGet);
            }
        }
        //Function to get My Sellers into Tab
        public async Task<JsonResult> GetMySellers(string AgentID)
        {
            try
            {
                string Baseurl = ConfigurationManager.AppSettings["WebApiURL"];
                List<MeSeller_Listings_Model> PropDetails = new List<MeSeller_Listings_Model>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string Method = "/MySellersGet?AgentID=" + AgentID;
                    HttpResponseMessage Res = await client.GetAsync(Method);
                    if (Res.IsSuccessStatusCode)
                    {
                        var DataByVIEWID = Res.Content.ReadAsStringAsync().Result;
                        PropDetails = JsonConvert.DeserializeObject<List<MeSeller_Listings_Model>>(DataByVIEWID);
                    }
                    var jsonResult = Json(PropDetails, JsonRequestBehavior.AllowGet);
                    jsonResult.MaxJsonLength = int.MaxValue;
                    return jsonResult;
                }
            }
            catch (Exception EX)
            {
                return Json(EX, JsonRequestBehavior.AllowGet);
            }
        }
        //Function to get Listings of MySeller
        public async Task<JsonResult> GetMySellersListings(string FK_UserID,string AgentID)
        {
           
            try
            {
                string Baseurl = ConfigurationManager.AppSettings["WebApiURL"];
                List<ListingsModel> PropDetails = new List<ListingsModel>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string Method = "/SellerListingsGet?FK_UserID=" + FK_UserID+ "&AgentID="+ AgentID;
                    HttpResponseMessage Res = await client.GetAsync(Method);
                    if (Res.IsSuccessStatusCode)
                    {
                        var DataByVIEWID = Res.Content.ReadAsStringAsync().Result;
                        PropDetails = JsonConvert.DeserializeObject<List<ListingsModel>>(DataByVIEWID);
                    }
                    var jsonResult = Json(PropDetails, JsonRequestBehavior.AllowGet);
                    jsonResult.MaxJsonLength = int.MaxValue;
                    return jsonResult;
                }
            }
            catch (Exception EX)
            {
                return Json(EX, JsonRequestBehavior.AllowGet);
            }
        }
    }
}