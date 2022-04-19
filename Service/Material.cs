using Hattmakarens_system.Models;
using Hattmakarens_system.Repositories;
using Hattmakarens_system.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hattmakarens_system.Service
{
    public class Material
    {
        public List<SelectListItem> GetSelectListMaterials()
        {
            var materialRepo = new MaterialRepository();
            var materials = new List<SelectListItem>();
            foreach (var material in materialRepo.GetAllMaterials())
            {
                var listitem = new SelectListItem
                {
                    Value = material.Id.ToString(),
                    Text = material.Name + ", " + material.Color.Name + ", " + material.Type
                };
                materials.Add(listitem);
            }
            return materials;
        }

        public List<ColorMaterialViewModel> GetTyg()
        {
            var materialRepo = new MaterialRepository();
            var tygLista = new List<ColorMaterialViewModel>();
            foreach( var material in materialRepo.GetAllMaterialsOfType("Tyg"))
            {
                using (var db = new ApplicationDbContext())
                {
                    var color = db.Color.FirstOrDefault(c => c.Id == material.ColorId).Name;
                    tygLista.Add(new ColorMaterialViewModel 
                    { 
                        Id = material.ColorId, 
                        Name = color, 
                        MaterialId = material.Id,
                        MaterialName = material.Name,
                        Description = material.Description,
                        Type = material.Type,
                        State = false
                    });
                }                  
            }
            return tygLista;
        }

        public List<ColorMaterialViewModel> ResetTygList(List<ColorMaterialViewModel> tygList)
        {
            var tygLista = tygList;
            foreach (var material in tygLista)
            {
                using (var db = new ApplicationDbContext())
                {
                    material.State = false;
                }
            }
            return tygLista;
        }

        public List<ColorMaterialViewModel> GetDecoration()
        {
            var materialRepo = new MaterialRepository();
            var decorationLista = new List<ColorMaterialViewModel>();
            foreach (var material in materialRepo.GetAllMaterialsOfType("Dekoration"))
            {
                using (var db = new ApplicationDbContext())
                {
                    var color = db.Color.FirstOrDefault(c => c.Id == material.ColorId).Name;
                    decorationLista.Add(new ColorMaterialViewModel
                    {
                        Id = material.ColorId,
                        Name = color,
                        MaterialId = material.Id,
                        MaterialName = material.Name,
                        Description = material.Description,
                        Type = material.Type,
                        State = false
                    });
                }
            }
            return decorationLista;
        }
        public List<ColorMaterialViewModel> ResetDecorationList(List<ColorMaterialViewModel> dekorationList)
        {
            var dekorationLista = dekorationList;
            foreach (var material in dekorationLista)
            {
                using (var db = new ApplicationDbContext())
                {
                    material.State = false;
                }
            }
            return dekorationLista;
        }
        public List<ColorMaterialViewModel> GetTrad()
        {
            var materialRepo = new MaterialRepository();
            var tradLista = new List<ColorMaterialViewModel>();
            foreach (var material in materialRepo.GetAllMaterialsOfType("Tråd"))
            {
                using (var db = new ApplicationDbContext())
                {
                    var color = db.Color.FirstOrDefault(c => c.Id == material.ColorId).Name;
                    tradLista.Add(new ColorMaterialViewModel
                    {
                        Id = material.ColorId,
                        Name = color,
                        MaterialId = material.Id,
                        MaterialName = material.Name,
                        Description = material.Description,
                        Type = material.Type,
                        State = false
                    });
                }
            }
            return tradLista;
        }
        public List<ColorMaterialViewModel> ResetTradList(List<ColorMaterialViewModel> tradList)
        {
            var tradLista = tradList;
            foreach (var material in tradLista)
            {
                using (var db = new ApplicationDbContext())
                {
                    material.State = false;
                }
            }
            return tradLista;
        }
    }
}