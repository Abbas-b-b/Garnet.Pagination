using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Garnet.Detail.Pagination.ListExtensions.Infrastructure;

namespace Garnet.Detail.Pagination.ListExtensions.Operators;

/// <summary>
/// An operator to for in list filter, like SQL in(...)
/// </summary>
internal class InListComparisionOperator : Operator
{
    /// <summary>
    /// An operator to for in list filter, like SQL in(...)
    /// </summary>
    public InListComparisionOperator() : base(ConfigProvider.PaginationFilterConfig.InListSign)
    {
    }

    /// <inheritdoc />
    public override IQueryable<T> Apply<T>(IQueryable<T> queryable, string expression)
    {
        var operands = GetOperands(expression);
        ValidateOperability(typeof(T), operands.Item1);

        var items = operands.Item2
            .Split(new[] { ConfigProvider.PaginationFilterConfig.InListSeparatorSign },
                StringSplitOptions.RemoveEmptyEntries)
            .Select(parameter => GetParameterObject(parameter.Trim(), operands.Item1.GetType()));

        return queryable.Where($"@0.Contains({operands.Item1})", items.ToList());
    }
}