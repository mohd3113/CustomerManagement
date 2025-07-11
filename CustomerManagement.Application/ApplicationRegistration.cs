using Microsoft.Extensions.DependencyInjection;

namespace CustomerManagement.Application
{
    public static class ApplicationRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ApplicationRegistration).Assembly));
            services.AddAutoMapper(cfg => cfg.AddMaps(typeof(ApplicationRegistration).Assembly));

            return services;
        }
    }
}