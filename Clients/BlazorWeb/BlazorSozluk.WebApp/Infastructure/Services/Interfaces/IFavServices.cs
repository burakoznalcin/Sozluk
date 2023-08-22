namespace BlazorSozluk.WebApp.Infastructure.Services.Interfaces
{
    public interface IFavServices
    {
        Task CreateEntryCommentFav(Guid entryCommentId);
        Task CreateEntryFav(Guid entryId);
        Task DeleteEntryCommentFav(Guid entryCommentId);
        Task DeleteEntryFav(Guid entryId);
    }
}