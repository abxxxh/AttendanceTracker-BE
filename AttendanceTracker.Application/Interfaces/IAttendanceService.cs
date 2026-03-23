using AttendanceTracker.Application.Dtos.Attendance;
using System;
using System.Collections.Generic;
using System.Text;

namespace AttendanceTracker.Application.Interfaces
{
    public interface IAttendanceService
    {
        Task<AttendanceResponseDto> AddAttendanceService(AttendanceRequestDto dto);
        Task<IList<AttendanceResponseDto>> GetAllAttendanceService();
        Task<AttendanceResponseDto> GetAttendanceByIdService(int id);
        Task<AttendanceResponseDto> UpdateAttendanceService(int id, AttendanceRequestDto dto);
        Task<int> DeleteAttendanceService(int id);
    }
}
