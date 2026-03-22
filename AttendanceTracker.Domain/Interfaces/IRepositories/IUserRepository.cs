using AttendanceTracker.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace AttendanceTracker.Domain.Interfaces.IRepositories
{
    public interface IUserRepository
    {
        Task<IList<User>> GetAllUsersRepo();
        Task<User> GetUsersByIdRepo(int id);

        Task<User> AddNewUserRepo(User user);

        Task<User> UpdateUserRepo(User user);

        Task<int> DeleteUserRepo(int id);
        Task<User> GetByEmail(string email);
    }
}
