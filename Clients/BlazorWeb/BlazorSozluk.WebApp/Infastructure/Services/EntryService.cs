using BlazorSozluk.WebApp.Infastructure.Services.Interfaces;
using BlazorSozlukCommon.ViewModels.Page;
using BlazorSozlukCommon.ViewModels.Queries;
using BlazorSozlukCommon.ViewModels.RequestModels;
using System.Net.Http.Json;

namespace BlazorSozluk.WebApp.Infastructure.Services
{
    public class EntryService : IEntryService
    {
        private readonly HttpClient client;

        public EntryService(HttpClient client)
        {
            this.client = client;
        }

        public async Task<List<GetEntriesViewModel>> GetEntries()
        {
            var result = await client.GetFromJsonAsync<List<GetEntriesViewModel>>($"/api/entry?TodaysEntries=false&count=30");

            return result;
        }

        public async Task<GetEntryDetailViewModel> GetById(Guid entryId)
        {
            var result = await client.GetFromJsonAsync<GetEntryDetailViewModel>($"/api/entry/{entryId}");

            return result;
        }

        public async Task<PagedViewModel<GetEntryDetailViewModel>> GetMainPageEntries(int page, int pageSize)
        {
            var result = await client.GetFromJsonAsync<PagedViewModel<GetEntryDetailViewModel>>($"/api/entry/MainPageEntries?page={page}page&pagesize={pageSize}");

            return result;
        }

        public async Task<PagedViewModel<GetEntryDetailViewModel>> GetProfilePageEntries(int page, int pageSize, string username = null)
        {
            var result = await client.GetFromJsonAsync<PagedViewModel<GetEntryDetailViewModel>>($"/api/entry/UserEntries?username={username}&page={page}&pagesize={pageSize}");

            return result;
        }

        public async Task<PagedViewModel<GetEntryCommentsViewModel>> GetEntryComments(Guid entryId, int pageSize, int page)
        {
            var result = await client.GetFromJsonAsync<PagedViewModel<GetEntryCommentsViewModel>>($"/api/entry/Comments/{entryId}?page={page}&pageSize={pageSize}");

            return result;
        }

        public async Task<Guid> CreateEntry(CreateEntryCommand command)
        {
            var result = await client.PostAsJsonAsync("/api/Entry/CreateEntry", command);

            if (!result.IsSuccessStatusCode)
                return Guid.Empty;

            var guidStr = await result.Content.ReadAsStringAsync();

            return new Guid(guidStr.Trim('"'));
        }

        public async Task<Guid> CreateEntryComment(CreateEntryCommentCommand command)
        {
            var result = await client.PostAsJsonAsync("/api/Entry/CreateEntryComment", command);

            if (!result.IsSuccessStatusCode)
                return Guid.Empty;

            var guidStr = await result.Content.ReadAsStringAsync();

            return new Guid(guidStr.Trim('"'));
        }
        public async Task<List<SearchEntryViewModel>> SearchBySubject(string searchText)
        {
            var result = await client.GetFromJsonAsync<List<SearchEntryViewModel>>($"/api/entry/Search?searchText={searchText}");

            return result;
        }



    }
}
