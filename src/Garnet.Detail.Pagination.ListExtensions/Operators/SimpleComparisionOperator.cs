using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using Garnet.Detail.Pagination.ListExtensions.Exceptions;
using Garnet.Detail.Pagination.ListExtensions.Infrastructure;

namespace Garnet.Detail.Pagination.ListExtensions.Operators;

/// <summary>
/// Represents simple operators like greater than, less than, less than or equal, ...
/// </summary>
internal class SimpleComparisionOperator : Operator
{
    private readonly Dictionary<string, string> _filterConfigSimpleOperatorToDynamicQueryOperatorMap =
        new()
        {
            { ConfigProvider.PaginationFilterConfig.GreaterThanOrEqualSign, ">=" },
            { ConfigProvider.PaginationFilterConfig.LessThanOrEqualSign, "<=" },
            { ConfigProvider.PaginationFilterConfig.EqualSign, "==" },
            { ConfigProvider.PaginationFilterConfig.NotEqualSign, "!=" },
            { ConfigProvider.PaginationFilterConfig.GreaterThanSign, ">" },
            { ConfigProvider.PaginationFilterConfig.LessThanSign, "<" }
        };

    /// <summary>
    /// Represents simple operators like greater than, less than, less than or equal, ...
    /// </summary>
    /// <param name="operatorSign">The operator corresponding sign</param>
    public SimpleComparisionOperator(string operatorSign) : base(operatorSign)
    {
    }

    /// <inheritdoc />
    public override IQueryable<T> Apply<T>(IQueryable<T> queryable, string expression)
    {
        var operands = GetOperands(expression);
        var operandFieldType = GetRequiredTypeOfFieldChain(typeof(T), operands.Item1);

        var dynamicQueryOperatorSign = GetDynamicQueryOperatorSign();

        return queryable.Where($"{operands.Item1} {dynamicQueryOperatorSign} @0",
            GetParameterObject(operands.Item2, operandFieldType));
    }

    private string GetDynamicQueryOperatorSign()
    {
        if (!_filterConfigSimpleOperatorToDynamicQueryOperatorMap.ContainsKey(OperatorSign))
        {
            throw new DynamicQueryOperatorSignNotFound(OperatorSign);
        }

        return _filterConfigSimpleOperatorToDynamicQueryOperatorMap[OperatorSign];
    }
}