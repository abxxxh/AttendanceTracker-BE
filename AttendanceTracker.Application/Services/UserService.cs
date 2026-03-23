using AttendanceTracker.Application.Dtos.Login;
using AttendanceTracker.Application.Dtos.User;
using AttendanceTracker.Application.Interfaces;
using AttendanceTracker.Domain.Entity;
using AttendanceTracker.Domain.Interfaces.IRepositories;
using AutoMapper;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AttendanceTracker.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;
        private readonly IMapper _mapper;
        private readonly ITokenService _token;
        private readonly ILogger<UserService> _logger;

        public UserService(
            IUserRepository repo,
            IMapper map,
            ITokenService token,
            ILogger<UserService> logger)
        {
            _repo = repo;
            _mapper = map;
            _token = token;
            _logger = logger;
        }

        public async Task<UserResponseDto> AddUserService(UserRequestDto userdto)
        {
            _logger.LogInformation(
                "AddUserService started for Email: {Email}",
                userdto.Email);

            try
            {
                var user = _mapper.Map<User>(userdto);
                user.PasswordHash = userdto.Password;
                user.UserDetails = _mapper.Map<UserDetails>(userdto.userdetailsnavigation);

                var result = await _repo.AddNewUserRepo(user);

                _logger.LogInformation(
                    "User added successfully in service with Id: {Id}",
                    result.Id);

                var response = _mapper.Map<UserResponseDto>(result);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Error occurred in AddUserService for Email: {Email}",
                    userdto.Email);
                throw;
            }
        }

        public async Task<int> DeleteUserService(int id)
        {
            _logger.LogInformation(
                "DeleteUserService started for Id: {Id}",
                id);

            try
            {
                var result = await _repo.DeleteUserRepo(id);

                if (result == 0)
                {
                    _logger.LogWarning(
                        "DeleteUserService: User not found for Id: {Id}",
                        id);
                }
                else
                {
                    _logger.LogInformation(
                        "User deleted successfully in service with Id: {Id}",
                        id);
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Error occurred in DeleteUserService for Id: {Id}",
                    id);
                throw;
            }
        }

        public async Task<IList<UserResponseDto>> GetAllService()
        {
            _logger.LogInformation("GetAllService started");

            try
            {
                var entity = await _repo.GetAllUsersRepo();

                _logger.LogInformation(
                    "Users fetched successfully in service. Count: {Count}",
                    entity.Count);

                return _mapper.Map<IList<UserResponseDto>>(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Error occurred in GetAllService");
                throw;
            }
        }

        public async Task<UserResponseDto> GetByIdService(int id)
        {
            _logger.LogInformation(
                "GetByIdService started for Id: {Id}",
                id);

            try
            {
                var user_entity = await _repo.GetUsersByIdRepo(id);

                if (user_entity == null)
                {
                    _logger.LogWarning(
                        "GetByIdService: User not found for Id: {Id}",
                        id);
                    return null;
                }

                _logger.LogInformation(
                    "User fetched successfully in service with Id: {Id}",
                    id);

                return _mapper.Map<UserResponseDto>(user_entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Error occurred in GetByIdService for Id: {Id}",
                    id);
                throw;
            }
        }

        public async Task<LoginResponseDto> Login(LoginRequestDto dto)
        {
            _logger.LogInformation(
                "Login service started for Email: {Email}",
                dto.Email);

            try
            {
                var user = await _repo.GetByEmail(dto.Email);

                if (user == null)
                {
                    _logger.LogWarning(
                        "Login failed in service. User not found for Email: {Email}",
                        dto.Email);
                    return null;
                }

                if (user.PasswordHash != dto.Password)
                {
                    _logger.LogWarning(
                        "Login failed in service. Invalid password for Email: {Email}",
                        dto.Email);
                    return null;
                }

                var token = _token.CreateToken(user);

                _logger.LogInformation(
                    "Login successful in service for Email: {Email}",
                    dto.Email);

                return new LoginResponseDto
                {
                    Token = token,
                    Email = user.Email,
                    UserName = user.UserName,
                    RoleName = user.Role != null ? user.Role.RoleName : null
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Error occurred in Login service for Email: {Email}",
                    dto.Email);
                throw;
            }
        }

        public async Task<UserResponseDto> UpdateUserService(int id, UserRequestDto userreq)
        {
            _logger.LogInformation(
                "UpdateUserService started for Id: {Id}",
                id);

            try
            {
                var existingUser = await _repo.GetUsersByIdRepo(id);

                if (existingUser == null)
                {
                    _logger.LogWarning(
                        "UpdateUserService: User not found for Id: {Id}",
                        id);
                    return null;
                }

                existingUser.UserName = userreq.UserName;
                existingUser.Email = userreq.Email;
                existingUser.RoleId = userreq.RoleId;
                existingUser.PasswordHash = userreq.Password;

                if (existingUser.UserDetails != null && userreq.userdetailsnavigation != null)
                {
                    existingUser.UserDetails.FullName = userreq.userdetailsnavigation.FullName;
                    existingUser.UserDetails.DOB = userreq.userdetailsnavigation.DOB;
                    existingUser.UserDetails.Gender = userreq.userdetailsnavigation.Gender;
                    existingUser.UserDetails.PhoneNumber = userreq.userdetailsnavigation.PhoneNumber;
                    existingUser.UserDetails.Address = userreq.userdetailsnavigation.Address;
                    existingUser.UserDetails.Department = userreq.userdetailsnavigation.Department;
                    existingUser.UserDetails.Year = userreq.userdetailsnavigation.Year;
                }

                var responserepo = await _repo.UpdateUserRepo(existingUser);

                _logger.LogInformation(
                    "User updated successfully in service with Id: {Id}",
                    id);

                return _mapper.Map<UserResponseDto>(responserepo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Error occurred in UpdateUserService for Id: {Id}",
                    id);
                throw;
            }
        }
    }
}