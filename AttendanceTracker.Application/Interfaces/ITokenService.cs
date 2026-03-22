using AttendanceTracker.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace AttendanceTracker.Application.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}
