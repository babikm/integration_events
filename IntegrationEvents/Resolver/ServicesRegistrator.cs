using Abstract;
using Dal;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Resolver
{
    public class ServicesRegistrator
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IEventService, EventService>();
        }
    }
}
