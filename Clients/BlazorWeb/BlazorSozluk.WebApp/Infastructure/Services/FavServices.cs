using BlazorSozluk.WebApp.Infastructure.Services.Interfaces;

namespace BlazorSozluk.WebApp.Infastructure.Services
{
    public class FavServices : IFavServices
    {
        private readonly HttpClient client;

        public FavServices(HttpClient httpClient)
        {
            this.client = httpClient;
        }
        #region Entry Favorite Operations
        public async Task CreateEntryFav(Guid entryId)
        {
            await client.PostAsync($"/api/favorite/Entry/{entryId}", null);
        }
        public async Task DeleteEntryFav(Guid entryId)
        {
            await client.PostAsync($"/api/favorite/DeleteEntryFav/{entryId}", null);
        }
        #endregion

        #region Entry Comment Favorite Operations

        public async Task CreateEntryCommentFav(Guid entryCommentId)
        {
            await client.PostAsync($"/api/favorite/EntryComment/{entryCommentId}", null);
        }
        public async Task DeleteEntryCommentFav(Guid entryCommentId)
        {
            await client.PostAsync($"/api/favorite/DeleteEntryCommentFav/{entryCommentId}", null);
        }
        #endregion
    }
}
