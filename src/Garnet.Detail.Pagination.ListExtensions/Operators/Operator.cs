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
    /// Get type of field that this operator can be applied to the <paramref name="fieldNameChain"/> of <paramref name="typeToApplyOperatorOn"/> and throws exception of type <see cref="FieldNotFoundToOperateException"/> if not found
    /// </summary>
    /// <param name="typeToApplyOperatorOn">Type with <paramref name="fieldNameChain"/></param>
    /// <param name="fieldNameChain">in type <paramref name="typeToApplyOperatorOn"/> to apply this operator</param>
    /// <exception cref="FieldNotFoundToOperateException">If <paramref name="fieldNameChain"/> not exists in <paramref name="typeToApplyOperatorOn"/></exception>
    protected static Type GetRequiredTypeOfFieldChain(Type typeToApplyOperatorOn, string fieldNameChain)
    {
        var fieldType = GetTypeOfFieldChain(typeToApplyOperatorOn, fieldNameChain);

        if (fieldType is null)
        {
            throw new FieldNotFoundToOperateException(typeToApplyOperatorOn, fieldNameChain);
        }

        return fieldType;
    }

    /// <summary>
    /// To get type of the last field name separated by '.' exists in nested properties of type <paramref name="typeToApplyOperatorOn"/>
    /// </summary>
    /// <param name="typeToApplyOperatorOn">Type to start checking the chain <paramref name="fieldNameChain"/></param>
    /// <param name="fieldNameChain">Fields chains to check against <paramref name="typeToApplyOperatorOn"/></param>
    /// <returns>The type of last field in the <paramref name="fieldNameChain"/> on <paramref name="typeToApplyOperatorOn"/> or null if not found</returns>
    private static Type GetTypeOfFieldChain(Type typeToApplyOperatorOn, string fieldNameChain)
    {
        foreach (var fieldName in fieldNameChain.Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries))
        {
            var property = typeToApplyOperatorOn.GetProperties()
                .FirstOrDefault(propertyInfo => propertyInfo.GetGetMethod().IsPublic
                                                && string.Compare(propertyInfo.Name,
                                                    fieldName,
                                                    StringComparison.OrdinalIgnoreCase) == 0);

            if (property is null)
            {
                return null;
            }

            typeToApplyOperatorOn = property.PropertyType;
        }

        return typeToApplyOperatorOn;
    }

    /// <summary>
    /// Get actual object from the value literal
    /// </summary>
    /// <param name="value">The value literal to extract actual object</param>
    /// <param name="fieldType">The known type of operand</param>
    /// <returns>Extracted object</returns>
    protected static object GetParameterObject(string value, Type fieldType)
    {
        if (string.Compare("null", value, StringComparison.OrdinalIgnoreCase) == 0)
        {
            return null;
        }

        if (fieldType.IsEquivalentTo(typeof(DateTime)) && DateTime.TryParse(value, out var resultDateTime))
        {
            return resultDateTime;
        }

        if (fieldType.IsEquivalentTo(typeof(TimeSpan)) && TimeSpan.TryParse(value, out var resultTimeSpan))
        {
            return resultTimeSpan;
        }

        if (fieldType.IsEquivalentTo(typeof(Guid)) && Guid.TryParse(value, out var resultGuid))
        {
            return resultGuid;
        }

        return value;
    }
}