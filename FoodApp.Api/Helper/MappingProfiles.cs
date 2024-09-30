﻿using AutoMapper;
using FoodApp.Api.CQRS.Roles.Commands;
using FoodApp.Api.CQRS.UserRoles.Commands;
using FoodApp.Api.CQRS.Users.Commands;
using FoodApp.Api.Data.Entities;
using FoodApp.Api.ViewModels;

namespace FoodApp.Api.Helper
{
    public class MappingProfiles :Profile
    {
        public MappingProfiles()
        {
            CreateMap<RegisterCommand, User>()
                .ForMember(dest => dest.DateCreated, opt => opt.MapFrom(src=>DateTime.Now));

            CreateMap<RegisterViewModel, RegisterCommand>();

            CreateMap<LoginViewModel, LoginCommand>();

            CreateMap<ChangePasswordViewModel, ChangePasswordCommand>();
            CreateMap<ForgotPasswordViewModel, ForgotPasswordCommand>();
            CreateMap<ResetPasswordViewModel, ResetPasswordCommand>();
            //roles
            CreateMap<CreateRoleCommand, Role>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.roleName)); ;

            CreateMap<CreateRoleViewModel, CreateRoleCommand>();
            CreateMap<AssignRoleToUserViewModel, AddRoleToUserCommand>();

            CreateMap<User, LoginResponse>()
               .ForMember(dest => dest.RefreshToken, opt => opt.MapFrom(src =>
                   src.RefreshTokens
                      .Where(r => r.IsActive) 
                      .Select(r => r.Token) 
                      .FirstOrDefault()));
        }
    }
}
