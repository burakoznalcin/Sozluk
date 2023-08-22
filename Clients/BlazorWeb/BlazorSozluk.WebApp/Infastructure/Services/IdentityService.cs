using Blazored.LocalStorage;
using BlazorSozluk.WebApp.Infastructure.Extensions;
using BlazorSozluk.WebApp.Infastructure.Services.Interfaces;
using BlazorSozlukCommon.Infastructure.Exceptions;
using BlazorSozlukCommon.Infastructure.Extensions.Results;
using BlazorSozlukCommon.ViewModels.Queries;
using BlazorSozlukCommon.ViewModels.RequestModels;
using System.Net.Http.Json;
using System.Text.Json;

namespace BlazorSozluk.WebApp.Infastructure.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly HttpClient httpClient;
        private readonly ISyncLocalStorageService syncLocalStorageService;

        public IdentityService(HttpClient httpClient, ISyncLocalStorageService syncLocalStorageService)
        {
            this.httpClient = httpClient;
            this.syncLocalStorageService = syncLocalStorageService;
        }

        public bool IsLoggedIn => !string.IsNullOrEmpty(GetUserToken());

        public string GetUserToken()
        {
            return syncLocalStorageService.GetToken();
        }
        public string GetUserName()
        {
            return syncLocalStorageService.GetToken();
        }

        public Guid GetUserID()
        {
            return syncLocalStorageService.GetUserId();
        }

        public async Task<bool> Login(LoginUserCommand command)
        {
            string responseStr;
            var httpResponse = await httpClient.PostAsJsonAsync("/api/User/login", command);

            if (httpResponse != null && !httpResponse.IsSuccessStatusCode)
            {
                if (httpResponse.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    responseStr = await httpResponse.Content.ReadAsStringAsync();
                    var validation = JsonSerializer.Deserialize<ValidationResponseModel>(responseStr);
                    responseStr = validation.FlattenErrors;
                    throw new DatabaseValidateExceptions(responseStr);
                }
                return false;

            }
            responseStr = await httpResponse.Content.ReadAsStringAsync();
            var response = JsonSerializer.Deserialize<LoginUserViewModel>(responseStr);
            if (!string.IsNullOrEmpty(response.Token))
            {
                syncLocalStorageService.SetToken(response.Token);
                syncLocalStorageService.SetUsername(response.UserName);
                syncLocalStorageService.SetUserId(response.Id);

                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", response.UserName);

                return true;
            }
            return false;
        }

        public void Logout()
        {
            syncLocalStorageService.RemoveItem(LocalStorageExtensions.TokenName);
            syncLocalStorageService.RemoveItem(LocalStorageExtensions.UserName);
            syncLocalStorageService.RemoveItem(LocalStorageExtensions.UserId);

            httpClient.DefaultRequestHeaders.Authorization = null;



        }
    }
}
