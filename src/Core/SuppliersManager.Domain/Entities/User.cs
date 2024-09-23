
using SuppliersManager.Domain.Contracts;

namespace SuppliersManager.Domain.Entities
{
    public class User: BaseEntity
    {
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string PasswordSalt { get; set; } = null!;
    }
}
