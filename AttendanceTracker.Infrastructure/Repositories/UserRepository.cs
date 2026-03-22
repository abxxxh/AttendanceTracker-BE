using AttendanceTracker.Domain.Entity;
using AttendanceTracker.Domain.Interfaces.IRepositories;
using AttendanceTracker.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AttendanceTracker.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AttendanceTrackerDbContext _userRepo;
        private readonly ILogger<UserRepository> _logger;

        public UserRepository(
            AttendanceTrackerDbContext db,
            ILogger<UserRepository> logger)
        {
            _userRepo = db;
            _logger = logger;
        }

        public async Task<User> AddNewUserRepo(User user)
        {
            _logger.LogInformation(
                "Repository: Adding new user with Email: {Email}",
                user.Email);

            try
            {
                await _userRepo.UsersTable.AddAsync(user);
                await _userRepo.SaveChangesAsync();

                _logger.LogInformation(
                    "Repository: User saved successfully with Id: {Id}",
                    user.Id);

                return await _userRepo.UsersTable
                    .Include(u => u.Role)
                    .Include(u => u.UserDetails)
                    .FirstOrDefaultAsync(u => u.Id == user.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Repository: Error while adding user with Email: {Email}",
                    user.Email);
                throw;
            }
        }

        public async Task<int> DeleteUserRepo(int id)
        {
            _logger.LogInformation(
                "Repository: Delete user started for Id: {Id}",
                id);

            try
            {
                var entity = await GetUsersByIdRepo(id);

                if (entity == null)
                {
                    _logger.LogWarning(
                        "Repository: User not found for delete with Id: {Id}",
                        id);
                    return 0;
                }

                _userRepo.Remove(entity);
                var result = await _userRepo.SaveChangesAsync();

                _logger.LogInformation(
                    "Repository: User deleted successfully with Id: {Id}",
                    id);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Repository: Error while deleting user with Id: {Id}",
                    id);
                throw;
            }
        }

        public async Task<IList<User>> GetAllUsersRepo()
        {
            _logger.LogInformation(
                "Repository: Fetching all users");

            try
            {
                var users = await _userRepo.UsersTable
                    .Include(u => u.Role)
                    .Include(x => x.UserDetails)
                    .ToListAsync();

                _logger.LogInformation(
                    "Repository: Users fetched successfully. Count: {Count}",
                    users.Count);

                return users;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Repository: Error while fetching all users");
                throw;
            }
        }

        public async Task<User> GetByEmail(string email)
        {
            _logger.LogInformation(
                "Repository: Fetching user by Email: {Email}",
                email);

            try
            {
                var user = await _userRepo.UsersTable
                    .Include(x => x.Role)
                    .Include(x => x.UserDetails)
                    .FirstOrDefaultAsync(x => x.Email == email);

                if (user == null)
                {
                    _logger.LogWarning(
                        "Repository: User not found with Email: {Email}",
                        email);
                }

                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Repository: Error while fetching user with Email: {Email}",
                    email);
                throw;
            }
        }

        public async Task<User> GetUsersByIdRepo(int id)
        {
            _logger.LogInformation(
                "Repository: Fetching user by Id: {Id}",
                id);

            try
            {
                var response = await _userRepo.UsersTable
                    .Include(u => u.Role)
                    .Include(x => x.UserDetails)
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (response == null)
                {
                    _logger.LogWarning(
                        "Repository: User not found with Id: {Id}",
                        id);
                }

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Repository: Error while fetching user with Id: {Id}",
                    id);
                throw;
            }
        }

        public async Task<User> UpdateUserRepo(User user)
        {
            _logger.LogInformation(
                "Repository: Updating user with Id: {Id}",
                user.Id);

            try
            {
                _userRepo.UsersTable.Update(user);
                await _userRepo.SaveChangesAsync();

                _logger.LogInformation(
                    "Repository: User updated successfully with Id: {Id}",
                    user.Id);

                return await _userRepo.UsersTable
                    .Include(u => u.Role)
                    .Include(u => u.UserDetails)
                    .FirstOrDefaultAsync(x => x.Id == user.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Repository: Error while updating user with Id: {Id}",
                    user.Id);
                throw;
            }
        }
    }
}