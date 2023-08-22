using BlazorSozlukCommon.ViewModels.Page;
using BlazorSozlukCommon.ViewModels.Queries;
using BlazorSozlukCommon.ViewModels.RequestModels;

namespace BlazorSozluk.WebApp.Infastructure.Services.Interfaces
{
    public interface IEntryService
    {
        Task<Guid> CreateEntry(CreateEntryCommand command);
        Task<Guid> CreateEntryComment(CreateEntryCommentCommand command);
        Task<GetEntryDetailViewModel> GetById(Guid entryId);
        Task<List<GetEntriesViewModel>> GetEntries();
        Task<PagedViewModel<GetEntryCommentsViewModel>> GetEntryComments(Guid entryId, int pageSize, int page);
        Task<PagedViewModel<GetEntryDetailViewModel>> GetMainPageEntries(int page, int pageSize);
        Task<PagedViewModel<GetEntryDetailViewModel>> GetProfilePageEntries(int page, int pageSize, string username = null);
        Task<List<SearchEntryViewModel>> SearchBySubject(string searchText);
    }
}