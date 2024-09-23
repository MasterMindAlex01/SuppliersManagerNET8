
namespace SuppliersManager.Application.Models.Responses.Suppliers
{
    public class SupplierResponse
    {
        public string Id { get; set; } = null!;
        public string TIN { get; set; } = null!;
        public string RegisteredName { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string City { get; set; } = null!;
        public string State { get; set; } = null!;
        public string Email { get; set; } = null!;
        public bool IsActive { get; set; }
    }
}
