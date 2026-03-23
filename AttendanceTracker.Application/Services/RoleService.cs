using AttendanceTracker.Application.Dtos.Role;
using AttendanceTracker.Application.Interfaces;
using AttendanceTracker.Domain.Entity;
using AttendanceTracker.Domain.Interfaces.IRepositories;
using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AttendanceTracker.Application.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepo;
        private readonly IMapper _map;
        private readonly ILogger<RoleService> _logger;

        public RoleService(
            IRoleRepository repo,
            IMapper map,
            ILogger<RoleService> logger)
        {
            _roleRepo = repo;
            _map = map;
            _logger = logger;
        }

        public async Task<RoleResponseDto> AddNewRoleService(RoleRequestDto role)
        {
            _logger.LogInformation(
                "AddNewRoleService started for RoleName: {RoleName}",
                role.RoleName);

            try
            {
                var entity = _map.Map<Role>(role);

                var response = await _roleRepo.AddNewRole(entity);

                _logger.LogInformation(
                    "Role added successfully with Id: {Id}",
                    response.RoleId);

                return _map.Map<RoleResponseDto>(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Error occurred in AddNewRoleService for RoleName: {RoleName}",
                    role.RoleName);

                throw;
            }
        }

        public async Task<int> DeleteRoleService(int id)
        {
            _logger.LogInformation(
                "DeleteRoleService started for Id: {Id}",
                id);

            try
            {
                var response = await _roleRepo.DeleteRole(id);

                if (response == 0)
                {
                    _logger.LogWarning(
                        "DeleteRoleService: Role not found for Id: {Id}",
                        id);
                }
                else
                {
                    _logger.LogInformation(
                        "Role deleted successfully with Id: {Id}",
                        id);
                }

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Error occurred in DeleteRoleService for Id: {Id}",
                    id);

                throw;
            }
        }

        public async Task<IList<RoleResponseDto>> GetAllRolesService()
        {
            _logger.LogInformation(
                "GetAllRolesService started");

            try
            {
                var entity = await _roleRepo.GetAllRoles();

                _logger.LogInformation(
                    "Roles fetched successfully. Count: {Count}",
                    entity.Count);

                var roles = _map.Map<IList<RoleResponseDto>>(entity);

                return roles;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Error occurred in GetAllRolesService");

                throw;
            }
        }

        public async Task<RoleResponseDto> GetRolesByIdService(int id)
        {
            _logger.LogInformation(
                "GetRolesByIdService started for Id: {Id}",
                id);

            try
            {
                var entity = await _roleRepo.GetRolesById(id);

                if (entity == null)
                {
                    _logger.LogWarning(
                        "GetRolesByIdService: Role not found for Id: {Id}",
                        id);

                    return null;
                }

                _logger.LogInformation(
                    "Role fetched successfully with Id: {Id}",
                    id);

                return _map.Map<RoleResponseDto>(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Error occurred in GetRolesByIdService for Id: {Id}",
                    id);

                throw;
            }
        }

        public async Task<RoleResponseDto> UpdatesRolesService(int id, RoleRequestDto role)
        {
            _logger.LogInformation(
                "UpdatesRolesService started for Id: {Id}",
                id);

            try
            {
                var existing = await _roleRepo.GetRolesById(id);

                if (existing == null)
                {
                    _logger.LogWarning(
                        "UpdatesRolesService: Role not found for Id: {Id}",
                        id);

                    return null;
                }

                var entity = _map.Map<Role>(role);
                entity.RoleId = id;

                var updated = await _roleRepo.UpdateRole(entity);

                _logger.LogInformation(
                    "Role updated successfully with Id: {Id}",
                    id);

                return _map.Map<RoleResponseDto>(updated);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Error occurred in UpdatesRolesService for Id: {Id}",
                    id);

                throw;
            }
        }
    }
}