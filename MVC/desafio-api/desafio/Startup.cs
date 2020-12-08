using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using desafio.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace desafio
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
            services.AddDbContext<DataContext>(options => options.UseMySql(Configuration.GetConnectionString("DefaultConnection")));
            
            services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddSwaggerGen(config => {
                    config.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo(){
                        Title = "API Vendas",
                        Version = "Versão 1",
                        Description = "API de Vendas, desenvolvida durante o treinamento de estágiário da GFT. Para ter acesso aos métodos, execute o método Seed antes de tudo, depois faça o Login em um Usuário com email e senha, a Senha é igual o Email.",
                        Contact = new Microsoft.OpenApi.Models.OpenApiContact
                        {
                            Name = "Pedro Henrique Padilha Portella da Cruz",
                            Email = "pedro.kadjin.sg@gmail.com",
                            Url = new Uri("https://github.com/PedroPadilhaPortella")
                        }
                    });
                    config.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme {
                            Name = "Authorization",
                            Type = SecuritySchemeType.ApiKey,
                            Scheme = "Bearer",
                            BearerFormat = "JWT",
                            In = ParameterLocation.Header,
                            Description = "JWT Authorization header using the Bearer scheme."
                    });
                    config.AddSecurityRequirement(new OpenApiSecurityRequirement {
                            {
                                new OpenApiSecurityScheme  {
                                        Reference = new OpenApiReference { 
                                            Type = ReferenceType.SecurityScheme, 
                                            Id = "Bearer" 
                                        }
                                },
                                new string[] {}
                            }
                    });

                    var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                    var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);
                    
                    config.IncludeXmlComments(xmlCommentsFullPath);
            });


            string chaveDeSeguranca = "secret_invoice_key";
            var chaveSimetrica  = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(chaveDeSeguranca));
            
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = "notafiscalAPI",
                    ValidAudience = "public_user",
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

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();

            app.UseSwaggerUI(config => {
                config.SwaggerEndpoint("/swagger/v1/swagger.json", "v1 documentation");
            });
        }
    }
}
