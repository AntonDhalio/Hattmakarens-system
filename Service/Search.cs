using Hattmakarens_system.Models;
using Hattmakarens_system.Repositories;
using Hattmakarens_system.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace Hattmakarens_system.Service
{
    public class Search
    {
        private OrderRepository orderRepository = new OrderRepository();

        public List<OrderModels> GetSearchList(string searchString, string searchOption, string statusOption)
        {
            var orderList = orderRepository.GetAllOrders();
            var searchOrderList = new List<OrderModels>();

            if (searchOption is null)
            {
                searchOrderList = orderList;
            }
            else if (searchOption.Equals("material"))
            {
                //BEHÖVER: hämta id från sambandstabell material-hat
                var materialList = new MaterialRepository().GetAllMaterials();
                var searchMaterialList = new List<MaterialModels>();

                foreach(var material in materialList)
                {
                    if(material.Type.Contains(searchString))
                    {
                        searchMaterialList.Add(material);
                    }
                }
                foreach(var material in searchMaterialList)
                {
                    foreach(var hat in material.Hats.ToList())
                    {
                        searchOrderList.Add(orderRepository.GetOrder(hat.OrderId));
                    }
                }
            }
            else if (searchOption.Equals("model"))
            {
                //FUNGERAR MEN KAN FINSLIPAS

                var modelList = new HatmodelRepository().GetAllHatmodels();
                var searchModelList = new List<HatModels>();

                foreach (var model in modelList)
                {
                    if (model.Name.Contains(searchString))
                    {
                        searchModelList.Add(model);
                    }
                }
                foreach (var model in searchModelList)
                {
                    foreach (var hat in model.Hats.ToList())
                    {
                        searchOrderList.Add(orderRepository.GetOrder(hat.OrderId));
                    }
                }
            }
            else if (searchOption.Equals("customer"))
            {
                //hämta alla ordrar på angivet kundnamn
                //addera alla ordrar till söklistan
            }

            if (statusOption.Equals("active"))
            {
                //söklistan ska filtreras på aktiva ordrar
            }
            if (statusOption.Equals("inactive"))
            {
                //Söklistan ska filtreras på inaktiva ordrar
            }
            
            var searchList = searchOrderList.Distinct().ToList();
            return searchList;
        }
       

    }
}