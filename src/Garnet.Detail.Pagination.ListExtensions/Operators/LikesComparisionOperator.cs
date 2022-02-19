using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Garnet.Detail.Pagination.ListExtensions.Exceptions;
using Garnet.Detail.Pagination.ListExtensions.Infrastructure;

namespace Garnet.Detail.Pagination.ListExtensions.Operators;

/// <summary>
/// Like operator similar to SQL Like '%%'
/// </summary>
internal class LikesComparisionOperator : Operator
{
    /// <summary>
    /// Like operator like SQL Like '%%'
    /// </summary>
    public LikesComparisionOperator() : base(ConfigProvider.PaginationFilterConfig.LikesSign)
    {
    }

    /// <inheritdoc />
    public override IQueryable<T> Apply<T>(IQueryable<T> queryable, string expression)
    {
        var operands = GetOperands(expression);
        ValidateOperability(typeof(T), operands.Item1);

        var secondOperandValue = operands.Item2.Replace(
            ConfigProvider.PaginationFilterConfig.ZeroOrMoreCharactersWildCardSign, "");

        var startsWithWildCard = operands.Item2.StartsWith(
            ConfigProvider.PaginationFilterConfig.ZeroOrMoreCharactersWildCardSign,
            StringComparison.Ordinal);

        var endsWithWildCard = operands.Item2.EndsWith(
            ConfigProvider.PaginationFilterConfig.ZeroOrMoreCharactersWildCardSign,
            StringComparison.Ordinal);

        var wildCardCount = (startsWithWildCard ? 1 : 0) + (endsWithWildCard ? 1 : 0);

        var dynamicFilter = wildCardCount switch
        {
            0 => $"{operands.Item1} == @0",

            2 => $"{operands.Item1}.Contains(@0)",

            1 => $"{operands.Item1}.{(startsWithWildCard ? "Ends" : "Starts")}With(@0)",

            _ => throw new InvalidUsageOfWildCard(expression,
                ConfigProvider.PaginationFilterConfig.ZeroOrMoreCharactersWildCardSign)
        };

        return queryable.Where(dynamicFilter, GetParameterObject(secondOperandValue, operands.Item1.GetType()));
    }
}
