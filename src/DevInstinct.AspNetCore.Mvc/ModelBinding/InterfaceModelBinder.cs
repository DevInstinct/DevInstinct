using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using System.Collections.Generic;

namespace DevInstinct.AspNetCore.Mvc.ModelBinding
{
    // Note: his code doesn't work
    // http://www.dotnet-programming.com/post/2017/03/17/Custom-Model-Binding-in-Aspnet-Core-2-Model-Binding-Interfaces.aspx
    public class InterfaceModelBinder : ComplexTypeModelBinder
    {

        public InterfaceModelBinder(IDictionary<ModelMetadata, IModelBinder> propertyBinder)
            : base(propertyBinder)
        {
        }

        protected override object CreateModel(ModelBindingContext bindingContext)
        {
            return bindingContext.HttpContext
                .RequestServices.GetService(bindingContext.ModelType);
        }
    }
}
