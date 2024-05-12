namespace CompanyName.MyProjectName.BuildingBlocks.Auth.JWT;

#nullable enable
public interface IJsonWebTokenManager
{
    JsonWebToken CreateToken(
        string userId, string? email = null, string? role = null, IDictionary<string, IEnumerable<string>>? claims = null);
}