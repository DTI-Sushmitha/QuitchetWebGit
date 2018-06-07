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
    public class LandingPageController : Controller
    {
        //
        // GET: /Customer/LandingPage/
        public ActionResult Index()
        {
            return View();
        }
        public class SearchResults
        {
            public string MLSNO { get; set; }
            public string ADDRESS { get; set; }
            public string Status { get; set; }
        }
        //Get MLS# Listing to to Search
        public async Task<JsonResult> SearchMLSByClassType(string mlsid, string classtype)
        {
           
            try
            {
                if (mlsid.Substring(0, 1) == "#")
                {
                    mlsid = mlsid.Remove(0, 1);

                    // Method = "AgentSearchMLSIDGet?MLSID=" + SearchEle;
                }
                //else
                //{
                //    Method = "AgentSearchAddressGet?Address=" + SearchEle;
                //}
               
                string Baseurl = ConfigurationManager.AppSettings["WebApiURL"];
                var M = "/SearchMLSIDByClassTypeGet?Search="+mlsid+ "&Class="+classtype;
                List<SearchResults> PropDetails = new List<SearchResults>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage Res = await client.GetAsync(M);
                    if (Res.IsSuccessStatusCode)
                    {
                        var DataByVIEWID = Res.Content.ReadAsStringAsync().Result;
                        PropDetails = JsonConvert.DeserializeObject<List<SearchResults>>(DataByVIEWID);
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