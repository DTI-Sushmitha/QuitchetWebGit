using Newtonsoft.Json;
using Quitchet.Core.Models.Customer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Quitchet.Web.Areas.Customer.Controllers
{
    public class AccountActivedController : Controller
    {
        // GET: Customer/AccountActived
        public ActionResult Index()
        {
            return View();
        }
        public async Task<JsonResult> ActivateAccount(string key)
        {
            try
            {
                
                    List<SellerToolsModel> PropDetails = new List<SellerToolsModel>();
                var res = "";
                string Baseurl = ConfigurationManager.AppSettings["WebApiURL"];
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string Method = "/ActiveUserAcount?key="+ key;
                    HttpResponseMessage Res = await client.GetAsync(Method);
                    if (Res.IsSuccessStatusCode)
                    {
                        var DataByVIEWID = Res.Content.ReadAsStringAsync().Result;
                        PropDetails = JsonConvert.DeserializeObject<List<SellerToolsModel>>(DataByVIEWID);

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