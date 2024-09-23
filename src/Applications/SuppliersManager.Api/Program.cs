using SuppliersManager.Api.Configurations;
using SuppliersManager.Api.Extensions;
using SuppliersManager.Application.Extensions;
using SuppliersManager.Infrastructure.MongoDBDriver.Extensions;

namespace SuppliersManager.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            
            var builder = WebApplication.CreateBuilder(args);

            builder.AddConfigurations();

            builder.Services.AddCors();
            builder.Services.AddControllers();

            // Add services to the container.
            builder.Services.AddCurrentUserService();

            MongoMappingConfig.RegisterMappings();
            
            builder.Services.AddSettings(builder.Configuration);
            
            //AddDatabase
            builder.Services.AddMongoDatabase();

            //Configurations Application Layer
            builder.Services.AddApplicationLayer();

            builder.Services.AddRepositories();

            builder.Services.AddApplicationServices();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.RegisterSwagger();

            builder.Services.AddJwtAuthentication(builder.Configuration);

            var app = builder.Build();

            app.UseCors();

            app.UseSwagger();
            app.UseSwaggerUI();


            //app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
