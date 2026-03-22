using AttendanceTracker.Application.Dtos.Role;
using System;
using System.Collections.Generic;
using System.Text;

namespace AttendanceTracker.Application.Interfaces
{
    public interface IRoleService
    {
        Task<IList<RoleResponseDto>> GetAllRolesService();
        Task<RoleResponseDto> GetRolesByIdService(int id);

        Task<RoleResponseDto> AddNewRoleService(RoleRequestDto role);

        Task<RoleResponseDto> UpdatesRolesService(int id,RoleRequestDto role);
        Task<int> DeleteRoleService(int id);
    }
}
