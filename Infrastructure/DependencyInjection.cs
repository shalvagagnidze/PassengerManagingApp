﻿using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<PassengerAppDbContext>(options =>
            {

                options.UseNpgsql(configuration.GetConnectionString("PassengerApp"), npgsqlOptions =>
                {
                    npgsqlOptions.EnableRetryOnFailure(
                                 maxRetryCount: 5,
                                 maxRetryDelay: TimeSpan.FromSeconds(30),
                                 errorCodesToAdd: null);
                    npgsqlOptions.CommandTimeout(30);
                });

            });

            services.AddTransient<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
