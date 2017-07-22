using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq;

namespace DevInstinct.AspNetCore.Mvc.ModelBinding
{
    // http://www.jerriepelser.com/blog/validation-response-aspnet-core-webapi/
    public class ModelStateValidationError : ModelValidationError
    {
        public ModelStateValidationError(ModelStateDictionary modelState)
            : base(modelState.Keys.SelectMany(key => modelState[key].Errors.Select(x => new FieldValidationError(key, x.ErrorMessage))))
        {
        }
    }
}
