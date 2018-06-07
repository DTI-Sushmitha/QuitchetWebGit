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

namespace Quitchet.Web.Areas.Agent.Controllers
{
    public class AgentNotificationSettingsController : Controller
    {
        // GET: Agent/AgentNotificationSettings
        public ActionResult Index()
        {
            return View();
        }
        public class AgentNotificationSettings
        {
            public string PK_NotificationSettigsID { get; set; }
            public int FK_UserID { get; set; }
            public string Messages { get; set; }
            public string Saved_Searches { get; set; }
            public string MyCustomer { get; set; }
            public string MyListings { get; set; }
            public string Request_Showings { get; set; }
            public string Showings_Feedback { get; set; }
            public string OpenHouse_Feedback { get; set; }
            public string Closer { get; set; }

        }
        public JsonResult SaveNotificationSettings(List<String> obj)
        {
            string UserID = "", res = "";
            if (Request.Cookies["UseridCookie"] != null)
            {
                UserID = Request.Cookies["UseridCookie"].Value;
            }
            string pagesource = "";
            try
            {
                string urlAddress = ConfigurationManager.AppSettings["WebApiURL"]+"/AgentNotificationSettingsPost?";

                using (WebClient client = new WebClient())
                {
                    NameValueCollection postData = new NameValueCollection()
                    {
                        {"Fk_UserID",UserID},
                        {"OS_TypeID","1"},
                        {"Messages",obj[0].ToString()},
                        {"SaveSearches",obj[1].ToString()},
                        {"MyCustomers",obj[2].ToString()},
                        {"MyListings",obj[3].ToString()},
                        {"RequestedShowings",obj[4].ToString()},
                        {"ShowingsFeedbacks",obj[5].ToString()},
                        {"OpenHouseFeedbacks",obj[6].ToString()},
                        {"Closer",obj[7].ToString()},
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
        }//To save Notification Setings
        public async Task<JsonResult> GetAgentNotificationSettingsData()
        {
            string UserID = "0";
            if (Request.Cookies["UseridCookie"] != null)
            {
                UserID = Request.Cookies["UseridCookie"].Value;
            }
            try
            {
                string Baseurl = ConfigurationManager.AppSettings["WebApiURL"];
                List<AgentNotificationSettings> result = new List<AgentNotificationSettings>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string Method = "/NotificationSettingsGet?FK_UserID=" + UserID;
                    HttpResponseMessage Res = await client.GetAsync(Method);
                    if (Res.IsSuccessStatusCode)
                    {
                        var resultData = Res.Content.ReadAsStringAsync().Result;
                        result = JsonConvert.DeserializeObject<List<AgentNotificationSettings>>(resultData);
                    }
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception EX)
            {
                return Json(EX, JsonRequestBehavior.AllowGet);
            }
        }
    }
}