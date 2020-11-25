using api.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Injeção do MySQL
            services.AddDbContext<ApplicationDbContext>(options => options.UseMySql(Configuration.GetConnectionString("DefaultConnection")));
            
            services.AddControllers();

            //Injeção do Swagger
            services.AddSwaggerGen(config => {
                    config.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo{Title = "API de produtos", Version = "v1"});
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger(config => {//Gera um arquivo JSON  => swagger.json
                config.RouteTemplate = "pedro/{documentName}/swagger.json";
            });

            app.UseSwaggerUI(config => { //Usar as views html para mostrar a documentação do swagger que foram gerados
                // config.SwaggerEndpoint("/swagger/v1/swagger.json", "v1 docs");
                config.SwaggerEndpoint("/pedro/v1/swagger.json", "v1 docs");
            });
        }
    }
}
