using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Quitchet.Core.Models.Customer;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Drawing;
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

namespace Quitchet.Web.Areas.Customer.Controllers
{
    public class ManageSearchAgentController : Controller
    {
        // GET: Customer/ManageSearchAgent
        public ActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> GetAgentCities(string statecode)
        {
            string UserID = "";
            if (Request.Cookies["UseridCookie"] != null)
            {
                UserID = Request.Cookies["UseridCookie"].Value;
            }
            try
            {
                string Baseurl = ConfigurationManager.AppSettings["WebApiURL"];
                List<ManageAgentSearchModel> PropDetails = new List<ManageAgentSearchModel>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string Method = "/AgentCitiesGet?StateCode=" + statecode;
                    HttpResponseMessage Res = await client.GetAsync(Method);
                    if (Res.IsSuccessStatusCode)
                    {
                        var DataByVIEWID = Res.Content.ReadAsStringAsync().Result;
                        PropDetails = JsonConvert.DeserializeObject<List<ManageAgentSearchModel>>(DataByVIEWID);
                    }
                    return Json(PropDetails, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception EX)
            {
                return Json(EX, JsonRequestBehavior.AllowGet);
            }
        }

        public async Task<JsonResult> GetAgentZipcodes(string statecode)
        {
            string UserID = "";
            if (Request.Cookies["UseridCookie"] != null)
            {
                UserID = Request.Cookies["UseridCookie"].Value;
            }
            try
            {
                string Baseurl = ConfigurationManager.AppSettings["WebApiURL"];
                List<ManageAgentSearchModel> PropDetails = new List<ManageAgentSearchModel>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string Method = "/StateZipsGet?StateCode=" + statecode;
                    HttpResponseMessage Res = await client.GetAsync(Method);
                    if (Res.IsSuccessStatusCode)
                    {
                        var DataBystate = Res.Content.ReadAsStringAsync().Result;
                        PropDetails = JsonConvert.DeserializeObject<List<ManageAgentSearchModel>>(DataBystate);
                    }
                    return Json(PropDetails, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception EX)
            {
                return Json(EX, JsonRequestBehavior.AllowGet);
            }
        }

        public async Task<JsonResult> GetAgentdetails(List<String> obj)
        {
            string UserID = "";
            var state = obj[0];
            var city = obj[1];
            var zipcode = obj[2];
            var agentname = obj[3];
            var sortby = obj[4];

            if (Request.Cookies["UseridCookie"] != null)
            {
                UserID = Request.Cookies["UseridCookie"].Value;
            }
            try
            {
                string Baseurl = ConfigurationManager.AppSettings["WebApiURL"];
                List<ManageAgentSearchModel> PropDetails = new List<ManageAgentSearchModel>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string Method = "/AgentSearchGet?StateCode=" + state + "&City=" + city + "&Zipcod=" + zipcode + "&Search=" + agentname + "&Sortby=" + sortby + "&UserID=" + UserID;
                    HttpResponseMessage Res = await client.GetAsync(Method);
                    if (Res.IsSuccessStatusCode)
                    {
                        var DataByagent = Res.Content.ReadAsStringAsync().Result;
                        PropDetails = JsonConvert.DeserializeObject<List<ManageAgentSearchModel>>(DataByagent);
                       
                    }
                    return Json(PropDetails, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception EX)
            {
                return Json(EX, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult PostAddAgent(string AgentID)
        {
            var UserID = "0";
            string ResponceData = "";
            try
            {
                if (Request.Cookies["UseridCookie"] != null)
                {
                    UserID = Request.Cookies["UseridCookie"].Value;
                }
                string urlAddress = ConfigurationManager.AppSettings["WebApiURL"]+ "/AddAgentPostWeb?";
                using (WebClient client = new WebClient())
                {
                    NameValueCollection postData = new NameValueCollection()
                    {
                      { "FK_USERID",UserID },
                      { "AGENTID",AgentID },
                    };
                    ResponceData = Encoding.UTF8.GetString(client.UploadValues(urlAddress, postData));
                   // ResponceData = ResponceData.TrimStart(new char[] { '[' }).TrimEnd(new char[] { ']' });
                    //var values = JsonConvert.DeserializeObject<Dictionary<string, string>>(ResponceData);
                    var response = JsonConvert.DeserializeObject(ResponceData);
                    //JArray v = JArray.Parse(ResponceData);
                    //var results = v[0]["Status"].ToString();
                    if (response.ToString() == "Agent Saved")
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

        public async Task<JsonResult> GetMyAgentdetails()
        {
            string UserID = "";
            if (Request.Cookies["UseridCookie"] != null)
            {
                UserID = Request.Cookies["UseridCookie"].Value;
            }
            try
            {
                string Baseurl = ConfigurationManager.AppSettings["WebApiURL"];
                List<ManageAgentSearchModel> PropDetails = new List<ManageAgentSearchModel>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string Method = "/UserAgentGet?UserID=" + UserID;
                    HttpResponseMessage Res = await client.GetAsync(Method);
                    if (Res.IsSuccessStatusCode)
                    {
                        var DataByagent = Res.Content.ReadAsStringAsync().Result;
                        PropDetails = JsonConvert.DeserializeObject<List<ManageAgentSearchModel>>(DataByagent);
                    }
                    return Json(PropDetails, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception EX)
            {
                return Json(EX, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult PostRemoveAgent(string AgentID, string Explanation)
        {
            var UserID = "0";
            string ResponceData = "";
            try
            {
                if (Request.Cookies["UseridCookie"] != null)
                {
                    UserID = Request.Cookies["UseridCookie"].Value;
                }
                string urlAddress = ConfigurationManager.AppSettings["WebApiURL"]+"/RemoveAgentPost?";
                using (WebClient client = new WebClient())
                {
                    NameValueCollection postData = new NameValueCollection()
                    {
                      { "FK_USERID",UserID },
                      { "AGENTID",AgentID },
                      { "Feedback",Explanation},
                    };
                    ResponceData = Encoding.UTF8.GetString(client.UploadValues(urlAddress, postData));
                    var response = JsonConvert.DeserializeObject<List<ManageAgentSearchModel>>(ResponceData);
                    return Json(response, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception EX)
            {
                return Json(EX, JsonRequestBehavior.AllowGet);
            }
        }

        //Get Agent Profile when clicked on View Profile
        public async Task<JsonResult> GetAgentProfile(string agentid)
        {
            string UserID = "";
            if (Request.Cookies["UseridCookie"] != null)
            {
                UserID = Request.Cookies["UseridCookie"].Value;
            }
            try
            {
                string Baseurl = ConfigurationManager.AppSettings["WebApiURL"];
                List<ManageAgentSearchModel> PropDetails = new List<ManageAgentSearchModel>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string Method = "/AgentGet?AgentID=" + agentid;
                    HttpResponseMessage Res = await client.GetAsync(Method);
                    if (Res.IsSuccessStatusCode)
                    {
                        var DataByagent = Res.Content.ReadAsStringAsync().Result;
                        PropDetails = JsonConvert.DeserializeObject<List<ManageAgentSearchModel>>(DataByagent);
                        //Response.Cookies["UserFirstNameCookie"].Value = PropDetails[0].FirstName;
                        //Response.Cookies["UserFirstNameCookie"].Expires = DateTime.Now.AddYears(1);

                        //Response.Cookies["UserLastNameCookie"].Value = PropDetails[0].LastName;
                        //Response.Cookies["UserLastNameCookie"].Expires = DateTime.Now.AddYears(1);

                        Response.Cookies["ProfilePicCookie"].Value = PropDetails[0].IMAGE;
                        Response.Cookies["ProfilePicCookie"].Expires = DateTime.Now.AddYears(1);
                    }
                    return Json(PropDetails, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception EX)
            {
                return Json(EX, JsonRequestBehavior.AllowGet);
            }
        }

        public async Task<JsonResult> GetAgentProfileForView(string agentid)
        {
            string UserID = "";
            if (Request.Cookies["UseridCookie"] != null)
            {
                UserID = Request.Cookies["UseridCookie"].Value;
            }
            try
            {
                string Baseurl = ConfigurationManager.AppSettings["WebApiURL"];
                List<ManageAgentSearchModel> PropDetails = new List<ManageAgentSearchModel>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string Method = "/AgentGet?AgentID=" + agentid;
                    HttpResponseMessage Res = await client.GetAsync(Method);
                    if (Res.IsSuccessStatusCode)
                    {
                        var DataByagent = Res.Content.ReadAsStringAsync().Result;
                        PropDetails = JsonConvert.DeserializeObject<List<ManageAgentSearchModel>>(DataByagent);
                        //Response.Cookies["UserFirstNameCookie"].Value = PropDetails[0].FirstName;
                        //Response.Cookies["UserFirstNameCookie"].Expires = DateTime.Now.AddYears(1);

                        //Response.Cookies["UserLastNameCookie"].Value = PropDetails[0].LastName;
                        //Response.Cookies["UserLastNameCookie"].Expires = DateTime.Now.AddYears(1);

                        //Response.Cookies["ProfilePicCookie"].Value = PropDetails[0].IMAGE;
                        //Response.Cookies["ProfilePicCookie"].Expires = DateTime.Now.AddYears(1);
                    }
                    return Json(PropDetails, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception EX)
            {
                return Json(EX, JsonRequestBehavior.AllowGet);
            }
        }
        //function to get Agent Active lsitings
        public async Task<JsonResult> ActiveListingsofAgent(string agentid)
        {
            string UserID = "";
            if (Request.Cookies["UseridCookie"] != null)
            {
                UserID = Request.Cookies["UseridCookie"].Value;
            }
            try
            {
                string Baseurl = ConfigurationManager.AppSettings["WebApiURL"];
                List<AgentListingsModel> PropDetails = new List<AgentListingsModel>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string Method = "/AgentActiveListingsGet?AgentID="+agentid+ "&FK_UserID=" + UserID;
                    HttpResponseMessage Res = await client.GetAsync(Method);
                    if (Res.IsSuccessStatusCode)
                    {
                        var DataByagent = Res.Content.ReadAsStringAsync().Result;
                        PropDetails = JsonConvert.DeserializeObject<List<AgentListingsModel>>(DataByagent);
                    }
                    var jsonResult = Json(PropDetails, JsonRequestBehavior.AllowGet);
                    jsonResult.MaxJsonLength = int.MaxValue;
                    return jsonResult;
                    //return Json(PropDetails, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception EX)
            {
                return Json(EX, JsonRequestBehavior.AllowGet);
            }
        }
        //function to get Recent listings of agent
        public async Task<JsonResult> RecentListingsofAgent(string agentid)
        {
            string UserID = "";
            if (Request.Cookies["UseridCookie"] != null)
            {
                UserID = Request.Cookies["UseridCookie"].Value;
            }
            try
            {
                string Baseurl = ConfigurationManager.AppSettings["WebApiURL"];
                List<AgentListingsModel> PropDetails = new List<AgentListingsModel>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string Method = "/AgentRecentSealesGet?AgentID=" + agentid + "&FK_UserID=" + UserID;
                    HttpResponseMessage Res = await client.GetAsync(Method);
                    if (Res.IsSuccessStatusCode)
                    {
                        var DataByagent = Res.Content.ReadAsStringAsync().Result;
                        PropDetails = JsonConvert.DeserializeObject<List<AgentListingsModel>>(DataByagent);
                    }
                    var jsonResult = Json(PropDetails, JsonRequestBehavior.AllowGet);
                    jsonResult.MaxJsonLength = int.MaxValue;
                    return jsonResult;
                    //return Json(PropDetails, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception EX)
            {
                return Json(EX, JsonRequestBehavior.AllowGet);
            }
        }
    }
}