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
    public class ManufacturerService: IManufacturerService
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;

        public ManufacturerService(IUnitOfWork unit, IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }

        public IEnumerable<ManufacturerModel> GetAll()
        {
            var manufacturers = _unit.ManufacturerRepository.GetAllWithDetails();

            return _mapper.Map<IEnumerable<ManufacturerModel>>(manufacturers);
        }

        public async Task AddAsync(ManufacturerModel model)
        {
            ManufacturerValidation.CheckManufacturer(model);

            var manufactuter = _mapper.Map<Manufacturer>(model);

            await _unit.ManufacturerRepository.AddAsync(manufactuter);
            await _unit.SaveAsync();

            model.Id = manufactuter.Id;
        }

        public async Task<ManufacturerModel> GetByIdAsync(int id)
        {
            var manufactuter = await _unit.ManufacturerRepository.GetByIdWithDetails(id);

            return _mapper.Map<ManufacturerModel>(manufactuter);
        }

        public async Task UpdateAsync(ManufacturerModel model)
        {
            ManufacturerValidation.CheckManufacturer(model);
            var manufactuter = _mapper.Map<Manufacturer>(model);

            _unit.ManufacturerRepository.Update(manufactuter);
            await _unit.SaveAsync();
        }

        public async Task DeleteByIdAsync(int modelId)
        {
            await _unit.ManufacturerRepository.DeleteByIdAsync(modelId);
            await _unit.SaveAsync();
        }

        public IEnumerable<ManufacturerModel> GetByFilter(FilterSearchModel filterSearch)
        {
            var manufacturers = _unit.ManufacturerRepository.FindAll();

            if (!string.IsNullOrEmpty(filterSearch.ManufacturerName))
            {
                manufacturers = manufacturers.Where(b => b.Name == filterSearch.ManufacturerName);
            }

            if (filterSearch.ManufacturerCountry != null)
            {
                manufacturers = manufacturers.Where(b => b.Country == filterSearch.ManufacturerCountry);
            }

            return _mapper.Map<IEnumerable<ManufacturerModel>>(manufacturers);
        }
    }

}
