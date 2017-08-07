using DevInstinct.ErrorHandling;
using DevInstinct.Patterns.CQRSPattern;
using DevInstinct.Patterns.TierPattern;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevInstinct.MediatR.FluentValidation.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IResponsibility
    {
        protected IEnumerable<IValidator<TRequest>> Validators { get; }

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            Validators = validators;
        }

        public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next)
        {
            var failures = Validators
                .Select(v => v.Validate(request))
                .SelectMany(result => result.Errors)
                .Where(f => f != null)
                .ToList();

            return failures.Count == 0 ? next() : throw GetException(request, failures);
        }

        protected virtual Exception GetException(TRequest request, IEnumerable<ValidationFailure> failures)
        {
            var error = failures.ToValidationError();
            if (request is IDomainTier) return new DomainErrorException(error);
            if (request is IRepositoryTier) return new RepositoryErrorException(error);
            return new ApplicationErrorException(error);
        }
    }
}
