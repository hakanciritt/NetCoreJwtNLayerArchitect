using System;
using System.Collections.Generic;
using System.Reflection;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NLayerProjectForJwt.Core.Configuration;
using NLayerProjectForJwt.Core.Entities;
using NLayerProjectForJwt.Core.Repositories;
using NLayerProjectForJwt.Core.Services;
using NLayerProjectForJwt.Core.UnitOfWork;
using NLayerProjectForJwt.Data.EntityFramework;
using NLayerProjectForJwt.Data.Repositories;
using NLayerProjectForJwt.Data.UnitOfWork;
using NLayerProjectForJwt.Service.Services;
using SharedLibrary.Configuration;
using SharedLibrary.Extensions;

namespace NLayerProjectForJwt.API
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
            //DI register
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IGenericService<,>), typeof(GenericService<,>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();


            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("SqlServer"), sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly("NLayerProjectForJwt.Data");
                });
            });
            services.AddIdentity<UserApp, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password.RequireNonAlphanumeric = false;
            }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

            services.Configure<CustomTokenOptions>(Configuration.GetSection("TokenOption"));
            var tokenOptions = Configuration.GetSection("TokenOption").Get<CustomTokenOptions>();

            services.Configure<List<Client>>(Configuration.GetSection("Clients"));
            services.AddCustomTokenAuth(tokenOptions);

            services.AddControllers().AddFluentValidation(options => options.RegisterValidatorsFromAssemblyContaining<Startup>());
            services.UseCustomValidationResponse();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "NLayerProjectForJwt.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "NLayerProjectForJwt.API v1"));
            }

            app.UseCustomException();


            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
