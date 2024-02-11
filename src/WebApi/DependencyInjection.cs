using System.Text;
using Application.Interfaces.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using WebApi.Configurations;
using KissLog.AspNetCore;
using KissLog.Formatters;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddWebApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Logging configuration
        services.AddLogging(provider =>
        {
            provider.AddKissLog(options =>
            {
                options.Formatter = (FormatterArgs args) =>
                {
                    if (args.Exception == null)
                        return args.DefaultValue;

                    string exceptionStr = new ExceptionFormatter().Format(args.Exception, args.Logger);
                    return string.Join(Environment.NewLine, [args.DefaultValue, exceptionStr]);
                };
            });
        });
        services.AddHttpContextAccessor();

        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        //Configurations for WebApi
        services.Configure<AuthCookiesConfigurations>(configuration.GetSection("AuthCookies"));

        // Authentication
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
            {
                options.SaveToken = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = configuration.GetValue<string>("JwtAccessToken:Issuer"),
                    ValidAudience = configuration.GetValue<string>("JwtAccessToken:Audience"),
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtAccessToken:Key"]!)),
                };
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        if (configuration.GetValue<string>("AuthCookies:CookieName") == null)
                        {
                            return Task.CompletedTask;
                        }

                        if (!context.Request.Cookies.ContainsKey(configuration.GetValue<string>("AuthCookies:CookieName")!))
                        {
                            return Task.CompletedTask;
                        }

                        string token = context.Request.Cookies[configuration.GetValue<string>("AuthCookies:CookieName")!]!;

                        var blacklistRepository = context.HttpContext.RequestServices.GetRequiredService<IBlacklistRepository>();
                        var whitelistRepository = context.HttpContext.RequestServices.GetRequiredService<IWhitelistRepository>();

                        if (
                            blacklistRepository.Exists(token) || !whitelistRepository.Exists(token)
                        )
                        {
                            return Task.CompletedTask;
                        }
                        context.HttpContext.Request.Headers.Authorization = "Bearer " + token;
                        return Task.CompletedTask;
                    }
                };
            });

        //CORS
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAll",
                builder =>
                {
                    builder
                    .SetIsOriginAllowed(origin=>true)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
                });
        });

        return services;
    }
}
