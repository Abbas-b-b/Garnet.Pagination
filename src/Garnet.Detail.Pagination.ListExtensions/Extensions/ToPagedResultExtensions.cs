using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Garnet.Detail.Pagination.ListExtensions.DependencyInjection;
using Garnet.Detail.Pagination.ListExtensions.Exceptions;
using Garnet.Detail.Pagination.ListExtensions.Infrastructure;
using Garnet.Detail.Pagination.ListExtensions.Operators;
using Garnet.Pagination;
using Garnet.Standard.Pagination;

namespace Garnet.Detail.Pagination.ListExtensions.Extensions
{
    /// <summary>
    /// Bunch of extension methods to apply pagination on <see cref="ListExtensions"/>
    /// </summary>
    public static class ToPagedResultExtensions
    {
        /// <summary>
        /// Apply <paramref name="pagination"/> to <paramref name="elements"/>
        /// </summary>
        /// <param name="elements">The list of elements to apply the <paramref name="pagination"/> on</param>
        /// <param name="pagination">The Pagination to apply on <paramref name="elements"/></param>
        /// <typeparam name="TElement">Type of <paramref name="elements"/> and PagedElement result</typeparam>
        /// <returns>After applying the <paramref name="pagination"/> on <paramref name="elements"/></returns>
        /// <exception cref="ComparisionOperatorNotFoundException">When comparison could not be found from filter expression</exception>
        /// <exception cref="FieldNotFoundToOperateException">When the specified field in filer or order expression does ont exist in the type under filer or order</exception>
        /// <exception cref="InvalidExpressionException">When the filter or order expression is not valid</exception>
        /// <exception cref="InvalidOrderTypeException">When order type in the order expression is not valid</exception>
        /// <exception cref="InvalidUsageOfWildCard">When wild card usage in filter expression is not valid</exception>
        /// <exception cref="PaginationFilterConfigNotAssignedException">When PaginationFilterConfig not assigned with any of the <see cref="Garnet.Detail.Pagination.ListExtensions.DependencyInjection"/> methods</exception>
        /// <exception cref="PaginationFilterConfigNotRegisteredException">When PaginationFilterConfig not registered with any of the <see cref="Garnet.Pagination.DependencyInjection.GarnetPaginationDependencyInjection"/> methods</exception>
        /// <exception cref="PaginationOrderConfigNotAssignedException">When PaginationOrderConfig not assigned with any of the <see cref="Garnet.Detail.Pagination.ListExtensions.DependencyInjection"/> methods</exception>
        /// <exception cref="PaginationOrderConfigNotRegisteredException">When PaginationOrderConfig not registered with any of the <see cref="Garnet.Pagination.DependencyInjection.GarnetPaginationDependencyInjection"/> methods</exception>
        public static IPagedElements<TElement> ToPagedResult<TElement>(this IEnumerable<TElement> elements,
            IPagination pagination) where TElement : class
        {
            return elements.AsQueryable().ToPagedResult(pagination);
        }
        
        /// <summary>
        /// Apply <paramref name="pagination"/> to <paramref name="queryable"/>
        /// </summary>
        /// <param name="queryable">The IQueryable to apply the <paramref name="pagination"/> on</param>
        /// <param name="pagination">The Pagination to apply on <paramref name="queryable"/></param>
        /// <typeparam name="TElement">Type of <paramref name="queryable"/> and PagedElement result</typeparam>
        /// <returns>After applying the <paramref name="pagination"/> on <paramref name="queryable"/></returns>
        /// <exception cref="ComparisionOperatorNotFoundException">When comparison could not be found from filter expression</exception>
        /// <exception cref="FieldNotFoundToOperateException">When the specified field in filer or order expression does ont exist in the type under filer or order</exception>
        /// <exception cref="InvalidExpressionException">When the filter or order expression is not valid</exception>
        /// <exception cref="InvalidOrderTypeException">When order type in the order expression is not valid</exception>
        /// <exception cref="InvalidUsageOfWildCard">When wild card usage in filter expression is not valid</exception>
        /// <exception cref="PaginationFilterConfigNotAssignedException">When PaginationFilterConfig not assigned with any of the <see cref="Garnet.Detail.Pagination.ListExtensions.DependencyInjection"/> methods</exception>
        /// <exception cref="PaginationFilterConfigNotRegisteredException">When PaginationFilterConfig not registered with any of the <see cref="Garnet.Pagination.DependencyInjection.GarnetPaginationDependencyInjection"/> methods</exception>
        /// <exception cref="PaginationOrderConfigNotAssignedException">When PaginationOrderConfig not assigned with any of the <see cref="Garnet.Detail.Pagination.ListExtensions.DependencyInjection"/> methods</exception>
        /// <exception cref="PaginationOrderConfigNotRegisteredException">When PaginationOrderConfig not registered with any of the <see cref="Garnet.Pagination.DependencyInjection.GarnetPaginationDependencyInjection"/> methods</exception>
        public static IPagedElements<TElement> ToPagedResult<TElement>(
            this IQueryable<TElement> queryable,
            IPagination pagination) where TElement : class
        {
            queryable = ApplyFilter(queryable, pagination.Filters);
            queryable = ApplyOrder(queryable, pagination.Orders);

            var totalElements = queryable.LongCount();

            queryable = ApplySkipTake(queryable, pagination.Offset, pagination.PageSize);

            var elements = queryable.ToList();

            return new PagedElements<TElement>(pagination, elements, totalElements);
        }

