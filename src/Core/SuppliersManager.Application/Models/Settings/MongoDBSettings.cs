
namespace SuppliersManager.Application.Models.Settings
{
    public class MongoDBSettings
    {
        public string ConnectionURI { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
    }

    public class MongoCollectionName
    {
        public string CollectionName { get; set; } = null!;
    }
}
