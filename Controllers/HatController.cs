using Hattmakarens_system.Models;
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

            var materials = new List<SelectListItem>();
            foreach(var material in materialRepository.GetAllMaterials())
            {
                var listitem = new SelectListItem
                {
                    Value = material.Id.ToString(),
                    Text = material.Name + ", " + material.Color.Name + ", " + material.Type
                };
                materials.Add(listitem);
            }
            HatViewModel model = new HatViewModel()
            {
                OrderId = orderId,
                CustomerEmail = customerEmail
            };
            ViewBag.MaterialsToPickFrom = materials;
            ViewBag.UsersToPickFrom = userRepository.UsersToDropDownList();
            return View(model);
        }

        // POST: Hat/Create
        [HttpPost]
        public ActionResult CreateSpec(HatViewModel model, IEnumerable<string> PickedMaterials)
        {
            try
            {
                model.HatModelID = 1; //Hårdkodat värde för att representera specialltillverkad hatt
                hatRepository.CreateHat(model, PickedMaterials, null);
                return RedirectToAction("CreateOrder", "Order", new {currentOrderId = model.OrderId, customerEmail = model.CustomerEmail});
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
                CustomerEmail = customerEmail
            };
            if(hatModelName != null)
            {
                model.Statuses = new List<SelectListItem>();
                foreach (var material in materialRepository.GetAllMaterials())
                {
                    var listitem = new SelectListItem
                    {
                        Value = material.Id.ToString(),
                        Text = material.Name + ", " + material.Color.Name + ", " + material.Type
                    };
                    model.Statuses.Add(listitem);
                }

                var SelectedMaterialsId = materialRepository.GetMaterialInHatmodel(hatModelName);
                model.SelectedStatuses = new int[100];

                int count = 0;
                foreach(var id in SelectedMaterialsId)
                {
                    model.SelectedStatuses[count] = id;
                    count++;
                }

                var hatModel = hatModelRepository.GetHatmodelByName(hatModelName);
                model.Price = hatModel.Price;
                //model.Path = hatModel.Path -- lägga till path-property på hatmodel i databas?
                //model.Materials = hatModel.Material;
                //model.HatModelName = 
                model.HatModelName = hatModel.Name;
                model.HatModelID = hatModel.Id;
                model.HatModelDescription = hatModel.Description;
            }
            ViewBag.UsersToPickFrom = userRepository.UsersToDropDownList();
            return View(model);
        }

        // POST: Hat/Create
        [HttpPost]
        public ActionResult CreateStored(HatViewModel model, IEnumerable<string> pickedMaterials, int[] SelectedStatuses)
        {
            try
            {
                hatRepository.CreateHat(model, pickedMaterials, SelectedStatuses);
     
                return RedirectToAction("CreateOrder", "Order", new { currentOrderId = model.OrderId, customerEmail = model.CustomerEmail });
            }
            catch
            {
                return View();
            }
        }

        // GET: Hat/Edit/5
        public ActionResult Edit(int hatId)
        {
            Hats hat = hatRepository.GetHat(hatId);
            /*List<int> materialIds = materialRepository.GetMaterialInHat(hatId);*/
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
            //return View();
        }

        // POST: Hat/Edit/5
        [HttpPost]
        public ActionResult Edit(HatViewModel model, int[] SelectedStatuses)
        {
            try
            {
                hatRepository.UpdateHat(model, SelectedStatuses);

            return RedirectToAction("ViewOrder", "Order", new { Id = (int)TempData.Peek("orderId")});
            }
            catch
            {
                return View();
            }
        }

        // GET: Hat/Delete/5
        public ActionResult Delete(int id, int orderId)
        {
            try
            {
                HatViewModel model = hatRepository.GetHatViewModel(id);
                model.OrderId = orderId;
                return View(model);
            }
            catch
            {
                return View();
            }
        }

        // POST: Hat/Delete/5
        [HttpPost]
        public ActionResult Delete(HatViewModel model)
        {
            try
            {
                hatRepository.DeleteHat(model.Id);
                var customer = customerRepository.GetCustomerByOrderId(model.OrderId);
                return RedirectToAction("CreateOrder", "Order", new { currentOrderId = model.OrderId, customerEmail = customer.Email});
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
                allHats = context.Hats.Include(h => h.Order).ToList();
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

    }
}
