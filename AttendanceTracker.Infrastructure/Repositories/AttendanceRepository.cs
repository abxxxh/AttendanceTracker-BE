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
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly AttendanceTrackerDbContext _db;
        private readonly ILogger<AttendanceRepository> _logger;

        public AttendanceRepository(
            AttendanceTrackerDbContext db,
            ILogger<AttendanceRepository> logger)
        {
            _db = db;
            _logger = logger;
        }

        public async Task<Attendance> AddAttendanceRepo(Attendance attendance)
        {
            _logger.LogInformation(
                "Repository: Adding attendance for UserId: {UserId}",
                attendance.UserId);

            try
            {
                await _db.AttendancesTable.AddAsync(attendance);
                await _db.SaveChangesAsync();

                _logger.LogInformation(
                    "Repository: Attendance saved successfully with Id: {AttendanceId}",
                    attendance.AttendanceID);

                return await _db.AttendancesTable
                    .Include(x => x.User)
                    .Include(x => x.RecordedByUser)
                    .FirstOrDefaultAsync(
                        x => x.AttendanceID == attendance.AttendanceID);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Repository: Error while adding attendance for UserId: {UserId}",
                    attendance.UserId);

                throw;
            }
        }

        public async Task<int> DeleteAttendanceRepo(int id)
        {
            _logger.LogInformation(
                "Repository: Delete attendance started for Id: {Id}",
                id);

            try
            {
                var entity = await GetAttendanceByIdRepo(id);

                if (entity == null)
                {
                    _logger.LogWarning(
                        "Repository: Attendance not found for delete with Id: {Id}",
                        id);

                    return 0;
                }

                _db.AttendancesTable.Remove(entity);

                var result = await _db.SaveChangesAsync();

                _logger.LogInformation(
                    "Repository: Attendance deleted successfully with Id: {Id}",
                    id);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Repository: Error while deleting attendance with Id: {Id}",
                    id);

                throw;
            }
        }

        public async Task<IList<Attendance>> GetAllAttendanceRepo()
        {
            _logger.LogInformation(
                "Repository: Fetching all attendance records");

            try
            {
                var data = await _db.AttendancesTable
                    .Include(x => x.User)
                    .Include(x => x.RecordedByUser)
                    .ToListAsync();

                _logger.LogInformation(
                    "Repository: Attendance records fetched successfully. Count: {Count}",
                    data.Count);

                return data;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Repository: Error while fetching all attendance records");

                throw;
            }
        }

        public async Task<Attendance> GetAttendanceByIdRepo(int id)
        {
            _logger.LogInformation(
                "Repository: Fetching attendance by Id: {Id}",
                id);

            try
            {
                var result = await _db.AttendancesTable
                    .Include(x => x.User)
                    .Include(x => x.RecordedByUser)
                    .FirstOrDefaultAsync(
                        x => x.AttendanceID == id);

                if (result == null)
                {
                    _logger.LogWarning(
                        "Repository: Attendance not found with Id: {Id}",
                        id);
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Repository: Error while fetching attendance with Id: {Id}",
                    id);

                throw;
            }
        }

        public async Task<Attendance> UpdateAttendanceRepo(Attendance attendance)
        {
            _logger.LogInformation(
                "Repository: Updating attendance with Id: {Id}",
                attendance.AttendanceID);

            try
            {
                _db.AttendancesTable.Update(attendance);

                await _db.SaveChangesAsync();

                _logger.LogInformation(
                    "Repository: Attendance updated successfully with Id: {Id}",
                    attendance.AttendanceID);

                return await GetAttendanceByIdRepo(
                    attendance.AttendanceID);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Repository: Error while updating attendance with Id: {Id}",
                    attendance.AttendanceID);

                throw;
            }
        }
    }
}