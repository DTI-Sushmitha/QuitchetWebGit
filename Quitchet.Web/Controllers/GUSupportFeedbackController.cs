using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Management;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Quitchet.Web.Controllers
{
    public class GUSupportFeedbackController : Controller
    {
        // GET: GUSupportFeedback
        public ActionResult Index()
        {
            return View();
        }
        //public JsonResult GuestInsertFeedback(string fname,string lname,string email,string comment, string bug)
        public JsonResult GuestInsertFeedback(List<String> obj)
        {
            string pagesource = "";
            try
            {
                string urlAddress = ConfigurationManager.AppSettings["WebApiURL"]+"/InsertGuestUserFeedBackPost?";

                using (WebClient client = new WebClient())
                {
                    NameValueCollection postData = new NameValueCollection()
                    {
                      //{"FirstName",fname},
                      //{"LastName",lname},
                      //{"Email",email},
                      //{ "Feedback",comment},
                      //{ "Is_BugOrIssue",bug},
                      {"FirstName",obj[0]},
                      {"LastName",obj[1]},
                      {"Email",obj[2]},
                      { "Feedback",obj[3]},
                      { "Is_BugOrIssue",obj[4]},
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
        }
    }
}