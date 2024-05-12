using Microsoft.IdentityModel.Tokens;

namespace CompanyName.MyProjectName.BuildingBlocks.Auth.JWT;

internal sealed record SecurityKeyDetails(SecurityKey Key, string Algorithm);