        /// <summary>
        /// Apply <paramref name="pagination"/> to <paramref name="queryable"/> asynchronously
        /// </summary>
        /// <param name="queryable">The IQueryable to apply the <paramref name="pagination"/> on</param>
        /// <param name="pagination">The Pagination to apply on <paramref name="queryable"/></param>
        /// <typeparam name="TElement">Type of <paramref name="queryable"/> and PagedElement result</typeparam>
        /// <returns>After applying the <paramref name="pagination"/> on <paramref name="queryable"/></returns>
        /// <exception cref="ComparisionOperatorNotFoundException">When comparison could not be found from filter expression</exception>
        /// <exception cref="FieldNotFoundToOperateException">When the specified field in filer or order expression does ont exist in the type under filer or order</exception>
        /// <exception cref="InvalidExpressionException">When the filter or order expression is not valid</exception>
        /// <exception cref="InvalidOrderTypeException">When order type in the order expression is not valid</exception>
        /// <exception cref="InvalidUsageOfWildCard">When wild card usage in filter expression is not valid</exception>
        /// <exception cref="PaginationFilterConfigNotAssignedException">When PaginationFilterConfig not assigned with any of the <see cref="Garnet.Detail.Pagination.ListExtensions.DependencyInjection"/> methods</exception>
        /// <exception cref="PaginationFilterConfigNotRegisteredException">When PaginationFilterConfig not registered with any of the <see cref="Garnet.Pagination.DependencyInjection.GarnetPaginationDependencyInjection"/> methods</exception>
        /// <exception cref="PaginationOrderConfigNotAssignedException">When PaginationOrderConfig not assigned with any of the <see cref="Garnet.Detail.Pagination.ListExtensions.DependencyInjection"/> methods</exception>
        /// <exception cref="PaginationOrderConfigNotRegisteredException">When PaginationOrderConfig not registered with any of the <see cref="Garnet.Pagination.DependencyInjection.GarnetPaginationDependencyInjection"/> methods</exception>
        public static async Task<IPagedElements<TElement>> ToPagedResultAsync<TElement>(
            this IQueryable<TElement> queryable,
            IPagination pagination) where TElement : class
        {
            queryable = ApplyFilter(queryable, pagination.Filters);
            queryable = ApplyOrder(queryable, pagination.Orders);

            var totalElements = await ConfigProvider.QueryableAsyncMethods.LongCountAsync(queryable);

            queryable = ApplySkipTake(queryable, pagination.Offset, pagination.PageSize);

            var elements = await ConfigProvider.QueryableAsyncMethods.ToListAsync(queryable);

            return new PagedElements<TElement>(pagination, elements, totalElements);
        }


        private static IQueryable<T> ApplyFilter<T>(IQueryable<T> queryable, string filterExpressions) where T : class
        {
            if (string.IsNullOrEmpty(filterExpressions) || string.IsNullOrWhiteSpace(filterExpressions))
            {
                return queryable;
            }

            var filters = filterExpressions.Split(
                new[] { ConfigProvider.PaginationFilterConfig.FilterExpressionSeparatorSign },
                StringSplitOptions.RemoveEmptyEntries);

            foreach (var filterExpression in filters)
            {
                var comparisionOperator = OperatorFactory.GetComparisionOperator(filterExpression);

                queryable = comparisionOperator.Apply(queryable, filterExpression);
            }

            return queryable;
        }

        private static IQueryable<T> ApplyOrder<T>(IQueryable<T> queryable, string orderExpression) where T : class
        {
            if (string.IsNullOrEmpty(orderExpression) || string.IsNullOrWhiteSpace(orderExpression))
            {
                return queryable;
            }

            var orderOperator = OperatorFactory.GetOrderOperator();

            return orderOperator.Apply(queryable, orderExpression);
        }

        private static IQueryable<T> ApplySkipTake<T>(IQueryable<T> queryable, long offset, int pageSize)
        {
            while (offset > 0)
            {
                var skipCount = offset >= int.MaxValue ? int.MaxValue : (int)offset;

                queryable = queryable.Skip(skipCount);

                offset -= skipCount;
            }

            return queryable.Take(pageSize);
        }
    }
}