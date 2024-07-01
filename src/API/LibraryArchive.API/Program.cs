using FluentValidation.AspNetCore;
using LibraryArchive.Data.Context;
using LibraryArchive.Data.Entities;
using LibraryArchive.Services;
using LibraryArchive.Services.Mapping;
using LibraryArchive.Services.Registration;
using LibraryArchive.Services.Services;
using LibraryArchive.Services.Validation.Order;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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

            // Register Identity services
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<LibraryArchiveContext>()
                .AddDefaultTokenProviders();

            // Register AutoMapper
            builder.Services.AddAutoMapper(typeof(MappingProfile));

            // Register services and validators
            builder.Services.AddLibraryArchiveServices();

            // Add UserService to the service collection
            builder.Services.AddScoped<UserService>();
            builder.Services.AddScoped<AuthService>();
            

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
