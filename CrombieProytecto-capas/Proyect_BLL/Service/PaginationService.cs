using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proyect_BLL.Service
{
    public class PaginationService<T>
    {
        public async Task<PaginatedResult<T>> GetPaginatedResult(IQueryable<T> source, int pageNumber, int pageSize)
        {
            var count = await Task.Run(() => source.Count());
            var items = await Task.Run(() => source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList());

            return new PaginatedResult<T>
            {
                Items = items,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = count,
                TotalPages = (int)Math.Ceiling(count / (double)pageSize)
            };
        }
    }

    public class PaginatedResult<T>
    {
        public List<T> Items { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
    }
}




