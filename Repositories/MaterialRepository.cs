using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Hattmakarens_system.Models;

namespace Hattmakarens_system.Repositories
{
    public class MaterialRepository
    {
        public MaterialModels GetMaterial(int id)
        {
            using (var hatCon = new ApplicationDbContext())
            {
                return hatCon.Material.Include(m => m.HatModels).FirstOrDefault(m => m.Id == id);
            }
        }
        public List<MaterialModels> GetAllMaterials()
        {
            using (var hatCon = new ApplicationDbContext())
            {
                return hatCon.Material.Include(m => m.HatModels).ToList();
            }
        }
        public MaterialModels SaveMaterial(MaterialModels material)
        {
            using (var hatCon = new ApplicationDbContext())
            {
                if (material.Id != 0)
                {
                    hatCon.Entry(material).State = EntityState.Modified;
                }
                else
                {
                    hatCon.Material.Add(material);
                }
                hatCon.SaveChanges();
                return material;
            }
        }
        public void DeleteMaterial(int id)
        {
            using (var hatCon = new ApplicationDbContext())
            {
                var material = hatCon.Material.FirstOrDefault(m => m.Id == id);
                if (material != null)
                {
                    hatCon.Material.Remove(material);
                    hatCon.SaveChanges();
                }
            }
        }
    }
}