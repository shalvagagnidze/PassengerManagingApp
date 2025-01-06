using AutoMapper;
using Domain.Entities;
using Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Bus, BusModel>();
            CreateMap<BusModel, Bus>();
            CreateMap<Driver, DriverModel>();
            CreateMap<DriverModel, Driver>();
            CreateMap<Flight, FlightModel>();
            CreateMap<FlightModel, Flight>();
        }
    }
}
