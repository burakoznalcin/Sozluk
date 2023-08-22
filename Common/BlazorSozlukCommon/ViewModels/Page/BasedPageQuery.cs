using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozlukCommon.ViewModels.Page
{
    public class BasedPageQuery
    {
        public int Page { get; set; }
        public int PageSize { get; set; }

        public BasedPageQuery(int page, int pageSize)
        {
            Page = page;
            PageSize = pageSize;
        }
    }
}
