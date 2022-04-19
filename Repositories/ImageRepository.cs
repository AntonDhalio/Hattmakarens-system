using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Hattmakarens_system.Models;

namespace Hattmakarens_system.Repositories
{
    public class ImageRepository
    {
        public ImageModels GetImage(int id)
        {
            using (var hatCon = new ApplicationDbContext())
            {
                return hatCon.Pictures.Include(p => p.HatModels).Include(p => p.Hats).FirstOrDefault(p => p.Id == id);
            }
        }
        public List<ImageModels> GetAllImages()
        {
            using (var hatCon = new ApplicationDbContext())
            {
                return hatCon.Pictures.Include(p => p.HatModels).Include(p => p.Hats).ToList();
            }
        }
        public ImageModels SaveImage(ImageModels image)
        {
            using (var hatCon = new ApplicationDbContext())
            {
                if (image.Id != 0)
                {
                    hatCon.Entry(image).State = EntityState.Modified;
                }
                else
                {
                    hatCon.Pictures.Add(image);
                }
                hatCon.SaveChanges();
                return image;
            }
        }

        public ImageModels GetLatestAddedImage()
        {
            using (var hatCon = new ApplicationDbContext())
            {
                return hatCon.Pictures.LastOrDefault();
            }
        }
        public void DeleteImage(int id)
        {
            using (var hatCon = new ApplicationDbContext())
            {
                var image = hatCon.Pictures.FirstOrDefault(p => p.Id == id);
                if (image != null)
                {
                    hatCon.Pictures.Remove(image);
                    hatCon.SaveChanges();
                }
            }
        }
    }
}