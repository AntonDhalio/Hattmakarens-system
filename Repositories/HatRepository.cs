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
                return hatCon.Hats.Include(h => h.Order).ToList();
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

        //public Hats CreateHat(HatViewModel hat, IEnumerable<string> PickedMaterials, int[] SelectedStatuses)
        //{
        //    using (var hatCon = new ApplicationDbContext())
        //    {
                
        //        Hats hats = new Hats()
        //        {
        //            Name = hat.Name,
        //            Size = hat.Size,
        //            Price = hat.Price,
        //            Comment = hat.Comment,
        //            UserId = hat.UserId,
        //            ModelID = hat.HatModelID,
        //            OrderId = hat.OrderId,
        //            Materials = new List<MaterialModels>()
        //        };
        //        if(hat.HatModelID == 1)
        //        {
        //            foreach (var material in PickedMaterials)
        //            {
        //                var id = int.Parse(material);
        //                var aMaterial = hatCon.Material.ToList().FirstOrDefault(h => h.Id == id);
        //                hats.Materials.Add(aMaterial);
        //            }
        //        } else
        //        {
        //            foreach (var material in SelectedStatuses)
        //            {
        //                var id = material;
        //                var aMaterial = hatCon.Material.ToList().FirstOrDefault(h => h.Id == id);
        //                hats.Materials.Add(aMaterial);
        //            }
        //        }
            
        //        hatCon.Hats.Add(hats);
        //        hatCon.SaveChanges();
        //        return hats;
        //    }
        //}
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

        public List<Hats> GetAllHatsByOrderId(int? id)
        {
            using (var hatCon = new ApplicationDbContext())
            {
                return hatCon.Hats.Where(h => h.OrderId == id).ToList();
            }
        }

        public void CreateHat(HatViewModel model, int orderId)
        {
            using (var hatCon = new ApplicationDbContext())
            {
                Hats hat = new Hats()
                {
                    Name = model.Name,
                    Size = model.Size,
                    Price = model.Price,
                    Status = "Aktiv",
                    Comment = model.Comment,
                    UserId = model.UserId,
                    ModelID = model.HatModelID,
                    OrderId = orderId,
                    Materials = model.Materials
                };
                hatCon.Hats.Add(hat);
                hatCon.SaveChanges();
            }
        }
    }
}