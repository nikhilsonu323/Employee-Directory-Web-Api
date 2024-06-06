using EmployeeDirectory.Repository.ScaffoldData;
using EmployeeDirectoryWebApi;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using WebApplication.Validators;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = Microsoft.AspNetCore.Builder.WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddDbContext<EmployeesDbContext>(opt =>
        {
            opt.UseSqlServer(builder.Configuration.GetConnectionString("EmployeeDbConnection"));
        });

        builder.Services.AddDependency();

        builder.Services.AddCors(opt =>
        {
            opt.AddPolicy("AllowAll", policy =>
            {
                policy.AllowAnyOrigin()
                      .AllowAnyMethod()
                      .AllowAnyHeader();
            });
        });

        builder.Services.AddFluentValidationAutoValidation();
        builder.Services.AddValidatorsFromAssemblyContaining<EmployeeValidator>();


        var app = builder.Build();

        app.UseCors("AllowAll");
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        /*app.UseAuthorization();*/

        app.MapControllers();

        app.Run();

    }
}