using Hattmakarens_system.Models;
using Hattmakarens_system.Repositories;
using Hattmakarens_system.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hattmakarens_system.Controllers
{
    public class MaterialController : Controller
    {
        // GET: Material
        public ActionResult AddMaterial()
        {
            ViewBag.ColorsToPickFrom = new Service.Color().GetSelectListColors();
            return View();
        }

        [HttpPost]
        public ActionResult AddMaterial(MaterialViewModel materialViewModel, FormCollection forms)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var hexCode = Request.Form["ColorId"].ToString();
                    var isRegistered = new Service.Color().IsColorSaved(hexCode);
                    if (!isRegistered)
                    {
                        new Service.Color().AddColor(hexCode);
                    }

                    var colorId = new ColorRepository().GetColor(hexCode).Id;
                    var matRepo = new MaterialRepository();
                    var material = new MaterialModels
                    {
                        Name = materialViewModel.Name,
                        Description = materialViewModel.Description,
                        Type = Request.Form["Type"].ToString(),
                        ColorId = colorId
                    };
                    matRepo.SaveMaterial(material);
                    return RedirectToAction("AddMaterial", "Material");
                }
                else
                {
                    ViewBag.ColorsToPickFrom = new Service.Color().GetSelectListColors();
                    return View(materialViewModel);
                }
            }
            catch
            {
                return View("Error");
            }
        }
    }
}