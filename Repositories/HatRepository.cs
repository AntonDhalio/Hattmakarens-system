using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Hattmakarens_system.Models;
using Hattmakarens_system.ViewModels;

namespace Hattmakarens_system.Repositories
{
    public class HatRepository
    {
        public Hats GetHat(int id)
        {
            using (var hatCon = new ApplicationDbContext())
            {
                return hatCon.Hats.FirstOrDefault(h => h.Id == id);
            }
        }

        public HatViewModel GetHatViewModel(int id)
        {
            using (var hatCon = new ApplicationDbContext())
            {
                Hats hat = hatCon.Hats.FirstOrDefault(h => h.Id == id);
                HatViewModel model = new HatViewModel()
                {
                    Name = hat.Name,
                    Id = hat.Id
                };
                return model;
            }
        }

        public List<Hats> GetAllHats()
        {
            using (var hatCon = new ApplicationDbContext())
            {
                return hatCon.Hats.ToList();
            }
        }
        //public Hats SaveHats(Hats hat)
        //{
        //    using (var hatCon = new ApplicationDbContext())
        //    {
        //        if (hat.Id != 0)
        //        {
        //            hatCon.Entry(hat).State = EntityState.Modified;
        //        }
        //        else
        //        {
        //            hatCon.Hats.Add(hat);
        //        }
        //        hatCon.SaveChanges();
        //        return hat;
        //    }
        //}

        public Hats CreateHat(HatViewModel hat)
        {
            using (var hatCon = new ApplicationDbContext())
            {
                Hats hats = new Hats()
                {
                    Name = hat.Name,
                    Size = hat.Size,
                    Price = hat.Price,
                    Status = "Under behandling", //Eller vad det nu ska stå när man bara registrerat en ny hatt
                    Comment = hat.Comment,
                    ModelID = hat.ModelID,
                    OrderId = hat.OrderId
                };
                hatCon.Hats.Add(hats);
                hatCon.SaveChanges();
                return hats;
            }
        }
        public void DeleteHat(int id)
        {
            using (var hatCon = new ApplicationDbContext())
            {
                var hat = hatCon.Hats.FirstOrDefault(h => h.Id == id);
                if (hat != null)
                {
                    hatCon.Hats.Remove(hat);
                    hatCon.SaveChanges();
                }
            }
        }

        public List<Hats> GetAllHatsByOrderId(int id)
        {
            using (var hatCon = new ApplicationDbContext())
            {
                //if(hatCon.Hats.Where(h => h.OrderId == id) == null)
                //{
                //    List<Hats> hats = new List<Hats>();
                //    return hats;
                //}
                return hatCon.Hats.Where(h => h.OrderId == id).ToList();
            }
        }
    }
}