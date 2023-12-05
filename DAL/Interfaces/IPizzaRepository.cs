using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
   
    public interface IPizzaRepository : IRepository<Pizza>
    {
        IQueryable<Pizza> GetAllWithDetails();
        Task<Pizza> GetByIdWithDetails(int id);
    }
}
