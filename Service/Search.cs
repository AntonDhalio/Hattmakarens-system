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
            using (var db = new ApplicationDbContext())
            {
                var orderList = orderRepository.GetAllOrders();
                var hatList = new List<Hats>();
                var searchOrderList = new List<OrderModels>();
                var finalList = new List<OrderModels>();


                if (searchOption is null)
                {
                    searchOrderList = orderList;
                }
                else if (searchOption.Equals("material"))
                {
                    //BEHÖVER: hämta id från sambandstabell material-hat
                    var materialList = new MaterialRepository().GetAllMaterials();
                    var searchMaterialList = new List<MaterialModels>();

                    foreach (var material in materialList)
                    {
                        if (material.Type.Contains(searchString))
                        {
                            searchMaterialList.Add(material);
                        }
                    }

                    foreach (var material in searchMaterialList)
                    {

                        var hattar = db.Material.Single(c => c.Id == material.Id).Hats.ToList();

                        foreach (var hat in hatList)
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
                    var CustomerList = new CustomerRepository().GetAllCostumers();
                    var searchCustomerList = new List<CustomerModels>();

                    foreach (var customer in CustomerList)
                    {
                        if (customer.Name.Contains(searchString))
                        {
                            searchCustomerList.Add(customer);
                        }
                    }

                    foreach (var customer in searchCustomerList)                     
                    {
                        foreach (var order in orderList)
                        {
                            if(customer.Id == order.CustomerId)
                            {
                                searchOrderList.Add(orderRepository.GetOrder(order.Id));
                            }
                        }
                    }
                }
                if (statusOption.Equals("active"))
                {
                    //söklistan ska filtreras på aktiva ordrar
                    finalList = searchOrderList.Where(c => c.Status == "active").ToList();
                }
                if (statusOption.Equals("inactive"))
                {
                    //Söklistan ska filtreras på inaktiva ordrar
                    finalList = searchOrderList.Where(c => c.Status == "inactive").ToList();

                }

                var searchList = finalList.Distinct().ToList();
                return searchList;
            }

        }
    }
}