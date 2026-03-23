using AttendanceTracker.Domain.Entity;
using AttendanceTracker.Domain.Interfaces.IRepositories;
using AttendanceTracker.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AttendanceTracker.Infrastructure.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly AttendanceTrackerDbContext _db;
        private readonly ILogger<RoleRepository> _logger;

        public RoleRepository(
            AttendanceTrackerDbContext db,
            ILogger<RoleRepository> logger)
        {
            _db = db;
            _logger = logger;
        }

        public async Task<Role> AddNewRole(Role role)
        {
            _logger.LogInformation(
                "Repository: Adding new role with RoleName: {RoleName}",
                role.RoleName);

            try
            {
                await _db.RolesTable.AddAsync(role);
                await _db.SaveChangesAsync();

                _logger.LogInformation(
                    "Repository: Role added successfully with Id: {Id}",
                    role.RoleId);

                return role;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Repository: Error while adding role with RoleName: {RoleName}",
                    role.RoleName);

                throw;
            }
        }

        public async Task<int> DeleteRole(int id)
        {
            _logger.LogInformation(
                "Repository: Delete role started for Id: {Id}",
                id);

            try
            {
                var result = await _db.RolesTable
                    .FirstOrDefaultAsync(x => x.RoleId == id);

                if (result == null)
                {
                    _logger.LogWarning(
                        "Repository: Role not found for delete with Id: {Id}",
                        id);

                    return 0;
                }

                _db.RolesTable.Remove(result);

                var response = await _db.SaveChangesAsync();

                _logger.LogInformation(
                    "Repository: Role deleted successfully with Id: {Id}",
                    id);

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Repository: Error while deleting role with Id: {Id}",
                    id);

                throw;
            }
        }

        public async Task<IList<Role>> GetAllRoles()
        {
            _logger.LogInformation(
                "Repository: Fetching all roles");

            try
            {
                var result = await _db.RolesTable.ToListAsync();

                _logger.LogInformation(
                    "Repository: Roles fetched successfully. Count: {Count}",
                    result.Count);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Repository: Error while fetching all roles");

                throw;
            }
        }

        public async Task<Role> GetRolesById(int id)
        {
            _logger.LogInformation(
                "Repository: Fetching role by Id: {Id}",
                id);

            try
            {
                var result = await _db.RolesTable
                    .FirstOrDefaultAsync(x => x.RoleId == id);

                if (result == null)
                {
                    _logger.LogWarning(
                        "Repository: Role not found with Id: {Id}",
                        id);
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Repository: Error while fetching role with Id: {Id}",
                    id);

                throw;
            }
        }

        public async Task<Role> UpdateRole(Role role)
        {
            _logger.LogInformation(
                "Repository: Updating role with Id: {Id}",
                role.RoleId);

            try
            {
                _db.RolesTable.Update(role);

                await _db.SaveChangesAsync();

                _logger.LogInformation(
                    "Repository: Role updated successfully with Id: {Id}",
                    role.RoleId);

                return role;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Repository: Error while updating role with Id: {Id}",
                    role.RoleId);

                throw;
            }
        }
    }
}