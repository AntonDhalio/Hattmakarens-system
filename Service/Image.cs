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
            if (files[0] != null)
            {
                foreach (var item in files)
                {
                    string filename = Path.GetFileName(item.FileName);
                    string imagePath = Path.Combine(path, filename);
                    item.SaveAs(imagePath);
                    var image = new ImageModels
                    {
                        Path = Path.Combine(@"~\NewFolder1", filename),
                        HatModels = new List<HatModels>()
                    };
                    images.Add(image);
                } 
            }
            return images;
        }
    }
}