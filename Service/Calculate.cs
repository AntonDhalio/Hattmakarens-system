using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hattmakarens_system.Service
{
    public class Calculate
    {
        public double CalculateTax(double price, bool isPriority)
        {
            if(isPriority)
            {
                price *= 1.2;
            }
            double priceWithTax = price*1.25;
            return priceWithTax;
        }
        public double GetTaxFromTotal(double priceIncTax)
        {
            double tax = priceIncTax * 0.8;
            return tax;
        }
        public double GetTaxOnly(double priceExTax)
        {
            double priceIncTax = priceExTax * 1.25;
            double tax = priceIncTax - priceExTax;
            return tax;
        }
    }
}