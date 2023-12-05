using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{

    public class ManufacturerRepository : Repository<Manufacturer>, IManufacturerRepository
    {
        public ManufacturerRepository(PizzaDbContext context) : base(context)
        {
        }

        public IQueryable<Manufacturer> GetAllWithDetails()
        {
            return FindAll()
                .Include(x => x.Pizzas);
        }

        public async Task<Manufacturer> GetByIdWithDetails(int id)
        {
            return await FindAll()
                .Include(x => x.Pizzas)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
        }
    }
}
