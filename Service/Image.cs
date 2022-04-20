using Hattmakarens_system.Models;
using Hattmakarens_system.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hattmakarens_system.Service
{
    public class Image
    {
       
        public List<ImageModels> AddImages(HttpPostedFileBase[] files, string path)
        {
            List<ImageModels> images = new List<ImageModels>();
            if (files.Length > 0)
            {
                foreach (var item in files)
                {
                    string filename = Path.GetFileName(item.FileName);
                    string imagePath = Path.Combine(path, filename);
                    var image = new ImageModels
                    {
                        Path = imagePath,
                        HatModels = new List<HatModels>()
                    };
                    images.Add(image);
                } 
            }
            return images;
        }
    }
}