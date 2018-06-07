using Newtonsoft.Json;
using Quitchet.Core.Models.Agent;
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

namespace Quitchet.Web.Areas.Agent.Controllers
{
    public class QCAgents
    {
        public string AgentID { get; set; }
        public string Name { get; set; }
        public string EMAIL { get; set; }
        public string PhotoPath { get; set; }
    }
    public class AgentProfileController : Controller
    {
        // GET: Agent/AgentProfile
        public ActionResult Index()
        {
            return View();
        }
        //Get Cities List  based on given State
        public async Task<JsonResult> GetCitiesBasedonState(string statecode, string AgentID)
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
                    string Method = "/NotAgentCitiesGet?StateCode=" + statecode + "&AgentID=" + AgentID;
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
        //Saving Agent Profile DAta
        public JsonResult InsertAgentProfile(List<String> obj)
        {
            string UserID = "", res = "";
            if (Request.Cookies["UseridCookie"] != null)
            {
                UserID = Request.Cookies["UseridCookie"].Value;
            }
            else
                UserID = "0";
            string pagesource = "";
            try
            {
                string urlAddress = ConfigurationManager.AppSettings["WebApiURL"]+"/AgentProfilePost?";
                using (WebClient client = new WebClient())
                {
                    NameValueCollection postData = new NameValueCollection()
                    {
                        {"RETS_AgentID",obj[0]},
                        {"First_Name",obj[1]},
                        {"Last_Name",obj[2]},
                        {"Nick_Name",obj[3]},
                        {"Brokerage",obj[4]},
                        {"Primary_Color",obj[5]},
                        {"Secondray_Color",obj[6]},
                        {"Broker_InCharge",obj[7]},
                        {"BrokerInCharge_Status",obj[8]},
                        {"Team_Leader",obj[9]},
                        {"TeamLeader_Status",obj[10]},
                        {"Email",obj[11]},
                        {"Gender",obj[12]},
                        {"Website",obj[13]},
                        {"MobileNo",obj[14]},
                        {"OfficeNo",obj[15]},
                        {"AgenType",obj[16]},
                        {"About",obj[17]},
                        {"Experience",obj[18]},
                        {"AreaServed",obj[19]},
                        {"Testmonials1",obj[20]},
                        {"Testmonials2",obj[21]},
                        {"Testmonials3",obj[22]},
                        {"Testmonials4",obj[23]},
                        {"Testmonials5",obj[24]},
                        {"AgentImage",obj[25]},
                        {"AgentLogo",obj[26]},
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
        }

        //Change Agent Password
        public JsonResult UpdateAgentPassword(string UserName, string Currentpwd, string NewPassword, string OperatingSystem, string IPAddress, string CurrentAddress)
        {
            var browsername = Request.Browser.Browser;
            string pagesource = "";
            string UserID = "0", UserEmail = "";
            if (IPAddress == "")
                IPAddress = "0";
            try
            {

                if (Request.Cookies["UseridCookie"] != null)
                {
                    UserID = Request.Cookies["UseridCookie"].Value;
                }
                if (Request.Cookies["UserEmailCookie"] != null)
                {
                    UserEmail = Request.Cookies["UserEmailCookie"].Value;
                }

                string urlAddress = ConfigurationManager.AppSettings["WebApiURL"]+"/ChangePasswordPost?";
                using (WebClient client = new WebClient())
                {
                    NameValueCollection postData = new NameValueCollection()
                    {
                      { "PK_UserID", UserID },  //order: {"parameter name", "parameter value"}
                      { "UserName", UserName},
                      { "UserEmail", UserEmail},
                      { "OldPassword", Currentpwd},
                      { "NewPassword", NewPassword},
                      { "Browser", browsername},
                      { "OperatingSys", OperatingSystem},
                      { "IPAddress", IPAddress},
                      { "CurrentAddress", CurrentAddress},
                      { "OsTypeID", "1"},
                    };
                    pagesource = Encoding.UTF8.GetString(client.UploadValues(urlAddress, postData));
                    var response = JsonConvert.DeserializeObject<List<AgentRequests_BuyersModel>>(pagesource);
                    return Json(response, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(ex.Message.ToString(), JsonRequestBehavior.AllowGet);
            }

        }//Method For Creating Quitchet User -- Post Method.

        //Saving Showings Safety Settings Data
        public JsonResult SaveShowingsSafetySettings(List<String> obj)
        {
            string res = "", pagesource = "";
            try
            {
                string urlAddress = ConfigurationManager.AppSettings["WebApiURL"]+"/ShowingsSaftySettingsPost?";
                using (WebClient client = new WebClient())
                {
                    NameValueCollection postData = new NameValueCollection()
                    {
                        {"AgentID",obj[0]},
                        {"SH_SaftyTimerStatus",obj[1]},
                        {"SH_EmergencyContact",obj[2]},
                        {"SH_EmergencyContactName",obj[3]},
                        {"SH_EmergencyContactImage",obj[4]},
                        {"SH_SafetyTimerLimit",obj[5]},
                        {"SH_TimeExtensionAgent",obj[6]},
                        {"SH_TimerAuthCode",obj[7]},
                        {"SH_EmergrncyCode",obj[8]},
                        {"SH_CallTimerExpire",obj[9]},
                        {"SH_TextEmergContact_TimerExpire",obj[10]},
                        {"SH_MessageTimerExpire",obj[11]},
                        {"SH_CallEmergCode",obj[12]},
                        {"SH_TextEmergContact_EmergCode",obj[13]},
                        {"SH_MessageEmergCode",obj[14]},
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
        }

        //Saving OpenHouses Safety Settings Data
        public JsonResult SaveOpenHousesSafetySettings(List<String> obj)
        {
            string res = "", pagesource = "";
            try
            {
                string urlAddress = ConfigurationManager.AppSettings["WebApiURL"]+"/OpenHouseSaftySettingsPost?";
                using (WebClient client = new WebClient())
                {
                    NameValueCollection postData = new NameValueCollection()
                    {
                        {"AgentID",obj[0]},
                        {"OH_SaftyTimerStatus",obj[1]},
                        {"OH_EmergencyContact",obj[2]},
                        {"OH_EmergencyContactName",obj[3]},
                        {"OH_EmergencyContactImage",obj[4]},
                        {"OH_TimeExtensionAgent",obj[5]},
                        {"OH_TimerAuthCode",obj[6]},
                        {"OH_EmergrncyCode",obj[7]},
                        {"OH_CallTimerExpire",obj[8]},
                        {"OH_TextEmergContact_TimerExpire",obj[9]},
                        {"OH_MessageTimerExpire",obj[10]},
                        {"OH_CallEmergCode",obj[11]},
                        {"OH_TextEmergContact_EmergCode",obj[12]},
                        {"OH_MessageEmergCode",obj[13]},
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
        }
        //Get Agent Safety Settings
        public async Task<JsonResult> GetAgentSafetySettings(string AgetId, string type)
        {
            string Method = "";
            try
            {
                string Baseurl = ConfigurationManager.AppSettings["WebApiURL"];
                List<AgentSafetySettingsModel> PropDetails = new List<AgentSafetySettingsModel>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    if (type == "sh")
                        Method = "/AgentShowingSettingsGet?AgentID=" + AgetId;
                    if (type == "oh")
                        Method = "/AgentOpenHouseSettingsGet?AgentID=" + AgetId;
                    HttpResponseMessage Res = await client.GetAsync(Method);
                    if (Res.IsSuccessStatusCode)
                    {
                        var DataByVIEWID = Res.Content.ReadAsStringAsync().Result;
                        PropDetails = JsonConvert.DeserializeObject<List<AgentSafetySettingsModel>>(DataByVIEWID);
                    }
                    return Json(PropDetails, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception EX)
            {
                return Json(EX, JsonRequestBehavior.AllowGet);
            }
        }
        //Get Cities (Areas Served by Agent) from RETS
        public async Task<JsonResult> GetAreasServedByAgent(string AgentID)
        {
            string Method = "";
            try
            {
                string Baseurl = ConfigurationManager.AppSettings["WebApiURL"];
                List<ManageAgentSearchModel> PropDetails = new List<ManageAgentSearchModel>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    Method = "/AgentSearvedCitiesGet?AgentID=" + AgentID;
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

        //Method to get Quitchet Agents for Brokerage and Team Member usign AutoSearch
        public async Task<JsonResult> GetQuitchetAgents_Old(string Name)
        {
            try
            {
                string Baseurl = ConfigurationManager.AppSettings["WebApiURL"];
                List<QCAgents> AgentDetails = new List<QCAgents>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string Method = "/SearchAgentsListForHostGet?Name=" + Name;
                    HttpResponseMessage Res = await client.GetAsync(Method);
                    if (Res.IsSuccessStatusCode)
                    {
                        var DataByVIEWID = Res.Content.ReadAsStringAsync().Result;
                        AgentDetails = JsonConvert.DeserializeObject<List<QCAgents>>(DataByVIEWID);
                    }
                    return Json(AgentDetails, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception EX)
            {
                return Json(EX, JsonRequestBehavior.AllowGet);
            }
        }
        public async Task<JsonResult> GetQuitchetAgents(string SearchEmail)
        {
            try
            {
                string Baseurl = ConfigurationManager.AppSettings["WebApiURL"];
                List<QCAgents> AgentDetails = new List<QCAgents>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string Method = "/SearchAgentEmailGet?SearchEmail=" + SearchEmail;
                    HttpResponseMessage Res = await client.GetAsync(Method);
                    if (Res.IsSuccessStatusCode)
                    {
                        var DataByVIEWID = Res.Content.ReadAsStringAsync().Result;
                        AgentDetails = JsonConvert.DeserializeObject<List<QCAgents>>(DataByVIEWID);
                    }
                    return Json(AgentDetails, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception EX)
            {
                return Json(EX, JsonRequestBehavior.AllowGet);
            }
        }
    }
}