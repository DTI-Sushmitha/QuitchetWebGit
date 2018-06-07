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
using System.Configuration;

namespace Quitchet.Web.Areas.Customer.Controllers
{
    public class FavouritesController : Controller
    {
        //
        // GET: /Customer/Favourites/
        public ActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> GetFavoriteData(string sortid)
        {
            var UserID = "0";
            string macAdress = " ";
            try
            {
                if (Request.Cookies["UseridCookie"] != null)
                {
                    UserID = Request.Cookies["UseridCookie"].Value;
                }

                ManagementClass objMgmtCls = new ManagementClass("Win32_NetworkAdapterConfiguration");
                foreach (ManagementObject objMgmt in objMgmtCls.GetInstances())
                {
                    if (!(bool)objMgmt["ipEnabled"])
                        continue;
                    macAdress = objMgmt["MACAddress"].ToString();
                }
                string Baseurl =  ConfigurationManager.AppSettings["WebApiURL"];
                List<CurrentCityPropartyModel> FavoritesData = new List<CurrentCityPropartyModel>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string Method = "/UserFavoritesGet?FK_UserID=" + UserID + "&SortBy=" + sortid;
                    HttpResponseMessage Res = await client.GetAsync(Method);
                    if (Res.IsSuccessStatusCode)
                    {
                        var UserFavouritesData = Res.Content.ReadAsStringAsync().Result;
                        FavoritesData = JsonConvert.DeserializeObject<List<CurrentCityPropartyModel>>(UserFavouritesData);
                    }
                    var jsonResult = Json(FavoritesData, JsonRequestBehavior.AllowGet);
                    jsonResult.MaxJsonLength = int.MaxValue;
                    return jsonResult;
                    //return Json(FavoritesData, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception EX)
            {
                return Json(EX, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult FavrouriteChanges(string MLSId, string Fav)
        {
            var UserID = "0";
            string macAdress = " ";
            string Is_Fav = "true";
            string ResponceData = "";
            if (Fav == "0")
            {
                Is_Fav = "false";
            }
            try
            {
                if (Request.Cookies["UseridCookie"] != null)
                {
                    UserID = Request.Cookies["UseridCookie"].Value;
                }
                ManagementClass objMgmtCls = new ManagementClass("Win32_NetworkAdapterConfiguration");
                foreach (ManagementObject objMgmt in objMgmtCls.GetInstances())
                {
                    if (!(bool)objMgmt["ipEnabled"])
                        continue;
                    macAdress = objMgmt["MACAddress"].ToString();
                }
                string urlAddress = ConfigurationManager.AppSettings["WebApiURL"]+"/UserFavoritesPost?";
                using (WebClient client = new WebClient())
                {
                    NameValueCollection postData = new NameValueCollection()
                    {
                      { "UserID", UserID },
                      { "DeviceID", macAdress },
                      { "MLSID", MLSId },
                      { "Is_Favorite",Is_Fav},
                      { "Is_DriveBy","false"}
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

            //return Json("", JsonRequestBehavior.AllowGet);
        }
        public JsonResult CompareFavrouriteChanges(string MLSId, string Is_Compared)
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
                      { "MLSID", MLSId },
                      { "ComparedHome",Is_Compared},
                        { "OS_TypeID","1"}
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
        public JsonResult favrating(string MLSId, string Count)
        {


            var UserID = "0";
            var pagesource = "";
            try
            {

                if (Request.Cookies["UseridCookie"] != null)
                {
                    UserID = Request.Cookies["UseridCookie"].Value;
                }
                string urlAddress = ConfigurationManager.AppSettings["WebApiURL"]+"/PropertyRatingPost?";
                using (WebClient client = new WebClient())
                {
                    NameValueCollection postData = new NameValueCollection()
                    {
                         { "UserID", UserID },
                      { "RatingCount",Count},
                        {"MLSID",MLSId },
                      {  "OS_TypeID","1" },
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
        public JsonResult RequestShowFavrouriteChanges(string MLSId, string Is_RequestShow)
        {
            var UserID = "0";
            string ResponceData = "";
            try
            {
                if (Request.Cookies["UseridCookie"] != null)
                {
                    UserID = Request.Cookies["UseridCookie"].Value;
                }
                string urlAddress = ConfigurationManager.AppSettings["WebApiURL"]+"/RequestShowingPost?";
                using (WebClient client = new WebClient())
                {
                    NameValueCollection postData = new NameValueCollection()
                    {
                      { "UserID", UserID },
                      { "MLSID", MLSId },
                      { "RequestShowing",Is_RequestShow},
                        { "OS_TypeID","1"}
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

        public JsonResult SheduleRequestShowing(List<String> obj)
         {
            string UserID = "", res = "";
            if (Request.Cookies["UseridCookie"] != null)
            {
                UserID = Request.Cookies["UseridCookie"].Value;
            }
            string pagesource = "";
            try
            {

                //obj[3] = Convert.ToDateTime(obj[3]).ToString("HH:mm:ss");
                obj[3] = DateTime.Parse(obj[3]).ToString("HH:mm:ss");
                obj[4] = Convert.ToDateTime(obj[4]).ToString("HH:mm:ss");
                obj[5] = Convert.ToDateTime(obj[5]).ToString("HH:mm:ss");
                obj[6] = Convert.ToDateTime(obj[6]).ToString("HH:mm:ss");
                obj[8] = Convert.ToDateTime(obj[8]).ToString("HH:mm:ss");
                obj[9] = Convert.ToDateTime(obj[9]).ToString("HH:mm:ss");
                obj[10] = Convert.ToDateTime(obj[10]).ToString("HH:mm:ss");
                obj[11] = Convert.ToDateTime(obj[11]).ToString("HH:mm:ss");
                string urlAddress = ConfigurationManager.AppSettings["WebApiURL"]+"/UserRequestShowingPost?";
                using (WebClient client = new WebClient())
                {
                    NameValueCollection postData = new NameValueCollection()
                    {
                         {"FK_USERID",UserID},
                        {"MLSID",obj[0]},
                        {"AGENTID",obj[1]},
                        {"Choice1Date",obj[2]},
                        {"Choice1FromTime1",obj[3].Replace('.',':')},
                        {"Choice1ToTime1",obj[4].Replace('.',':')},
                        {"Choice1FromTime2",obj[5].Replace('.',':')},
                        {"Choice1ToTime2",obj[6].Replace('.',':')},
                        {"Choice2Date",obj[7]},
                        {"Choice2FromTime1",obj[8].Replace('.',':')},
                        {"Choice2ToTime1",obj[9].Replace('.',':')},
                        {"Choice2FromTime2",obj[10].Replace('.',':')},
                        {"Choice2ToTime2",obj[11].Replace('.',':')},

                    };
                    pagesource = Encoding.UTF8.GetString(client.UploadValues(urlAddress, postData));

                    var response = JsonConvert.DeserializeObject(pagesource);

                    if (response.ToString() == "Successfully Created.")
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
    }
}