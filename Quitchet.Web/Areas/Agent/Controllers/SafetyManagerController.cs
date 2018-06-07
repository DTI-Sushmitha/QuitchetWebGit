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
    public class SafetyManagerController : Controller
    {
        // GET: Agent/SafetyManager
        public ActionResult Index()
        {
            return View();
        }
        //Function to get Safety Manager Reports
        public JsonResult GetReportforSafetyManager(List<String> safetyManagerObj)
        {
            string UserID = "";
            if (Request.Cookies["UseridCookie"] != null)
            {
                UserID = Request.Cookies["UseridCookie"].Value;
            }
            string pagesource = "";
            try
            {
                string urlAddress = ConfigurationManager.AppSettings["WebApiURL"]+"/SafetyManagerReportGet";
                List<SafetyManagerReportModal> PropDetails = new List<SafetyManagerReportModal>();
                using (WebClient client = new WebClient())
                {
                    NameValueCollection postData = new NameValueCollection()
                    {
                       {"AgentID",safetyManagerObj[0]},
                        {"FromDate",safetyManagerObj[1]},
                        {"ToDate",safetyManagerObj[2]},
                        {"SortyBy",safetyManagerObj[3]},
                    };
                    pagesource = Encoding.UTF8.GetString(client.UploadValues(urlAddress, postData));
                    var proplist = JsonConvert.DeserializeObject<List<SafetyManagerReportModal>>(pagesource);
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
        //Method to Open House Details based on Open House Pkey Id and [AgetID]
        public async Task<JsonResult> GetOpenHouseDetails(string OH_PKID, string AgentId)
        {
            string UserID = "";
            if (Request.Cookies["UseridCookie"] != null)
            {
                UserID = Request.Cookies["UseridCookie"].Value;
            }
            try
            {
                string Baseurl = ConfigurationManager.AppSettings["WebApiURL"];
                List<OHDetailsModel> PropDetails = new List<OHDetailsModel>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string Method = "GetOpenHousesByPK?PK_OpenHouseID=" + OH_PKID + "&AgentID=" + AgentId;
                    HttpResponseMessage Res = await client.GetAsync(Method);
                    if (Res.IsSuccessStatusCode)
                    {
                        var DataByagent = Res.Content.ReadAsStringAsync().Result;
                        PropDetails = JsonConvert.DeserializeObject<List<OHDetailsModel>>(DataByagent);
                    }
                    return Json(PropDetails, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception EX)
            {
                return Json(EX, JsonRequestBehavior.AllowGet);
            }
        }
    }
}