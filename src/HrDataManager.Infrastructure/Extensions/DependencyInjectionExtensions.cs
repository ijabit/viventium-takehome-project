using Hellang.Middleware.ProblemDetails;
using HrDataManager.Application.Common.Interfaces;
using HrDataManager.Application.Common.Exceptions;
using HrDataManager.Infrastructure;
using HrDataManager.Infrastructure.Data;
using HrDataManager.Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            if (connectionString == null)
            {
                throw new ArgumentNullException(nameof(connectionString));
            }

            services.AddDbContext<HrDataDbContext>((sp, options) =>
            {
                options.UseSqlite(connectionString);
            });
            services.AddScoped<IHrDataDbContext>(provider => provider.GetRequiredService<HrDataDbContext>());
            services.AddScoped<HrDataDbContextInitializer>();
            services.AddScoped<ICsvParserService, CsvParserService>();
            services.AddSingleton(TimeProvider.System);
            services.AddDatabaseDeveloperPageExceptionFilter();
            ProblemDetailsExtensions.AddProblemDetails(services, options =>
            {
                options.IncludeExceptionDetails = (ctx, _) =>
                    Environment.GetEnvironmentVariable("AZURE_FUNCTIONS_ENVIRONMENT") == "Development" ||
                    Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";

                options.Map<BusinessRuleValidationException>(ex => new ProblemDetails
                {
                    Title = "Business rule invalid exception",
                    Status = StatusCodes.Status403Forbidden,
                    Detail = ex.Message,
                });
            });

            return services;
        }
    }
}
