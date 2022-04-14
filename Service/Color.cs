using Hattmakarens_system.Models;
using Hattmakarens_system.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hattmakarens_system.Service
{
    public class Color
    {
        public void AddColor(string colorName)
        {
            var colorReop = new ColorRepository();
            var color = new ColorModels
            {
                Name = colorName
            };
            colorReop.SaveColor(color);
        }

        public bool IsColorSaved(string colorName)
        {
            var colorList = new ColorRepository().GetAllColors();

            foreach (var color in colorList)
            {
                if (colorName.Equals(color.Name))
                {
                    return true;
                }
            }         
            return false;
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