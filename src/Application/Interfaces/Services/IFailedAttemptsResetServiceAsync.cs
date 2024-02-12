namespace Application.Interfaces.Services;

public interface IFailedAttemptsResetServiceAsync
{
    public Task ResetFailedAttemptsAsync();
}