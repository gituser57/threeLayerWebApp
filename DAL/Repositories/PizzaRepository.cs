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
       public class PizzaRepository : Repository<Pizza>, IPizzaRepository
    {
        public PizzaRepository(PizzaDbContext context) : base(context)
        {
        }

        public IQueryable<Pizza> GetAllWithDetails()
        {
            return FindAll()
                .Include(x => x.Manufacturer);
        }

        public async Task<Pizza> GetByIdWithDetails(int id)
        {
            return await FindAll()
                .Include(x => x.Manufacturer)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
        }
    }
}
