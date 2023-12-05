using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    // https://learn.microsoft.com/en-us/ef/core/modeling/relationships/one-to-many
    public class Pizza: BaseEntity
    {
        public string? Name { get; set; }
        public Decimal Price { get; set; }
        public int ManufacturerId { get; set; }
        public Manufacturer? Manufacturer { get; set; } = null!;
    }

}
