using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hattmakarens_system.Models;
using Hattmakarens_system.ViewModels;

namespace Hattmakarens_system.Repositories
{
    public class HatRepository
    {
        UserRepository userRepository = new UserRepository();
        HatmodelRepository hatmodelRepository = new HatmodelRepository();
        public Hats GetHat(int id)
        {
            using (var hatCon = new ApplicationDbContext())
            {
                return hatCon.Hats.Include(h => h.Materials).FirstOrDefault(h => h.Id == id);
            }
        }

        public HatViewModel GetHatViewModel(int id)
        {
            using (var hatCon = new ApplicationDbContext())
            {
                Hats hat = hatCon.Hats.Include(h => h.Materials).Include(h => h.Images).FirstOrDefault(h => h.Id == id);
                HatViewModel model = new HatViewModel()
                {
                    Name = hat.Name,
                    Id = hat.Id,
                    Comment = hat.Comment,
                    Size = hat.Size,
                    Price = hat.Price,
                    Status = hat.Status,
                    UserName = userRepository.GetUser(hat.UserId).Name,
                    Materials = hat.Materials,
                    HatModelID = hat.ModelID,
                    HatModelName = hatmodelRepository.GetHatmodel(hat.ModelID).Name,
                    HatModelDescription = hatmodelRepository.GetHatmodel(hat.ModelID).Description,
                    OrderId = hat.OrderId,
                    Images = hat.Images
                };
                return model;
            }
        }

        public List<Hats> GetAllHats()
        {
            using (var hatCon = new ApplicationDbContext())
            {
                return hatCon.Hats.Include(h => h.Order).Include(h => h.User).ToList();
            }
        }

        public Hats CreateHat(HatViewModel hat, List<int> valdMaterial)
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
                    Materials = new List<MaterialModels>(),
                    Images = new List<ImageModels>()
                };
                if (hat.HatModelID == 1)
                {
                    foreach (var id in valdMaterial)
                    {
                        var aMaterial = hatCon.Material.ToList().FirstOrDefault(h => h.Id == id);
                        hats.Materials.Add(aMaterial);
                    }
                } 
                else
                {
                    foreach (var id in valdMaterial)
                    {
                        var aMaterial = hatCon.Material.ToList().FirstOrDefault(h => h.Id == id);
                        hats.Materials.Add(aMaterial);
                    }
                }
                if(hat.Images != null)
                {
                    foreach(var image in hat.Images)
                    {
                        hats.Images.Add(image);
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
                return hatCon.Hats.Include(h => h.Images).Where(h => h.OrderId == id).ToList();
            }
        }


        public void UpdateHat(HatViewModel hat)
        {
            using (var hatCon = new ApplicationDbContext())
            {
                Hats existingHat = GetHat(hat.Id);
                hatCon.Hats.Attach(existingHat);

                existingHat.Id = hat.Id;
                existingHat.Name = hat.Name;
                existingHat.Size = hat.Size;
                existingHat.Comment = hat.Comment;
                existingHat.Status = hat.Status;
                existingHat.Price = hat.Price;
                existingHat.UserId = hat.UserId;

                existingHat.Materials = hat.Materials;

                hatCon.Entry(existingHat).State = EntityState.Modified;
                hatCon.SaveChanges();
            }
        }

        public List<SelectListItem> StatusesToDropDownList()
        {
            var statuses = new List<SelectListItem>()
            {
                new SelectListItem { Value = "Aktiv", Text= "Aktiv"},
                new SelectListItem { Value = "Inaktiv", Text = "Inaktiv"}
            };
            return statuses;
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
                    Materials = model.Materials,
                    Images = model.Images
                };
                hatCon.Hats.Attach(hat);
                hatCon.Entry(hat).State = EntityState.Added;
                hatCon.SaveChanges();
            }
        }
    }
}
    
