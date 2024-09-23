using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SuppliersManager.Api.Services;
using SuppliersManager.Application.Interfaces.Services;
using SuppliersManager.Application.Models.Settings;
using SuppliersManager.Infrastructure.MongoDbEF.Contexts;
using SuppliersManager.Infrastructure.MongoDbEF.Services;
using System.Text;

namespace SuppliersManager.Api.Extensions
{
    internal static class ServiceCollectionExtensions
    {
        internal static WebApplicationBuilder AddConfigurations(this WebApplicationBuilder builder)
        {
            var env = builder.Environment;
            builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                    .AddEnvironmentVariables();
            
            return builder;
        }

        internal static IServiceCollection AddSettings(
            this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MongoDBSettings>(configuration.GetSection("MongoDB"));
            services.AddSingleton<IPasswordHasherSettings, PasswordHasherSettings>();

            return services;
        }

        internal static IServiceCollection AddMongoDatabase(
            this IServiceCollection services, IConfiguration configuration)
        {

            var mongoDBSettings = configuration.GetSection("MongoDB").Get<MongoDBSettings>();

            return services.AddDbContext<ApplicationMongoDbContext>(options =>
            {
                options.UseMongoDB(mongoDBSettings!.ConnectionURI ?? "", mongoDBSettings.DatabaseName ?? "");
            });
            
            //return services.AddSingleton(sp =>
            //{
            //    var mongoSettings = sp.GetService<IOptions<MongoDBSettings>>();
            //    var connectionString = mongoSettings!.Value.ConnectionURI;
            //    var client = new MongoClient(connectionString);
            //    var database = mongoSettings!.Value.DatabaseName;
            //    return client.GetDatabase(database);
            //});
        }

        internal static IServiceCollection AddCurrentUserService(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            return services;
        }

        internal static void RegisterSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "You api title", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header,
                    },
                    new List<string>()
                    }
                });
                //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                //c.IncludeXmlComments(xmlPath);
            });
        }

        internal static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, MongoEFAuthService>();
            services.AddScoped<IUserService, MongoEFUserService>();
            services.AddScoped<ISupplierService, MongoEFSupplierService>();

            return services;
        }

        internal static IServiceCollection AddJwtAuthentication(
            this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddAuthorization()
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!)
                        )
                };
            }).Services;
        }

    }
}
