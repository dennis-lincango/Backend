namespace Infrastructure.Configurations;

public class JwtAccessTokenConfigurations
{
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public int LifetimeInMinutes { get; set; }
    public string Key { get; set; } = string.Empty;
}
