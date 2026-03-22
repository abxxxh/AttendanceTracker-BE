using AttendanceTracker.Application.Interfaces;
using AttendanceTracker.Application.Services;
using AttendanceTracker.Domain.Interfaces.IRepositories;
using AttendanceTracker.Infrastructure.DbContext;
using AttendanceTracker.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using AttendanceTracker.Api.Middleware;

namespace AttendanceTracker.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<AttendanceTrackerDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Role
            builder.Services.AddScoped<IRoleRepository, RoleRepository>();
            builder.Services.AddScoped<IRoleService, RoleService>();

            // User
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IUserService, UserService>();

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            builder.Services.AddScoped<IAttendanceRepository, AttendanceRepository>();
            builder.Services.AddScoped<IAttendanceService, AttendanceService>();
            builder.Services.AddScoped<ITokenService, TokenService>();
            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();
            builder.Logging.AddLog4Net("log4net.config");
            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseMiddleware<GlobalExceptionMiddleware>();
            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}