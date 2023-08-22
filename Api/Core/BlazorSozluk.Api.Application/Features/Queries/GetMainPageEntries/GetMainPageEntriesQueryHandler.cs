using AutoMapper;
using BlazorSozluk.Api.Application.Interfaces.Repositories;
using BlazorSozlukCommon.Infastructure.Extensions;
using BlazorSozlukCommon.ViewModels.Page;
using BlazorSozlukCommon.ViewModels.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlazorSozluk.Api.Application.Features.Queries.GetEntries.GetMainPageEntries;

public class GetMainPageEntriesQueryHandler : IRequestHandler<GetMainPageEntriesQuery, PagedViewModel<GetEntryDetailViewModel>>
{
    private readonly IEntryRepository entryRepository;

    public GetMainPageEntriesQueryHandler(IEntryRepository entryRepository)
    {
        this.entryRepository = entryRepository;
    }
    public async Task<PagedViewModel<GetEntryDetailViewModel>> Handle(GetMainPageEntriesQuery request, CancellationToken cancellationToken)
    {
        var query = entryRepository.AsQueryable();

        query = query.Include(i => i.EntryFavorites)
            .Include(i => i.CreatedBy)
            .Include(i => i.EntryVotes);
        var list = query.Select(i => new GetEntryDetailViewModel()
        {
            Id = i.Id,
            Subject = i.Subject,
            Content = i.Content,
            IsFavorited = request.UserId.HasValue && i.EntryFavorites.Any(j => j.CreatedById == request.UserId),
            FavoritedCount = i.EntryFavorites.Count,
            CreatedDate = i.CreatedDate,
            CreatedByUserName = i.CreatedBy.UserName,
            VoteType =
            request.UserId.HasValue && i.EntryVotes.Any(j => j.CreatedById == request.UserId)
            ? i.EntryVotes.FirstOrDefault(j => j.CreatedById == request.UserId).VoteType
            : BlazorSozlukCommon.ViewModels.VoteType.None
        });

        var entries = await list.GetPaged(request.Page, request.PageSize);

        return entries;
    }
}