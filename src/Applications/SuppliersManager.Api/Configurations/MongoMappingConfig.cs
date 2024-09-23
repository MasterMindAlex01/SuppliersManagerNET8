using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using SuppliersManager.Domain.Contracts;

namespace SuppliersManager.Api.Configurations
{
    public static class MongoMappingConfig
    {
        public static void RegisterMappings()
        {

            if (!BsonClassMap.IsClassMapRegistered(typeof(BaseEntity)))
            {
                BsonClassMap.RegisterClassMap<BaseEntity>(cm =>
                {
                    cm.AutoMap();
                    cm.GetMemberMap(x => x.Id)
                      .SetIdGenerator(StringObjectIdGenerator.Instance)
                      .SetSerializer(new StringSerializer(BsonType.ObjectId));
                });
            }

            // Registrar otros mapeos si es necesario...
        }
    }
}
