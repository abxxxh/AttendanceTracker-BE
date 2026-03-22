using AttendanceTracker.Application.Dtos.Attendance;
using AttendanceTracker.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace AttendanceTracker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private readonly IAttendanceService _service;
        private readonly ILogger<AttendanceController> _logger;

        public AttendanceController(
            IAttendanceService service,
            ILogger<AttendanceController> logger)
        {
            _service = service;
            _logger = logger;
        }

        // GET ALL ATTENDANCE
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation(
                "Get All Attendance API called");

            try
            {
                var result = await _service.GetAllAttendanceService();

                _logger.LogInformation(
                    "Attendance records fetched successfully. Count: {Count}",
                    result.Count);

                return Ok(new { message = "Success", result });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Error occurred while fetching attendance records");

                return BadRequest(new
                {
                    message = "Failed to fetch attendance"
                });
            }
        }

        // GET ATTENDANCE BY ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            _logger.LogInformation(
                "Get Attendance By Id API called with Id: {Id}",
                id);

            try
            {
                var result =
                    await _service.GetAttendanceByIdService(id);

                if (result == null)
                {
                    _logger.LogWarning(
                        "Attendance not found with Id: {Id}",
                        id);

                    return NotFound(new
                    {
                        message = "Attendance not found"
                    });
                }

                _logger.LogInformation(
                    "Attendance fetched successfully with Id: {Id}",
                    id);

                return Ok(new { message = "Success", result });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Error occurred while fetching attendance with Id: {Id}",
                    id);

                return BadRequest(new
                {
                    message = "Failed to fetch attendance"
                });
            }
        }

        // CREATE ATTENDANCE
        [HttpPost]
        public async Task<IActionResult> Create(AttendanceRequestDto dto)
        {
            _logger.LogInformation(
                "Create Attendance API called for UserId: {UserId} Date: {Date}",
                dto.UserId,
                dto.Date);

            try
            {
                var result =
                    await _service.AddAttendanceService(dto);

                if (result == null)
                {
                    _logger.LogWarning(
                        "Attendance insert failed for UserId: {UserId}",
                        dto.UserId);

                    return BadRequest(new
                    {
                        message = "Attendance insert failed"
                    });
                }

                _logger.LogInformation(
                    "Attendance added successfully with Id: {AttendanceId}",
                    result.AttendanceID);

                return Ok(new
                {
                    message = "Attendance added successfully",
                    result
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Error occurred while creating attendance for UserId: {UserId}",
                    dto.UserId);

                return BadRequest(new
                {
                    message = "Attendance creation failed"
                });
            }
        }

        // UPDATE ATTENDANCE
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
            AttendanceRequestDto dto)
        {
            _logger.LogInformation(
                "Update Attendance API called for Id: {Id}",
                id);

            try
            {
                var result =
                    await _service.UpdateAttendanceService(id, dto);

                if (result == null)
                {
                    _logger.LogWarning(
                        "Attendance not found for update with Id: {Id}",
                        id);

                    return NotFound(new
                    {
                        message = "Attendance not found"
                    });
                }

                _logger.LogInformation(
                    "Attendance updated successfully with Id: {Id}",
                    id);

                return Ok(new
                {
                    message = "Attendance updated successfully",
                    result
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Error occurred while updating attendance with Id: {Id}",
                    id);

                return BadRequest(new
                {
                    message = "Attendance update failed"
                });
            }
        }

        // DELETE ATTENDANCE
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation(
                "Delete Attendance API called for Id: {Id}",
                id);

            try
            {
                var result =
                    await _service.DeleteAttendanceService(id);

                if (result == 0)
                {
                    _logger.LogWarning(
                        "Attendance not found for delete with Id: {Id}",
                        id);

                    return NotFound(new
                    {
                        message = "Attendance not found"
                    });
                }

                _logger.LogInformation(
                    "Attendance deleted successfully with Id: {Id}",
                    id);

                return Ok(new
                {
                    message = "Attendance deleted successfully"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Error occurred while deleting attendance with Id: {Id}",
                    id);

                return BadRequest(new
                {
                    message = "Attendance delete failed"
                });
            }
        }
    }
}