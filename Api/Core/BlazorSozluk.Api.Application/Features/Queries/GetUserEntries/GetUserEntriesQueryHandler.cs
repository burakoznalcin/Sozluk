using BlazorSozluk.Api.Application.Interfaces.Repositories;
using BlazorSozlukCommon.Infastructure.Exceptions;
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

namespace BlazorSozluk.Api.Application.Features.Queries.GetUserEntries
{
    public class GetUserEntriesQueryHandler : IRequestHandler<GetUserEntriesQuery, PagedViewModel<GetUserEntriesDetailViewModel>>
    {
        private readonly IEntryRepository entryRepository;
        public GetUserEntriesQueryHandler(IEntryRepository entryRepository)
        {
            this.entryRepository = entryRepository;
        }

        public async Task<PagedViewModel<GetUserEntriesDetailViewModel>> Handle(GetUserEntriesQuery request, CancellationToken cancellationToken)
        {
            var query = entryRepository.AsQueryable();

            if (request.UserId != null && request.UserId.HasValue && request.UserId != Guid.Empty)
                query = query.Where(i => i.CreatedById == request.UserId);

            else if (!string.IsNullOrEmpty(request.UserName))
                query = query.Where(i => i.CreatedBy.UserName == request.UserName);

            else  throw new DatabaseValidateExceptions("User is not found!");

            query = query.Include(i => i.EntryFavorites)
                         .Include(i => i.CreatedBy);

            var list = query.Select(i => new GetUserEntriesDetailViewModel()
            {
                Id = i.Id,
                Content = i.Content,
                CreatedByUserName = i.CreatedBy.UserName,
                CreatedDate = i.CreatedDate,
                FavoritedCount = i.EntryFavorites.Count,
                Subject = i.Subject,
                IsFavorited = false
            });

            var entries = await list.GetPaged(request.Page, request.PageSize);

            return entries;
        }
    }
}
