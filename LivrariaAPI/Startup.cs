using Livraria.Data.EFCore;
using Livraria.Data.EFCore.Repositorios;
using Livraria.Data.EFCore.UnidadeDeTrabalho;
using Livraria.Domain.Interfaces;
using Livraria.Domain.Interfaces.Repositorios;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

namespace LivrariaAPI
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
       
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddDbContext<LivrariaDBContext>(options => options.UseSqlServer(Configuration.GetConnectionString("ConexaoBancoLivrariaAPI_Azure"), b => b.MigrationsAssembly("Livraria.Data.EFCore")));

            services.AddScoped<IRepositorioAutor, RepositorioAutor>(); 
            services.AddScoped<IRepositorioLivro, RepositorioLivro>();
            services.AddScoped<IUnidadeDeTrabalho, UnidadeDeTrabalho>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {
                    Title = "API para Livraria",
                    Version = "v1",
                    Description = "Esta API Restful foi desenvolvida com ASP.NET Core 5.0 + Entity Framework Core 5.0.16 + FluentAPI + DataAnnotation + SQL Server + Swagger + OpenAPI" +
                                   "+ Padrões Repository e Unit of Work" 
                    ,
                    Contact = new OpenApiContact {
                        Name = "Desenvolvedor Felipe Rodrigues",
                        Email = "feliperodriguesdeveloper@hotmail.com",
                        Url = new Uri("https://github.com/FelipeRodriguesDeveloper/Livraria_API_Restful")
                    }
                });

                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "LivrariaAPI v1"));
            

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
                   endpoints.MapControllerRoute(
                       name: "default",
                       pattern: "{controller=Home}/{action=Index}/{id?}") 
            );
        }
    }
}
