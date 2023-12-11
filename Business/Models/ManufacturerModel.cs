using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models
{
    public class ManufacturerModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }

        public ICollection<int> PizzasIds { get; set; } = new List<int>();
        //public ICollection<Pizza> Pizzas { get; set; } = new List<Pizza>();
    }
}
