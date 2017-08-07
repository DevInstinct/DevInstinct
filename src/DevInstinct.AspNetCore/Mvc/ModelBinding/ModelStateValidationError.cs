using DevInstinct.ErrorHandling;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq;

namespace DevInstinct.AspNetCore.Mvc.ModelBinding
{
    // http://www.jerriepelser.com/blog/validation-response-aspnet-core-webapi/
    public class ModelStateValidationError : ValidationError
    {
        public ModelStateValidationError(ModelStateDictionary modelState)
            : base(modelState.Keys.SelectMany(key => modelState[key].Errors.Select(x => new PropertyValidationError(key, x.ErrorMessage))))
        {
        }
    }
}
