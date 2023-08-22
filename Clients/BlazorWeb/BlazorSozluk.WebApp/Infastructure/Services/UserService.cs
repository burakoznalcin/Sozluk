using BlazorSozluk.WebApp.Infastructure.Services.Interfaces;
using BlazorSozlukCommon.Infastructure.Exceptions;
using BlazorSozlukCommon.Infastructure.Extensions.Results;
using BlazorSozlukCommon.ViewModels.Queries;
using BlazorSozlukCommon.ViewModels.RequestModels;
using System.Net.Http.Json;
using System.Text.Json;

namespace BlazorSozluk.WebApp.Infastructure.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient client;
        public UserService(HttpClient client)
        {
            this.client = client;
        }
        public async Task<UserDetailViewModel> GetUserDetail(Guid? id)
        {
            var userDetail = await client.GetFromJsonAsync<UserDetailViewModel>($"/api/user/{id}");
            if (userDetail is null)
                throw new DatabaseValidateExceptions("User not Found!");

            return userDetail;
        }
        public async Task<UserDetailViewModel> GetUserDetail(string username)
        {
            var userDetail = await client.GetFromJsonAsync<UserDetailViewModel>($"/api/user/{username}");
            return userDetail;
        }
        public async Task<bool> UpdateUser(UserDetailViewModel user)
        {
            var result = await client.PostAsJsonAsync($"/api/user/update", user);

            return result.IsSuccessStatusCode;
        }

        public async Task<bool> ChangeUserPassword(string oldPassword, string newPassword)
        {
            var command = new ChangeUserPasswordCommand(null, oldPassword, newPassword);
            var httpResponse = await client.PostAsJsonAsync($"/api/User/ChangePassword", command);

            if (httpResponse != null && !httpResponse.IsSuccessStatusCode)
            {
                if (httpResponse.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    var responseStr = await httpResponse.Content.ReadAsStringAsync();
                    var validation = JsonSerializer.Deserialize<ValidationResponseModel>(responseStr);
                    responseStr = validation.FlattenErrors;
                    throw new DatabaseValidateExceptions(responseStr);

                }
                return false;

            }
            return httpResponse.IsSuccessStatusCode;
        }
    }
}
