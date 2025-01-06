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
    public class DriverService : IDriverService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly PassengerAppDbContext _db;
        public DriverService(PassengerAppDbContext db, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _db = db;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IActionResult> GetAllDrivers()
        {
            var drivers = await _unitOfWork.DriverRepository.GetAllAsync();
            return new OkObjectResult(_mapper.Map<List<DriverModel>>(drivers));
        }

        public async Task<IActionResult> GetDriver(int id)
        {
            var driver = await _unitOfWork.DriverRepository.GetByIdAsync(id);

            return new OkObjectResult(_mapper.Map<DriverModel>(driver));
        }
        public async Task<IActionResult> AddDriver(DriverModel model)
        {
            var driverExists = await _db.Drivers.FirstOrDefaultAsync(x => x.FirstName == model.FirstName && x.LastName == model.LastName && x.PhoneNumber == x.PhoneNumber);

            if (driverExists is not null)
            {
                return new BadRequestObjectResult("Driver Already Exists");
            }

            var bus = await _unitOfWork.BusRepository.GetByIdAsync(model.BusId);

            if (bus is null)
            {
                return new BadRequestObjectResult("Bus not found!");
            }

            string phoneNumber = $"+995{model.PhoneNumber}";

            var driver = new Driver
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = phoneNumber,
                Bus = bus
            };

            try
            {
                await _unitOfWork.DriverRepository.AddAsync(driver);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult($"Error occurred: {ex.Message}");
            }

            return new OkObjectResult("Driver has been added successfully");
        }

        public async Task<IActionResult> DeleteDriver(int id)
        {
            var driver = await _unitOfWork.DriverRepository.GetByIdAsync(id);

            if (driver is null)
            {
                return new BadRequestObjectResult("Driver not found!");
            }

            try
            {
                await _unitOfWork.DriverRepository.DeleteByIdAsync(id);
                _unitOfWork.DriverRepository.Update(driver);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult($"Error occurred: {ex.Message}");
            }

            return new OkObjectResult("Driver has been deleted successfully");
        }

        public async Task<IActionResult> UpdateDriver(DriverModel model)
        {
            var existing = await _unitOfWork.DriverRepository.GetByIdAsync(model.Id);
            if (existing is null)
            {
                return new BadRequestObjectResult("Bus not found!");
            }
            

            existing.FirstName = model.FirstName;
            existing.LastName = model.LastName;
            existing.PhoneNumber = model.PhoneNumber;
            //existing.Bus = _mapper.Map<Bus>(model.Bus);
            var newBus = await _unitOfWork.BusRepository.GetByIdAsync(model.BusId);

            if (newBus is null)
            {
                return new NotFoundObjectResult("Bus not found!");
            }

            existing.Bus  = newBus;

            try
            {
                _unitOfWork.DriverRepository.Update(existing);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult($"Error occurred: {ex.Message}");
            }

            return new OkObjectResult("Driver has been updated successfully");
        }
    }
}
