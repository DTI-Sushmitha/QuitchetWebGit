using Newtonsoft.Json;
using Quitchet.Core.Models.Customer;
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

namespace Quitchet.Web.Areas.Customer.Controllers
{
    public class ShowingsController : Controller
    {
        // GET: Customer/Showings
        public ActionResult Index()
        {
            return View();
        }
        //Mehtod to Get user Personal Agent
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
        //Method to get User Requested showings List based on status
        public async Task<JsonResult> GetUserRequestedShowings(string Status)
        {
            string UserID = "";
            if (Request.Cookies["UseridCookie"] != null)
            {
                UserID = Request.Cookies["UseridCookie"].Value;
            }
            try
            {
                string Baseurl = ConfigurationManager.AppSettings["WebApiURL"];
                List<RequestShowingsModel> PropDetails = new List<RequestShowingsModel>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string Method = "/UserRequestGet?FK_UserID=" + UserID+ "&Status="+Status;
                    HttpResponseMessage Res = await client.GetAsync(Method);
                    if (Res.IsSuccessStatusCode)
                    {
                        var DataByagent = Res.Content.ReadAsStringAsync().Result;
                        PropDetails = JsonConvert.DeserializeObject<List<RequestShowingsModel>>(DataByagent);
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
        //Cancel Request by User in REquestet Showings Tab
        public JsonResult CancelRequest(string id)
        {
            string ResponceData = "";
            try
            {
                
                string urlAddress = ConfigurationManager.AppSettings["WebApiURL"]+"/CancelRequest?";
                using (WebClient client = new WebClient())
                {
                    NameValueCollection postData = new NameValueCollection()
                    {
                      { "PK_RequestShowingID", id },
                    };
                    ResponceData = Encoding.UTF8.GetString(client.UploadValues(urlAddress, postData));
                    var response = JsonConvert.DeserializeObject(ResponceData);
                    if (response.ToString() == "Successfully Canceled.")
                    {
                        return Json("Success", JsonRequestBehavior.AllowGet);
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
        //Edit request showings
        public async Task<JsonResult> EditRequestShowing(string ShowingID)
        {
            string UserID = "";
            if (Request.Cookies["UseridCookie"] != null)
            {
                UserID = Request.Cookies["UseridCookie"].Value;
            }
            try
            {
                string Baseurl = ConfigurationManager.AppSettings["WebApiURL"];
                List<EditRequestModel> PropDetails = new List<EditRequestModel>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string Method = "/UserRequestGetByPK?PK_RequestShowingID=" + ShowingID + "&FK_UserID=" + UserID;
                    HttpResponseMessage Res = await client.GetAsync(Method);
                    if (Res.IsSuccessStatusCode)
                    {
                        var DataByagent = Res.Content.ReadAsStringAsync().Result;
                        PropDetails = JsonConvert.DeserializeObject<List<EditRequestModel>>(DataByagent);
                    }
                    return Json(PropDetails, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception EX)
            {
                return Json(EX, JsonRequestBehavior.AllowGet);
            }
        }

        //Update Request showing
        public JsonResult Update_RequestShowing(List<String> obj)
        {
            string UserID = "", res = "";
            if (Request.Cookies["UseridCookie"] != null)
            {
                UserID = Request.Cookies["UseridCookie"].Value;
            }
            string pagesource = "";
            try
            {
                obj[2] = Convert.ToDateTime(obj[2]).ToString("HH:mm:ss");
                obj[3] = Convert.ToDateTime(obj[3]).ToString("HH:mm:ss");
                obj[4] = Convert.ToDateTime(obj[4]).ToString("HH:mm:ss");
                obj[5] = Convert.ToDateTime(obj[5]).ToString("HH:mm:ss");
                obj[7] = Convert.ToDateTime(obj[7]).ToString("HH:mm:ss");
                obj[8] = Convert.ToDateTime(obj[8]).ToString("HH:mm:ss");
                obj[9] = Convert.ToDateTime(obj[9]).ToString("HH:mm:ss");
                obj[10] = Convert.ToDateTime(obj[10]).ToString("HH:mm:ss");
                string urlAddress = ConfigurationManager.AppSettings["WebApiURL"]+"/EditUserRequestShowingPost?";
                using (WebClient client = new WebClient())
                {
                    NameValueCollection postData = new NameValueCollection()
                    {
                         
                        {"PK_RequestShowingID",obj[0]},
                        {"Choice1Date",obj[1]},
                        {"Choice1FromTime1",obj[2]},
                        {"Choice1ToTime1",obj[3]},
                        {"Choice1FromTime2",obj[4]},
                        {"Choice1ToTime2",obj[5]},
                        {"Choice2Date",obj[6]},
                        {"Choice2FromTime1",obj[7]},
                        {"Choice2ToTime1",obj[8]},
                        {"Choice2FromTime2",obj[9]},
                        {"Choice2ToTime2",obj[10]},

                    };
                    pagesource = Encoding.UTF8.GetString(client.UploadValues(urlAddress, postData));

                    var response = JsonConvert.DeserializeObject(pagesource);

                    if (response.ToString() == "Successfully Changed.")
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
            //return Json("", JsonRequestBehavior.AllowGet);
        }
        
        //Post Fedback
        public JsonResult PostFeedbackFromShowings(List<String> obj)
        {
            string UserID = "", res = "";
            if (Request.Cookies["UseridCookie"] != null)
            {
                UserID = Request.Cookies["UseridCookie"].Value;
            }
            string pagesource = "";
            try
            {
                string urlAddress = ConfigurationManager.AppSettings["WebApiURL"]+"/ShowingsFeedbackPost?";
                using (WebClient client = new WebClient())
                {
                    NameValueCollection postData = new NameValueCollection()
                    {
                        {"FK_UserID",UserID},
                        {"PK_RequestShowingID",obj[0]},
                        {"Comments",obj[1]},
                        {"Reating",obj[2]},
                        {"Top3",obj[3]},
                        {"PriceCompare",obj[4]},
                        {"Exterior",obj[5]},
                        {"Interior",obj[6]},
                        {"Landscaping",obj[7]},
                        {"Consideringoffer",obj[8]},
                        {"FeatureToImprove",obj[9]},
                        {"MLSID",obj[10]},
                        {"WouldPreferToSave",obj[11] }
                    };
                    pagesource = Encoding.UTF8.GetString(client.UploadValues(urlAddress, postData));

                    var response = JsonConvert.DeserializeObject(pagesource);

                    if (response.ToString() == "Successfully Changed.")
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
            //return Json("", JsonRequestBehavior.AllowGet);
        }
        //Edit request showings
        public async Task<JsonResult> GetFeedbackByID(string PK_FeedbackID)
        {
            string UserID = "";
            if (Request.Cookies["UseridCookie"] != null)
            {
                UserID = Request.Cookies["UseridCookie"].Value;
            }
            try
            {
                string Baseurl = ConfigurationManager.AppSettings["WebApiURL"];
                List<FeedbackModel> PropDetails = new List<FeedbackModel>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string Method = "/UserShowingsFeedbackGetPK?PK_UserShowingFeedbackID=" + PK_FeedbackID;
                    HttpResponseMessage Res = await client.GetAsync(Method);
                    if (Res.IsSuccessStatusCode)
                    {
                        var DataByagent = Res.Content.ReadAsStringAsync().Result;
                        PropDetails = JsonConvert.DeserializeObject<List<FeedbackModel>>(DataByagent);
                    }
                    return Json(PropDetails, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception EX)
            {
                return Json(EX, JsonRequestBehavior.AllowGet);
            }
        }


        //------------------Get Full Details to Show in Feedback Popup when came from Notifications
        public async Task<JsonResult> GetListingDetailsForFeedbackPopup(String MLSID, string UserID)
        {

            try
            {
                if (UserID == null)
                {
                    UserID = "0";
                }
                string Baseurl = ConfigurationManager.AppSettings["WebApiURL"];
                List<PropertyDetailsModel> CurrentCity = new List<PropertyDetailsModel>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string Method = "/DetailsByMLSID?MLSID=" + MLSID + "&UserID=" + UserID;
                    HttpResponseMessage Res = await client.GetAsync(Method);
                    if (Res.IsSuccessStatusCode)
                    {
                        var DataByMLSID = Res.Content.ReadAsStringAsync().Result;
                        CurrentCity = JsonConvert.DeserializeObject<List<PropertyDetailsModel>>(DataByMLSID);
                    }
                    var jsonResult = Json(CurrentCity, JsonRequestBehavior.AllowGet);
                    jsonResult.MaxJsonLength = int.MaxValue;
                    return jsonResult;
                }
            }
            catch (Exception EX)
            {
                return Json(EX, JsonRequestBehavior.AllowGet);
            }
        }
        //Post feedback as Not Now when came from notifications
        public JsonResult PostFeedbackAsNotNow(string Fk_UserID, string Fk_NotificationID, string PK_ShowingsID)
        {
            string res = "";
            string pagesource = "";
            try
            {
                string urlAddress = ConfigurationManager.AppSettings["WebApiURL"]+ "/ShowingsFeedbackNotNowPost?";
                using (WebClient client = new WebClient())
                {
                    NameValueCollection postData = new NameValueCollection()
                    {
                        {"FK_UserID",Fk_UserID},
                        {"Fk_NotificationID",Fk_NotificationID},
                        {"PK_ShowingsID",PK_ShowingsID},
                    };
                    pagesource = Encoding.UTF8.GetString(client.UploadValues(urlAddress, postData));
                    var response = JsonConvert.DeserializeObject(pagesource);
                    if (response.ToString() == "Successfully Deleted.")
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
    }
}