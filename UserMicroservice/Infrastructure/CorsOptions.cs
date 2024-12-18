namespace UserMicroservice.Infrastructure;

public class CorsOptions {
    public bool AllowAnyOrigin { get; set; } = false;
    public string[] AllowedOrigins { get; set; } = [];
}