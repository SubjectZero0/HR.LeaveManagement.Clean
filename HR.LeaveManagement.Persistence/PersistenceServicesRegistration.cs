using HR.LeaveManagement.Persistence.DatabaseContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Persistence
{
    public static class PersistenceServicesRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<HRDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString(nameof(HRDbConnectionString)));
            });
            return services;
        }
    }
}