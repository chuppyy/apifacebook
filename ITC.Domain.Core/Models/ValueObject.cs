﻿namespace ITC.Domain.Core.Models;

public abstract class ValueObject<T> where T : ValueObject<T>
{
#region Methods

    public static bool operator ==(ValueObject<T> a, ValueObject<T> b)
    {
        if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
            return true;

        if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
            return false;

        return a.Equals(b);
    }

    public static bool operator !=(ValueObject<T> a, ValueObject<T> b)
    {
        return !(a == b);
    }

    public override bool Equals(object obj)
    {
        var valueObject = obj as T;
        return !ReferenceEquals(valueObject, null) && EqualsCore(valueObject);
    }

    public override int GetHashCode()
    {
        return GetHashCodeCore();
    }

    protected abstract bool EqualsCore(T other);

    protected abstract int GetHashCodeCore();

#endregion
}