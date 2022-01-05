using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text.RegularExpressions;
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

        var wildCardCount = Regex.Match(operands.Item2,
            ConfigProvider.PaginationFilterConfig.ZeroOrMoreCharactersWildCardSign).Length;

        var wildCardFirstIndex = operands.Item2.IndexOf(
            ConfigProvider.PaginationFilterConfig.ZeroOrMoreCharactersWildCardSign,
            StringComparison.Ordinal);

        var dynamicFilter = wildCardCount switch
        {
            0 => $"{operands.Item1} == @0",

            2 => $"{operands.Item1}.Contains(@0)",

            1 => $"{operands.Item1}.{(wildCardFirstIndex == 0 ? "Ends" : "Starts")}With(@0)",

            _ => throw new InvalidUsageOfWildCard(expression,
                ConfigProvider.PaginationFilterConfig.ZeroOrMoreCharactersWildCardSign)
        };

        return queryable.Where(dynamicFilter, GetParameterObject(secondOperandValue));
    }
}