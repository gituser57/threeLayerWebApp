using Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Validation
{
    public static class ManufacturerValidation
    {
        public static void CheckManufacturer(ManufacturerModel manufacturerModel)
        {
            if (string.IsNullOrEmpty(manufacturerModel.Name))
            {
                throw new PizzaException("Name is empty.");
            }

            if (string.IsNullOrEmpty(manufacturerModel.Country))
            {
                throw new PizzaException("Country is empty.");
            }
        }
    }
}
