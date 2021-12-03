using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
                         Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
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
                             new string[] {}
                         }
                     });
            });
            services.AddDbContext<MainDbContext>(opt => { opt.UseSqlite("Data Source=main.db"); });
            services.AddDbContext<AuthDbContext>(opt =>
            {
                opt.UseSqlite("Data Source=auth.db"); 
            });
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IAuthService, AuthService>();
            //
            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options => {
                 options.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuer = true,
                     ValidateAudience = true,
                     ValidateLifetime = false,
                    ValidateIssuerSigningKey = true,
                     ValidIssuer = Configuration["Jwt:Issuer"],
                     ValidAudience = Configuration["Jwt:Audience"],
                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"])) //Configuration["JwtToken:SecretKey"]
                 };
            });
             services.AddSingleton<IAuthorizationHandler, CanWriteProductsHandler>();
             services.AddSingleton<IAuthorizationHandler, CanReadProductsHandler>();
             services.AddAuthorization(options =>
             {
                 options.AddPolicy(nameof(CanWriteProductsHandler), 
                     policy => policy.Requirements.Add(new CanWriteProductsHandler()));
                 options.AddPolicy(nameof(CanReadProductsHandler), 
                     policy => policy.Requirements.Add(new CanReadProductsHandler()));
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
                        .WithOrigins("https://ruds-fashion2021.azurewebsites.net/")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .WithExposedHeaders("X-Pagination");
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app, IWebHostEnvironment env,
            MainDbContext mainContext,AuthDbContext authDbContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Innotech.LegosforLife.WebApi v1"));
                app.UseCors("Development-cors");


                #region Setup Contexts

                mainContext.Database.EnsureDeleted();
                mainContext.Database.EnsureCreated();
                mainContext.SaveChanges();
                mainContext.Products.AddRange(
                    new ProductEntity {ProductName = "P1"},
                    new ProductEntity {ProductName = "P2"},
                    new ProductEntity {ProductName = "P3"});
                mainContext.Categories.AddRange(
                        new CategoryEntity {Name = "Bukser"},
                        new CategoryEntity {Name = "Sko"},
                        new CategoryEntity {Name = "Kjoler"});
                mainContext.SaveChanges();

                authDbContext.Database.EnsureDeleted();
                authDbContext.Database.EnsureCreated();
                AuthService.CreateHashAndSalt("123456", out var passwordHash, out var salt);
                authDbContext.LoginUsers.Add(new LoginUser
                {
                     UserName = "ljuul",
                     HashedPassword = passwordHash,
                     PasswordSalt = salt,
                     DbUserId = 1,
                 });
                 AuthService.CreateHashAndSalt("123456", out passwordHash, out salt);
                 authDbContext.LoginUsers.Add(new LoginUser
                 {
                     UserName = "ljuul2",
                     HashedPassword = passwordHash,
                     PasswordSalt = salt,
                     DbUserId = 2,
                 });
                 authDbContext.Permissions.AddRange(new Permission()
                 {
                     Name = "CanWriteProducts"
                 }, new Permission()
                 {
                     Name = "CanReadProducts"
                 });
                 authDbContext.SaveChanges();
                  authDbContext.UserPermissions.Add(new UserPermission { PermissionId = 1, UserId = 1 });
                  authDbContext.UserPermissions.Add(new UserPermission { PermissionId = 2, UserId = 1 });
                 authDbContext.UserPermissions.Add(new UserPermission { PermissionId = 2, UserId = 2 });
                 authDbContext.SaveChanges();

                #endregion
                
                app.UseMiddleware<JWTMiddleware>();
            
                app.UseHttpsRedirection();

                app.UseRouting();

                app.UseAuthentication();
                app.UseAuthorization();

                app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            }
        }
    }
}