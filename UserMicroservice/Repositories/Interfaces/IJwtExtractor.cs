namespace UserMicroservice.Repositories.Interfaces;

public interface IJwtExtractor
{
    public string ExtractUserIdFromJwtToken(string token);
}