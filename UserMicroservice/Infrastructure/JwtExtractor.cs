using System.IdentityModel.Tokens.Jwt;
using UserMicroservice.Repositories.Interfaces;

namespace UserMicroservice.Infrastructure;

public class JwtExtractor : IJwtExtractor
{
    public string ExtractUserIdFromJwtToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var jsonToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

        var userIdClaim = jsonToken?.Claims.FirstOrDefault(c => c.Type == CustomClaims.UserId);

        return userIdClaim?.Value;  
    }
}