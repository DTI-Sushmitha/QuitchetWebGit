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
using System.Configuration;

namespace Quitchet.Web.Areas.Customer.Controllers
{
    public class MyProfileController : Controller
    {
        // GET: Customer/MyProfile
        public ActionResult Index()
        {
            return View();
       }
     
        public class ProfileUpdateProperty
        {
            public string PK_UserID { get; set; }
            public string Email { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Image { get; set; }
            public string MobileNo { get; set; }
            public string Password { get; set; }
            public string Status { get; set; }
        }//Class members for holding Service returned data
        public JsonResult UpdateMyProfile(List<String> obj)
        {
            string pagesource = "";
            string base64String = null;
            try
            {
                if (obj[5] != "")
                {
                    //string path = Path.Combine(Directory.GetCurrentDirectory(), obj[5]);
                    //// string filepath = AppDomain.CurrentDomain.BaseDirectory + ProPic;

                    //using (System.Drawing.Image image = System.Drawing.Image.FromFile(path))
                    //{
                    //    using (MemoryStream m = new MemoryStream())
                    //    {
                    //        image.Save(m, image.RawFormat);
                    //        byte[] imageBytes = m.ToArray();
                    //        base64String = Convert.ToBase64String(imageBytes);
                    //    }
                    //}
                    obj[6] = "";
                }
                string urlAddress = ConfigurationManager.AppSettings["WebApiURL"]+"/MyProfilePost?";
                using (WebClient client = new WebClient())
                {
                    NameValueCollection postData = new NameValueCollection()
                    {
                      { "PK_UserID", obj[0] },  //order: {"parameter name", "parameter value"}
                      { "FirstName", obj[1] },
                      { "LastName", obj[2] },
                      { "Email",obj[3]},
                      { "MobileNo",obj[4]},
                      { "ConvertedImageString",obj[5] },
                        { "ExistedImagePath",obj[6] }
                    };
                    pagesource = Encoding.UTF8.GetString(client.UploadValues(urlAddress, postData));
                    var response = JsonConvert.DeserializeObject(pagesource);
                    if (response.ToString() == "Successfully Created.")
                    {
                        HttpCookie UserFirstNameCookie = new HttpCookie("CookieUserFirstName");
                        Response.Cookies["UserFirstNameCookie"].Value = obj[1];
                        HttpCookie UserLastNameCookie = new HttpCookie("CookieUserLastName");
                        Response.Cookies["UserLastNameCookie"].Value = obj[2];

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
        public JsonResult ChangePassword(string oldPwd,string newPwd,string OperatingSystem,string IPAddress,string CurrentAddress)
        {
            var browsername = Request.Browser.Browser;
            string pagesource = "";
            string UserID = "0",UserName="",UserEmail="";
            try
            {
               
                if (Request.Cookies["UseridCookie"] != null)
                {
                    UserID = Request.Cookies["UseridCookie"].Value;
                }
                UserName = Request.Cookies["UserFirstNameCookie"].Value + " " + Request.Cookies["UserLastNameCookie"].Value;
                UserEmail= Request.Cookies["CookieUserEmailforCP"].Value;

                List<ProfileUpdateProperty> statusresult = new List<ProfileUpdateProperty>();
                string urlAddress = ConfigurationManager.AppSettings["WebApiURL"]+"/ChangePasswordPost?";
                using (WebClient client = new WebClient())
                {
                    NameValueCollection postData = new NameValueCollection()
                    {
                      { "PK_UserID", UserID },  //order: {"parameter name", "parameter value"}
                      { "UserName", UserName},
                      { "UserEmail", UserEmail},
                      { "OldPassword", oldPwd},
                      { "NewPassword", newPwd},
                      { "Browser", browsername},
                      { "OperatingSys", OperatingSystem},
                      { "IPAddress", IPAddress},
                      { "CurrentAddress", CurrentAddress},
                      { "OsTypeID", "1"},
                    };
                    pagesource = Encoding.UTF8.GetString(client.UploadValues(urlAddress, postData));
                //    var response = JsonConvert.DeserializeObject(pagesource);
                    statusresult = JsonConvert.DeserializeObject<List<ProfileUpdateProperty>>(pagesource);
                    return Json(statusresult, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(ex.Message.ToString(), JsonRequestBehavior.AllowGet);
            }

        }//Method For Creating Quitchet User -- Post Method.
    }
}