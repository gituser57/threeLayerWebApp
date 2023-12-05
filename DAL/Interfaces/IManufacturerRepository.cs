using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IManufacturerRepository : IRepository<Manufacturer>
    {
        IQueryable<Manufacturer> GetAllWithDetails();
        Task<Manufacturer> GetByIdWithDetails(int id);
    }
}
