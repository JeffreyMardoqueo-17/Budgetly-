using Budgetly.service;
using Budgetly.service.interfaces;
using Budgetly.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Budgetly.Extensions
{
    public static class ServiceRegistration
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUsuarioService, UsuarioService>();
            //aqui voya  agregar los demas servicios para inyectarlos  al program
        }
    }
}
