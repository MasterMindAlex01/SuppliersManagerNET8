using SuppliersManager.Application.Features.Users.Commands;
using SuppliersManager.Application.Helpers;
using SuppliersManager.Application.Interfaces.Repositories;
using SuppliersManager.Application.Interfaces.Services;
using SuppliersManager.Application.Models.Responses.Users;
using SuppliersManager.Application.Models.Settings;
using SuppliersManager.Domain.Entities;
using SuppliersManager.Shared.Wrapper;

namespace SuppliersManager.Infrastructure.MongoDBDriver.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly string _pepper;
        private readonly int _iteration;

        public UserService(
            IUnitOfWork unitOfWork, 
            IUserRepository userRepository, 
            IPasswordHasherSettings passwordHasherSettings)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _pepper = passwordHasherSettings.Pepper;
            _iteration = passwordHasherSettings.Iteration;
        }

        public async Task<IResult> AddAsync(CreateUserCommand entity)
        {
            var user = new User()
            {
                Email = entity.Email,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Password = entity.Password,
                PasswordSalt = PasswordHasher.GenerateSalt(),
                UserName = entity.UserName,
            };
            user.Password = PasswordHasher.ComputeHash(
                user.Password, user.PasswordSalt, _pepper, _iteration);

            await _unitOfWork.Repository<User>().AddAsync(user);

            return await Result<string>.SuccessAsync(user.Id,"User created");
        }

        public async Task<IResult> DeleteAsync(string id)
        {
            var currentUser = await _unitOfWork.Repository<User>().GetByIdAsync(id);
            if (currentUser == null) return await Result.FailAsync("User not found");

            await _unitOfWork.Repository<User>().DeleteAsync(currentUser);

            return await Result.SuccessAsync("User deleted");
        }

        public async Task<PaginatedResult<UserResponse>> GetAllAsync(int pageNumber, int pageSize)
        {
            return await _userRepository.GetPagedResponseAsync(pageNumber, pageSize);
        }

        public async Task<IResult<UserResponse>> GetByIdAsync(string id)
        {
            var currentUser = await _unitOfWork.Repository<User>().GetByIdAsync(id);
            if (currentUser == null) return await Result<UserResponse>.FailAsync("User not found");
            var userResponse = new UserResponse()
            {
                Id = currentUser.Id,
                Email = currentUser.Email,
                FirstName = currentUser.FirstName,
                LastName = currentUser.LastName,
                UserName = currentUser.UserName,
            };
            return await Result<UserResponse>.SuccessAsync(userResponse, "Ok");
        }

        public async Task<IResult> UpdateAsync(UpdateUserCommand command)
        {
            var currentUser = await _unitOfWork.Repository<User>().GetByIdAsync(command.Id);
            if (currentUser == null) return await Result.FailAsync("User not found");

            currentUser.Email = command.Email;
            currentUser.FirstName = command.FirstName;
            currentUser.LastName = command.LastName;

            await _unitOfWork.Repository<User>().UpdateAsync(currentUser);
            
            return await Result.SuccessAsync("User updated");
        }
    }
}
