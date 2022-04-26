using Hattmakarens_system.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hattmakarens_system.Controllers
{
    public class HomeController : Controller
    {
        HatmodelRepository hatmodelRepository = new HatmodelRepository();
        // GET: Home
        public ActionResult Index()
        {
            return RedirectToAction("ActiveHats", "Hat");

        }
    }
}