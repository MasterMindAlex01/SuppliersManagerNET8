using Microsoft.EntityFrameworkCore;
using MongoDB.EntityFrameworkCore.Extensions;
using SuppliersManager.Domain.Entities;

namespace SuppliersManager.Infrastructure.MongoDbEF.Contexts
{
    public class ApplicationMongoDbContext : DbContext
    {
        public ApplicationMongoDbContext(
            DbContextOptions<ApplicationMongoDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            builder.Entity<User>().ToCollection("users");

            builder.Entity<Supplier>().ToCollection("suppliers");

        }


    }
}
