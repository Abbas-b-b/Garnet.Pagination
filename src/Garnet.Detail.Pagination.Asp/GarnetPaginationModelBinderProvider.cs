using System;
using Garnet.Standard.Pagination;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace Garnet.Detail.Pagination.Asp;

/// <summary>
/// Use <see cref="GarnetPaginationModelBinder"/> for action parameters of type <see cref="IPagination"/> or <see cref="Garnet.Pagination.Pagination"/>
/// </summary>
public class GarnetPaginationModelBinderProvider : IModelBinderProvider
{
    /// <summary>
    /// Set <see cref="GarnetPaginationModelBinder"/> for action parameters of type <see cref="IPagination"/> or <see cref="Garnet.Pagination.Pagination"/>
    /// </summary>
    public IModelBinder GetBinder(ModelBinderProviderContext context)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        return context.Metadata.ModelType.IsAssignableFrom(typeof(IPagination))
               || context.Metadata.ModelType.IsAssignableFrom(typeof(Garnet.Pagination.Pagination))
            ? new BinderTypeModelBinder(typeof(GarnetPaginationModelBinder))
            : null;
    }
}