using DevInstinct.ErrorHandling;
using FluentValidation.Results;
using System.Collections.Generic;
using System.Linq;

namespace DevInstinct.MediatR.FluentValidation
{
    public static class ValidationFailureExtensions
    {
        public static ValidationError ToValidationError(this IEnumerable<ValidationFailure> failures)
        {
            var propertyErrors = failures.Select(f => new PropertyValidationError(f.PropertyName, f.ErrorMessage));
            return new ValidationError(propertyErrors);
        }
    }
}
