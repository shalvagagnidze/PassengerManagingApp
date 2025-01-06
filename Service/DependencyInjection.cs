using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Service.Interfaces;
using Service.Mapping;
using Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

            services.AddTransient<IBusService,BusService>();
            services.AddTransient<IDriverService,DriverService>();
            services.AddTransient<IFlightService,FlightService>();

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            return services;
        }
    }
}
