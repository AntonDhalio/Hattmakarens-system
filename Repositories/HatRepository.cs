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
                    Status = hat.Status,
                    Comment = hat.Comment,
                    ModelID = hat.ModelID
                    

                };
                hatCon.Hats.Add(hats);
                //hatCon.SaveChanges();
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
    }
}