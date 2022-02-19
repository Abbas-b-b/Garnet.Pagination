using System.Linq;
using System.Linq.Dynamic.Core;
using Garnet.Detail.Pagination.ListExtensions.Exceptions;
using Garnet.Detail.Pagination.ListExtensions.Infrastructure;

namespace Garnet.Detail.Pagination.ListExtensions.Operators;

/// <summary>
/// Order operator
/// </summary>
internal class OrderOperator : Operator
{
    /// <summary>
    /// Order operator
    /// </summary>
    public OrderOperator() : base(ConfigProvider.PaginationOrderConfig.OrderFieldAndTypeSeparator)
    {
    }

    /// <inheritdoc />
    public override IQueryable<T> Apply<T>(IQueryable<T> queryable, string expression)
    {
        var operands = GetOperands(expression);
        GetRequiredTypeOfFieldChain(typeof(T), operands.Item1);

        if (string.CompareOrdinal(operands.Item2,
                ConfigProvider.PaginationOrderConfig.AscendingSign) == 0)
        {
            return queryable.OrderBy(operands.Item1);
        }

        if (string.CompareOrdinal(operands.Item2,
                ConfigProvider.PaginationOrderConfig.DescendingSign) == 0)
        {
            return queryable.OrderBy($"{operands.Item1} DESC");
        }

        throw new InvalidOrderTypeException(operands.Item2);
    }
}