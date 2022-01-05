using System;
using System.Linq;
using Garnet.Detail.Pagination.ListExtensions.Exceptions;

namespace Garnet.Detail.Pagination.ListExtensions.Operators;

/// <summary>
/// Base implementation for <see cref="IOperator"/>
/// </summary>
internal abstract class Operator : IOperator
{
    /// <summary>
    /// The operator corresponding sign
    /// </summary>
    protected readonly string OperatorSign;

    /// <summary>
    /// Base implementation for <see cref="IOperator"/>
    /// </summary>
    /// <param name="operatorSign">The operator corresponding sign</param>
    protected Operator(string operatorSign)
    {
        OperatorSign = operatorSign;
    }

    /// <inheritdoc />
    public bool TryParseExpression(string expression, out Tuple<string, string> operands)
    {
        var extractedOperands = expression.Split(new[] { OperatorSign }, StringSplitOptions.RemoveEmptyEntries);

        if (extractedOperands.Length != 2)
        {
            //It's hard to determine the exact problem with the expression
            //like missing operator or using operator multiple times and so on
            //So a general exception is thrown
            operands = null;

            return false;
        }

        operands = new Tuple<string, string>(extractedOperands[0].Trim(), extractedOperands[1].Trim());

        return true;
    }

    /// <inheritdoc />
    public abstract IQueryable<T> Apply<T>(IQueryable<T> queryable, string expression) where T : class;


    /// <summary>
    /// Get operands from <paramref name="expression"/>
    /// </summary>
    /// <param name="expression">The expression literal to get the operands</param>
    /// <returns>Extracted operands</returns>
    /// <exception cref="InvalidExpressionException">If unable to extract operands from <paramref name="expression"/></exception>
    protected Tuple<string, string> GetOperands(string expression)
    {
        var isValidExpression = TryParseExpression(expression, out var operands);

        if (!isValidExpression || operands is null)
        {
            throw new InvalidExpressionException(expression, OperatorSign);
        }

        return operands;
    }

    /// <summary>
    /// To validate whether this operator can be applied to the <paramref name="fieldName"/> of <paramref name="typeToApplyOperatorOn"/>
    /// </summary>
    /// <param name="typeToApplyOperatorOn">Type with <paramref name="fieldName"/></param>
    /// <param name="fieldName">in type <paramref name="typeToApplyOperatorOn"/> to apply this operator</param>
    /// <exception cref="FieldNotFoundToOperateException">If <paramref name="fieldName"/> not exists in <paramref name="typeToApplyOperatorOn"/></exception>
    protected static void ValidateOperability(Type typeToApplyOperatorOn, string fieldName)
    {
        if (!FieldNameChainExist(typeToApplyOperatorOn, fieldName))
        {
            throw new FieldNotFoundToOperateException(typeToApplyOperatorOn, fieldName);
        }
    }

    /// <summary>
    /// Get actual object from the value literal
    /// </summary>
    /// <param name="value">The value literal to extract actual object</param>
    /// <returns>Extracted object</returns>
    protected static object GetParameterObject(string value)
    {
        if (string.Compare("null", value, StringComparison.OrdinalIgnoreCase) == 0)
        {
            return null;
        }

        if (DateTime.TryParse(value, out var resultDatetime))
        {
            return resultDatetime;
        }

        return value;
    }

    /// <summary>
    /// Checks whether all field name separated by '.' exists in nested properties of type <paramref name="type"/>
    /// </summary>
    /// <param name="type">Type to start checking the chain <paramref name="fieldNameChain"/></param>
    /// <param name="fieldNameChain">Fields chains to check against <paramref name="type"/></param>
    /// <returns></returns>
    private static bool FieldNameChainExist(Type type, string fieldNameChain)
    {
        foreach (var fieldName in fieldNameChain.Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries))
        {
            var property = type.GetProperties()
                .FirstOrDefault(propertyInfo => propertyInfo.GetGetMethod().IsPublic
                                                && string.Compare(propertyInfo.Name,
                                                    fieldName,
                                                    StringComparison.OrdinalIgnoreCase) == 0);

            if (property is null)
            {
                return false;
            }

            type = property.PropertyType;
        }

        return true;
    }
}