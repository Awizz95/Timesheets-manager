using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TimesheetsProj.Data.Ef;
using TimesheetsProj.Data.Implementation;
using TimesheetsProj.Data.Interfaces;
using TimesheetsProj.Domain.Managers.Implementation;
using TimesheetsProj.Domain.Managers.Interfaces;
using TimesheetsProj.Infrastructure.Validation;
using TimesheetsProj.Models.Dto.Authentication;
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
            services.Configure<JwtAccessOptions>(configuration.GetSection("Authentication:JwtAccessOptions"));

            var jwtSettings = new JwtOptions();
            configuration.Bind("Authentication:JwtAccessOptions", jwtSettings);

            services.AddTransient<ILoginManager, LoginManager>();

            services.AddAuthentication(x => {
                        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;})
                    .AddJwtBearer(options =>
                    {
                        options.RequireHttpsMetadata = false;
                        options.TokenValidationParameters = jwtSettings.GetTokenValidationParameters();
                    });
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
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme()
                        {
                            Reference = new OpenApiReference(){Type = ReferenceType.SecurityScheme, Id = "Bearer"}
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

