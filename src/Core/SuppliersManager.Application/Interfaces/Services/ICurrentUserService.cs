namespace SuppliersManager.Application.Interfaces.Services
{
    public interface ICurrentUserService
    {
        string? GetUserId();
        string? GetUsername();
        string? GetUserEmail();
    }
}
