using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hattmakarens_system.Models;

namespace Hattmakarens_system.Repositories
{
    public class ColorRepository
    {
        public ColorModels GetColor(int id)
        {
            using (var hatCon = new ApplicationDbContext())
            {
                return hatCon.Color.Include(c => c.Material).FirstOrDefault(c => c.Id == id);
            }
        }

        public List<ColorModels> GetAllColors()
        {
            using (var hatCon = new ApplicationDbContext())
            {
                hatCon.Configuration.LazyLoadingEnabled = false;
                return hatCon.Color.ToList();
            }
        }
        public ColorModels SaveColor (ColorModels color)
        {
            using (var hatCon = new ApplicationDbContext())
            {
                if (color.Id != 0)
                {
                    hatCon.Entry(color).State = EntityState.Modified;
                }
                else
                {
                    hatCon.Color.Add(color);
                }
                hatCon.SaveChanges();
                return color;
            }
        }
    }
}
