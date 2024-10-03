using AutoMapper;
using FoodApp.Api.CQRS.Discounts.Commands;
using FoodApp.Api.CQRS.Discounts.Queries;
using FoodApp.Api.CQRS.Recipes.Commands;
using FoodApp.Api.CQRS.Roles.Commands;
using FoodApp.Api.CQRS.UserRoles.Commands;
using FoodApp.Api.CQRS.Users.Commands;
using FoodApp.Api.Data.Entities;
using FoodApp.Api.Helper.RecipeUrlResolve;
using FoodApp.Api.ViewModels;
using static System.Net.Mime.MediaTypeNames;

namespace FoodApp.Api.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            /***************  User  ***************/

            CreateMap<RegisterCommand, User>()
                .ForMember(dest => dest.DateCreated, opt => opt.MapFrom(src => DateTime.Now));

            CreateMap<RegisterViewModel, RegisterCommand>();

            CreateMap<LoginViewModel, LoginCommand>();

            CreateMap<ChangePasswordViewModel, ChangePasswordCommand>();
            CreateMap<ForgotPasswordViewModel, ForgotPasswordCommand>();
            CreateMap<ResetPasswordViewModel, ResetPasswordCommand>();

            /***************  Role  ***************/

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

            /***************  Recipe  ***************/

            CreateMap<CreateRecipeCommand, Recipe>()
                 .ForMember(dest => dest.ImageUrl, opt => opt.Ignore())
                 .AfterMap(async (src, dest) =>
                 {
                     dest.ImageUrl = await DocumentSettings.UploadFileAsync(src.ImageUrl, "Images");
                 });

            CreateMap<CreateRecipeViewModel, CreateRecipeCommand>();

            CreateMap<UpdateRecipeViewModel, UpdateRecipeCommand>();

            CreateMap<UpdateRecipeCommand, Recipe>()
                 .ForMember(dest => dest.ImageUrl, opt => opt.Ignore())
                 .AfterMap(async (src, dest) =>
                 {
                     dest.ImageUrl = await DocumentSettings.UploadFileAsync(src.ImageUrl, "Images");
                 });

            //Discount
            CreateMap<AddDiscountViewModel, AddDiscountCommand>();
            CreateMap<AddDiscountCommand, Data.Entities.Discount>();
            CreateMap<ApplyDiscountViewModel, ApplyDiscountCommand>();
            CreateMap<Discount, Discount>().ReverseMap();
            CreateMap<UpdateDiscountCommand, Data.Entities.Discount>()    
                .ForMember(dest => dest.DiscountPercent, opt => opt.Condition(src => src.DiscountPercent.HasValue))
                .ForMember(dest => dest.StartDate, opt => opt.Condition(src => src.StartDate.HasValue))
                .ForMember(dest => dest.EndDate, opt => opt.Condition(src => src.EndDate.HasValue)); ;
        }
    }
}
