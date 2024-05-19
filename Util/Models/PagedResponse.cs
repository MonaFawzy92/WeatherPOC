using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Util.Models
{
    public class PagedResponse<T>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }
        public IEnumerable<T> Data { get; set; }

        public PagedResponse(IEnumerable<T> data, PaginationFilter Filter, int TotalRecords)
        {
            this.PageNumber = Filter.PageNumber;
            this.PageSize = Filter.PageSize;
            this.Data = data;
            this.TotalRecords = TotalRecords;
            if (Filter.PageSize > 0)
                this.TotalPages = (int)Math.Ceiling(TotalRecords / (double)Filter.PageSize);
            else
                this.TotalPages = 0;
        }

        public static async Task<PagedResponse<T>> ToPagedList(IQueryable<T> source, PaginationFilter Filter)
        {
            var count = source.Count();
            var items = await source.Skip((Filter.PageNumber - 1) * Filter.PageSize).Take(Filter.PageSize).ToListAsync();
            return new PagedResponse<T>(items, Filter, count);
        }
    }
}
