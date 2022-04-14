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

        public Hats CreateHat(HatViewModel hat, IEnumerable<string> PickedMaterials, int[] SelectedStatuses)
        {
            using (var hatCon = new ApplicationDbContext())
            {
                
                Hats hats = new Hats()
                {
                    Name = hat.Name,
                    Size = hat.Size,
                    Price = hat.Price,
                    Status = "Aktiv", 
                    Comment = hat.Comment,
                    UserId = hat.UserId,
                    ModelID = hat.HatModelID,
                    OrderId = hat.OrderId,
                    Materials = new List<MaterialModels>()
                };
                if(hat.HatModelID == 1)
                {
                    foreach (var material in PickedMaterials)
                    {
                        var id = int.Parse(material);
                        var aMaterial = hatCon.Material.ToList().FirstOrDefault(h => h.Id == id);
                        hats.Materials.Add(aMaterial);
                    }
                } else
                {
                    foreach (var material in SelectedStatuses)
                    {
                        var id = material;
                        var aMaterial = hatCon.Material.ToList().FirstOrDefault(h => h.Id == id);
                        hats.Materials.Add(aMaterial);
                    }
                }
            
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

        public List<Hats> GetAllHatsByOrderId(int? id)
        {
            using (var hatCon = new ApplicationDbContext())
            {
                return hatCon.Hats.Where(h => h.OrderId == id).ToList();
            }
        }

        //public void UpdateHat(HatViewModel hat, int[] SelectedStatuses)
        //{
        //    using (var hatCon = new ApplicationDbContext())
        //    {
        //        hatCon.Hats.AsNoTracking();
        //        Hats existingHat = GetHat(hat.Id);
        //        Hats newHat = new Hats()
        //        {
        //            Id = hat.Id,
        //            Name = hat.Name,
        //            Size = hat.Size,
        //            Comment = hat.Comment,
        //            Status = hat.Status,
        //            Price = hat.Price,
        //            UserId = hat.UserId
        //        };


        //        existingHat.Id = hat.Id;
        //        existingHat.Name = hat.Name;
        //        existingHat.Size = hat.Size;
        //        existingHat.Comment = hat.Comment;
        //        existingHat.Status = hat.Status;
        //        existingHat.Price = hat.Price;
        //        existingHat.UserId = hat.UserId;


        //        existingHat.Materials = new List<MaterialModels>();

        //        List<MaterialModels> materials = new List<MaterialModels>();
        //        foreach (var materialId in SelectedStatuses)
        //        {
        //            var id = material;
        //            var aMaterial = hatCon.Material.Include(m => m.Hats).FirstOrDefault(m => m.Id == materialId);
        //            materials.Add(aMaterial);
        //            existingHat.Materials.Add(aMaterial);
        //        }
        //        existingHat.Materials = materials;
        //        newHat.Materials = materials;
        //        hatCon.Hats.Attach(newHat);
        //        var entry = hatCon.Entry(newHat);
        //        entry.State = EntityState.Modified;
        //        entry.Property("OrderId").IsModified = false;


        //        hatCon.Hats.Attach(existingHat);
        //        hatCon.Entry(existingHat).State = EntityState.Modified;
        //        hatCon.SaveChanges();

        //    }
        //}
    }
}