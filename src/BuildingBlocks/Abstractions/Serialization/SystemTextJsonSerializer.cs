using System.Text.Json;
using System.Text.Json.Serialization;

namespace CompanyName.MyProjectName.BuildingBlocks.Abstractions.Serialization;

public sealed class SystemTextJsonSerializer : IJsonSerializer
{
    private readonly JsonSerializerOptions _options = new()
    {
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }
    };

    public string Serialize<T>(T value) => JsonSerializer.Serialize(value, _options);

#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
    public T? Deserialize<T>(string value) => JsonSerializer.Deserialize<T>(value, _options);
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.

    public object Deserialize(string value, Type type) => JsonSerializer.Deserialize(value, type, _options);
}