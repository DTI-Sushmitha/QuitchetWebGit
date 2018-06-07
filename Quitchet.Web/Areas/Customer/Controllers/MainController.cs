using GoogleMaps.LocationServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Management;
using System.Net.NetworkInformation;
using System.Threading;
using Quitchet.Core.Models.Customer;
using System.Configuration;

namespace Quitchet.Web.Areas.Customer.Controllers
{
    public class MainController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public class unreadNotifications{
            public string UnViewedCount { get; set; }
        }
        public class CurrentUserDetialsProperty
        {
            public string PK_UserID { get; set; }
            public string Email { get; set; }
            public string FirstName { get; set; }
            public string First_Name { get; set; }
            public string LastName { get; set; }
            public string Last_Name { get; set; }
            public string Password { get; set; }
            public string Image { get; set; }
            public string postal_code { get; set; }
            public string MobileNo { get; set; }
            public string UserTypeID { get; set; }
            public string AgentID { get; set; }
            public string Manager { get; set; }
        }//Class members for holding Service returned data
        public JsonResult CreateUserRegistration(string EmailID, string Password, string FName, string LName, string ProPic, string latt, string lngt, string ZipCode)
        {
            //-----------To Get Mac Adress of Client Machine-----------------
            string macAdress = "";
            ManagementClass objMgmtCls = new ManagementClass("Win32_NetworkAdapterConfiguration");
            foreach (ManagementObject objMgmt in objMgmtCls.GetInstances())
            {
                if (!(bool)objMgmt["ipEnabled"])
                    continue;
                macAdress = objMgmt["MACAddress"].ToString();
            }
            //---------------------------------------------------------------

            string pagesource = "";
            string base64String = null;
            try
            {
                if (ProPic != "")
                {
                    string path = Path.Combine(Directory.GetCurrentDirectory(), ProPic);
                    // string filepath = AppDomain.CurrentDomain.BaseDirectory + ProPic;

                    using (System.Drawing.Image image = System.Drawing.Image.FromFile(path))
                    {
                        using (MemoryStream m = new MemoryStream())
                        {
                            image.Save(m, image.RawFormat);
                            byte[] imageBytes = m.ToArray();
                            base64String = Convert.ToBase64String(imageBytes);
                        }
                    }
                }

                string urlAddress = ConfigurationManager.AppSettings["WebApiURL"]+"/InsertUpdateUsers?";
                using (WebClient client = new WebClient())
                {
                    NameValueCollection postData = new NameValueCollection()
                    {
                      { "Password", Password },  //order: {"parameter name", "parameter value"}
                      { "FirstName", FName },
                      { "LastName", LName },
                      { "Email",EmailID},
                      { "ZipCode","0"},
                      { "Lat",latt},
                      { "Lang",lngt},
                      { "OSTypeID","1"},
                      { "DeviceID",macAdress},
                      { "Image",base64String}
                    };
                    pagesource = Encoding.UTF8.GetString(client.UploadValues(urlAddress, postData));
                    var response = JsonConvert.DeserializeObject(pagesource);
                    if (response.ToString() == "Successfully Created.")
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
        }//Method For Creating Quitchet User -- Post Method.
        //Method for User Login
        public JsonResult LoginUser(string EmailID, string Password, string RememberDevice)
        {
            string pagesource = "";
            List<CurrentUserDetialsProperty> currentUserDetails = new List<CurrentUserDetialsProperty>();
            try
            {
                string urlAddress = ConfigurationManager.AppSettings["WebApiURL"]+"/IsValidUser?";

                using (WebClient client = new WebClient())
                {
                    NameValueCollection postData = new NameValueCollection()
                    {
                      { "EmailID", EmailID },  //order: {"parameter name", "parameter value"}
                      { "Password", Password }
                    };

                    pagesource = Encoding.UTF8.GetString(client.UploadValues(urlAddress, postData));
                    var response = JsonConvert.DeserializeObject(pagesource);

                    if (response.ToString() == "There is no User.")
                    {
                        return Json("1", JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        currentUserDetails = JsonConvert.DeserializeObject<List<CurrentUserDetialsProperty>>(pagesource);
                        Session["PK_UserID"] = currentUserDetails[0].PK_UserID;
                        Session["UserFullName"] = currentUserDetails[0].FirstName + " " + currentUserDetails[0].LastName;

                        HttpCookie UseridCookie = new HttpCookie("CookieUserID");
                        Response.Cookies["UseridCookie"].Value = currentUserDetails[0].PK_UserID;
                        Response.Cookies["UseridCookie"].Expires = DateTime.Now.AddYears(1);

                        HttpCookie UserFirstNameCookie = new HttpCookie("CookieUserFirstName");
                        Response.Cookies["UserFirstNameCookie"].Value = currentUserDetails[0].FirstName;
                        Response.Cookies["UserFirstNameCookie"].Expires = DateTime.Now.AddYears(1);

                        HttpCookie UserTypeIDCookie = new HttpCookie("CookieUserTypeID");
                        Response.Cookies["UserTypeIDCookie"].Value = currentUserDetails[0].UserTypeID;
                        Response.Cookies["UserTypeIDCookie"].Expires = DateTime.Now.AddYears(1);

                        HttpCookie ProfilePicCookie = new HttpCookie("CookieProfilePic");
                        Response.Cookies["ProfilePicCookie"].Value = currentUserDetails[0].Image;
                        Response.Cookies["ProfilePicCookie"].Expires = DateTime.Now.AddYears(1);

                        HttpCookie UserLastNameCookie = new HttpCookie("CookieUserLastName");
                        Response.Cookies["UserLastNameCookie"].Value = currentUserDetails[0].LastName;
                        Response.Cookies["UserLastNameCookie"].Expires = DateTime.Now.AddYears(1);

                        HttpCookie UserEmailCookieforCP = new HttpCookie("CookieUserEmailforCP");
                        Response.Cookies["CookieUserEmailforCP"].Value = EmailID;
                        Response.Cookies["CookieUserEmailforCP"].Expires = DateTime.Now.AddYears(1);

                        HttpCookie RememberMyDeviceCookie = new HttpCookie("CookieRememberDevice");
                        Response.Cookies["RememberMyDeviceCookie"].Value = RememberDevice;
                        Response.Cookies["RememberMyDeviceCookie"].Expires = DateTime.Now.AddYears(1);

                        HttpCookie isManagerCookie = new HttpCookie("CookieisManager");
                        Response.Cookies["isManagerCookie"].Value = currentUserDetails[0].Manager;
                        Response.Cookies["isManagerCookie"].Expires = DateTime.Now.AddYears(1);

                        if (currentUserDetails[0].AgentID!="")
                        {
                            HttpCookie AgentIDCookie = new HttpCookie("CookieAgentID");
                            Response.Cookies["AgentIDCookie"].Value = currentUserDetails[0].AgentID;
                            Response.Cookies["AgentIDCookie"].Expires = DateTime.Now.AddYears(1);
                        }
                        if (RememberDevice == "1")
                        {
                            HttpCookie UserEmailCookie = new HttpCookie("CookieUserEmail");
                            Response.Cookies["UserEmailCookie"].Value = EmailID;
                            Response.Cookies["UserEmailCookie"].Expires = DateTime.Now.AddYears(1);

                            HttpCookie UserPasswordCookie = new HttpCookie("CookieUserPassword");
                            Response.Cookies["UserPasswordCookie"].Value = Password;
                            Response.Cookies["UserPasswordCookie"].Expires = DateTime.Now.AddYears(1);

                        }

                        return Json(currentUserDetails, JsonRequestBehavior.AllowGet);
                    }
                }

            }
            catch (Exception ex)
            {
                return Json(ex.Message.ToString(), JsonRequestBehavior.AllowGet);
            }
        }// Method for Login QuitChet Users.
        //Method to Check Email is exist or not
        public JsonResult CheckEmailExistOrNot(string EmailID)
        {
            string userid = "0";
            bool mail = false;
            List<CurrentUserDetialsProperty> currentUserDetails = new List<CurrentUserDetialsProperty>();
            try
            {
                string urlAddress = ConfigurationManager.AppSettings["WebApiURL"]+"/CheckEmailExistOrNot?";

                using (WebClient client = new WebClient())
                {
                    var emails = client.DownloadString(urlAddress);
                    currentUserDetails = JsonConvert.DeserializeObject<List<CurrentUserDetialsProperty>>(emails);
                    int count = currentUserDetails.Count;
                    for (int i = 0; i < count; i++)
                    {
                        if (currentUserDetails[i].Email == EmailID)
                        {
                            userid = currentUserDetails[i].PK_UserID;
                            mail = true;

                            //forgetsubmit();
                        }
                    }

                    if (mail == true)
                    {
                        return Json(userid, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(userid, JsonRequestBehavior.AllowGet);
                    }
                }

            }
            catch (Exception ex)
            {
                return Json(ex.Message.ToString(), JsonRequestBehavior.AllowGet);
            }
        }
        public async Task<JsonResult> GetUserDetails(string UserId)
        {
            try
            {
                string Baseurl = ConfigurationManager.AppSettings["WebApiURL"];
                List<CurrentUserDetialsProperty> currentUserDetails = new List<CurrentUserDetialsProperty>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string Method = "/GetUsers?Fkey_userID=" + UserId;
                    HttpResponseMessage Res = await client.GetAsync(Method);
                    if (Res.IsSuccessStatusCode)
                    {
                        var CurrentUserData = Res.Content.ReadAsStringAsync().Result;
                        currentUserDetails = JsonConvert.DeserializeObject<List<CurrentUserDetialsProperty>>(CurrentUserData);
                        HttpCookie UserFirstNameCookie = new HttpCookie("CookieUserFirstName");
                        Response.Cookies["UserFirstNameCookie"].Value = currentUserDetails[0].First_Name;
                        HttpCookie UserLastNameCookie = new HttpCookie("CookieUserLastName");
                        Response.Cookies["UserLastNameCookie"].Value = currentUserDetails[0].Last_Name;
                        HttpCookie ProfilePicCookie = new HttpCookie("CookieProfilePic");
                        Response.Cookies["ProfilePicCookie"].Value = currentUserDetails[0].Image;
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
        //Method to REset Password
        public async Task<JsonResult> SendValuesToResetPassWrd(string UserID, string EmailAddres)
        {
            try
            {
                string Baseurl = ConfigurationManager.AppSettings["WebApiURL"];
                List<CurrentUserDetialsProperty> currentUserDetails = new List<CurrentUserDetialsProperty>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string Method = "/ForgetPasswodLinkSend?EmailID=" + EmailAddres + "&FK_UserID=" + UserID;
                    HttpResponseMessage Res = await client.GetAsync(Method);
                    if (Res.IsSuccessStatusCode)
                    {
                        var CurrentUserData = Res.Content.ReadAsStringAsync().Result;
                        currentUserDetails = JsonConvert.DeserializeObject<List<CurrentUserDetialsProperty>>(CurrentUserData);
                    }
                    return Json(currentUserDetails, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception EX)
            {
                return Json(EX, JsonRequestBehavior.AllowGet);
            }
        }//To get Quitchet Login User Details.
        //Logout Action Method
        public JsonResult LogOut()
        {
            Response.Cookies["UseridCookie"].Value = null;
            Response.Cookies["UseridCookie"].Expires = DateTime.Now;
            Response.Cookies["UserFirstNameCookie"].Value = null;
            Response.Cookies["UserFirstNameCookie"].Expires = DateTime.Now;
            Response.Cookies["UserLastNameCookie"].Value = null;
            Response.Cookies["UserLastNameCookie"].Expires = DateTime.Now;
            Response.Cookies["AgentIDCookie"].Value = null;
            Response.Cookies["AgentIDCookie"].Expires = DateTime.Now;
            Response.Cookies["CookieUserEmailforCP"].Value = null;
            Response.Cookies["CookieUserEmailforCP"].Expires = DateTime.Now;
            Response.Cookies["ProfilePicCookie"].Value = null;
            Response.Cookies["ProfilePicCookie"].Expires = DateTime.Now;
            Response.Cookies["UserTypeIDCookie"].Value = null;
            Response.Cookies["UserTypeIDCookie"].Expires = DateTime.Now;
            Session["PK_UserID"] = null;
            Session["UserFullName"] = null;
            Response.Cookies.Add(Response.Cookies["UseridCookie"]);
            Response.Cookies.Add(Response.Cookies["UserFirstNameCookie"]);
            Response.Cookies.Add(Response.Cookies["UserLastNameCookie"]);
            //return Redirect("/Customer/LandingPage/Index");
           return Json("", JsonRequestBehavior.AllowGet);
        }
        //Method to Send Values to password link
        public JsonResult SendValuesnewPasswordLink(string Fname, string EmailID, string UserID, string Newpassword)
        {
            var pagesource = "";
            try
            {
                string urlAddress = ConfigurationManager.AppSettings["WebApiURL"]+"/ForgetPasswodReset?";
                using (WebClient client = new WebClient())
                {
                    NameValueCollection postData = new NameValueCollection()
                    {
                        {"First_Name",Fname},
                      { "EmailID", EmailID },
                        {"NewPassword",Newpassword },//order: {"parameter name", "parameter value"}
                      { "FK_UserID", UserID },
                    };
                    pagesource = Encoding.UTF8.GetString(client.UploadValues(urlAddress, postData));
                    var response = JsonConvert.DeserializeObject(pagesource);
                    if (response.ToString() == "Successfully Created.")
                    {
                        return Json("1", JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json("0", JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception EX)
            {
                return Json(EX, JsonRequestBehavior.AllowGet);
            }
        }
        //Method to Get User Notifications based on user id
        public async Task<JsonResult> GetUserNotificatons(string UserID)
        {
            try
            {
                string Baseurl = ConfigurationManager.AppSettings["WebApiURL"];
                List<NotificationsModel> notifications = new List<NotificationsModel>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string Method = "/NotificationsGet?FK_UserID=" + UserID;
                    HttpResponseMessage Res = await client.GetAsync(Method);
                    if (Res.IsSuccessStatusCode)
                    {
                        var CurrentUserData = Res.Content.ReadAsStringAsync().Result;
                        notifications = JsonConvert.DeserializeObject<List<NotificationsModel>>(CurrentUserData);
                    }
                    return Json(notifications, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception EX)
            {
                return Json(EX, JsonRequestBehavior.AllowGet);
            }
        }
        //Mehtod to upload images from Property Details view Popup\
        public JsonResult PostUploadImages(string MLSID, string PhotoBase)
        {
            var UserID = "0";
            string ResponceData = "";
            try
            {
                if (Request.Cookies["UseridCookie"] != null)
                {
                    UserID = Request.Cookies["UseridCookie"].Value;
                }
                string urlAddress = ConfigurationManager.AppSettings["WebApiURL"]+"/UserAddedPropertyPhotosPost?";
                using (WebClient client = new WebClient())
                {
                    NameValueCollection postData = new NameValueCollection()
                    {
                      { "MLSID",MLSID },
                      { "UserID",UserID },
                      { "Os_TypeID","1" },
                      { "Photo",PhotoBase },
                    };
                    ResponceData = Encoding.UTF8.GetString(client.UploadValues(urlAddress, postData));
                    var response = JsonConvert.DeserializeObject(ResponceData);
                    if (response.ToString() == "Successfully Saved.")
                    {
                        return Json("Sucess", JsonRequestBehavior.AllowGet);
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

        //Method to get Unviewed Notifications
        public async Task<JsonResult> GetUnReadUserNotificatons(string UserId)
        {
            try
            {
                string Baseurl = ConfigurationManager.AppSettings["WebApiURL"];
                List<unreadNotifications> currentUserDetails = new List<unreadNotifications>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string Method = "/UnViewedNotificationsCountGet?FK_UserID=" + UserId;
                    HttpResponseMessage Res = await client.GetAsync(Method);
                    if (Res.IsSuccessStatusCode)
                    {
                        var CurrentUserData = Res.Content.ReadAsStringAsync().Result;
                        currentUserDetails = JsonConvert.DeserializeObject<List<unreadNotifications>>(CurrentUserData);
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


        //Method to update satus of viewed notifications
        public async Task<JsonResult> UpdateNotificationViewStatus(string Pk_NotificationID)
        {
            try
            {
                string Baseurl = ConfigurationManager.AppSettings["WebApiURL"];
                List<unreadNotifications> currentUserDetails = new List<unreadNotifications>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string Method = "/UpdaeViewedNotificationsGet?Pk_NotificationID=" + Pk_NotificationID;
                    HttpResponseMessage Res = await client.GetAsync(Method);
                    if (Res.IsSuccessStatusCode)
                    {
                        var CurrentUserData = Res.Content.ReadAsStringAsync().Result;
                        currentUserDetails = JsonConvert.DeserializeObject<List<unreadNotifications>>(CurrentUserData);
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
        }
    }
}