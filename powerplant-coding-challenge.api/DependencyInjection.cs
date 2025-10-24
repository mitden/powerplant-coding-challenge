using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using powerplantcodingchallenge.application.Interfaces;
using powerplantcodingchallenge.application.Services;

namespace powerplantcodingchallenge.application
{
    public static class DependencyInjection
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IProductionPlanService, ProductionPlanService>();
        }
    }
}
