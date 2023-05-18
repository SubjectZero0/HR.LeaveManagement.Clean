using HR.LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;
using HR.LeaveManagement.Application.Services.Validators;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace HR.LeaveManagement.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(options =>
            {
                options.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });

            services.AddScoped(typeof(IGenericValidatorService<,>), typeof(GenericValidatorService<,>));
            services.AddScoped<ICreateLeaveTypeValidatorService, CreateLeaveTypeValidatorService>();
            services.AddScoped<IUpdateLeaveTypeValidatorService, UpdateLeaveTypeValidatorService>();

            return services;
        }
    }
}