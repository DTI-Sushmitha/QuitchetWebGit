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

namespace Quitchet.Web.Areas.Customer.Controllers
{
    public class CustomerSupportController : Controller
    {
        //
        // GET: /Customer/CustomerSupport/
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult InsertFeedback(string comment,string bug)
        {
            string UserID = "", macAdress="";
            if (Request.Cookies["UseridCookie"] != null)
            {
                UserID = Request.Cookies["UseridCookie"].Value;
            }
            string pagesource = "";
            try
            {
                ManagementClass objMgmtCls = new ManagementClass("Win32_NetworkAdapterConfiguration");
                foreach (ManagementObject objMgmt in objMgmtCls.GetInstances())
                {
                    if (!(bool)objMgmt["ipEnabled"])
                        continue;
                    macAdress = objMgmt["MACAddress"].ToString();
                }
            string urlAddress = ConfigurationManager.AppSettings["WebApiURL"]+ "/InsertFeedBack?";

            using (WebClient client = new WebClient())
            {
                NameValueCollection postData = new NameValueCollection()
                    {
                      {"UserID",UserID},
                      {"Is_BugOrIssue",bug},
                      {"Feedback",comment},
                      { "DeviceID",macAdress},
                      { "OSTypeID","1"},                           
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