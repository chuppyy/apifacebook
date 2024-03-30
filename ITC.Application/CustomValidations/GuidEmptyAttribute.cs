#region

using System;
using System.ComponentModel.DataAnnotations;

#endregion

namespace ITC.Application.CustomValidations;

public class GuidEmptyAttribute : ValidationAttribute
{
#region Methods

    public override bool IsValid(object value)
    {
        var result = value.ToString();
        return result == Guid.Empty.ToString() ? false : true;
    }

#endregion
}