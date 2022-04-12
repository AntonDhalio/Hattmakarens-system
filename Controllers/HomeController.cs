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
         // TA BORT SEN    
            //var hatmodels = hatmodelRepository.GetAllHatmodels();
            //bool specExist = false;
            //foreach(var hatmodel in hatmodels)
            //{
            //    if(hatmodel.Name.Equals("Specialtillverkad"))
            //    {
            //        specExist = true;
            //    }
            //}
            //if(specExist == false)
            //{
            //    hatmodelRepository.CreateSpecHatModel();
            //}
            //return View();
            return RedirectToAction("ActiveHats", "Hat");

        }
    }
}