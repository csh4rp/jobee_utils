using Jobee.Utils.Api.ExceptionHandlers;
using Jobee.Utils.Api.Validation;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Jobee.Utils.Api;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddExceptionHandlers(this IServiceCollection services)
    {
        return services.AddTransient<CatchAllExceptionHandler>()
            .AddTransient<ForbiddenExceptionHandler>()
            .AddTransient<EntityNotFoundExceptionHandler>()
            .AddTransient<ValidationExceptionHandler>()
            .AddTransient<ConflictExceptionHandler>();
    }

    public static IServiceCollection AddValidationUtils(this IServiceCollection services)
    {
        services.TryAddSingleton<IErrorCodeMapper, DefaultErrorCodeMapper>();
        services.TryAddSingleton<IValidationArgumentParser, DefaultValidationArgumentParser>();
        
        return services;
    }
}