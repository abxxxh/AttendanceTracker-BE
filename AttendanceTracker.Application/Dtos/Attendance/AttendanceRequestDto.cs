using System;
using System.Collections.Generic;
using System.Text;

namespace AttendanceTracker.Application.Dtos.Attendance
{
    public class AttendanceRequestDto
    {
        public int UserId { get; set; }
        public DateOnly Date { get; set; }
        public string Status { get; set; }
        public string Course { get; set; }
        public int RecordedBy { get; set; }
    }
}
