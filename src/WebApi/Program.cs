using Domain.Enums;
using KissLog.AspNetCore;
using KissLog.CloudListeners.RequestLogsListener;
using WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddWebApiServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors("AllowAll");
}

if (app.Environment.IsProduction())
{
    app.ConfigureExceptionHandler(app.Logger);
    app.UseCors("ProductionCorsPolicy");
}

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();

app.UseKissLogMiddleware(options =>
{
    options.Listeners.Add(new RequestLogsApiListener(new KissLog.CloudListeners.Auth.Application(
        builder.Configuration["KissLog:OrganizationId"],
        builder.Configuration["KissLog:ApplicationId"])
    )
    {
        ApiUrl = builder.Configuration["KissLog:ApiUrl"]
    });
});

app.MapControllers();

app.Run();