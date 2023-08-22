using BlazorSozlukCommon.ViewModels.Page;
using BlazorSozlukCommon.ViewModels.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Api.Application.Features.Queries.GetUserEntries
{
    public class GetUserEntriesQuery : BasedPageQuery, IRequest<PagedViewModel<GetUserEntriesDetailViewModel>>
    {
       

        public Guid? UserId { get; set; }
        public string UserName { get; set; }
        public GetUserEntriesQuery(Guid? userId,string userName=null,int page=1, int pageSize=10) : base(page, pageSize)
        {
            this.UserId = userId;
            this.UserName = userName;
        }

    }
}
