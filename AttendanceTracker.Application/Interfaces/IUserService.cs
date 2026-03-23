using AttendanceTracker.Application.Dtos.Login;
using AttendanceTracker.Application.Dtos.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace AttendanceTracker.Application.Interfaces
{
    public interface IUserService
    {
        Task<IList<UserResponseDto>> GetAllService();
        Task<UserResponseDto> GetByIdService(int id);

        Task<UserResponseDto> AddUserService(UserRequestDto user);

        Task<UserResponseDto> UpdateUserService(int id, UserRequestDto user);
        Task<int> DeleteUserService(int id);
        Task<LoginResponseDto> Login(LoginRequestDto dto);
    }
}
