using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hattmakarens_system.Controllers
{
    public class StatisticController : Controller
    {
        // GET: Statistic
        public ActionResult GetStatistics()
        {
            return View();
        }
    }
}