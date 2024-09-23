using MongoDB.Driver;
using SuppliersManager.Application.Extensions;
using SuppliersManager.Application.Interfaces.Repositories;
using SuppliersManager.Application.Models.Responses.Users;
using SuppliersManager.Domain.Entities;
using SuppliersManager.Shared.Wrapper;

namespace SuppliersManager.Infrastructure.MongoDBDriver.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> _collection;

        public UserRepository(IMongoDatabase database)
        {
            _collection = database.GetCollection<User>("users");
        }

        public async Task<User> GetUserByUserNameAsync(string username)
        {
            return await _collection.Find(x => x.UserName == username).FirstOrDefaultAsync();
        }

        public async Task<PaginatedResult<UserResponse>> GetPagedResponseAsync(int pageNumber, int pageSize)
        {
            var paged =  await _collection.Find(_ => true).ToPaginatedListAsync(pageNumber, pageSize);
            
            var list =  paged.Data.Select(x => new UserResponse
            {
                Id = x.Id,
                Email = x.Email,
                FirstName = x.FirstName,
                LastName = x.LastName,
                UserName = x.UserName
            }).ToList();

            return new PaginatedResult<UserResponse>(list)
            {
                CurrentPage = paged.CurrentPage,
                PageSize = paged.PageSize,
                Messages = paged.Messages,
                Succeeded = paged.Succeeded,
                TotalCount = paged.TotalCount,
                TotalPages = paged.TotalPages,
                
            };
        }
    }
}
