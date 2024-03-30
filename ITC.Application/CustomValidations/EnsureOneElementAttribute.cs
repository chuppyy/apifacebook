#region

using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;

#endregion

namespace ITC.Application.CustomValidations;

public class EnsureOneElementAttribute : ValidationAttribute
{
#region Methods

    public override bool IsValid(object value)
    {
        var list = value as IList;
        if (list != null) return list.Count > 0;
        return false;
    }

#endregion
}

public class MaxValueAttribute : ValidationAttribute
{
#region Fields

    private readonly double _maxValue;

#endregion

#region Constructors

    public MaxValueAttribute(double maxValue)
    {
        _maxValue = maxValue;
    }

#endregion

#region Methods

    public override bool IsValid(object value)
    {
        if (value is int intValue)
            return Convert.ToDouble(intValue) <= _maxValue;
        return (double)value <= _maxValue;
    }

#endregion
}

public class MinValueAttribute : ValidationAttribute
{
#region Fields

    private readonly double _minValue;

#endregion

#region Constructors

    public MinValueAttribute(double maxValue)
    {
        _minValue = maxValue;
    }

#endregion

#region Methods

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        double currentvalue = 0;
        if (value is int intValue)
            currentvalue  = Convert.ToDouble(intValue);
        else currentvalue = (double)value;


        return currentvalue >= _minValue
                   ? ValidationResult.Success
                   : new ValidationResult($"Student should be at least {_minValue} years old.");
    }

#endregion
}