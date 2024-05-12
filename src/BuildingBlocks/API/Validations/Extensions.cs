using System.Reflection;
using CompanyName.MyProjectName.BuildingBlocks.Abstractions;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CompanyName.MyProjectName.BuildingBlocks.API.Validations;

public static class Extensions
{
    private const string SectionName = "validations";

    public static IServiceCollection AddValidations(this IServiceCollection services, IConfiguration configuration, IList<Assembly> assemblies)
    {
        var section = configuration.GetSection(SectionName);
        services.Configure<ValidationOptions>(section);
        var options = section.BindOptions<ValidationOptions>();

        if (!options.Enabled)
        {
            return services;
        }

        var types = assemblies.SelectMany(x => x.GetTypes()).ToArray();

        var validatorTypes = types
            .Where(t => t.IsClass && typeof(IValidator).IsAssignableFrom(t) && !t.IsAbstract)
            .ToArray();
        foreach (var validatorType in validatorTypes)
        {
            var assembly = validatorType.Assembly;
            services.AddValidatorsFromAssembly(assembly, includeInternalTypes: true);
        }

        services
            .AddFluentValidationAutoValidation(options =>
            {
                options.DisableDataAnnotationsValidation = true;
                ValidatorOptions.Global.LanguageManager.Enabled = false;
            });
        services.AddTransient<IValidatorInterceptor, UseCustomErrorModelInterceptor>();
        return services;
    }
}