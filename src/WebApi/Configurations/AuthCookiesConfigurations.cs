namespace WebApi.Configurations;

public class AuthCookiesConfigurations
{
    public string CookieName { get; set; } = string.Empty;
    public int TimeoutInMinutes { get; set; }
}
