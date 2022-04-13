using Hattmakarens_system.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Hattmakarens_system.Service
{
    public class Order
    {
        public void ChangePriorityStatus(int id, bool status)
        {
            using (var db = new ApplicationDbContext())
            {

                var order = db.Order.Single(c => c.Id == id);
                if (!status)
                {
                    order.Priority = true;
                    db.Entry(order).State = EntityState.Modified;
                    db.SaveChanges();

                }
                else
                {
                    order.Priority = false;
                    db.Entry(order).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            
        }
    }
}