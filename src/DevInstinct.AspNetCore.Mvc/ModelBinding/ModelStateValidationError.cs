using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq;

namespace DevInstinct.AspNetCore.Mvc.ModelBinding
{
    public class ModelStateValidationError : ModelValidationError
    {
        public ModelStateValidationError(ModelStateDictionary modelState)
            : base(modelState.Keys.SelectMany(key => modelState[key].Errors.Select(x => new FieldValidationError(key, x.ErrorMessage))))
        {
        }
    }
}
