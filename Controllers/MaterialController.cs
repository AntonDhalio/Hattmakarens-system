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
            ViewBag.ColorsToPickFrom = GetSelectListColors();
            return View();
        }
        [HttpPost]
        public ActionResult AddMaterial(MaterialViewModel materialViewModel, FormCollection forms)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var matRepo = new MaterialRepository();
                    var material = new MaterialModels
                    {
                        Name = materialViewModel.Name,
                        Description = materialViewModel.Description,
                        Type = Request.Form["Type"].ToString(),
                        ColorId = int.Parse(Request.Form["ColorId"])
                    };
                    matRepo.SaveMaterial(material);
                    return RedirectToAction("AddMaterial", "Material");
                }
                else
                {
                    ViewBag.ColorsToPickFrom = GetSelectListColors();
                    return View(materialViewModel);
                }
            }
            catch
            {
                return View("Error");
            }
        }
        public List<SelectListItem> GetSelectListColors()
        {
            var colorRepo = new ColorRepository();
            List<SelectListItem> colors = new List<SelectListItem>();

            foreach (var color in colorRepo.GetAllColors())
            {
                var listitem = new SelectListItem
                {
                    Value = color.Id.ToString(),
                    Text = color.Name
                };
                colors.Add(listitem);
            }
            return colors;
        }
    }
}