﻿using Hattmakarens_system.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hattmakarens_system.Controllers
{
    public class SearchController : Controller
    {
        // GET: Search
        public ActionResult OrderSearch()
        {
            return View();
        }

        // GET: Search
        public ActionResult CustomerSearch()
        {
            return View();
        }
        // GET: Search/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Search/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Search/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Search/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Search/Edit/5
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

        // GET: Search/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Search/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult CustomerSearch(string searchString)
        {
            var customerList = new Repositories.CustomerRepository().GetAllCostumers();
            var searchList = new List<CustomerModels>();
            foreach (var customer in customerList)
            {
                if (customer.Name.Contains(searchString))
                {
                    searchList.Add(customer);
                }
            }
            foreach (var customer in customerList)
            {
                if (customer.Email.Contains(searchString))
                {
                    foreach(var item in searchList)
                    {
                        if(item.Id != customer.Id)
                        {
                            searchList.Add(customer);
                        } 
                    }
                }
            }
            ViewBag.ViewBagList = searchList;
            return View();
        }
        [HttpPost]
        public ActionResult OrderSearch(string searchString, string searchOption, string statusOption)
        {
            var orderList = new Repositories.OrderRepository().GetAllOrders();
            var searchList = new List<OrderModels>();

            if(searchOption is null)
            {
                searchList = orderList;
            }
            if (searchOption.Equals("material"))
            {
                //hämta alla ordrar på angivet material
                //addera alla ordrar till söklistan
            }
            if (searchOption.Equals("model"))
            {
                //hämta alla ordrar på angivet modell
                //addera alla ordrar till söklistan
            }
            if (searchOption.Equals("customer"))
            {
                //hämta alla ordrar på angivet kundnamn
                //addera alla ordrar till söklistan
            }
            if (statusOption.Equals("active"))
            {
                //söklistan ska filtreras på aktiva ordrar
            }
            if(statusOption.Equals("inactive"))
            {
                //Söklistan ska filtreras på inaktiva ordrar
            }

            ViewBag.ViewBagList = searchList;
            return View();
        }
    }
}