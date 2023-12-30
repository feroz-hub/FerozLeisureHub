using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FerozLeisureHub.Application.Services.Implentations;
using FerozLeisureHub.Application.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace FerozLeisureHub.Insfrastructure.DependencyInjection
{
    public static class DependencyInjectionExtension
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            // Register other application services

            services.AddScoped<IRoleService, RoleService>();
        }
        
    }
}