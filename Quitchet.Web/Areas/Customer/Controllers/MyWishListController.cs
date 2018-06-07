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
    public class MyWishListController : Controller
    {
        //
        // GET: /Customer/MyWishList/
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult SaveHomeSearchFilters(List<String> obj)
        {
            //-----------To Get Mac Adress of Client Machine-----------------  
            string UserID = "", res = "";
            if (Request.Cookies["UseridCookie"] != null)
            {
                UserID = Request.Cookies["UseridCookie"].Value;
            }

            string pagesource = "";
            try
            {
                obj[17] = "";
                string urlAddress = ConfigurationManager.AppSettings["WebApiURL"]+"/MyWishListPost?";

                using (WebClient client = new WebClient())
                {
                    NameValueCollection postData = new NameValueCollection()
                    {
                        {"FK_UserID",UserID},
                        {"FilterType",obj[0]},
                        {"DistanceMinutes",obj[1]},
                        {"DistanceLocation",obj[2]},
                        {"PropertyType",obj[3]},
                        {"FromPrice",obj[4]},
                        {"ToPrice",obj[5]},
                        {"FromSqft",obj[6]},
                        {"ToSqft",obj[7]},
                        {"Bedrooms",obj[8]},
                        {"Bathrooms",obj[9]},
                        {"Elementary",""},
                        {"Middle",""},
                        {"High",""},
                        {"MasterBedrooms",obj[11]},
                        {"NoOfStories",obj[12]},
                        {"FromLotAcrage",obj[13]},
                        {"ToLotAcrage",obj[14]},
                        {"GarageCapacity",obj[15]},
                        {"GarageType",obj[16]},
                        {"Subdivision",obj[17]},
                        {"Age",obj[18]},
                        {"LotFeatures",obj[19]},
                        {"FoundationType",obj[20]},
                        {"ExteriorFeatures",obj[21]},
                        {"InteriorFeatures",obj[22]},
                        {"FlooringType",obj[23]},
                        {"Appliances",obj[24]},
                        {"MasterFeatures",obj[26]},
                         {"LaundryRoom",obj[25]},
                        {"Fireplace",obj[27]},
                        {"CommunityAmenities",obj[28]},
                        {"HeatingSystem",obj[29]},
                        {"WaterHeaterType",obj[30]},
                        {"WaterType",obj[31]},
                        {"SewerType",obj[32]},
                        {"ParkingType",""},
                    };
                    pagesource = Encoding.UTF8.GetString(client.UploadValues(urlAddress, postData));
                    var response = JsonConvert.DeserializeObject(pagesource);

                    if (response.ToString() == "Successfully Saved.")
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

        public JsonResult SaveRentalFilters(List<String> obj)
        {
            //-----------To Get Mac Adress of Client Machine-----------------  
            string UserID = "", res = "";
            if (Request.Cookies["UseridCookie"] != null)
            {
                UserID = Request.Cookies["UseridCookie"].Value;
            }

            string pagesource = "";
            try
            {
                string urlAddress = ConfigurationManager.AppSettings["WebApiURL"]+"/MyWishListPost?";

                using (WebClient client = new WebClient())
                {
                    NameValueCollection postData = new NameValueCollection()
                    {
                        {"FK_UserID",UserID},
                        {"FilterType",obj[0]},
                        {"DistanceMinutes",obj[1]},
                        {"DistanceLocation",obj[2]},
                        {"PropertyType",obj[3]},
                        {"FromPrice",obj[4]},
                        {"ToPrice",obj[5]},
                        {"FromSqft",obj[6]},
                        {"ToSqft",obj[7]},
                        {"Bedrooms",obj[8]},
                        {"Bathrooms",obj[9]},
                        {"Elementary",""},
                        {"Middle",""},
                        {"High",""},
                        {"MasterBedrooms",""},
                        {"NoOfStories",""},
                        {"FromLotAcrage",""},
                        {"ToLotAcrage",""},
                        {"GarageCapacity",obj[13]},
                        {"GarageType",""},
                        {"Subdivision",""},
                        {"Age",obj[12]},
                        {"LotFeatures",""},
                        {"FoundationType",""},
                        {"ExteriorFeatures",""},
                        {"InteriorFeatures",""},
                        {"FlooringType",""},
                        {"Appliances",""},
                        {"MasterFeatures",""},
                         {"LaundryRoom",""},
                        {"Fireplace",""},
                        {"CommunityAmenities",""},
                        {"HeatingSystem",""},
                        {"WaterHeaterType",""},
                        {"WaterType",""},
                        {"SewerType",""},
                        {"ParkingType",obj[11]},
                    };
                    pagesource = Encoding.UTF8.GetString(client.UploadValues(urlAddress, postData));
                    var response = JsonConvert.DeserializeObject(pagesource);

                    if (response.ToString() == "Successfully Saved.")
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

        public JsonResult SaveLotLandFilters(List<String> obj)
        {
            //-----------To Get Mac Adress of Client Machine-----------------  
            string UserID = "", res = "";
            if (Request.Cookies["UseridCookie"] != null)
            {
                UserID = Request.Cookies["UseridCookie"].Value;
            }

            string pagesource = "";
            try
            {
                string urlAddress = ConfigurationManager.AppSettings["WebApiURL"]+"/MyWishListPost?";

                using (WebClient client = new WebClient())
                {
                    NameValueCollection postData = new NameValueCollection()
                    {
                        {"FK_UserID",UserID},
                        {"FilterType",obj[0]},
                        {"DistanceMinutes",obj[1]},
                        {"DistanceLocation",obj[2]},
                        {"PropertyType",obj[3]},
                        {"FromPrice",obj[4]},
                        {"ToPrice",obj[5]},
                        {"FromSqft",""},
                        {"ToSqft",""},
                        {"Bedrooms",""},
                        {"Bathrooms",""},
                        {"Elementary",""},
                        {"Middle",""},
                        {"High",""},
                        {"MasterBedrooms",""},
                        {"NoOfStories",""},
                        {"FromLotAcrage",obj[6]},
                        {"ToLotAcrage",obj[7]},
                        {"GarageCapacity",""},
                        {"GarageType",""},
                        {"Subdivision",""},
                        {"Age",""},
                        {"LotFeatures",obj[12]},
                        {"FoundationType",""},
                        {"ExteriorFeatures",""},
                        {"InteriorFeatures",""},
                        {"FlooringType",""},
                        {"Appliances",""},
                        {"MasterFeatures",""},
                         {"LaundryRoom",""},
                        {"Fireplace",""},
                        {"CommunityAmenities",""},
                        {"HeatingSystem",""},
                        {"WaterHeaterType",""},
                        {"WaterType",obj[13]},
                        {"SewerType",obj[14]},
                        {"ParkingType",""},
                    };
                    pagesource = Encoding.UTF8.GetString(client.UploadValues(urlAddress, postData));
                    var response = JsonConvert.DeserializeObject(pagesource);

                    if (response.ToString() == "Successfully Saved.")
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

        public async Task<JsonResult> GetForsaleMywishlist(int type)
        {
            string UserID = "";
            if (Request.Cookies["UseridCookie"] != null)
            {
                UserID = Request.Cookies["UseridCookie"].Value;
            }
            try
            {

                string Baseurl = ConfigurationManager.AppSettings["WebApiURL"];
                List<MyWishListModel> PropDetails = new List<MyWishListModel>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string Method = "/MyWishListGet?FK_UserID=" + UserID + "&FilterTypeID=" + type;
                    HttpResponseMessage Res = await client.GetAsync(Method);
                    if (Res.IsSuccessStatusCode)
                    {
                        var DataByVIEWID = Res.Content.ReadAsStringAsync().Result;
                        PropDetails = JsonConvert.DeserializeObject<List<MyWishListModel>>(DataByVIEWID);
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