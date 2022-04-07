using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hattmakarens_system.Controllers
{
    public class HatController : Controller
    {
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
        public ActionResult CreateSpec()
        {
            return View();
        }

        // POST: Hat/Create
        [HttpPost]
        public ActionResult CreateSpec(FormCollection collection)
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
        // GET: Hat/Create
        public ActionResult CreateStored()
        {
            return View();
        }

        // POST: Hat/Create
        [HttpPost]
        public ActionResult CreateStored(FormCollection collection)
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
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Hat/Delete/5
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
    }
}
