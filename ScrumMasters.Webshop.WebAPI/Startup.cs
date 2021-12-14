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
            services.AddDbContext<MainDbContext>(opt => { opt.UseSqlite("Data Source=main.db").LogTo(Console.WriteLine); });
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
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();
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
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidAudience = Configuration["Jwt:Audience"],
                    IssuerSigningKey =
                        new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(Configuration["Jwt:Key"])) //Configuration["JwtToken:SecretKey"]
                };
            });
            services.AddSingleton<IAuthorizationHandler, AdminHandler>();
            services.AddSingleton<IAuthorizationHandler, CanManageAccountHandler>();
            services.AddSingleton<IAuthorizationHandler, CanManageCategoriesHandler>();
            services.AddSingleton<IAuthorizationHandler, CanManageProductsHandler>();
            services.AddSingleton<IAuthorizationHandler, CanManageUsersHandler>();
            services.AddSingleton<IAuthorizationHandler, CanManageSizesHandler>();
            services.AddSingleton<IAuthorizationHandler, CanManageColorsHandler>();
            services.AddAuthorization(options =>
            {
                options.AddPolicy(nameof(AdminHandler),
                    policy => policy.Requirements.Add(new AdminHandler()));
                options.AddPolicy(nameof(CanManageCategoriesHandler),
                    policy => policy.Requirements.Add(new CanManageCategoriesHandler()));
                options.AddPolicy(nameof(CanManageProductsHandler),
                    policy => policy.Requirements.Add(new CanManageProductsHandler()));
                options.AddPolicy(nameof(CanManageUsersHandler),
                    policy => policy.Requirements.Add(new CanManageUsersHandler()));
                options.AddPolicy(nameof(CanManageSizesHandler),
                    policy => policy.Requirements.Add(new CanManageSizesHandler()));
                options.AddPolicy(nameof(CanManageColorsHandler),
                    policy => policy.Requirements.Add(new CanManageColorsHandler()));
                options.AddPolicy(nameof(CanManageAccountHandler),
                    policy => policy.Requirements.Add(new CanManageAccountHandler()));
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
            MainDbContext mainContext, AuthDbContext authDbContext)
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
                ProductEntity pe1 = new ProductEntity
                {
                    ProductName = "P1", ProductPrice=250,ProductFeatured = true, Categories = new List<CategoryEntity>(),
                    Sizes = new List<SizeEntity>(), Colors = new List<ColorEntity>(), Images = new List<ImageEntity>()
                };
                ProductEntity pe2 = new ProductEntity
                {
                    ProductName = "P2" ,ProductPrice=200, Categories = new List<CategoryEntity>(), Sizes = new List<SizeEntity>(),
                    Colors = new List<ColorEntity>(), Images = new List<ImageEntity>()
                };
                ProductEntity pe3 = new ProductEntity
                {
                    ProductName = "P3",ProductPrice=350, Categories = new List<CategoryEntity>(), Sizes = new List<SizeEntity>(),
                    Colors = new List<ColorEntity>(), Images = new List<ImageEntity>()
                };

                CategoryEntity ce1 = new CategoryEntity {Name = "Bukser"};
                CategoryEntity ce2 = new CategoryEntity {Name = "Sko"};
                CategoryEntity ce3 = new CategoryEntity {Name = "Kjoler"};

                ColorEntity color1 = new ColorEntity {Title = "Rød",ColorString= "red"};
                ColorEntity color2 = new ColorEntity {Title = "Blå",ColorString="blue"};
                ColorEntity color3 = new ColorEntity {Title = "Gul",ColorString="yellow"};
                ColorEntity color4 = new ColorEntity {Title = "Grøn",ColorString="green"};
                ColorEntity color5 = new ColorEntity {Title = "Grå",ColorString="grey"};

                SizeEntity se1 = new SizeEntity {Title = "30/30"};
                SizeEntity se2 = new SizeEntity {Title = "25"};
                SizeEntity se3 = new SizeEntity {Title = "30"};

                ImageEntity ie1 = new ImageEntity
                {
                    Title = "Some title",
                    Tags = "Some, Tags, Hey, World",
                    Path = "test2.jpg",
                    Desc = "Some description",
                };
                
                ImageEntity ie2 = new ImageEntity {
                    Title = "Some title34",
                    Tags = "Some, Tags, Hey34, World",
                    Path = "test1.jpg",
                    Desc = "Some description34",
                };
                
                ImageEntity ie3 = new ImageEntity
                {
                    Title = "Some title",
                    Tags = "Some, Tags, Hey, World",
                    Path = "billed1.jpg",
                    Desc = "Some description",
                };
                
                ImageEntity ie4 = new ImageEntity
                {
                    Title = "Some title",
                    Tags = "Some, Tags, Hey, World",
                    Path = "billed2.jpg",
                    Desc = "Some description",
                };
                
                pe1.Categories.Add(ce1);
                pe1.Categories.Add(ce2);
                pe1.Colors.Add(color1);
                pe1.Colors.Add(color5);
                pe1.Sizes.Add(se1);
                pe1.Images.Add(ie1);
                pe1.Images.Add(ie4);

                pe2.Categories.Add(ce2);
                pe2.Colors.Add(color2);
                pe2.Colors.Add(color1);
                pe2.Sizes.Add(se1);
                pe2.Sizes.Add(se2);
                pe2.Images.Add(ie2);
                
                pe3.Categories.Add(ce3);
                pe3.Colors.Add(color3);
                pe3.Colors.Add(color4);
                pe3.Sizes.Add(se3);
                pe3.Images.Add(ie3);


                mainContext.Products.AddRange(pe1, pe2, pe3);
                mainContext.SaveChanges();

                authDbContext.Database.EnsureDeleted();
                authDbContext.Database.EnsureCreated();
                AuthService.CreateHashAndSalt("123456", out var passwordHash, out var salt);

                authDbContext.LoginUsers.Add(new LoginUser
                {
                    Email = "ljuul@ljuul.dk",
                    HashedPassword = passwordHash,
                    PasswordSalt = salt,
                    DbUserId = 1,
                });
                AuthService.CreateHashAndSalt("123456", out passwordHash, out salt);
                authDbContext.LoginUsers.Add(new LoginUser
                {
                    Email = "ljuul2@ljuul.dk",
                    HashedPassword = passwordHash,
                    PasswordSalt = salt,
                    DbUserId = 2,
                });
                authDbContext.Permissions.AddRange(new Permission()
                {
                    Name = "Admin"
                }, new Permission()
                {
                    Name = "CanManageProducts"
                }, new Permission()
                {
                    Name = "CanManageCategories"
                }, new Permission()
                {
                    Name = "CanManageAccount"
                }, new Permission());
                authDbContext.SaveChanges();
                authDbContext.UserPermissions.Add(new UserPermission {PermissionId = 1, UserId = 1});
                authDbContext.UserPermissions.Add(new UserPermission {PermissionId = 2, UserId = 1});
                authDbContext.UserPermissions.Add(new UserPermission {PermissionId = 3, UserId = 2});
                authDbContext.UserPermissions.Add(new UserPermission {PermissionId = 4, UserId = 2});
    

                #endregion

                app.UseMiddleware<JWTMiddleware>();

                app.UseHttpsRedirection();

                app.UseStaticFiles();
                app.UseStaticFiles(new StaticFileOptions()
                {
                    FileProvider =
                        new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Resources")),
                    RequestPath = new PathString("/Resources")
                });

                app.UseRouting();

                app.UseAuthentication();
                app.UseAuthorization();

                app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            }
        }
    }
}