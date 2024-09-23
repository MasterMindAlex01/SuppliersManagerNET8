
namespace SuppliersManager.Application.Models.Settings
{
    public class MongoDBSettings
    {
        public string ConnectionURI { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public List<MongoCollectionName> Collections { get; set; } = new List<MongoCollectionName>();
    }

    public class MongoCollectionName
    {
        public string CollectionName { get; set; } = null!;
    }
}
