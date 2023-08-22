using BlazorSozlukCommon.ViewModels.Queries;

namespace BlazorSozluk.WebApp.Infastructure.Services.Interfaces
{
    public interface IUserService
    {
        Task<bool> ChangeUserPassword(string oldPassword, string newPassword);
        Task<UserDetailViewModel> GetUserDetail(Guid? id);
        Task<UserDetailViewModel> GetUserDetail(string username);
        Task<bool> UpdateUser(UserDetailViewModel user);
    }
}