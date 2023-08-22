namespace BlazorSozluk.WebApp.Infastructure.Services.Interfaces
{
    public interface IVoteServices
    {
        Task CreateComemntEntryUpVote(Guid entryCommentId);
        Task CreateEntryCommentDownVote(Guid entryCommentId);
        Task CreateEntryDownVote(Guid entryId);
        Task CreateEntryUpVote(Guid entryId);
        Task DeleteEntryCommentVote(Guid entryCommentId);
        Task DeleteEntryVote(Guid entryId);
    }
}