using AttendanceTracker.Application.Dtos.Attendance;
using AttendanceTracker.Application.Dtos.Role;
using AttendanceTracker.Application.Dtos.User;
using AttendanceTracker.Application.Dtos.UserDetails;
using AttendanceTracker.Domain.Entity;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace AttendanceTracker.Application.Mapper
{
    public class ApplicationProfile:Profile
    {
        public ApplicationProfile()
        {
            /// Role Table
            CreateMap<RoleRequestDto, Role>();
            CreateMap<Role, RoleResponseDto>();

            /// User Table
            CreateMap<UserRequestDto, User>()
                .ForMember(dest => dest.PasswordHash,
                    opt => opt.Ignore());

            /// UserDetails
            CreateMap<UserDetailsDto, UserDetails>()
                .ReverseMap();

            /// User Response
            CreateMap<User, UserResponseDto>()
                .ForMember(dest => dest.RoleName,
                    opt => opt.MapFrom(src => src.Role.RoleName))
                .ForMember(dest => dest.UserDetails,
                    opt => opt.MapFrom(src => src.UserDetails));



            /// Attendance Table
            CreateMap<AttendanceRequestDto, Attendance>();

            CreateMap<Attendance, AttendanceResponseDto>()
                .ForMember(dest => dest.UserName,
                    opt => opt.MapFrom(src => src.User.UserName))
                .ForMember(dest => dest.RecordedByName,
                    opt => opt.MapFrom(src => src.RecordedByUser.UserName));



        }
    }
}
