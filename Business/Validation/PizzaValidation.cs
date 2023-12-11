using System;
using Business.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Validation
{
    public static class PizzaValidation
    {
        public static void CheckPizza(PizzaModel pizzaModel)
        {
            if (pizzaModel.ManufacturerId <= 0)
            {
                throw new PizzaException("ManufacturerId should be valid.");
            }

            if (string.IsNullOrEmpty(pizzaModel.Name))
            {
                throw new PizzaException("Name is empty.");
            }

            if (pizzaModel.Price <= 0)
            {
                throw new PizzaException("Price should be valid.");
            }
        }
    }
}
