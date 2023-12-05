using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Manufacturer:BaseEntity
    {
        public string Name { get; set; }
        public string Country { get; set; }
        public ICollection<Pizza> Pizzas { get; set; } = new List<Pizza>();
    }
}
