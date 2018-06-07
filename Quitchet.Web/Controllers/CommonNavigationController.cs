using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Quitchet.Web.Controllers
{
    public class CommonNavigationController : Controller
    {
        // GET: CommonNavigation
        public ActionResult Index()
        {
            return View();
        }
    }
}