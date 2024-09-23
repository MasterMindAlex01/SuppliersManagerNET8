using MongoDB.Bson;

namespace SuppliersManager.Domain.Contracts
{
    public class BaseEntity
    {
        public ObjectId Id { get; set; }
    }
}
