using System.Collections.Generic;
using System.Linq;
using Garnet.Detail.Pagination.ListExtensions.Exceptions;
using Garnet.Detail.Pagination.ListExtensions.Infrastructure;

namespace Garnet.Detail.Pagination.ListExtensions.Operators;

/// <summary>
/// The factory pattern for operators
/// </summary>
internal static class OperatorFactory
{
    private static readonly Dictionary<string, IOperator> ComparisionOperators =
        new()
        {
            {
                ConfigProvider.PaginationFilterConfig.GreaterThanOrEqualSign,
                new SimpleComparisionOperator(ConfigProvider.PaginationFilterConfig
                    .GreaterThanOrEqualSign)
            },
            {
                ConfigProvider.PaginationFilterConfig.LessThanOrEqualSign,
                new SimpleComparisionOperator(ConfigProvider.PaginationFilterConfig
                    .LessThanOrEqualSign)
            },
            {
                ConfigProvider.PaginationFilterConfig.EqualSign,
                new SimpleComparisionOperator(ConfigProvider.PaginationFilterConfig.EqualSign)
            },
            {
                ConfigProvider.PaginationFilterConfig.NotEqualSign,
                new SimpleComparisionOperator(ConfigProvider.PaginationFilterConfig
                    .NotEqualSign)
            },
            {
                ConfigProvider.PaginationFilterConfig.GreaterThanSign,
                new SimpleComparisionOperator(ConfigProvider.PaginationFilterConfig
                    .GreaterThanSign)
            },
            {
                ConfigProvider.PaginationFilterConfig.LessThanSign,
                new SimpleComparisionOperator(ConfigProvider.PaginationFilterConfig
                    .LessThanSign)
            },
            {
                ConfigProvider.PaginationFilterConfig.LikesSign,
                new LikesComparisionOperator()
            },
            {
                ConfigProvider.PaginationFilterConfig.InListSign,
                new InListComparisionOperator()
            }
        };

    private static readonly string[] SortedComparisionOperatorsSign = ComparisionOperators.Keys
        .OrderByDescending(sign => sign.Length)
        .ToArray();

    private static readonly OrderOperator OrderOperator = new();

    /// <summary>
    /// Get a comparision operator for <paramref name="expression"/>
    /// </summary>
    /// <param name="expression">The expression literal to get a comparision operator</param>
    /// <returns>Extracted operator from <paramref name="expression"/></returns>
    /// <exception cref="ComparisionOperatorNotFoundException">In no operator found for <paramref name="expression"/></exception>
    internal static IOperator GetComparisionOperator(string expression)
    {
        var operatorKey = SortedComparisionOperatorsSign.FirstOrDefault(expression.Contains);

        if (operatorKey is null)
        {
            throw new ComparisionOperatorNotFoundException(expression);
        }

        return ComparisionOperators[operatorKey];
    }

    /// <summary>
    /// Get a order operator
    /// </summary>
    /// <returns>An order operator</returns>
    internal static IOperator GetOrderOperator()
    {
        return OrderOperator;
    }
}