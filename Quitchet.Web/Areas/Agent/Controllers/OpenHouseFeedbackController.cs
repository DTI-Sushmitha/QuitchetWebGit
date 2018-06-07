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
    public class OpenHouseFeedbackController : Controller
    {
        // GET: Agent/OpenHouseFeedback
        public ActionResult Index()
        {
            return View();
        }
        public async Task<JsonResult> GetDataByHomeID_UserID(string PrimaryKeyID, string UserID)
        {
            try
            {
                string Baseurl = ConfigurationManager.AppSettings["WebApiURL"];
                List<OpenHouseFeedbackModel> HouseData = new List<OpenHouseFeedbackModel>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string Method = "/GetOpenHousesByPK?PK_OpenHouseID=" + PrimaryKeyID+"&AgentID="+ UserID;
                    HttpResponseMessage Res = await client.GetAsync(Method);
                    if (Res.IsSuccessStatusCode)
                    {
                        var CurrentHousePkeyData = Res.Content.ReadAsStringAsync().Result;
                        HouseData = JsonConvert.DeserializeObject<List<OpenHouseFeedbackModel>>(CurrentHousePkeyData);
                    }
                    return Json(HouseData, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception EX)
            {
                return Json(EX, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult InsertOpenHouseFeedback(string HouseID, string MlsID, string Comments, string Reating, string Top3, string PriceCompare, string Exterior, string Interior, string Landscaping, string Consideringoffer, string FeatureToImprove)
        {
            string pagesource = "";
            try
            {
                string urlAddress = ConfigurationManager.AppSettings["WebApiURL"]+"/OpenHouseFeedbackPost?";

                using (WebClient client = new WebClient())
                {
                    NameValueCollection postData = new NameValueCollection()
                    {
                      {"FK_OpenHouseID",HouseID},
                      {"MLSID",MlsID},
                      {"Comments",Comments},
                      { "Reating",Reating},
                      { "Top3",Top3},
                      { "PriceCompare",PriceCompare},
                      { "Exterior", Exterior},
                      { "Interior", Interior},
                      { "Landscaping", Landscaping},
                      { "Consideringoffer", Consideringoffer},
                      { "FeatureToImprove", FeatureToImprove},
                      { "WouldPreferToSave","false"},
                    };
                    pagesource = Encoding.UTF8.GetString(client.UploadValues(urlAddress, postData));
                    var response = JsonConvert.DeserializeObject(pagesource);

                    if (response.ToString() == "Successfully Saved.")
                    {
                        return Json("1", JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json("0", JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(ex.Message.ToString(), JsonRequestBehavior.AllowGet);
            }
        }
    }
}