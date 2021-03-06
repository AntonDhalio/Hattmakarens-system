using Hattmakarens_system.Models;
using Hattmakarens_system.Repositories;
using Hattmakarens_system.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hattmakarens_system.Controllers
{
    public class ColorController : Controller
    {
        // GET: Color
        public ActionResult AddColor()
        {
            return View();
        }
        public int AddColor(string colorName)
        {
            var colorReop = new ColorRepository();
            var color = new ColorModels
            {
                Name = colorName
            };
            colorReop.SaveColor(color);

            return color.Id;
        }
        [HttpPost]
        public ActionResult AddColor(ColorViewModel colorViewModel)
        {
            try
            {
                var colorReop = new ColorRepository();
                var color = new ColorModels
                {
                    Name = colorViewModel.Name
                    
                };
                colorReop.SaveColor(color);
                ModelState.Clear();
                return View();
            }
            catch
            {
                return View("Error");              
            }

        }

        

    }
}