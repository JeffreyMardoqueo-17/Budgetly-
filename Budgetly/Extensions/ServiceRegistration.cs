using Budgetly.service;
using Budgetly.service.interfaces;
using Budgetly.Services;
using Budgetly.Services.Auth;
using Microsoft.Extensions.DependencyInjection;
using Budgetly.service.Auth;
using Budgetly.service.Email;

namespace Budgetly.Extensions
{
    public static class ServiceRegistration
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUsuarioService, UsuarioService>();
            //aqui voya  agregar los demas servicios para inyectarlos  al program
             services.AddSingleton<IEmailVerificationService, EmailVerificationService>(); // Singleton porque mantiene código temporal
            services.AddTransient<IEmailService,EmailService>();

        }
    }
}
