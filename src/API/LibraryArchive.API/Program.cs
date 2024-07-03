using FluentValidation.AspNetCore;
using LibraryArchive.API.Middleware;
using LibraryArchive.Data.Context;
using LibraryArchive.Data.Entities;
using LibraryArchive.Services.Mapping;
using LibraryArchive.Services.Registration;
using LibraryArchive.Services.Validation.Order;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using System.Collections.ObjectModel;
using System.Data;
using System.Reflection;
using System.Text;

namespace LibraryArchive.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configure Logging
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File("logs/log.txt")
                .WriteTo.MSSqlServer(builder.Configuration.GetConnectionString("LibraryArchiveConnection"),
                                     sinkOptions: new Serilog.Sinks.MSSqlServer.MSSqlServerSinkOptions
                                     {
                                         TableName = "logs",
                                         AutoCreateSqlTable = true
                                     },
                                     columnOptions: new ColumnOptions
                                     {
                                         AdditionalColumns = new Collection<SqlColumn>
                                         {
                                        new SqlColumn("UserName", SqlDbType.NVarChar)
                                         }
                                     })
                .Enrich.FromLogContext()
                .MinimumLevel.Information()
                .CreateLogger();

            builder.Host.UseSerilog();

            builder.Services.AddHttpLogging(logging =>
            {
                logging.LoggingFields = HttpLoggingFields.All;
                logging.RequestHeaders.Add("sec-ch-ua");
                logging.MediaTypeOptions.AddText("application/javascript");
                logging.RequestBodyLogLimit = 4096;
                logging.ResponseBodyLogLimit = 4096;
            });

            // Add services to the container.
            builder.Services.AddControllers()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<OrderCreateDtoValidator>());

            // Configure Swagger/OpenAPI
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "LibraryArchive API",
                    Version = "v1",
                    Description = "Bu proje, kütüphane arþiv yönetimi, kullanýcý etkileþimi ve e-ticaret özellikleri için kapsamlý bir arka uç sistemi kapsamaktadýr.",
                    Contact = new OpenApiContact
                    {
                        Name = "LibraryArchive Admin",
                        Email = "libraryrchive@info.com",
                        Url = new Uri("https://api.libraryArchive.com")
                    }
                });

                // JWT authorization configuration for Swagger
                var securityScheme = new OpenApiSecurityScheme
                {
                    Name = "JWT Authentication",
                    Description = "Enter JWT token",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                };

                c.AddSecurityDefinition("Bearer", securityScheme);
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                { securityScheme, new[] { "Bearer" } }
            });

                // XML comments configuration
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            // Configure DbContext
            builder.Services.AddDbContext<LibraryArchiveContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("LibraryArchiveConnection")));

            // Configure Identity
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
            })
            .AddEntityFrameworkStores<LibraryArchiveContext>()
            .AddDefaultTokenProviders();


            // Configure JWT Authentication
            var jwtSettings = builder.Configuration.GetSection("Jwt");
            var key = Encoding.ASCII.GetBytes(jwtSettings["Key"]);

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });

            // Configure AutoMapper
            builder.Services.AddAutoMapper(typeof(MappingProfile));

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

            app.UseAuthentication();

            app.UseAuthorization();

            // Global exception handling middleware
            app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
            app.UseMiddleware<UserContextMiddleware>();

            app.MapControllers();

            app.Run();
        }
    }
}
