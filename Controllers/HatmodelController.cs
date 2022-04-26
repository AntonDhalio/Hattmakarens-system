using Hattmakarens_system.Models;
using Hattmakarens_system.Repositories;
using Hattmakarens_system.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace Hattmakarens_system.Controllers
{
    public class HatmodelController : Controller
    {
        HatmodelRepository hatModelRepository = new HatmodelRepository();
        MaterialRepository MaterialRepository = new MaterialRepository();
        ColorRepository colorRepository = new ColorRepository();
        static List<ColorMaterialViewModel> TygMaterial = new Service.Material().GetTyg();
        static List<ColorMaterialViewModel> DekorationMaterial = new Service.Material().GetDecoration();
        static List<ColorMaterialViewModel> TrådMaterial = new Service.Material().GetTrad();


        // GET: Hatmodel
        public ActionResult Hatmodel(bool isAdded)
        {
            var newMaterial = (MaterialModels)TempData["material"];
            if(newMaterial != null)
            {
                ColorMaterialViewModel newMat = new ColorMaterialViewModel()
                {
                    Type = newMaterial.Type,
                    Id = newMaterial.ColorId,
                    Name = colorRepository.GetColor(newMaterial.ColorId).Name,                    
                    Description = newMaterial.Description,
                    MaterialId = newMaterial.Id,
                    MaterialName = newMaterial.Name
                };
                if (newMaterial.Type.Equals("Tyg"))
                {
                    TygMaterial.Add(newMat);
                }
                if (newMaterial.Type.Equals("Dekoration"))
                {
                    DekorationMaterial.Add(newMat);
                }
                if (newMaterial.Type.Equals("Tråd"))
                {
                    TrådMaterial.Add(newMat);
                }
            }
            var model = new HatmodelViewModel()
            {
                TygMaterial = TygMaterial,
                DekorationMaterial = DekorationMaterial,
                TrådMaterial = TrådMaterial,
                IsAdded = isAdded
            };
            if (!Request.UrlReferrer.ToString().Contains("Material/AddMaterial") && !Request.UrlReferrer.ToString().Contains("Hatmodel/Hatmodel"))
            {
                TempData["hatmodel"] = null;
                TempData["image"] = null;
            }
            if((HatmodelViewModel)TempData.Peek("hatmodel") != null)
            {
                var tempModel = (HatmodelViewModel)TempData["hatmodel"];
                model.Name = tempModel.Name;
                model.Price = tempModel.Price;
                model.Description = tempModel.Description;
                TempData["hatmodel"] = null;
            }
            
            
            ViewBag.MaterialsToPickFrom = new Service.Material().GetSelectListMaterials();
            return View(model);
        }

        [HttpPost]
        public ActionResult Hatmodel(HatmodelViewModel hatmodel, HttpPostedFileBase[] file, string submitBtn) 
        {
            switch (submitBtn)
            {
                case "Lägg till nytt material":
                    TempData["hatmodel"] = hatmodel;
                    TempData.Keep("hatmodel");
                    return RedirectToAction("AddMaterial", "Material", new { isAdded = false});
                case "Lägg till hattmodell":
                    hatmodel.TygMaterial = TygMaterial;
                    hatmodel.TrådMaterial = TrådMaterial;
                    hatmodel.DekorationMaterial = DekorationMaterial;

                    var valdMaterial = TygMaterial.Union(DekorationMaterial).Union(TrådMaterial).Where(s => s.State.Equals(true)).Select(s => s.MaterialId).ToList();

                    if (valdMaterial.Count != 0)
                    {
                        if (hatModelRepository.ExistingHatModelName(hatmodel.Name) == false)
                        {
                            if (ModelState.IsValid)
                            {
                                var newHatmodel = new HatModels
                                {
                                    Name = hatmodel.Name,
                                    Description = hatmodel.Description,
                                    Price = hatmodel.Price,
                                    Material = new List<MaterialModels>(),
                                    Images = new List<ImageModels>()
                                };
                                if (file[0] != null)
                                {
                                    var path = Server.MapPath(@"~\NewFolder1");
                                    var images = new Service.Image().AddImages(file, path);
                                    foreach(var image in images)
                                    {
                                        newHatmodel.Images.Add(image);
                                    }
                                }
                                using (var context = new ApplicationDbContext())
                                {
                                    foreach (var Id in valdMaterial)
                                    {
                                        newHatmodel.Material.Add(MaterialRepository.GetMaterial(Id));
                                    }
                                    context.HatModels.Attach(newHatmodel);
                                    context.Entry(newHatmodel).State = EntityState.Added;
                                    context.SaveChanges();
                                    TygMaterial = new Service.Material().ResetTygList(TygMaterial);
                                    DekorationMaterial = new Service.Material().ResetDecorationList(DekorationMaterial);
                                    TrådMaterial = new Service.Material().ResetTradList(TrådMaterial);
                                }
                                return RedirectToAction("Hatmodel", "Hatmodel", new { IsAdded = true });
                            }
                            else
                            {
                                ViewBag.MaterialsToPickFrom = new Service.Material().GetSelectListMaterials();
                                return View(hatmodel);
                            }
                        }
                        else
                        {
                            ViewBag.Message = "Det finns redan en hatmodell med detta namn";
                            return View(hatmodel);
                        }
                    }
                    else
                    {
                        TempData["materialError"] = "Du måste välja minst 1 material";
                        ViewBag.MaterialsToPickFrom = new Service.Material().GetSelectListMaterials();
                        return View(hatmodel);
                    }
                default:
                    return View();
            }
        }

        public ActionResult SearchHatModel(int orderId, string customerEmail)
        {
            var hatModels = hatModelRepository.GetAllHatmodels();
            List<HatmodelViewModel> hatmodelViewModels = new List<HatmodelViewModel>();
            foreach(var hatModel in hatModels)
            {
                HatmodelViewModel newHatmodelViewModel = new HatmodelViewModel
                {
                    Id = hatModel.Id,
                    Name = hatModel.Name,
                    Description = hatModel.Description,
                    Price = hatModel.Price,
                    OrderId = orderId,
                    CustomerEmail = customerEmail,
                    Images = hatModel.Images
                };
                hatmodelViewModels.Add(newHatmodelViewModel);
            }
            return View(hatmodelViewModels);
        }

        public ActionResult PickMaterialModel(int Id)
        {
            foreach (var item in TygMaterial)
            {
                if (item.MaterialId.Equals(Id))
                {
                    item.State = !item.State;
                }
            }
            foreach (var item in DekorationMaterial)
            {
                if (item.MaterialId.Equals(Id))
                {
                    item.State = !item.State;
                }
            }
            foreach (var item in TrådMaterial)
            {
                if (item.MaterialId.Equals(Id))
                {
                    item.State = !item.State;
                }
            }

            return RedirectToAction("Hatmodel", new { IsAdded = false });
        }
    }
}