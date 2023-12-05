using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PizzaDbContext _context;

        public UnitOfWork(PizzaDbContext context, IManufacturerRepository manufacturerRepository,
            IPizzaRepository pizzaRepository)
        {
            _context = context;
            ManufacturerRepository = manufacturerRepository;
            PizzaRepository = pizzaRepository;
        }

        public IManufacturerRepository ManufacturerRepository { get; }
        public IPizzaRepository PizzaRepository { get; }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
