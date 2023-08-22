using BlazorSozlukCommon.ViewModels.RequestModels;

namespace BlazorSozluk.WebApp.Infastructure.Services.Interfaces
{
    public interface IIdentityService
    {
        bool IsLoggedIn { get; }

        Guid GetUserID();
        string GetUserName();
        string GetUserToken();
        Task<bool> Login(LoginUserCommand command);
        void Logout();
    }
}