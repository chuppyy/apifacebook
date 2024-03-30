#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using ITC.Domain.Core.Extensions;
using ITC.Domain.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using NCore.Systems;

#endregion

namespace ITC.Domain.Core.Behaviors;

public class ValidatorBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
#region Constructors

    public ValidatorBehavior(
        IEnumerable<IValidator<TRequest>> validators,
        IHttpContextAccessor              httpContextAccessor)
    {
        _validators          = validators;
        _httpContextAccessor = httpContextAccessor;
    }

#endregion

#region IPipelineBehavior<TRequest,TResponse> Members

    public async Task<TResponse> Handle(TRequest                          request, CancellationToken cancellationToken,
                                        RequestHandlerDelegate<TResponse> next)
    {
        var failures = _validators
                       .Select(v => v.Validate(request))
                       .SelectMany(result => result.Errors)
                       .Where(error => error != null)
                       .ToList();

        if (failures.Any())
        {
            var exception = new ValidationException("Validation exception", failures);

            var responseType = typeof(TResponse);

            if (responseType.IsGenericType)
            {
                var resultType          = responseType.GetGenericArguments()[0];
                var invalidResponseType = typeof(JsonResponse<>).MakeGenericType(resultType);

                var invalidResponse =
                    (TResponse)Activator.CreateInstance(invalidResponseType, null);
                invalidResponseType.GetProperty(nameof(JsonResponse<bool>.Code))
                                   ?.SetValue(invalidResponse, (int)HttpStatusCode.BadRequest);
                invalidResponseType.GetProperty(nameof(JsonResponse<bool>.Message))
                                   ?.SetValue(invalidResponse, "Validation Error");
                invalidResponseType.GetProperty(nameof(JsonResponse<bool>.Errors))
                                   ?.SetValue(invalidResponse, exception.GetErrorDetails());
                return invalidResponse;
            }

            throw new Exception("Validation Error", exception);
        }

        if (request is RequestBase requestBase && _httpContextAccessor.HttpContext?.User is { } user)
        {
            requestBase.ManagementUnitId = user.GetClaim("Mui")?.Value;
            requestBase.UserId           = user.GetClaim("uId")?.Value;
            requestBase.ManagementId     = user.GetClaim("Unu").Value;
        }

        return await next();
    }

#endregion

#region Fields

    private readonly IHttpContextAccessor _httpContextAccessor;

    private readonly IEnumerable<IValidator<TRequest>> _validators;

#endregion
}