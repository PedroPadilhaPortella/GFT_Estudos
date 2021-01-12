using api.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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

            // Configurando o metodo de autenticação, via JWT, e como o sistema deve ler o Token
            string chaveDeSeguranca = "securitykeypedroportella";//Chave de Segurança
            var chaveSimetrica  = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(chaveDeSeguranca)); //chave simétrica
            
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,

                    //Dados de Validação do JWT
                    ValidIssuer = "PedroPortellaAPI",
                    ValidAudience = "usuario_comum",
                    IssuerSigningKey = chaveSimetrica 
                };
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication(); //usar autenticacao do JWT

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
