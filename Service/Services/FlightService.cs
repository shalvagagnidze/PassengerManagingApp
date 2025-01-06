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
    public class FlightService : IFlightService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly PassengerAppDbContext _db;
        public FlightService(PassengerAppDbContext db, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _db = db;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IActionResult> GetAllFlights()
        {
            var flights = await _unitOfWork.FlightRepository.GetAllAsync();
            return new OkObjectResult(_mapper.Map<List<FlightModel>>(flights));
        }

        public async Task<IActionResult> GetFlight(int id)
        {
            var flight = await _unitOfWork.FlightRepository.GetByIdAsync(id);

            return new OkObjectResult(_mapper.Map<FlightModel>(flight));
        }
        public async Task<IActionResult> AddFlight(FlightModel model)
        {
            var flightExists = await _db.Flights.FirstOrDefaultAsync(x => x.Name == model.Name);

            if (flightExists is not null)
            {
                return new BadRequestObjectResult("Flight Already Exists");
            }

            var flight = new Flight
            {
                Name = model.Name
            };


            try
            {
                await _unitOfWork.FlightRepository.AddAsync(flight);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult($"Error occurred: {ex.Message}");
            }

            return new OkObjectResult("Flight has been added successfully");
        }

        public async Task<IActionResult> DeleteFlight(int id)
        {
            var flight = await _unitOfWork.FlightRepository.GetByIdAsync(id);

            if (flight is null)
            {
                return new BadRequestObjectResult("Flight not found!");
            }



            try
            {
                await _unitOfWork.FlightRepository.DeleteByIdAsync(id);
                _unitOfWork.FlightRepository.Update(flight);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult($"Error occurred: {ex.Message}");
            }

            return new OkObjectResult("Flight has been deleted successfully");
        }

        public async Task<IActionResult> UpdateFlight(FlightModel model)
        {
            var existing = await _unitOfWork.FlightRepository.GetByIdAsync(model.Id);

            if (existing is null)
            {
                return new BadRequestObjectResult("Flight not found!");
            }

            existing.Name = model.Name;

            try
            {
                _unitOfWork.FlightRepository.Update(existing);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult($"Error occurred: {ex.Message}");
            }

            return new OkObjectResult("Flight has been updated successfully");
        }
    }
}
