using AutoMapper;
using Business.Interfaces;
using Business.Models;
using Business.Validation;
using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class PizzaService : IPizzaService
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;

        public PizzaService(IUnitOfWork unit, IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }

        public IEnumerable<PizzaModel> GetAll()
        {
            var pizzas = _unit.PizzaRepository.FindAll();

            return _mapper.Map<IEnumerable<PizzaModel>>(pizzas);


        }

        public async Task AddAsync(PizzaModel model)
        {
            PizzaValidation.CheckPizza(model);

            var pizza = _mapper.Map<Pizza>(model);

            await _unit.PizzaRepository.AddAsync(pizza);
            await _unit.SaveAsync();

            model.Id = pizza.Id;
        }

        public async Task<PizzaModel> GetByIdAsync(int id)
        {
            var pizza = await _unit.PizzaRepository.GetByIdWithDetails(id);

            return _mapper.Map<PizzaModel>(pizza);
        }

        public async Task UpdateAsync(PizzaModel model)
        {
            PizzaValidation.CheckPizza(model);
            var pizza = _mapper.Map<Pizza>(model);

            _unit.PizzaRepository.Update(pizza);
            await _unit.SaveAsync();
        }

        public async Task DeleteByIdAsync(int modelId)
        {
            await _unit.PizzaRepository.DeleteByIdAsync(modelId);
            await _unit.SaveAsync();
        }

        public IEnumerable<PizzaModel> GetPizzaByManufacturerId(int manufacturerId)
        {
            var pizzas = _unit.PizzaRepository.FindAll().Where(m => m.ManufacturerId == manufacturerId);
            return _mapper.Map<IEnumerable<PizzaModel>>(pizzas);
        }

        public IEnumerable<PizzaModel> GetPizzaByPrice(decimal minPrice, decimal maxPrice)
        {
            var pizzas = _unit.PizzaRepository.FindAll().Where(m => (m.Price >= minPrice) && (m.Price <= maxPrice));
            return _mapper.Map<IEnumerable<PizzaModel>>(pizzas);
        }
    }
}
