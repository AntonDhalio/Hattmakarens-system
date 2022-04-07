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
        public HatModels GetHatmodel(int id)
        {
            using (var hatCon = new ApplicationDbContext())
            {
                return hatCon.HatModels.Include(h => h.Material).FirstOrDefault(h => h.Id == id);
            }
        }
        public List<HatModels> GetAllHatmodels()
        {
            using (var hatCon = new ApplicationDbContext())
            {
                return hatCon.HatModels.Include(h => h.Material).Include(h => h.Hats).ToList();
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
    }
}