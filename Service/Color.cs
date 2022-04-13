using Hattmakarens_system.Models;
using Hattmakarens_system.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
    }
}