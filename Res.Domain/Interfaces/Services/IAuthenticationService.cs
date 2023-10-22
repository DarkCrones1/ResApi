namespace Res.Domain.Interfaces.Services;

public interface IAuthenticationService
{
    Task<bool> IsValidUser(string userName, string password);
}