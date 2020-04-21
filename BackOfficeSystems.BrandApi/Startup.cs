using AutoMapper;
using BackOfficeSystems.BrandApi.Domain.BrandAggregate;
using BackOfficeSystems.BrandApi.Infrastructure.DataAccess;
using BackOfficeSystems.BrandApi.Infrastructure.DataAccess.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace BackOfficeSystems.BrandApi
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
            services.AddCors();

            services.AddControllers(options =>
            {
                // Return json or xml response, respecting the 'Accept' request header
                options.RespectBrowserAcceptHeader = true;
            })
                .AddXmlSerializerFormatters()
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.SuppressModelStateInvalidFilter = true;
                });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Brand API", Version = "v1" });
            });

            services.AddDbContext<BackOfficeSystemsContext>(options =>
                options.UseMySql(Configuration.GetConnectionString("BackOfficeSystems")));

            services.AddAutoMapper(typeof(Startup));
            services.AddScoped<InputValidationAttribute>();

            services.AddScoped<IBrandRepository, BrandRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Configure CORS policy to allow access of client app that hosted on another host
            app.UseCors(builder =>
                builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .SetIsOriginAllowed((host) => true)
                    .AllowAnyHeader());
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Brand API V1");
            });

            app.UseHttpsRedirection();
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseRouting();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}