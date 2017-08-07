using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Linq;
using System.Reflection;

namespace DevInstinct.AspNetCore.Mvc.ModelBinding
{
    // Note: his code doesn't work
    // http://www.dotnet-programming.com/post/2017/03/17/Custom-Model-Binding-in-Aspnet-Core-2-Model-Binding-Interfaces.aspx
    public class InterfaceModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (!context.Metadata.IsCollectionType &&
                (context.Metadata.ModelType.GetTypeInfo().IsInterface ||
                context.Metadata.ModelType.GetTypeInfo().IsAbstract) &&
                (context.BindingInfo.BindingSource == null ||
                !context.BindingInfo.BindingSource
                .CanAcceptDataFrom(BindingSource.Services)))
            {
                var propertyBinders = context.Metadata.Properties.ToDictionary(property => property, context.CreateBinder);
                return new InterfaceModelBinder(propertyBinders);
            }

            return null;
        }
    }
}
