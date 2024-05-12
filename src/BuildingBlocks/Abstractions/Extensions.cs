using CompanyName.MyProjectName.BuildingBlocks.Abstractions.Serialization;
using CompanyName.MyProjectName.BuildingBlocks.Abstractions.Time;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CompanyName.MyProjectName.BuildingBlocks.Abstractions
{
    public static class Extensions
    {
        public static T BindOptions<T>(this IConfiguration configuration, string sectionName)
            where T : new()
            => BindOptions<T>(configuration.GetSection(sectionName));

        public static T BindOptions<T>(this IConfigurationSection section)
            where T : new()
        {
            var options = new T();
            section.Bind(options);
            return options;
        }

        public static IServiceCollection AddMicro(this IServiceCollection services, IConfiguration configuration)
        => services
            .Configure<AppOptions>(configuration.GetSection("app"))
            .AddSingleton<IClock, UtcClock>()
            .AddSingleton<IJsonSerializer, SystemTextJsonSerializer>();

        public static string GetModuleName(this object value)
      => value?.GetType().GetModuleName() ?? string.Empty;

        public static string GetModuleName(this Type type, string namespacePart = "Modules", int splitIndex = 2)
        {
            if (type?.Namespace is null)
            {
                return string.Empty;
            }

            return type.Namespace.Contains(namespacePart)
                ? type.Namespace.Split(".")[splitIndex].ToLowerInvariant()
                : string.Empty;
        }

        public static T GetOptions<T>(this IServiceCollection services, string sectionName)
            where T : new()
        {
            using var serviceProvider = services.BuildServiceProvider();
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            return configuration.GetOptions<T>(sectionName);
        }

        public static T GetOptions<T>(this IConfiguration configuration, string sectionName)
            where T : new()
        {
            var options = new T();
            configuration.GetSection(sectionName).Bind(options);
            return options;
        }
    }
}
