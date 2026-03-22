using AttendanceTracker.Application.Dtos;
using AttendanceTracker.Application.Dtos.Login;
using AttendanceTracker.Application.Dtos.User;
using AttendanceTracker.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AttendanceTracker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;
        public UserController(IUserService service, ILogger<UserController> log)
        {
            _userService = service;
            _logger = log;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            _logger.LogInformation("Get All Users API called");
            try
            {
                var result = await _userService.GetAllService();
                _logger.LogInformation(
                    "Users fetched successfully. Count: {Count}",
                    result.Count);
                return Ok(new { message = "Success", result = result });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                   "Error occurred while fetching all users");
                return BadRequest(new { message = "Something went wrong" });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                _logger.LogInformation(
                "Get User By Id called with Id: {Id}",
                id);
                var result = await _userService.GetByIdService(id);
                if (result == null)
                {
                    _logger.LogWarning(
                        "User not found with Id: {Id}",
                        id);
                    return NotFound(new { message = "User not found" });
                }
                _logger.LogInformation(
                    "User found with Id: {Id}",
                    id);
                return Ok(new { message = "Fetched Successfully", result = result });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Error while fetching user with Id: {Id}",
                    id);
                return BadRequest(new { message = "Failed" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(UserRequestDto dto)
        {
            _logger.LogInformation(
                "Create User API called for Email: {Email}",
                dto.Email);
            try
            {

                var result = await _userService.AddUserService(dto);
                _logger.LogInformation(
                    "User created successfully with Id: {Id}",
                    result.Id);
                return Ok(new { message = "Success", result = result });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Error occurred while creating user for Email: {Email}",
                    dto.Email);
                return BadRequest(new { message = "Nothing found" });
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserRequestDto dto)
        {
            _logger.LogInformation(
               "Update User API called for Id: {Id}",
               id);
            try
            {
                var result = await _userService.UpdateUserService(id, dto);
                if (result == null)
                {
                    _logger.LogWarning(
                        "User not found for update with Id: {Id}",
                        id);
                    return NotFound(new { message = "User not found" });
                }
                _logger.LogInformation(
                   "User updated successfully with Id: {Id}",
                   id);

                return Ok(new { message = "Updated Successfully", result = result });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Error occurred while updating user with Id: {Id}",
                    id);
                return BadRequest(new { message = "Update failed" });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDto dto)
        {
            _logger.LogInformation(
                "Login API called for Email: {Email}",
                dto.Email);
            try
            {
                var result = await _userService.Login(dto);

                if (result == null)
                {
                    _logger.LogWarning(
                        "Login failed for Email: {Email}",
                        dto.Email);
                    return Unauthorized(new { message = "Invalid email or password" });
                }
                _logger.LogInformation(
                    "Login successful for Email: {Email}",
                    dto.Email);
                return Ok(new { message = "Login successful", result = result });
            }
            catch (Exception ex )
            {
                _logger.LogError(ex,
                    "Error occurred during login for Email: {Email}",
                    dto.Email);
                return BadRequest(new { message = "Login failed" });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation(
                "Delete User API called for Id: {Id}",
                id);
            try
            {

                var result = await _userService.DeleteUserService(id);
                if (result == 0)
                {
                    _logger.LogWarning(
                       "User not found for delete with Id: {Id}",
                       id);
                    return NotFound(new { message = "User not found" });
                }
                _logger.LogInformation(
                    "User deleted successfully with Id: {Id}",
                    id);


                return Ok(new { message = "Deleted Successfully" });
            }
            catch (Exception ex )
            {
                _logger.LogError(ex,
                    "Error occurred while deleting user with Id: {Id}",
                    id);
                return BadRequest(new { message = "Delete failed" });
            }
        }
    }
}