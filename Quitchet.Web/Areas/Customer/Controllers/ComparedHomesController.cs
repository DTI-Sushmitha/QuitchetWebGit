using Newtonsoft.Json;
using Quitchet.Core.Models.Customer;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Drawing;
using System.IO;
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
    public class ComparedHomesController : Controller
    {
        //s GET: /Customer/ComparedHomes/
        public ActionResult Index()
        {
            return View();
        }
        public async Task<JsonResult> GetComaparedPropertyDetails(int ViewID)
        {
            string UserID = "";
            if (Request.Cookies["UseridCookie"] != null)
            {
                UserID = Request.Cookies["UseridCookie"].Value;
            }
            try
            {
                string Baseurl = ConfigurationManager.AppSettings["WebApiURL"];
                List<ComparedHomeDetailsModel> PropDetails = new List<ComparedHomeDetailsModel>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string Method = "/ComparedHomeViewsGet?FK_UserID=" + UserID + "&ViewID=" + ViewID;
                    HttpResponseMessage Res = await client.GetAsync(Method);
                    if (Res.IsSuccessStatusCode)
                    {
                        var DataByVIEWID = Res.Content.ReadAsStringAsync().Result;
                        PropDetails = JsonConvert.DeserializeObject<List<ComparedHomeDetailsModel>>(DataByVIEWID);
                    }
                    return Json(PropDetails, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception EX)
            {
                return Json(EX, JsonRequestBehavior.AllowGet);
            }
        }
        private byte[] GetImage(string url)
        {
            Stream stream = null;
            byte[] buf;

            try
            {
                WebProxy myProxy = new WebProxy();
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);

                HttpWebResponse response = (HttpWebResponse)req.GetResponse();
                stream = response.GetResponseStream();

                using (BinaryReader br = new BinaryReader(stream))
                {
                    int len = (int)(response.ContentLength);
                    buf = br.ReadBytes(len);
                    br.Close();
                }

                stream.Close();
                response.Close();
            }
            catch (Exception exp)
            {
                buf = null;
            }

            return (buf);
        }
        public JsonResult PostuplaodedPhoto(string PK_CompareHome_ID, string MLSID, string ViewID, string Photo, string MlsidPhotoPath)
        {
            var UserID = "0";
            string ResponceData = "";
            try
            {
                if (Request.Cookies["UseridCookie"] != null)
                {
                    UserID = Request.Cookies["UseridCookie"].Value;
                }
                string urlAddress = ConfigurationManager.AppSettings["WebApiURL"]+"/CompareHomePhotoPost?";
                using (WebClient client = new WebClient())
                {
                    NameValueCollection postData = new NameValueCollection()
                    {
                      { "PK_CompareHomeID",PK_CompareHome_ID },
                      { "MLSID",MLSID },
                      { "ViewID",ViewID },
                      { "Photo",Photo},
                      {"MlsidPhotoPath",MlsidPhotoPath }
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


        public JsonResult UserSaveChanges(ComparedHomeDetailsModel postdat)
        {
            var UserID = "0";
            string ResponceData = "";
            try
            {
                if (Request.Cookies["UseridCookie"] != null)
                {
                    UserID = Request.Cookies["UseridCookie"].Value;
                }
                string urlAddress = ConfigurationManager.AppSettings["WebApiURL"]+"/CompareViewsOrderPost?";
                using (WebClient client = new WebClient())
                {
                    NameValueCollection postData = new NameValueCollection()
                    {
                      { "FK_USERID",    UserID },
                      { "OS_TYPEID",   "1" },
                      { "MAINVIEW" ,postdat.MAINVIEW.ToString() },
                      { "MASTERBEDROOM",postdat.MASTERBEDROOM.ToString()},
                      { "MASTERBATHROOM",postdat.MASTERBATHROOM.ToString() },
                      { "KITCHEN", postdat.KITCHEN.ToString()},
                      { "LIVING_GREATROOM",postdat.Living_Great_Room.ToString() },
                      { "BACKVIEW",postdat.BACKVIEW.ToString()},
                      { "OTHERINTERIOR",postdat.Other_Interior.ToString() },
                      { "OTHER", postdat.OTHER.ToString() },
                      { "ORDER1", postdat.ORDER1.ToString() },
                      { "ORDER2", postdat.ORDER2.ToString()},
                      { "ORDER3",postdat.ORDER3.ToString() },
                      { "ORDER4", postdat.ORDER4.ToString() },
                      { "ORDER5",postdat.ORDER5.ToString() },
                      { "ORDER6",postdat.ORDER6.ToString()},
                      { "ORDER7",postdat.ORDER7.ToString() },
                      { "ORDER8",postdat.ORDER8.ToString()},
                    };
                    ResponceData = Encoding.UTF8.GetString(client.UploadValues(urlAddress, postData));
                    var response = JsonConvert.DeserializeObject(ResponceData);
                    if (response.ToString() == "Successfully Created.")
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
        public async Task<JsonResult> GetUserSaveChangesDetails()
        {
            string UserID = "";
            if (Request.Cookies["UseridCookie"] != null)
            {
                UserID = Request.Cookies["UseridCookie"].Value;
            }
            try
            {
                string Baseurl = ConfigurationManager.AppSettings["WebApiURL"];
                List<ComparedHomeDetailsModel> PropDetails = new List<ComparedHomeDetailsModel>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string Method = "/ComparedHomeViewsOrdersGet?FK_UserID=" + UserID;
                    HttpResponseMessage Res = await client.GetAsync(Method);
                    if (Res.IsSuccessStatusCode)
                    {
                        var DataByVIEWID = Res.Content.ReadAsStringAsync().Result;
                        PropDetails = JsonConvert.DeserializeObject<List<ComparedHomeDetailsModel>>(DataByVIEWID);
                    }
                    return Json(PropDetails, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception EX)
            {
                return Json(EX, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult DeleteMLSID(string MLSID)
        {
            var UserID = "0";
            string ResponceData = "";
            try
            {
                if (Request.Cookies["UseridCookie"] != null)
                {
                    UserID = Request.Cookies["UseridCookie"].Value;
                }
                string urlAddress = ConfigurationManager.AppSettings["WebApiURL"]+"/ComparedHomesPost?";
                using (WebClient client = new WebClient())
                {
                    NameValueCollection postData = new NameValueCollection()
                    {
                      { "UserID", UserID },
                      { "MLSID", MLSID },
                      { "ComparedHome", "false" },
                      { "RequstShowing",""},
                    };
                    ResponceData = Encoding.UTF8.GetString(client.UploadValues(urlAddress, postData));
                    var response = JsonConvert.DeserializeObject(ResponceData);
                    if (response.ToString() == "Successfully Created.")
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
    }
}