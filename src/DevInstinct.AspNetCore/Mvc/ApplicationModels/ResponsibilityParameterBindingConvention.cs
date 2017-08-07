using DevInstinct.Patterns.CQRSPattern;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;

namespace DevInstinct.AspNetCore.Mvc.ApplicationModels
{
    // Removes the need for the FromBodyAttribute.
    // http://benfoster.io/blog/aspnet-core-customising-model-binding-conventions
    public class ResponsibilityParameterBindingConvention : IActionModelConvention
    {
        public void Apply(ActionModel action)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            foreach (var parameter in action.Parameters)
            {
                if (typeof(IResponsibility).IsAssignableFrom((parameter.ParameterInfo.ParameterType)))
                {
                    parameter.BindingInfo = parameter.BindingInfo ?? new BindingInfo();
                    parameter.BindingInfo.BindingSource = BindingSource.Body;
                }
            }
        }
    }
}
