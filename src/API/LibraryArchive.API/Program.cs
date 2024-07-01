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
using System.Net;
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
                    Description = "Bu proje, aday�n k�t�phane ar�iv y�netimi, kullan�c� etkile�imi ve e-ticaret �zellikleri i�in kapsaml� bir arka u� sistemi tasarlama ve uygulama becerisini de�erlendirmek �zere tasarlanm��t�r.",
                    Contact = new OpenApiContact
                    {
                        Name = "Dev",
                        Email = "dev@gmail.com",
                        Url = new Uri("https://example.com")
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
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
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

    // Global exception handling middleware
    public class GlobalExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;

        public GlobalExceptionHandlingMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception has occurred.");
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = new
            {
                StatusCode = context.Response.StatusCode,
                Message = "Internal Server Error. Please try again later.",
                Detailed = exception.Message
            };

            return context.Response.WriteAsJsonAsync(response);
        }
    }
}
