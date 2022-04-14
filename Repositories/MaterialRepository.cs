using Hattmakarens_system.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

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
                return hatCon.Material.Include(m => m.HatModels).Include(m => m.Color).ToList();
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

        public List<int> GetMaterialInHatmodel(string hatModelName)
        {
            using (var hatCon = new ApplicationDbContext())
            {
                List<int> materialIds = new List<int>();
                var aHatmodel = hatCon.HatModels.FirstOrDefault(h => h.Name.Equals(hatModelName));
                foreach(var materials in aHatmodel.Material)
                { 
                    materialIds.Add(materials.Id);
                }
                return materialIds;
            }  
        }
    }
}