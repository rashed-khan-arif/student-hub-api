using System;
using System.Collections.Generic;
using System.Text;

namespace StudentHub.Models.Common
{
    public class PagedList<T>
    {
        public PagedList()
        {

        }

        public PagedList(IEnumerable<T> items, int totalCount, int page, int pageSize)
        {
            Items = items;
            TotalCount = totalCount;
            Page = page;
            PageSize = pageSize;
        }

        public IEnumerable<T> Items { get; set; }
        public int TotalCount { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }

    }
}
