using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Mongo2Go;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;
using MongoDB.Driver;
using SuppliersManager.Application.Models.Settings;
using SuppliersManager.Api.Configurations;

namespace SuppliersManager.IntegrationTesting
{
    public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
    {
        private readonly MongoDbRunner _mongoRunner;

        public CustomWebApplicationFactory()
        {
            _mongoRunner = MongoDbRunner.Start(); // Iniciar MongoDB en memoria
        }
        
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(async services =>
            {
                // Reemplazar la configuración de MongoDB para pruebas
                services.Configure<MongoDBSettings>(options =>
                {
                    options.ConnectionURI = _mongoRunner.ConnectionString;
                    options.DatabaseName = "TestDatabase";
                });

                // Crear el cliente y el contexto de MongoDB en memoria
                var sp = services.BuildServiceProvider();
                using var scope = sp.CreateScope();
                var database = scope.ServiceProvider.GetRequiredService<IMongoDatabase>();

                await database.CreateCollectionAsync("users");

                string textUsers = await File.ReadAllTextAsync(@"Imports\suppliersdb.users.json");
                var document = BsonSerializer.Deserialize<IList<BsonDocument>>(textUsers);
                var collection = database.GetCollection<BsonDocument>("users");
                await collection.InsertManyAsync(document);

                //Add Suppliers
                await database.CreateCollectionAsync("suppliers");

                string textSuppliers = await File.ReadAllTextAsync(@"Imports\suppliersdb.suppliers.json");
                document = BsonSerializer.Deserialize<IList<BsonDocument>>(textSuppliers);
                collection = database.GetCollection<BsonDocument>("suppliers");
                await collection.InsertManyAsync(document);

            });
        }

        //public void Dispose()
        //{
        //    _mongoRunner.Dispose(); // Detener MongoDB en memoria al finalizar las pruebas
        //    base.Dispose();
        //}
    }
}
