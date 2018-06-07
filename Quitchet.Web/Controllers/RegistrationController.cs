using Newtonsoft.Json;
using Quitchet.Core.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Quitchet.Web.Controllers
{
    public class RegistrationController : Controller
    {
        // GET: Registration
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult RegisterUser(RegistrationModel res)
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
            try
            {
                string urlAddress = ConfigurationManager.AppSettings["WebApiURL"]+"/InsertUpdateUsers?";
                using (WebClient client = new WebClient())
                {
                    NameValueCollection postData = new NameValueCollection()
                    {
                      { "Password", res.Password },  //order: {"parameter name", "parameter value"}
                      { "FirstName", res.FirstName },
                      { "LastName", res.LastName },
                      { "Email",res.Email},
                      { "ZipCode",res.PostalCode},
                      { "Lat",res.Latitude},
                      { "Lang",res.Longitude},
                      { "OSTypeID","1"},
                      { "DeviceID",macAdress},
                      { "Image",res.ProfilePic}
                    };
                    pagesource = Encoding.UTF8.GetString(client.UploadValues(urlAddress, postData));
                    var response = JsonConvert.DeserializeObject(pagesource);
                    if (response.ToString() == "Successfully Created.")
                    {
                        TempData["Message"] = "Successfully Registered";
                    }
                    else
                    {
                        TempData["Message"] = response.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Message"] = "Exception : -" +ex.ToString();
            }
            return RedirectToAction("Index");
        }
    }
}