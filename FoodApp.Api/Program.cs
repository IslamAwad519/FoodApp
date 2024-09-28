using AutoMapper;
using FoodApp.Api.Extensions;
using ProjectManagementSystem.Helper;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddApplicationService(builder.Configuration);

        var app = builder.Build();
        {

            MapperHandler.mapper = app.Services.GetService<IMapper>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            
            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}