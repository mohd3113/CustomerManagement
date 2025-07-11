using CustomerManagement.Application.Contracts.Repositories;
using CustomerManagement.Application.Contracts.Services;
using CustomerManagement.Infrastructure.Data;
using CustomerManagement.Infrastructure.Repositories;
using CustomerManagement.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CustomerManagement.Infrastructure
{
    public static class InfrastructureRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CMDbContext>(option =>
            {
                option.UseSqlServer(configuration["ConnectionString"]);
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IMessageSenderService, MessageSenderService>();
            services.AddScoped<ITokenService, TokenService>();

            var emailSettings = configuration.GetSection("EmailSettings");
            var defaultFromEmail = emailSettings["From"];
            var host = emailSettings["Host"];
            var port = int.Parse(emailSettings["Port"]);
            var userName = emailSettings["UserName"];
            var password = emailSettings["Password"];

            services.AddFluentEmail(defaultFromEmail).AddSmtpSender(host, port, userName, password);

            return services;
        }
    }
}