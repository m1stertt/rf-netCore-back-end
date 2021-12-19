using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ScrumMasters.Webshop.Core.IServices;
using ScrumMasters.Webshop.DataAccess;
using ScrumMasters.Webshop.DataAccess.Entities;
using ScrumMasters.Webshop.DataAccess.Repositories;
using ScrumMasters.Webshop.Domain.IRepositories;
using ScrumMasters.Webshop.Domain.Service;
using ScrumMasters.Webshop.Security;
using ScrumMasters.Webshop.Security.Model;
using ScrumMasters.Webshop.Security.Services;
using ScrumMasters.Webshop.WebAPI.Middleware;
using ScrumMasters.Webshop.WebAPI.PolicyHandlers;

namespace ScrumMasters.Webshop.WebAPI
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
            services.AddControllers();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo {Title = "ScrumMasters.Webshop.WebApi", Version = "v1"});
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description =
                        "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });
            });
            // Dependency Injection.
            services.AddDbContext<MainDbContext>(opt =>
            {
                opt.UseSqlite("Data Source=main.db").LogTo(Console.WriteLine);
            });
            services.AddDbContext<AuthDbContext>(opt => { opt.UseSqlite("Data Source=auth.db"); });
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IColorRepository, ColorRepository>();
            services.AddScoped<IColorService, ColorService>();
            services.AddScoped<ISizeRepository, SizeRepository>();
            services.AddScoped<ISizeService, SizeService>();
            services.AddScoped<IImageRepository, ImageRepository>();
            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IMainDbSeeder, MainDbSeeder>();
            services.AddScoped<IInventoryStockService, InventoryStockService>();
            services.AddScoped<IInventoryStockRepository, InventoryStockRepository>();

            // Dependency Injection for security.
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IAuthDbSeeder, AuthDbSeeder>();

            //
            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["JwtConfig:Issuer"],
                    ValidAudience = Configuration["JwtConfig:Audience"],
                    IssuerSigningKey =
                        new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(Configuration["JwtConfig:Secret"]))
                };
            });
            services.AddSingleton<IAuthorizationHandler, CanManageCategoriesHandler>();
            services.AddSingleton<IAuthorizationHandler, CanManageProductsHandler>();
            services.AddSingleton<IAuthorizationHandler, CanManageSizesHandler>();
            services.AddSingleton<IAuthorizationHandler, CanManageColorsHandler>();
            services.AddSingleton<IAuthorizationHandler, IsLoggedInHandler>();
            services.AddAuthorization(options =>
            {
                options.AddPolicy(nameof(CanManageCategoriesHandler),
                    policy => policy.Requirements.Add(new CanManageCategoriesHandler()));
                options.AddPolicy(nameof(CanManageProductsHandler),
                    policy => policy.Requirements.Add(new CanManageProductsHandler()));
                options.AddPolicy(nameof(CanManageSizesHandler),
                    policy => policy.Requirements.Add(new CanManageSizesHandler()));
                options.AddPolicy(nameof(CanManageColorsHandler),
                    policy => policy.Requirements.Add(new CanManageColorsHandler()));
                options.AddPolicy(nameof(IsLoggedInHandler),
                    policy => policy.Requirements.Add(new IsLoggedInHandler()));
            });
            services.AddCors(options =>
            {
                options.AddPolicy("Development-cors", devPolicy =>
                {
                    devPolicy
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .WithExposedHeaders("X-Pagination");
                });
                options.AddPolicy("Production-cors", prodPolicy =>
                {
                    prodPolicy
                        .WithOrigins("https://rf-front-end-master.azurewebsites.net")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .WithExposedHeaders("X-Pagination");
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app, IWebHostEnvironment env,
            IMainDbSeeder mainDbSeeder,
            IAuthDbSeeder authDbSeeder)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Innotech.LegosforLife.WebApi v1"));
                app.UseCors("Development-cors");
                
                mainDbSeeder.SeedDevelopment();
                authDbSeeder.SeedDevelopment();
            }
            else
            {
                mainDbSeeder.SeedProduction();
                authDbSeeder.SeedProduction();
                app.UseCors("Production-cors");
            }

            app.UseRouting();
            app.UseMiddleware<JWTMiddleware>();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider =
                    new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Resources")),
                RequestPath = new PathString("/Resources")
            });
        }
    }
}
