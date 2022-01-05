using System;

namespace Garnet.Detail.Pagination.ListExtensions.Exceptions;

/// <summary>
/// Field not found in type to apply the operator
/// </summary>
public class FieldNotFoundToOperateException : Exception
{
    /// <summary>
    /// Field not found in type to apply the operator
    /// </summary>
    /// <param name="type">Type to apply the operator on an field of it</param>
    /// <param name="fieldName">The field name of type <paramref name="type"/> to apply the operator on</param>
    public FieldNotFoundToOperateException(Type type, string fieldName)
        : base($"Field {fieldName} not found on {type.FullName ?? type.Name}")
    {
    }
}