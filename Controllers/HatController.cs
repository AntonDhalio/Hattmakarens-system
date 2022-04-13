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
            return View(model);
        }

        // POST: Hat/Create
        [HttpPost]
        public ActionResult CreateSpec(HatViewModel model, IEnumerable<string> PickedMaterials)
        {
            try
            {
                model.HatModelID = 1; //Hårdkodat värde för att representera specialltillverkad hatt
                           //ÄNDRA TILL TILLVERKARE
                model.UserId = User.Identity.GetUserId();
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
                //var vm = new MaterialListToHatmodelViewModel();

                model.Statuses = new List<SelectListItem>();
                foreach (var material in materialRepository.GetAllMaterials())
                {
                    var listitem = new SelectListItem
                    {
                        Value = material.Id.ToString(),
                        Text = material.Name + ", " + material.Color.Name + ", " + material.Type
                    };
                    model.Statuses.Add(listitem);
                        //Add(listitem);
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
            return View(model);
        }

        // POST: Hat/Create
        [HttpPost]
        public ActionResult CreateStored(HatViewModel model, IEnumerable<string> pickedMaterials, int[] SelectedStatuses)
        {
            try
            {
                //model.ModelID = 2; //Hårdkodat värde för att representera icke-specialltillverkad hatt
                //hatRepository.CreateHat(model);

                //ÄNDRA TILL TILLVERKARE
                model.UserId = User.Identity.GetUserId();
                hatRepository.CreateHat(model, pickedMaterials, SelectedStatuses);
                //orderRepository.OrderAddHat(model);
                //OrderViewModel orderModel = new OrderViewModel();
                //orderModel.Id = model.OrderId;
                //OrderRepository.AddSpecHat(model);
                return RedirectToAction("CreateOrder", "Order", new { currentOrderId = model.OrderId, customerEmail = model.CustomerEmail });
            }
            catch
            {
                return View();
            }
        }

        // GET: Hat/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Hat/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
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
                //var customer = customerRepository.GetCustomerByOrderId(orderId);
                //model.CustomerEmail = customer.Email;
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
