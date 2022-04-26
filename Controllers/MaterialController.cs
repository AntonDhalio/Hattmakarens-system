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
        public ActionResult AddMaterial(bool isAdded)
        {  
            var model = new MaterialViewModel()
            {
                IsAdded = isAdded
            };
            ViewBag.ColorsToPickFrom = new Service.Color().GetSelectListColors();
            return View(model);
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
                    if(TempData.Peek("hatmodel") == null)
                    {
                        return RedirectToAction("AddMaterial", "Material", new { IsAdded = true });
                    }
                    else
                    {
                        return RedirectToAction("Hatmodel", "Hatmodel", new { isAdded = false });
                    }
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