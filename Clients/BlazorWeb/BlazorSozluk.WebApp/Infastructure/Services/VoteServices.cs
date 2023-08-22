using BlazorSozluk.WebApp.Infastructure.Services.Interfaces;
using BlazorSozlukCommon.Infastructure.Exceptions;
using BlazorSozlukCommon.ViewModels;

namespace BlazorSozluk.WebApp.Infastructure.Services
{
    public class VoteServices : IVoteServices
    {
        private readonly HttpClient client;

        public VoteServices(HttpClient client)
        {
            this.client = client;
        }
        public async Task DeleteEntryVote(Guid entryId)
        {
            var response = await client.PostAsync($"/api/Vote/DeleteEntryVote/{entryId}", null);

            if (!response.IsSuccessStatusCode)
                throw new Exception("DeleteEntryVote ERROR!");
        }
        public async Task DeleteEntryCommentVote(Guid entryCommentId)
        {
            var response = await client.PostAsync($"/api/Vote/DeleteEntryCommentVote/{entryCommentId}", null);

            if (!response.IsSuccessStatusCode)
                throw new Exception("DeleteEntryCommentVote ERROR!");
        }

        #region CreateEntryVote
        private async Task<HttpResponseMessage> CreateEntryVote(Guid entryId, VoteType voteType = VoteType.UpVote)
        {
            var result = await client.PostAsync($"/api/vote/entry/{entryId}?voteType={voteType}", null);

            if (result is null)
                throw new DatabaseValidateExceptions("EntryVOTE atanamadı");

            return result;

        }

        public async Task CreateEntryUpVote(Guid entryId)
        {
            await CreateEntryVote(entryId, VoteType.UpVote);
        }
        public async Task CreateEntryDownVote(Guid entryId)
        {
            await CreateEntryVote(entryId, VoteType.DownVote);
        }
        #endregion

        #region CreateEntryCommentVote
        private async Task<HttpResponseMessage> CreateEntryCommentVote(Guid entryCommentId, VoteType voteType = VoteType.UpVote)
        {
            var result = await client.PostAsync($"/api/vote/entrycomment/{entryCommentId}?voteType={voteType}", null);

            if (result is null)
                throw new DatabaseValidateExceptions("EntryCOMMENTvOTE atanamadı");

            return result;
        }
        public async Task CreateComemntEntryUpVote(Guid entryCommentId)
        {
            await CreateEntryCommentVote(entryCommentId, VoteType.UpVote);
        }
        public async Task CreateEntryCommentDownVote(Guid entryCommentId)
        {
            await CreateEntryCommentVote(entryCommentId, VoteType.DownVote);
        }
        #endregion

    }
}
