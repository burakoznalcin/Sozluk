using BlazorSozluk.Api.Application.Interfaces.Repositories;
using BlazorSozlukCommon.Infastructure.Extensions;
using BlazorSozlukCommon.ViewModels.Page;
using BlazorSozlukCommon.ViewModels.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Api.Application.Features.Queries.GetEntryComments
{
    public class GetEntryCommentsQueryHandler : IRequestHandler<GetEntryCommentsQuery, PagedViewModel<GetEntryCommentsViewModel>>
    {
        private readonly IEntryCommentRepository entryCommentRepository;

        public GetEntryCommentsQueryHandler(IEntryCommentRepository entryCommentRepository)
        {
            this.entryCommentRepository = entryCommentRepository;
        }
        public async Task<PagedViewModel<GetEntryCommentsViewModel>> Handle(GetEntryCommentsQuery request, CancellationToken cancellationToken)
        {
            var query = entryCommentRepository.AsQueryable();

            query = query.Include(i => i.EntryCommentFavorite)
                .Include(i => i.CreatedBy)
                .Include(i => i.EntryCommentVote)
                .Where(i=> i .EntryId == request.EntryId);
            var list = query.Select(i => new GetEntryCommentsViewModel()
            {
                Id = i.Id,
                Content = i.Content,
                IsFavorited = request.UserId.HasValue && i.EntryCommentFavorite.Any(j => j.CreatedById == request.UserId),
                FavoritedCount = i.EntryCommentFavorite.Count,
                CreatedDate = i.CreatedDate,
                CreatedByUserName = i.CreatedBy.UserName,
                VoteType =
                request.UserId.HasValue && i.EntryCommentVote.Any(j => j.CreatedById == request.UserId)
                ? i.EntryCommentVote.FirstOrDefault(j => j.CreatedById == request.UserId).VoteType
                : BlazorSozlukCommon.ViewModels.VoteType.None
            });

            var entries = await list.GetPaged(request.Page, request.PageSize);

            return entries;
        }
    }
}
