using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using SuppliersManager.Application.Extensions;
using SuppliersManager.Application.Interfaces.Repositories;
using SuppliersManager.Application.Models.Responses.Users;
using SuppliersManager.Domain.Entities;
using SuppliersManager.Infrastructure.MongoDbEF.Contexts;
using SuppliersManager.Shared.Wrapper;

namespace SuppliersManager.Infrastructure.MongoDbEF.Repositories
{
    public class MongoEFUserRepository : IUserRepository
    {
        private readonly ApplicationMongoDbContext _dbContext;

        public MongoEFUserRepository(ApplicationMongoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User?> GetUserByUserNameAsync(string username)
        {
            return await _dbContext.Users.Where(x => x.UserName == username).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<PaginatedResult<UserResponse>> GetPagedResponseAsync(int pageNumber, int pageSize)
        {
            return  await _dbContext.Users.Select(x => new UserResponse
            {
                Id = x.Id.ToString(),
                Email = x.Email,
                FirstName = x.FirstName,
                LastName = x.LastName,
                UserName = x.UserName
            }).AsNoTracking().ToPaginatedListAsync(pageNumber, pageSize);
           
        }
    }
}
