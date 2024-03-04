﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MoviesList.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesList.Core.Validator
{
    public static class Paginator
    {
        public static async Task<PaginationResult<IEnumerable<TDestination>>> PaginationAsync<TSource, TDestination>(this IQueryable<TSource> queryable,
           int pageSize, int pageNumber, IMapper mapper)
           where TSource : class
           where TDestination : class
        {
            var count = queryable.Count();
            var pageResult = new PaginationResult<IEnumerable<TDestination>>
            {
                PageSize = (pageSize > 10 || pageNumber < 1) ? 10 : pageSize,
                CurrentPage = pageNumber > 1 ? pageNumber : 1,
                PreviousPage = pageNumber > 0 ? pageNumber - 1 : 0,
            };
            pageResult.NumberOfPages = count % pageResult.PageSize != 0
                ? count / pageResult.PageSize + 1
                : count / pageResult.PageSize;
            List<TSource> sourceList = await queryable.Skip((pageResult.CurrentPage - 1) * pageResult.PageSize).Take(pageResult.PageSize).ToListAsync();
            var destinationList = mapper.Map<IEnumerable<TDestination>>(sourceList);
            pageResult.PageItems = destinationList;
            return pageResult;
        }
    }
}
