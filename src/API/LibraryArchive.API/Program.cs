using LibraryArchive.Data.Context;
using Microsoft.EntityFrameworkCore;
using LibraryArchive.Services.Registration;
using FluentValidation;
using FluentValidation.AspNetCore;
using LibraryArchive.Services.Validation.Order;

namespace LibraryArchive.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<OrderCreateDtoValidator>());

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<LibraryArchiveContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("LibraryArchiveConnection")));

            // Register services and validators
            builder.Services.AddLibraryArchiveServices();

            var app = builder.Build();

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
