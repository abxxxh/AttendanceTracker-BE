using AttendanceTracker.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace AttendanceTracker.Domain.Interfaces.IRepositories
{
    public interface IAttendanceRepository
    {
        Task<Attendance> AddAttendanceRepo(Attendance attendance);
        Task<IList<Attendance>> GetAllAttendanceRepo();
        Task<Attendance> GetAttendanceByIdRepo(int id);
        Task<Attendance> UpdateAttendanceRepo(Attendance attendance);
        Task<int> DeleteAttendanceRepo(int id);
    }
}
