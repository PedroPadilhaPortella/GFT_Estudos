using AutoMapper.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuncionariosWAClean.Infra.IoC
{
    public static class DependecyInjection
    {
        public static IServiceCollection AddInfraestructure(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddDbContext<ApplicationDbContext>(options =>
            //    options.UseSqlServer(configuration.GetConnectionString("SqlServerConnection"), b =>
            //        b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            //services.AddScoped<ICategoryRepository, CategoryRepository>();
            //services.AddScoped<IProductRepository, ProductRepository>();

            //services.AddScoped<IProductService, ProductService>();
            //services.AddScoped<ICategoryService, CategoryService>();

            //services.AddAutoMapper(typeof(DomainToDTOMappingProfile));
            //services.AddAutoMapper(typeof(DTOToCommandMappingProfile));

            //var myHandlers = AppDomain.CurrentDomain.Load("CleanArchMVC.Application");
            //services.AddMediatR(myHandlers);

            return services;
        }
    }
}
