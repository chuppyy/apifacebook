#region

using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using NCore.Systems;

#endregion

namespace ITC.Domain.Core.Extensions;

public static class ValidationExceptionExtensions
{
#region Methods

    public static IEnumerable<ErrorDetail> GetErrorDetails(this ValidationException validationException)
    {
        return validationException.Errors.Select(error => new ErrorDetail
        {
            Location = error.PropertyName, LocationType = "parameter", Message = error.ErrorMessage
        }).ToList();
    }

#endregion
}