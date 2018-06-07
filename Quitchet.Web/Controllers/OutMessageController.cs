using Newtonsoft.Json;
using Quitchet.Core.Models.Agent;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Configuration;

namespace Quitchet.Web.Controllers
{
    public class OutMessageController : Controller
    {
        // GET: OutMessage
        public ActionResult Index()
        {
            return View();
        }
        public async Task<JsonResult> GetFromUserDetails(string ChatID, string UserID)
        {
            try
            {
                string Baseurl = ConfigurationManager.AppSettings["WebApiURL"];
                List<FormUserDetailsModel> ChattingHistory = new List<FormUserDetailsModel>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string Method = "/ChatListGetByChatID?ChatID=" + ChatID + "&UserID=" + UserID ;
                    HttpResponseMessage Res = await client.GetAsync(Method);
                    if (Res.IsSuccessStatusCode)
                    {
                        var TotalChattingHistory = Res.Content.ReadAsStringAsync().Result;
                        ChattingHistory = JsonConvert.DeserializeObject<List<FormUserDetailsModel>>(TotalChattingHistory);
                    }
                    return Json(ChattingHistory, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception EX)
            {
                return Json(EX, JsonRequestBehavior.AllowGet);
            }
        }
        public async Task<JsonResult> GetChattingHistory(string ChatID, string UserID)
        {
            try
            {
                string Baseurl = ConfigurationManager.AppSettings["WebApiURL"];
                List<OutMessagesModel> ChattingHistory = new List<OutMessagesModel>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string Method = "/MessagesGet?ChatID=" + ChatID + "&UserID=" + UserID + "&Os_TypeID=1";
                    HttpResponseMessage Res = await client.GetAsync(Method);
                    if (Res.IsSuccessStatusCode)
                    {
                        var TotalChattingHistory = Res.Content.ReadAsStringAsync().Result;
                        ChattingHistory = JsonConvert.DeserializeObject<List<OutMessagesModel>>(TotalChattingHistory);
                    }
                    return Json(ChattingHistory, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception EX)
            {
                return Json(EX, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult PostChatting(string ChatID, string From_UserID, string Messages, string To_UserID, string Attachment, string OS_Type_ID)
        {
            string pagesource = "";
            try
            {
                string urlAddress = ConfigurationManager.AppSettings["WebApiURL"]+"/SendMessagePost?";

                using (WebClient client = new WebClient())
                {
                    NameValueCollection postData = new NameValueCollection()
                    {
                      {"ChatID",ChatID},
                      {"From_UserID",From_UserID},
                      {"Messages",Messages},
                      { "To_UserID",To_UserID},
                      { "Attachment",Attachment},
                      { "OS_Type_ID","1"}
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