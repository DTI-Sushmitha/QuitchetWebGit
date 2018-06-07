using Newtonsoft.Json;
using Quitchet.Core.Models.Agent;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Quitchet.Web.Areas.Customer.Controllers
{
    public class ClosedPartiesController : Controller
    {
        // GET: Customer/ClosedParties
        public ActionResult Index()
        {
            return View();
        }
        //Get Closing Listings to Assigned to Me
        public async Task<JsonResult> GetUserClosingListigns()
        {
            try
            {
                string UserID = "";
                if (Request.Cookies["UseridCookie"] != null)
                {
                    UserID = Request.Cookies["UseridCookie"].Value;
                }
                string Baseurl = ConfigurationManager.AppSettings["WebApiURL"];
                List<CloserListingsModel> PropDetails = new List<CloserListingsModel>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string Method = "/UserClosingsGet?FK_UserID=" + UserID;
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
    }
}