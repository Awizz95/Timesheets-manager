using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Security.Claims;
using System.Text;
using TimesheetsProj.Data.Ef;
using TimesheetsProj.Data.Implementation;
using TimesheetsProj.Data.Interfaces;
using TimesheetsProj.Domain.Managers.Implementation;
using TimesheetsProj.Domain.Managers.Interfaces;
using TimesheetsProj.Infrastructure.Auth;
using TimesheetsProj.Infrastructure.Validation;
using TimesheetsProj.Models.Dto.Requests;

namespace TimesheetsProj.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TimesheetDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("Postgres"));
            });
        }

        public static void ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ILoginManager, LoginManager>();
            services.AddScoped<IJwtProvider, JwtProvider>();

            JwtProvider jwtProvider = new(configuration);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                    .AddJwtBearer(options =>
                    {
                        options.RequireHttpsMetadata = false;
                        options.SaveToken = true;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidIssuer = configuration["Authentication:JwtOptions:Issuer"],
                            ValidateAudience = true,
                            ValidAudience = configuration["Authentication:JwtOptions:Audience"],
                            ValidateLifetime = true,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Authentication:JwtOptions:SigningKey"]!)),
                            ValidateIssuerSigningKey = true,
                            ClockSkew = TimeSpan.Zero
                        };
                        options.IncludeErrorDetails = true;

                        //чтобы токен записывался из куки
                        options.Events = new JwtBearerEvents
                        {
                            OnMessageReceived = context =>
                            {
                                context.Token = context.Request.Cookies["Timesheets-access-token"];

                                return Task.CompletedTask;
                            }
                        };
                    });

            services.AddAuthorization(options => options.DefaultPolicy =
                    new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser()
                    .Build());
        }

        public static void ConfigureDomainManagers(this IServiceCollection services)
        {
            services.AddScoped<ISheetManager, SheetManager>();
            services.AddScoped<IContractManager, ContractManager>();
            services.AddScoped<IUserManager, UserManager>();
            services.AddScoped<ILoginManager, LoginManager>();
            services.AddScoped<IInvoiceManager, InvoiceManager>();
        }

        public static void ConfigureRepositories(this IServiceCollection services)
        {
            services.AddScoped<ISheetRepo, SheetRepo>();
            services.AddScoped<IContractRepo, ContractRepo>();
            services.AddScoped<IUserRepo, UserRepo>();
            services.AddScoped<IInvoiceRepo, InvoiceRepo>();
            services.AddScoped<IServiceRepo, ServiceRepo>();
        }

        public static void ConfigureBackendSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Timesheets", Version = "v1" });

                //добавляет кнопку авторизовать
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Autorization",
                    Description = "Please enter a valid token",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme()
                        {
                            Reference = new OpenApiReference(){
                                Type = ReferenceType.SecurityScheme, 
                                Id = "Bearer"}
                        },
                        Array.Empty<string>()
                    }
                });
            });
        }

        public static void ConfigureValidation(this IServiceCollection services)
        {
            services.AddScoped<IValidator<SheetRequest>, SheetRequestValidator>();
            services.AddScoped<IValidator<CreateUserRequest>, CreateUserRequestValidator>();
        }
    }
}

