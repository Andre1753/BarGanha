using BarGanha.DAL.Interfaces;
using BarGanha.DAL.Repositorios;
using Microsoft.Extensions.DependencyInjection;

namespace BarGanha.DAL
{
    public static class ConfiguracaoRepositoriosExtension
    {
        public static void ConfigurarRepositorios(this IServiceCollection services)
        {
            services.AddTransient<IUsuarioRepositorio, UsuarioRepositorio>();
            services.AddTransient<IFuncaoRepositorio, FuncaoRepositorio>();        

        }
    }
}
