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
using System.IO;
using System.Configuration;

namespace Quitchet.Web.Areas.Customer.Controllers
{
    public class OpenChatModel
    {
        public string CHATID { get; set; }
    }
    public class MessagesController : Controller
    {
        // GET: Customer/Messages
        public ActionResult Index()
        {
            return View();
        }
        public class MessageDetailsModel
        {
            public string CHATID { get; set; }
            public string PK_CHATID { get; set; }
            public string ChatType { get; set; }
            public string PK_USERID { get; set; }
            public string NAME { get; set; }
            public string COUNT_OF_MSGS { get; set; }
            public string IMAGE { get; set; }
            public string TO_USERS { get; set; }
            //MEssages Get Modal values and Send 
            public string PK_MessageID { get; set; }
            public string ChatID { get; set; }
            public string From_IDs { get; set; }
            public string To_IDs { get; set; }
            public string FROM_FRISTNAME { get; set; }
            public string FROM_LASTNAME { get; set; }
            public string TO_FRISTNAME { get; set; }
            public string TO_LASTNAME { get; set; }
            public string Message { get; set; }
            public string Attachments { get; set; }
            public string View_Status { get; set; }
            public string OS_Type_ID { get; set; }
            public string Created_Date { get; set; }
            public string Date { get; set; }
            public string Time { get; set; }
            public string First_Name { get; set; }
            public string Last_Name { get; set; }
            public string Email { get; set; }
            //MEssages to Send
            public string Messages { get; set; }
            public string From_UserID { get; set; }
            public string Attachment { get; set; }
            public string To_UserID { get; set; }
            public string MLSID { get; set; }
        }
        public class unreadMessages
        {
            public string MSG_COUNT { get; set; }
        }
        public class AllMessagesModel
        {
            public string PK_ChatIDs { get; set; }
            public string ChatTypes { get; set; }
            public string FK_UserIDs { get; set; }
            public string Names { get; set; }
            public string Images { get; set; }
            public string Have_Homes { get; set; }
            public string COUNT_OF_MSGS { get; set; }
            public string Home_UnReadCount { get; set; }
        }
        public class PropertyMessagesModel
        {
            public string PK_ChatID { get; set; }
            public string ChatType { get; set; }
            public string NAME { get; set; }
            public string Primary_Color { get; set; }
            public string Secondary_Color { get; set; }
            public string Brokerage { get; set; }
            public string MobileNo { get; set; }
            public string Email { get; set; }
            public string Image { get; set; }
            public string MLSID { get; set; }
            public string PRP_PRICE { get; set; }
            public string PRP_ADDRESS { get; set; }
            public string PRP_IMAGES { get; set; }
            public string LastMessageDate { get; set; }
            public string LastMessageTime { get; set; }
            public string UNREAD { get; set; }
          
        }
        //Method to create Chat Connection
        public async Task<JsonResult> creatChatConnectionID(string FromUser, string ToUser,string MLSID)
        {
            try
            {
                string Baseurl = ConfigurationManager.AppSettings["WebApiURL"];
                List<MessageDetailsModel> messagedetails = new List<MessageDetailsModel>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string Method = "/ChatConnectionIDGet?FromUser=" + FromUser + "&ToUser=" + ToUser + "&OS_TypeID=1&MLSID="+ MLSID;
                    HttpResponseMessage Res = await client.GetAsync(Method);
                    if (Res.IsSuccessStatusCode)
                    {
                        var UserMessagesData = Res.Content.ReadAsStringAsync().Result;
                        messagedetails = JsonConvert.DeserializeObject<List<MessageDetailsModel>>(UserMessagesData);
                    }
                    var jsonResult = Json(messagedetails, JsonRequestBehavior.AllowGet);
                    jsonResult.MaxJsonLength = int.MaxValue;
                    return jsonResult;
                    //return Json(messagedetails, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception EX)
            {
                return Json(EX, JsonRequestBehavior.AllowGet);
            }
        }
        //Method to get All Messages group by Chat ID
        public async Task<JsonResult> getAllMessagesList(string Sortby)
        {
            var UserID = "0";
            try
            {
                if (Request.Cookies["UseridCookie"] != null)
                {
                    UserID = Request.Cookies["UseridCookie"].Value;
                }
                string Baseurl = ConfigurationManager.AppSettings["WebApiURL"];
                List<AllMessagesModel> messagedetails = new List<AllMessagesModel>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                     string Method = "/MessagesMainMenuListGetWeb?FK_UserID=" + UserID+ "&SortBy="+Sortby;
                    HttpResponseMessage Res = await client.GetAsync(Method);
                    if (Res.IsSuccessStatusCode)
                    {
                        var UserMessagesData = Res.Content.ReadAsStringAsync().Result;
                        messagedetails = JsonConvert.DeserializeObject<List<AllMessagesModel>>(UserMessagesData);
                    }
                    var jsonResult = Json(messagedetails, JsonRequestBehavior.AllowGet);
                    jsonResult.MaxJsonLength = int.MaxValue;
                    return jsonResult;
                    //  return Json(messagedetails, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception EX)
            {
                return Json(EX, JsonRequestBehavior.AllowGet);
            }
        }
        //Method to get All Messages based on Chat ID and User ID
        public async Task<JsonResult> getMessagesbyChatID(string ChatId)
        {
            var UserID = "0";
            try
            {
                if (Request.Cookies["UseridCookie"] != null)
                {
                    UserID = Request.Cookies["UseridCookie"].Value;
                }
                string Baseurl = ConfigurationManager.AppSettings["WebApiURL"];
                List<MessageDetailsModel> messagedetails = new List<MessageDetailsModel>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string Method = "/MessagesGet?ChatID=" + ChatId + "&UserID=" + UserID + "&Os_TypeID=1";
                    HttpResponseMessage Res = await client.GetAsync(Method);
                    if (Res.IsSuccessStatusCode)
                    {
                        var UserMessagesData = Res.Content.ReadAsStringAsync().Result;
                        messagedetails = JsonConvert.DeserializeObject<List<MessageDetailsModel>>(UserMessagesData);
                    }
                    var jsonResult = Json(messagedetails, JsonRequestBehavior.AllowGet);
                    jsonResult.MaxJsonLength = int.MaxValue;
                    return jsonResult;
                    //  return Json(messagedetails, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception EX)
            {
                return Json(EX, JsonRequestBehavior.AllowGet);
            }
        }
        //Method to get Only Unread Messages based on Chat ID and User ID which runs within time intervals
        public async Task<JsonResult> getUnreadMessagesbyChatID(string ChatId,string pk_msgid)
        {
            var UserID = "0";
            try
            {
                if (Request.Cookies["UseridCookie"] != null)
                {
                    UserID = Request.Cookies["UseridCookie"].Value;
                }
                string Baseurl = ConfigurationManager.AppSettings["WebApiURL"];
                List<MessageDetailsModel> messagedetails = new List<MessageDetailsModel>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string Method = "/UnReadMessagesGet?ChatID=" + ChatId + "&UserID=" + UserID + "&Os_TypeID="+ pk_msgid;
                    HttpResponseMessage Res = await client.GetAsync(Method);
                    if (Res.IsSuccessStatusCode)
                    {
                        var UserMessagesData = Res.Content.ReadAsStringAsync().Result;
                        messagedetails = JsonConvert.DeserializeObject<List<MessageDetailsModel>>(UserMessagesData);
                    }
                    var jsonResult = Json(messagedetails, JsonRequestBehavior.AllowGet);
                    jsonResult.MaxJsonLength = int.MaxValue;
                    return jsonResult;
                    //  return Json(messagedetails, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception EX)
            {
                return Json(EX, JsonRequestBehavior.AllowGet);
            }
        }
        //Method to get All Messages based on Chat ID and User ID
        public async Task<JsonResult> GetListofPropertyChats(string ToUserID)
        {
            var UserID = "0";
            try
            {
                if (Request.Cookies["UseridCookie"] != null)
                {
                    UserID = Request.Cookies["UseridCookie"].Value;
                }
                string Baseurl = ConfigurationManager.AppSettings["WebApiURL"];
                List<PropertyMessagesModel> messagedetails = new List<PropertyMessagesModel>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string Method = "/MessagesSubMenuHomesListGetWeb?From_UserID=" + UserID + "&To_UserID=" + ToUserID;
                    HttpResponseMessage Res = await client.GetAsync(Method);
                    if (Res.IsSuccessStatusCode)
                    {
                        var UserMessagesData = Res.Content.ReadAsStringAsync().Result;
                        messagedetails = JsonConvert.DeserializeObject<List<PropertyMessagesModel>>(UserMessagesData);
                    }
                    var jsonResult = Json(messagedetails, JsonRequestBehavior.AllowGet);
                    jsonResult.MaxJsonLength = int.MaxValue;
                    return jsonResult;
                    //  return Json(messagedetails, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception EX)
            {
                return Json(EX, JsonRequestBehavior.AllowGet);
            }
        }
        //Send Messages Method
        public JsonResult SendMessages(string ChatID, string Messages, string To_UserID, string Attachment)
        {
            //-----------To Get Mac Adress of Client Machine-----------------  
            string UserID = "",  res = "";
            if (Request.Cookies["UseridCookie"] != null)
            {
                UserID = Request.Cookies["UseridCookie"].Value;
            }

            string pagesource = "";
            try
            {

                string urlAddress = ConfigurationManager.AppSettings["WebApiURL"]+"/SendMessagePost?";

                //string fileName = Attachment;
                //FileStream uss;
                //Directory.CreateDirectory(Path.GetDirectoryName(fileName));

                //using (FileStream fs = new FileStream(fileName, FileMode.Create))
                //{
                //    uss = fs;
                //}


                using (WebClient client = new WebClient())
                {
                    NameValueCollection postData = new NameValueCollection()
                    {
                        {"ChatID",ChatID},
                        {"Messages",Messages},
                        {"To_UserID",To_UserID},
                        {"Attachment",Attachment},
                        {"From_UserID",UserID},
                        {"OS_Type_ID","1" },
                    };
                    pagesource = Encoding.UTF8.GetString(client.UploadValues(urlAddress, postData));
                    var response = JsonConvert.DeserializeObject(pagesource);

                    if (response.ToString() == "Successfully Send.")
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

        //Remove Chat by ChatId
        public JsonResult removeChatbyChatID(string ChatID, string FullDelete)
        {
            string pagesource = "",res="", UserID="";
            if (Request.Cookies["UseridCookie"] != null)
            {
                UserID = Request.Cookies["UseridCookie"].Value;
            }
            try
            {
                string urlAddress = ConfigurationManager.AppSettings["WebApiURL"]+"/DeleteAllChatPost?";
                using (WebClient client = new WebClient())
                {
                    NameValueCollection postData = new NameValueCollection()
                    {
                        {"ChatID",ChatID},
                        {"USERID",UserID},
                        {"FullDelete",FullDelete},
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

        //Method to create Chat Connection ID for Non Quitchet  User
        public JsonResult createChatForNonQCUser(string FromUser, string Email, string Mobile, string Name, string MLSID)
        {
            List<OpenChatModel> dataBasedonLotsFilter = new List<OpenChatModel>();
            string pagesource = "",res="";
            try
            {
                string urlAddress = ConfigurationManager.AppSettings["WebApiURL"]+"/AddNewOpenChatMemberPost?";
                using (WebClient client = new WebClient())
                {
                    NameValueCollection postData = new NameValueCollection()
                    {
                        { "From_UserID",FromUser},
                        { "Email",Email},
                        { "MobileNo",Mobile},
                        { "Name",Name},
                        { "MLSID",MLSID},
                        { "OsTypeID","1"},
                        { "IsGroup ","False"},
                    };
                    pagesource = Encoding.UTF8.GetString(client.UploadValues(urlAddress, postData));
                    var proplist = JsonConvert.DeserializeObject<List<OpenChatModel>>(pagesource);
                    var jsonResult = Json(proplist, JsonRequestBehavior.AllowGet);
                    jsonResult.MaxJsonLength = int.MaxValue;
                    return jsonResult;
                }
            }
            catch (Exception ex)
            {
                res = ex.Message.ToString();
                return Json(res, JsonRequestBehavior.AllowGet);
            }
        }

        //Method to get Unviewed Notifications
        public async Task<JsonResult> GetUnReadUserMessages(string UserId)
        {
            try
            {
                string Baseurl = ConfigurationManager.AppSettings["WebApiURL"];
                List<unreadMessages> currentUserDetails = new List<unreadMessages>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string Method = "/MessagesCountGet?UserID=" + UserId;
                    HttpResponseMessage Res = await client.GetAsync(Method);
                    if (Res.IsSuccessStatusCode)
                    {

                        var CurrentUserData = Res.Content.ReadAsStringAsync().Result;
                        currentUserDetails = JsonConvert.DeserializeObject<List<unreadMessages>>(CurrentUserData);
                    }
                    var jsonResult = Json(currentUserDetails, JsonRequestBehavior.AllowGet);
                    jsonResult.MaxJsonLength = int.MaxValue;
                    return jsonResult;

                }
            }
            catch (Exception EX)
            {
                return Json(EX, JsonRequestBehavior.AllowGet);
            }
        }//To get Quitchet Login User Details.
    }
}