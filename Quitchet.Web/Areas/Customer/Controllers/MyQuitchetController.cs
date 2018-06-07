using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Quitchet.Web.Areas.Customer.Controllers
{
    public class MyQuitchetController : Controller
    {
        // GET: Customer/MyQuitchet
        public ActionResult Index()
        {
            return View();
        }
    }
}