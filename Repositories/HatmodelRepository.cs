using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Hattmakarens_system.Models;

namespace Hattmakarens_system.Repositories
{
    public class HatmodelRepository
    {
        public HatModels GetHatmodel(int? id)
        {
            using (var hatCon = new ApplicationDbContext())
            {
                return hatCon.HatModels.Include(h => h.Material).Include(h => h.Images)
                    .FirstOrDefault(h => h.Id == id);
            }
        }
        public List<HatModels> GetAllHatmodels()
        {
            using (var hatCon = new ApplicationDbContext())
            {
                return hatCon.HatModels.Include(h => h.Material).Include(h => h.Hats).Include(h => h.Images).ToList();
            }
        }
        public HatModels SaveHatmodel(HatModels hatmodel)
        {
            using (var hatCon = new ApplicationDbContext())
            {
                if (hatmodel.Id != 0)
                {
                    hatCon.Entry(hatmodel).State = EntityState.Modified;
                }
                else
                {
                    hatCon.HatModels.Add(hatmodel);
                }
                hatCon.SaveChanges();
                return hatmodel;
            }
        }
        public void CreateSpecHatModel()
        {
            using (var hatCon = new ApplicationDbContext())
            {
                HatModels hatmodel = new HatModels()
                {
                    Name = "Specialtillverkad"
                };
                hatCon.HatModels.Add(hatmodel);
                hatCon.SaveChanges();
            }
        }
        public void DeleteHatmodel(int id)
        {
            using (var hatCon = new ApplicationDbContext())
            {
                var hatmodel = hatCon.HatModels.FirstOrDefault(h => h.Id == id);
                if (hatmodel != null)
                {
                    hatCon.HatModels.Remove(hatmodel);
                    hatCon.SaveChanges();
                }
            }
        }

        public HatModels GetHatmodelByName(string hatModelName)
        {
            using (var hatCon = new ApplicationDbContext())
            {
                return hatCon.HatModels.FirstOrDefault(h => h.Name == hatModelName);
            }
        }

        public bool ExistingHatModelName(string hatModelName)
        {
            bool existingHatModelName = false;
            var hatModels = GetAllHatmodels();
            foreach (var hatModel in hatModels)
            {
                if (hatModel.Name.Equals(hatModelName))
                {
                    existingHatModelName = true;
                }
            }
            return existingHatModelName;
        }
    }
}