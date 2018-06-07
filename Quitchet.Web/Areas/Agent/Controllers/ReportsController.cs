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
    public class ReportsController : Controller
    {
        // GET: Agent/Reports
        public ActionResult Index()
        {
            return View();
        }
        //Method to get Agents in Select Agents From Buyer Metrics Filters
        public async Task<JsonResult> GetAgentsBelongstoMe(string MyAgetnID)
        {
            string UserID = "";
            if (Request.Cookies["UseridCookie"] != null)
            {
                UserID = Request.Cookies["UseridCookie"].Value;
            }
            try
            {
                string Baseurl = ConfigurationManager.AppSettings["WebApiURL"];
                List<MyAgentsModel> PropDetails = new List<MyAgentsModel>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string Method = "/AgentsByRoleWisetGet?AgentID=" + MyAgetnID;
                    HttpResponseMessage Res = await client.GetAsync(Method);
                    if (Res.IsSuccessStatusCode)
                    {
                        var DataByagent = Res.Content.ReadAsStringAsync().Result;
                        PropDetails = JsonConvert.DeserializeObject<List<MyAgentsModel>>(DataByagent);
                    }
                    return Json(PropDetails, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception EX)
            {
                return Json(EX, JsonRequestBehavior.AllowGet);
            }
        }
        //Method to get Buyers of My Agents form Select Buyers Popup
        public async Task<JsonResult> GetBuyersofMyAgents(string MyAgents,string Status)
        {
            string UserID = "";
            if (Status == "")
                Status = "Both";
            if (Request.Cookies["UseridCookie"] != null)
            {
                UserID = Request.Cookies["UseridCookie"].Value;
            }
            try
            {
                string Baseurl = ConfigurationManager.AppSettings["WebApiURL"];
                List<BuyersofMyAgentsModel> PropDetails = new List<BuyersofMyAgentsModel>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string Method = "/BuyersListByAgentsWiseGet?AgentID=" + MyAgents+ "&Status=" + Status;
                    HttpResponseMessage Res = await client.GetAsync(Method);
                    if (Res.IsSuccessStatusCode)
                    {
                        var DataByagent = Res.Content.ReadAsStringAsync().Result;
                        PropDetails = JsonConvert.DeserializeObject<List<BuyersofMyAgentsModel>>(DataByagent);
                    }
                    return Json(PropDetails, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception EX)
            {
                return Json(EX, JsonRequestBehavior.AllowGet);
            }
        }
        //Method to get Main Report for Buyer Metrics Without Filters
        public async Task<JsonResult> GetMainReport_BuyerMetrics(string MyAgentID)
        {
            string UserID = "";
            if (Request.Cookies["UseridCookie"] != null)
            {
                UserID = Request.Cookies["UseridCookie"].Value;
            }
            try
            {
                
                string Baseurl = ConfigurationManager.AppSettings["WebApiURL"];
                List<BuyersMetricsReportModel> PropDetails = new List<BuyersMetricsReportModel>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                     String Method = "/BuyersMetricsResultOnloadGet?AgentID=" + MyAgentID;
                    HttpResponseMessage Res = await client.GetAsync(Method);
                    if (Res.IsSuccessStatusCode)
                    {
                        var DataByagent = Res.Content.ReadAsStringAsync().Result;
                        PropDetails = JsonConvert.DeserializeObject<List<BuyersMetricsReportModel>>(DataByagent);
                    }
                    return Json(PropDetails, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception EX)
            {
                return Json(EX, JsonRequestBehavior.AllowGet);
            }
        }
        //Method to get Filterd Report for Buyer Metrics After applying filters
       
        
        public JsonResult GetFilteredReport_BuyerMetrics(List<String> bmFiltersObj)
        {
            string UserID = "";
            if (Request.Cookies["UseridCookie"] != null)
            {
                UserID = Request.Cookies["UseridCookie"].Value;
            }
            string pagesource = "";
            try
            {
                string urlAddress = ConfigurationManager.AppSettings["WebApiURL"]+"/BuyersMetricsResultGet?";
                List<BuyersMetricsReportModel> PropDetails = new List<BuyersMetricsReportModel>();
                using (WebClient client = new WebClient())
                {
                    NameValueCollection postData = new NameValueCollection()
                    {
                        {"AgentID",bmFiltersObj[0]},
                        {"Buyers",bmFiltersObj[1]},
                        {"FromDate",bmFiltersObj[2]},
                        {"ToDate",bmFiltersObj[3]},
                        {"FromPrice",bmFiltersObj[4]},
                        {"ToPrice",bmFiltersObj[5]},
                    };
                    pagesource = Encoding.UTF8.GetString(client.UploadValues(urlAddress, postData));
                    var proplist = JsonConvert.DeserializeObject<List<BuyersMetricsReportModel>>(pagesource);
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
        //Fucntion to Get Listings to show in Map View and Listing View from Buyer MEtrics Tab by default and applied filters
        public JsonResult GetListings_BuyerMetrics(List<String> bmFiltersObj)
        {
            string UserID = "";
            if (Request.Cookies["UseridCookie"] != null)
            {
                UserID = Request.Cookies["UseridCookie"].Value;
            }
            string pagesource = "";
            try
            {
                string urlAddress = ConfigurationManager.AppSettings["WebApiURL"]+"/BuyersMetricsResultListingsGet";
                List<BM_Listings_Model> PropDetails = new List<BM_Listings_Model>();
                using (WebClient client = new WebClient())
                {
                    NameValueCollection postData = new NameValueCollection()
                    {
                        {"AgentID",bmFiltersObj[0]},
                        {"Buyers",bmFiltersObj[1]},
                        {"FromDate",bmFiltersObj[2]},
                        {"ToDate",bmFiltersObj[3]},
                        {"FromPrice",bmFiltersObj[4]},
                        {"ToPrice",bmFiltersObj[5]},
                        {"Status",bmFiltersObj[6]},
                    };
                    pagesource = Encoding.UTF8.GetString(client.UploadValues(urlAddress, postData));
                    var proplist = JsonConvert.DeserializeObject<List<BM_Listings_Model>>(pagesource);
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
        
        //Method to get Main Report for Listing Metrics Without Filters
        public JsonResult GetMainReport_ListingMetrics(List<String> lmFiltersObj)
        {
            string UserID = "",pagesource="";
            if (Request.Cookies["UseridCookie"] != null)
            {
                UserID = Request.Cookies["UseridCookie"].Value;
            }
            try
            {
                string urlAddress = ConfigurationManager.AppSettings["WebApiURL"]+"/ListingMetricsResultPost";
                List<ListingMetricsReportModel> PropDetails = new List<ListingMetricsReportModel>();
                using (WebClient client = new WebClient())
                {
                    NameValueCollection postData = new NameValueCollection()
                    {
                        {"AgentID",lmFiltersObj[0]},
                        {"FromDate",lmFiltersObj[1]},
                        {"ToDate",lmFiltersObj[2]},
                        {"FromPrice",lmFiltersObj[3]},
                        {"ToPrice",lmFiltersObj[4]},
                        //{"ToPrice",lmFiltersObj[5]},
                        {"Status",lmFiltersObj[5]},
                    };
                    pagesource = Encoding.UTF8.GetString(client.UploadValues(urlAddress, postData));
                    var proplist = JsonConvert.DeserializeObject<List<ListingMetricsReportModel>>(pagesource);
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
        //Fucntion to Get Listings to show in Map View and Listing View from Buyer MEtrics Tab by default and applied filters
        public JsonResult GetListings_ListingMetrics(List<String> lmFiltersObj)
        {
            string UserID = "";
            if (Request.Cookies["UseridCookie"] != null)
            {
                UserID = Request.Cookies["UseridCookie"].Value;
            }
            string pagesource = "";
            try
            {
                string urlAddress = ConfigurationManager.AppSettings["WebApiURL"]+"/ListingMetricsResultListingsPost";
                List<ListingDetails_ListingMetrics> PropDetails = new List<ListingDetails_ListingMetrics>();
                using (WebClient client = new WebClient())
                {
                    NameValueCollection postData = new NameValueCollection()
                    {
                       {"AgentID",lmFiltersObj[0]},
                        {"FromDate",lmFiltersObj[1]},
                        {"ToDate",lmFiltersObj[2]},
                        {"FromPrice",lmFiltersObj[3]},
                        {"ToPrice",lmFiltersObj[4]},
                        {"Status",lmFiltersObj[5]},
                    };
                    pagesource = Encoding.UTF8.GetString(client.UploadValues(urlAddress, postData));
                    var proplist = JsonConvert.DeserializeObject<List<ListingDetails_ListingMetrics>>(pagesource);
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
        //Function to get Buyer Detail Reports 
        public JsonResult GetBuyerDetailsReport(List<String> buyerDetailsObj)
        {
            string UserID = "";
            if (Request.Cookies["UseridCookie"] != null)
            {
                UserID = Request.Cookies["UseridCookie"].Value;
            }
            string pagesource = "";
            try
            {
                string urlAddress = ConfigurationManager.AppSettings["WebApiURL"]+"/BuyerDetailsReportPost";
                List<BD_BuyerDetailsReportModel> PropDetails = new List<BD_BuyerDetailsReportModel>();
                using (WebClient client = new WebClient())
                {
                    NameValueCollection postData = new NameValueCollection()
                    {
                       {"AgentID",buyerDetailsObj[0]},
                        {"BuyerID",buyerDetailsObj[1]},
                        {"FromDate",buyerDetailsObj[2]},
                        {"ToDate",buyerDetailsObj[3]},
                    };
                    pagesource = Encoding.UTF8.GetString(client.UploadValues(urlAddress, postData));
                    var proplist = JsonConvert.DeserializeObject<List<BD_BuyerDetailsReportModel>>(pagesource);
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
        //Method to get Listings of Selected Agents in Listing Details Reports
        public async Task<JsonResult> LD_GetAgentListings(string MyAgentID, string ListingStatus)
        {
            string UserID = "";
            if (Request.Cookies["UseridCookie"] != null)
            {
                UserID = Request.Cookies["UseridCookie"].Value;
            }
            try
            {
                string Baseurl = ConfigurationManager.AppSettings["WebApiURL"];
                List<LD_ListingDetailsReportModel> PropDetails = new List<LD_ListingDetailsReportModel>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string Method = "/ListingsByAgentsWiseGet?AgentID=" + MyAgentID + "&Status="+ ListingStatus;
                    HttpResponseMessage Res = await client.GetAsync(Method);
                    if (Res.IsSuccessStatusCode)
                    {
                        var DataByagent = Res.Content.ReadAsStringAsync().Result;
                        PropDetails = JsonConvert.DeserializeObject<List<LD_ListingDetailsReportModel>>(DataByagent);
                    }
                    return Json(PropDetails, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception EX)
            {
                return Json(EX, JsonRequestBehavior.AllowGet);
            }
        }
        //Function to get Buyer Detail Reports 
        public JsonResult GetListingDetailsReport(List<String> listingDetailsObj)
        {
            string UserID = "";
            if (Request.Cookies["UseridCookie"] != null)
            {
                UserID = Request.Cookies["UseridCookie"].Value;
            }
            string pagesource = "";
            try
            {
                string urlAddress = ConfigurationManager.AppSettings["WebApiURL"]+"/ListingsDetailsReportPost";
                List<LD_ListingDetailsReportModel> PropDetails = new List<LD_ListingDetailsReportModel>();
                using (WebClient client = new WebClient())
                {
                    NameValueCollection postData = new NameValueCollection()
                    {
                       {"AgentID",listingDetailsObj[0]},
                        {"MLSID",listingDetailsObj[1]},
                        {"FromDate",listingDetailsObj[2]},
                        {"ToDate",listingDetailsObj[3]},
                    };
                    pagesource = Encoding.UTF8.GetString(client.UploadValues(urlAddress, postData));
                    var proplist = JsonConvert.DeserializeObject<List<LD_ListingDetailsReportModel>>(pagesource);
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
    }
}