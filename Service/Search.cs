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
                //hämta alla ordrar på angivet material
                //addera alla ordrar till söklistan
                var materialList = new MaterialRepository().GetAllMaterials();
                var searchMaterialList = new List<MaterialModels>();
                var hatlist = new List<Hats>();
                foreach(var material in materialList)
                {
                    if(material.Name.Contains(searchString))
                    {
                        searchMaterialList.Add(material);
                    }
                }
                foreach(var material in searchMaterialList)
                {
                    foreach(var hat in material.Hats)
                    {
                        searchOrderList.Add(orderRepository.GetOrder(hat.OrderId));
                    }
                }
                
            }
            else if (searchOption.Equals("model"))
            {
                //hämta alla ordrar på angivet modell
                //addera alla ordrar till söklistan


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