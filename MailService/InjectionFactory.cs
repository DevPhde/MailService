using MailService.Persistence.Context;
using MailService.Persistence.Repository;
using MailService.Providers.Jwt;
using MailService.Providers.MailTrap;
using MailService.Services;

namespace MailService
{
	public static class InjectionFactory
	{
		public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
		{
			// Dapper Context
			services.AddSingleton<DapperContext>();

			//Repository
			services.AddScoped<IMailRepository, MailRepository>();

			//Services
			services.AddScoped<IEmailService, EmailService>();

			//Providers
			services.AddScoped<IMailTrap, MailTrap>();
			services.AddScoped<IJwtProvider, JwtProvider>();

			return services;
		}
	}
}
