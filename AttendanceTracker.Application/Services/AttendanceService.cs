using AttendanceTracker.Application.Dtos.Attendance;
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
    public class AttendanceService : IAttendanceService
    {
        private readonly IAttendanceRepository _arepo;
        private readonly IMapper _mapper;
        private readonly ILogger<AttendanceService> _logger;

        public AttendanceService(
            IAttendanceRepository repo,
            IMapper map,
            ILogger<AttendanceService> logger)
        {
            _arepo = repo;
            _mapper = map;
            _logger = logger;
        }

        public async Task<AttendanceResponseDto> AddAttendanceService(AttendanceRequestDto dto)
        {
            _logger.LogInformation(
                "AddAttendanceService started for UserId: {UserId} Date: {Date}",
                dto.UserId,
                dto.Date);

            try
            {
                var entity = _mapper.Map<Attendance>(dto);

                var result = await _arepo.AddAttendanceRepo(entity);

                _logger.LogInformation(
                    "Attendance added successfully in service with Id: {AttendanceId}",
                    result.AttendanceID);

                return _mapper.Map<AttendanceResponseDto>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Error occurred in AddAttendanceService for UserId: {UserId}",
                    dto.UserId);

                throw;
            }
        }

        public async Task<int> DeleteAttendanceService(int id)
        {
            _logger.LogInformation(
                "DeleteAttendanceService started for Id: {Id}",
                id);

            try
            {
                var result = await _arepo.DeleteAttendanceRepo(id);

                if (result == 0)
                {
                    _logger.LogWarning(
                        "DeleteAttendanceService: Attendance not found for Id: {Id}",
                        id);
                }
                else
                {
                    _logger.LogInformation(
                        "Attendance deleted successfully in service with Id: {Id}",
                        id);
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Error occurred in DeleteAttendanceService for Id: {Id}",
                    id);

                throw;
            }
        }

        public async Task<IList<AttendanceResponseDto>> GetAllAttendanceService()
        {
            _logger.LogInformation(
                "GetAllAttendanceService started");

            try
            {
                var entity = await _arepo.GetAllAttendanceRepo();

                _logger.LogInformation(
                    "Attendance records fetched successfully in service. Count: {Count}",
                    entity.Count);

                return _mapper.Map<IList<AttendanceResponseDto>>(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Error occurred in GetAllAttendanceService");

                throw;
            }
        }

        public async Task<AttendanceResponseDto> GetAttendanceByIdService(int id)
        {
            _logger.LogInformation(
                "GetAttendanceByIdService started for Id: {Id}",
                id);

            try
            {
                var result = await _arepo.GetAttendanceByIdRepo(id);

                if (result == null)
                {
                    _logger.LogWarning(
                        "GetAttendanceByIdService: Attendance not found for Id: {Id}",
                        id);

                    return null;
                }

                _logger.LogInformation(
                    "Attendance fetched successfully in service with Id: {Id}",
                    id);

                return _mapper.Map<AttendanceResponseDto>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Error occurred in GetAttendanceByIdService for Id: {Id}",
                    id);

                throw;
            }
        }

        public async Task<AttendanceResponseDto> UpdateAttendanceService(int id, AttendanceRequestDto dto)
        {
            _logger.LogInformation(
                "UpdateAttendanceService started for Id: {Id}",
                id);

            try
            {
                var existing = await _arepo.GetAttendanceByIdRepo(id);

                if (existing == null)
                {
                    _logger.LogWarning(
                        "UpdateAttendanceService: Attendance not found for Id: {Id}",
                        id);

                    return null;
                }

                existing.UserId = dto.UserId;
                existing.Date = dto.Date;
                existing.Status = dto.Status;
                existing.Course = dto.Course;
                existing.RecordedBy = dto.RecordedBy;

                var updated = await _arepo.UpdateAttendanceRepo(existing);

                _logger.LogInformation(
                    "Attendance updated successfully in service with Id: {Id}",
                    id);

                return _mapper.Map<AttendanceResponseDto>(updated);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Error occurred in UpdateAttendanceService for Id: {Id}",
                    id);

                throw;
            }
        }
    }
}