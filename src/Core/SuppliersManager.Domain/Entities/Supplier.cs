using SuppliersManager.Domain.Contracts;

namespace SuppliersManager.Domain.Entities
{
    public class Supplier : BaseEntity
    {
        public string TIN { get; set; } = null!;
        public string RegisteredName { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string City { get; set; } = null!;
        public string State { get; set; } = null!;
        public string Email { get; set; } = null!;
        public bool IsActive { get; set; }
        public DateTime CreationDate { get; set; }
        public string CreateByContact { get; set; } = null!;
        public string EmailContact { get; set; } = null!;
    }
}
