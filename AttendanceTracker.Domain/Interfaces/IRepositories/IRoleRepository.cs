using AttendanceTracker.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace AttendanceTracker.Domain.Interfaces.IRepositories
{
    public interface IRoleRepository
    {
        Task<IList<Role>> GetAllRoles();
        Task<Role> GetRolesById(int id);
        Task<Role> AddNewRole(Role role);
        Task<Role> UpdateRole(Role role);

        Task<int> DeleteRole(int id);

    }
}
