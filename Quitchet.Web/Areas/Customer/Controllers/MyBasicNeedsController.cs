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
    public class MyBasicNeedsController : Controller
    {
        // GET: Customer/MyBasicNeeds
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult PostBasicNeeds(MyBasicNeedsAndWishListModel postdat)
        {
            var UserID = "0";
            string ResponceData = "";
            try
            {
                if (Request.Cookies["UseridCookie"] != null)
                {
                    UserID = Request.Cookies["UseridCookie"].Value;
                }
                if (postdat.Filter_TypeID == "1")
                {
                    postdat.To_LotAcreage = string.Empty;
                    postdat.From_LotAcreage = string.Empty;
                }
                else if (postdat.Filter_TypeID == "2")
                {
                    postdat.To_LotAcreage = string.Empty;
                    postdat.From_LotAcreage = string.Empty;
                    postdat.Master_Bedroom = string.Empty;
                }
                else if (postdat.Filter_TypeID == "3")
                {
                    postdat.Master_Bedroom = string.Empty;
                    postdat.Bedrooms = string.Empty;
                    postdat.Bathrooms = string.Empty;
                }
                string urlAddress = ConfigurationManager.AppSettings["WebApiURL"]+"/MyBasicNeedsPost?";
                using (WebClient client = new WebClient())
                {
                    NameValueCollection postData = new NameValueCollection()
                    {
                      { "FK_UserID",    UserID },
                      { "FilterType",    postdat.Filter_TypeID },
                      { "DistanceMinutes",    postdat.Distance_Minutes },
                      { "DistanceLocation",    postdat.Distance_Location },
                      { "PropertyType",   postdat.Property_Type },
                      { "FromPrice",    postdat.From_Price },
                      { "ToPrice",    postdat.To_Price },
                      { "Bedrooms",    postdat.Bedrooms },
                      { "Bathrooms",    postdat.Bathrooms },
                      { "MasterBedrooms",    postdat.Master_Bedroom },
                      { "HandicapAccess",    postdat.HandicapAccess },
                      { "FromLotAcrage",    postdat.From_LotAcreage },
                      { "ToLotAcrage",   postdat.To_LotAcreage }
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

        public async Task<JsonResult> GetBasicNeeds(int type)
        {
            string UserID = "";
            if (Request.Cookies["UseridCookie"] != null)
            {
                UserID = Request.Cookies["UseridCookie"].Value;
            }
            try
            {
                string Baseurl = ConfigurationManager.AppSettings["WebApiURL"];
                List<MyBasicNeedsAndWishListModel> PropDetails = new List<MyBasicNeedsAndWishListModel>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string Method = "/MyBasicNeedsGet?FK_UserID=" + UserID + "&FilterTypeID=" + type;
                    HttpResponseMessage Res = await client.GetAsync(Method);
                    if (Res.IsSuccessStatusCode)
                    {
                        var DataByVIEWID = Res.Content.ReadAsStringAsync().Result;
                        PropDetails = JsonConvert.DeserializeObject<List<MyBasicNeedsAndWishListModel>>(DataByVIEWID);
                    }
                    return Json(PropDetails, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception EX)
            {
                return Json(EX, JsonRequestBehavior.AllowGet);
            }
        }
    }
}