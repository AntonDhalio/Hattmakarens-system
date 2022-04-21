﻿using Hattmakarens_system.Models;
using Hattmakarens_system.Repositories;
using Hattmakarens_system.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace Hattmakarens_system.Controllers
{
    public class HatController : Controller
    {
        HatRepository hatRepository = new HatRepository();
        OrderRepository orderRepository = new OrderRepository();
        CustomerRepository customerRepository = new CustomerRepository();
        HatmodelRepository hatModelRepository = new HatmodelRepository();
        MaterialRepository materialRepository = new MaterialRepository();
        UserRepository userRepository = new UserRepository();
        static List<ColorMaterialViewModel> TygMaterial = new Service.Material().GetTyg();
        static List<ColorMaterialViewModel> DekorationMaterial = new Service.Material().GetDecoration();
        static List<ColorMaterialViewModel> TrådMaterial = new Service.Material().GetTrad();


        // GET: Hat
        public ActionResult Index()
        {
            return View();
        }

        // GET: Hat/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Hat/Create

        public ActionResult CreateSpec(int orderId, string customerEmail)
        {   
            HatViewModel model = new HatViewModel()
            {
                OrderId = orderId,
                CustomerEmail = customerEmail
            };
            model.TygMaterial = TygMaterial;
            model.DekorationMaterial = DekorationMaterial;
            model.TrådMaterial = TrådMaterial;

            ViewBag.UsersToPickFrom = userRepository.UsersToDropDownList();
            return View(model);
        }

        // POST: Hat/Create
        [HttpPost]
        public ActionResult CreateSpec(HatViewModel model, IEnumerable<string> PickedMaterials)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if(PickedMaterials != null)
                    {
                        var SelectedStatuses = new int[100];
                        OrderModel order = (OrderModel)TempData.Peek("order");
                        HatViewModel hat = new HatViewModel()
                        {
                            HatModelID = 1,
                            Name = model.Name,
                            Size = model.Size,
                            Price = model.Price,
                            Status = "Aktiv",
                            Comment = model.Comment,
                            UserId = model.UserId,
                            UserName = userRepository.GetUser(model.UserId).Name,
                            Materials = new List<MaterialModels>()

                        };
                        var valdMaterial = TygMaterial.Union(DekorationMaterial).Union(TrådMaterial).Where(s => s.State.Equals(true)).Select(s => s.MaterialId).ToList();
                        hat.Materials = materialRepository.GetMaterialById(valdMaterial);
                        TempData["hat"] = hat;
                        TempData.Keep("hat");
                        model.HatModelID = 1; //Hårdkodat värde för att representera specialltillverkad hatt

                        hatRepository.CreateHat(model, valdMaterial);

                        TygMaterial = new Service.Material().ResetTygList(TygMaterial);
                        DekorationMaterial = new Service.Material().ResetDecorationList(DekorationMaterial);
                        TrådMaterial = new Service.Material().ResetTradList(TrådMaterial);

                        return RedirectToAction("CreateOrder", "Order", new { currentOrderId = model.OrderId, customerEmail = model.CustomerEmail });
                    }
                    else
                    {
                        TempData["message"] = "Fältet Material saknas";
                        ViewBag.MaterialsToPickFrom = new Service.Material().GetSelectListMaterials();
                        ViewBag.UsersToPickFrom = userRepository.UsersToDropDownList();
                        return View(model);
                    }
                }
                else
                {
                    ViewBag.MaterialsToPickFrom = new Service.Material().GetSelectListMaterials();
                    ViewBag.UsersToPickFrom = userRepository.UsersToDropDownList();
                    return View(model);
                }
                
            }
            catch
            {
                return View();
            }
        }

        // GET: Hat/Create
        public ActionResult CreateStored(int orderId, string customerEmail, string hatModelName)
        {

            HatViewModel model = new HatViewModel()
            {
                OrderId = orderId,
                CustomerEmail = customerEmail,
                HatModelName = hatModelName
            };
            if (hatModelName != null)
            {
                model.HatModelName = hatModelName;
                model = new Service.Material().SetMaterials(model);
                var SelectedMaterialsId = materialRepository.GetMaterialInHatmodel(hatModelName);
                model.TygMaterial = TygMaterial;
                model.DekorationMaterial = DekorationMaterial;
                model.TrådMaterial = TrådMaterial;

                foreach (var id in SelectedMaterialsId)                   
                {
                    foreach (var material in model.TygMaterial)
                    {
                        if(material.MaterialId == id)
                        {
                            material.State = true;
                        }
                    }
                    foreach (var material in model.DekorationMaterial)
                    {
                        if (material.MaterialId == id)
                        {
                            material.State = true;
                        }
                    }
                    foreach (var material in model.TrådMaterial)
                    {
                        if (material.MaterialId == id)
                        {
                            material.State = true;
                        }
                    }
                }
                
                var hatModel = hatModelRepository.GetHatmodelByName(hatModelName);
                model.Price = hatModel.Price;
                model.HatModelName = hatModel.Name;
                model.HatModelID = hatModel.Id;
                model.HatModelDescription = hatModel.Description;
                TempData["hat"] = model;
                TempData.Keep("hat");
            }
            ViewBag.UsersToPickFrom = userRepository.UsersToDropDownList();
            return View(model);
        }

        // POST: Hat/Create
        [HttpPost]
        public ActionResult CreateStored(HatViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    OrderModel order = (OrderModel)TempData.Peek("order");
                    HatViewModel hat = new HatViewModel()
                    {
                        HatModelID = model.HatModelID,
                        Name = model.Name,
                        Size = model.Size,
                        Price = model.Price,
                        Status = "Aktiv",
                        Comment = model.Comment,
                        UserId = model.UserId,
                        UserName = userRepository.GetUser(model.UserId).Name,
                        Materials = new List<MaterialModels>()

                    };
                    var hatModel = hatModelRepository.GetHatmodel(model.HatModelID);
                    hat.HatModelName = hatModel.Name;
                    hat.HatModelDescription = hatModel.Description;
                    TempData["hat"] = hat;
                    TempData.Keep("hat");
                    var valdMaterial = TygMaterial.Union(DekorationMaterial).Union(TrådMaterial).Where(s => s.State.Equals(true)).Select(s => s.MaterialId).ToList();

                    hatRepository.CreateHat(model, valdMaterial);

                    TygMaterial = new Service.Material().ResetTygList(TygMaterial);
                    DekorationMaterial = new Service.Material().ResetDecorationList(DekorationMaterial);
                    TrådMaterial = new Service.Material().ResetTradList(TrådMaterial);

                    return RedirectToAction("CreateOrder", "Order", new { currentOrderId = model.OrderId, customerEmail = model.CustomerEmail });
                }
                else
                {
                    model = new Service.Material().SetMaterials(model);
                    ViewBag.UsersToPickFrom = userRepository.UsersToDropDownList();
                    return View(model);
                }
            }
            catch
            {
                return View();
            }
        }
        public ActionResult PickMaterial(int orderId, string customerEmail, string hatModelName, int Id)
        {
            foreach(var item in TygMaterial)
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
            
            return RedirectToAction("CreateStored", new{ orderId, customerEmail, hatModelName});
        }

        public ActionResult PickMaterialSpec(int orderId, string customerEmail, int Id)
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

            return RedirectToAction("CreateSpec", new { orderId, customerEmail});
        }

        // GET: Hat/Edit/5
        public ActionResult Edit(int hatId)
        {
            Hats hat = hatRepository.GetHat(hatId);
            HatViewModel model = new HatViewModel()
            {
                Id = hat.Id,
                Name = hat.Name,
                Comment = hat.Comment,
                Price = hat.Price,
                Size = hat.Size,
                HatModelID = hat.ModelID,
                Status = hat.Status,
                UserId = hat.UserId
            };
            TempData["orderId"] = hat.OrderId;
            TempData.Keep("orderId");
            model.HatModelName = hatModelRepository.GetHatmodel(hat.ModelID).Name;
            model.HatModelDescription = hatModelRepository.GetHatmodel(hat.ModelID).Description;

            model.Statuses = new List<SelectListItem>();
            foreach (var material in materialRepository.GetAllMaterials())
            {
                var listItem = new SelectListItem()
                {
                    Value = material.Id.ToString(),
                    Text = material.Name + ", " + material.Color.Name + ", " + material.Type
                };
                model.Statuses.Add(listItem);
            }
            var SelectedMaterialsId = new List<int>();
            foreach (var materialId in materialRepository.GetMaterialInHat(hatId))
            {
                SelectedMaterialsId.Add(materialId);
            }
            model.SelectedStatuses = new int[100];

            int count = 0;
            foreach (var id in SelectedMaterialsId)
            {
                model.SelectedStatuses[count] = id;
                count++;
            }
            ViewBag.UsersToPickFrom = userRepository.UsersToDropDownList();
            ViewBag.StatusesToPickFrom = hatRepository.StatusesToDropDownList();
            return View(model);
        }

        // POST: Hat/Edit/5
        [HttpPost]
        public ActionResult Edit(HatViewModel model, int[] SelectedStatuses)
        {
            try
            {
                hatRepository.UpdateHat(model, SelectedStatuses);
                orderRepository.UpdateOrderPrice((int)TempData.Peek("orderId"));
                return RedirectToAction("ModifyOrder", "Order", new { Id = (int)TempData.Peek("orderId")});
            }
            catch
            {
                return View();
            }
        }

        // GET: Hat/Delete/5
        public ActionResult DeleteInRegOrder(int id)
        {
            try
            {
                HatViewModel hat = new HatViewModel();
                foreach(HatViewModel item in (List<HatViewModel>)TempData.Peek("listOfHats"))
                {
                    if(item.Id == id)
                    {
                        hat = item;
                    }
                }
                return View(hat);
            }
            catch
            {
                return View();
            }
        }

        // POST: Hat/Delete/5
        [HttpPost]
        public ActionResult DeleteInRegOrder(HatViewModel model)
        {
            try
            {
                List<HatViewModel> listOfHats = new List<HatViewModel>();
                foreach(HatViewModel hat in (List<HatViewModel>)TempData.Peek("listOfHats"))
                {
                    if(hat.Id != model.Id)
                    {
                        listOfHats.Add(hat);
                    }
                }
                TempData["listOfHats"] = listOfHats;
                TempData.Keep("listOfHats");
                OrderModel currentOrder = (OrderModel)TempData.Peek("order");
                return RedirectToAction("CreateOrder", "Order", new { customerEmail = currentOrder.CustomerEmail });
            }
            catch
            {
                return View();
            }
        }
        public ActionResult ActiveHats()
        {
            //Lägger till specialtillverkad hattmodell första gången man går in på denna view
            HatmodelRepository hatmodelRepository = new HatmodelRepository();
            var hatmodels = hatmodelRepository.GetAllHatmodels();
            bool specExist = false;
            foreach (var hatmodel in hatmodels)
            {
                if (hatmodel.Name.Equals("Specialtillverkad"))
                {
                    specExist = true;
                }
            }
            if (specExist == false)
            {
                hatmodelRepository.CreateSpecHatModel();
            }
            
            var repo = new HatRepository();
            var allHats = new List<Hats>();
            using (var context = new ApplicationDbContext())
            {
                allHats = context.Hats.Include(h => h.Order).Include(h => h.Models).ToList();
            }
            var hatstoShow = allHats.Where(h => h.UserId.Equals(User.Identity.GetUserId())).Where(h => h.Status == "Aktiv");
            var viewModel = new ActiveHatsViewModel
            {
                hats = hatstoShow.ToList(),
                Orders = new List<OrderModels>()
            };

            var repos = new OrderRepository();
            var allOrders = repos.GetAllOrders();
            foreach (var order in allOrders)
            {
                if (order.Status.Equals("Aktiv"))
                {
                    viewModel.Orders.Add(order);
                }
            }
            return View(viewModel);
        }

        //GET
        public ActionResult ViewHat(int hatId)
        {
            var hat = hatRepository.GetHatViewModel(hatId);
            return View(hat);
        }

    }
}
