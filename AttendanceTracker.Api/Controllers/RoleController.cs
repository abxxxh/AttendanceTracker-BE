using AttendanceTracker.Application.Dtos.Role;
using AttendanceTracker.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace AttendanceTracker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _role;
        private readonly ILogger<RoleController> _logger;

        public RoleController(
            IRoleService role,
            ILogger<RoleController> logger)
        {
            _role = role;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployeesAsync()
        {
            _logger.LogInformation("Get All Roles API called");

            try
            {
                var roles = await _role.GetAllRolesService();

                _logger.LogInformation(
                    "Roles fetched successfully. Count: {Count}",
                    roles.Count);

                return Ok(new { roles = roles, message = "All Roles" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Error occurred while fetching all roles");

                return BadRequest(new { message = "Something Went Wrong" });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRolesById(int id)
        {
            _logger.LogInformation(
                "Get Role By Id API called with Id: {Id}",
                id);

            try
            {
                var roles = await _role.GetRolesByIdService(id);

                if (roles == null)
                {
                    _logger.LogWarning(
                        "Role not found with Id: {Id}",
                        id);

                    return NotFound(new { message = "User Not Found" });
                }

                _logger.LogInformation(
                    "Role fetched successfully with Id: {Id}",
                    id);

                return Ok(roles);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Error occurred while fetching role with Id: {Id}",
                    id);

                return BadRequest(new { message = $"Unable to find {id}" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddNewRoles(RoleRequestDto role)
        {
            _logger.LogInformation(
                "Create Role API called for RoleName: {RoleName}",
                role.RoleName);

            try
            {
                var createdRole = await _role.AddNewRoleService(role);

                _logger.LogInformation(
                    "Role created successfully with Id: {Id}",
                    createdRole.RoleId);

                return CreatedAtAction(
                    nameof(GetRolesById),
                    new { id = createdRole.RoleId },
                    createdRole
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Error occurred while creating role with RoleName: {RoleName}",
                    role.RoleName);

                return BadRequest(new { message = "Something went wrong while adding role" });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRole(int id, RoleRequestDto role)
        {
            _logger.LogInformation(
                "Update Role API called for Id: {Id}",
                id);

            try
            {
                var updatedRole = await _role.UpdatesRolesService(id, role);

                if (updatedRole == null)
                {
                    _logger.LogWarning(
                        "Role not found for update with Id: {Id}",
                        id);

                    return NotFound(new { message = "Role Not Found" });
                }

                _logger.LogInformation(
                    "Role updated successfully with Id: {Id}",
                    id);

                return Ok(new { message = "Role Updated Successfully", data = updatedRole });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Error occurred while updating role with Id: {Id}",
                    id);

                return BadRequest(new { message = "Something went wrong while updating role" });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            _logger.LogInformation(
                "Delete Role API called for Id: {Id}",
                id);

            try
            {
                var result = await _role.DeleteRoleService(id);

                if (result == 0)
                {
                    _logger.LogWarning(
                        "Role not found for delete with Id: {Id}",
                        id);

                    return NotFound(new { message = "Role Not Found" });
                }

                _logger.LogInformation(
                    "Role deleted successfully with Id: {Id}",
                    id);

                return Ok(new { message = "Role Deleted Successfully" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Error occurred while deleting role with Id: {Id}",
                    id);

                return BadRequest(new { message = "Something went wrong while deleting role" });
            }
        }
    }
}