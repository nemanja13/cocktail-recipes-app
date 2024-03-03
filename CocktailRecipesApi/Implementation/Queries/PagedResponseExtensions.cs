using Application.Queries;
using Application.Searches;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Queries
{
    public static class PagedResponseExtensions
    {
        public static PagedResponse<D> GetPagedResponse<T, D>(this IQueryable<T> query, PagedSearch search, IMapper mapper)
            where T : class
            where D : class
        {
            if (search.PerPage.HasValue && search.Page.HasValue)
            {
                var skipCount = search.PerPage * (search.Page - 1);

                var response = new PagedResponse<D>
                {
                    CurrentPage = search.Page,
                    ItemsPerPage = search.PerPage,
                    TotalCount = query.Count(),
                    Data = mapper.Map<List<D>>(query.Skip(skipCount.Value).Take(search.PerPage.Value))
                };
                return response;
            }
            else
            {
                var response = new PagedResponse<D>
                {
                    Data = mapper.Map<List<D>>(query)
                };
                return response;
            }

        }
    }
}
