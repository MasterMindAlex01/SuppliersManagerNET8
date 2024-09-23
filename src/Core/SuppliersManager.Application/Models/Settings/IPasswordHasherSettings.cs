namespace SuppliersManager.Application.Models.Settings
{
    public interface IPasswordHasherSettings
    {
        int Iteration { get; set; }
        string Pepper { get; set; }
    }
}