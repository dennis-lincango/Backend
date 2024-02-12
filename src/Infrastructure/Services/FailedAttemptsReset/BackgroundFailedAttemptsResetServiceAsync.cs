using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Infrastructure.Services.FailedAttemptsReset;

public class BackgroundFailedAttemptsResetServiceAsync(
    IServiceProvider services
) : BackgroundService, IFailedAttemptsResetServiceAsync
{
    public async Task ResetFailedAttemptsAsync()
    {
        using var scope = services.CreateScope();
        IUsersRepositoryAsync usersRepository = scope.ServiceProvider.GetRequiredService<IUsersRepositoryAsync>();
        IEnumerable<User> users = await usersRepository.GetAsync(
            filter: u => !u.IsLocked && u.LoginFailedAttempts > 0 && u.LoginFailedAttempts < 3
        );

        foreach (User user in users)
        {
            user.LoginFailedAttempts = 0;
        }

        await usersRepository.UpdateRangeAsync(users);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await ResetFailedAttemptsAsync();
            DateTime now = DateTime.UtcNow;
            DateTime nextMidnightUtc = now.Date.AddDays(1);
            TimeSpan delay = nextMidnightUtc - now;
            await Task.Delay(delay, stoppingToken);
        }
    }
}
