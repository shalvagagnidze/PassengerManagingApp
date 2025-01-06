using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.Interfaces;
using Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class BusService : IBusService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly PassengerAppDbContext _db;
        public BusService(PassengerAppDbContext db,IUnitOfWork unitOfWork,IMapper mapper)
        {
            _db = db;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IActionResult> GetAllBuses()
        {
            var buses = await _unitOfWork.BusRepository.GetAllAsync();
            return new OkObjectResult(_mapper.Map<List<BusModel>>(buses));
        }

        public async Task<IActionResult> GetBus(int id)
        {
            var bus = await _unitOfWork.BusRepository.GetByIdAsync(id);

            return new OkObjectResult(_mapper.Map<BusModel>(bus));
        }
        public async Task<IActionResult> AddBus(BusModel model)
        {
            var busExists = await _db.Buses.FirstOrDefaultAsync(x => x.Number == model.Number);

            if (busExists is not null)
            {
                return new BadRequestObjectResult("Bus Already Exists");
            }

            string number = $"GB-{model.Number}-US";
            var bus = new Bus
            {
                Number = number,
                //Driver = _mapper.Map<Driver>(model.Driver)
            };

            
            try
            {
                await _unitOfWork.BusRepository.AddAsync(bus);
                await _unitOfWork.SaveAsync();
            }catch (Exception ex)
            {
                return new BadRequestObjectResult($"Error occurred: {ex.Message}");
            }

            return new OkObjectResult("Bus has been added successfully");
        }

        public async Task<IActionResult> DeleteBus(int id)
        {
            var bus = await _unitOfWork.BusRepository.GetByIdAsync(id);

            if(bus is null)
            {
                return new BadRequestObjectResult("Bus not found!");
            }

            

            try
            {
                await _unitOfWork.BusRepository.DeleteByIdAsync(id);
                _unitOfWork.BusRepository.Update(bus);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult($"Error occurred: {ex.Message}");
            }

            return new OkObjectResult("Bus has been deleted successfully");
        }

        public async Task<IActionResult> UpdateBus(BusModel model)
        {
            var existing = await _unitOfWork.BusRepository.GetByIdAsync(model.Id);

            if(existing is null)
            {
                return new BadRequestObjectResult("Bus not found!");
            }

            existing.Number = model.Number;
            //existing.Driver = _mapper.Map<Driver>(model.Driver);

            try
            {
                _unitOfWork.BusRepository.Update(existing);
                await _unitOfWork.SaveAsync();
            }catch (Exception ex)
            {
                return new BadRequestObjectResult($"Error occurred: {ex.Message}");
            }

            return new OkObjectResult("Bus has been updated successfully");
        }
    }
}
