using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitectureDemo.Application.Common.Behaviours
{
    //public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    //    where TRequest : IRequest<TResponse>
    //{
    //    private readonly IEnumerable<IValidator<TRequest>> _validators;
    //    public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
    //    {
    //        _validators = validators;
    //    }
    //    public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    //    {
    //        var context = new ValidationContext(request);
    //        var failuers = _validators.Select(x => x.Validate(context))
    //            .SelectMany(x => x.Errors)
    //            .Where(x => x != null)
    //            .ToList();
    //        if (failuers.Any())
    //        {
    //            throw new CustomValidationException(failuers);
    //        }
    //        return next();
    //    }
    //}
}
