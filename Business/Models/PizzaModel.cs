using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models
{
    public class PizzaModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public Decimal Price { get; set; }
        public int ManufacturerId { get; set; }

    }
}
