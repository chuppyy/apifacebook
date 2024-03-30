#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

#endregion

namespace ITC.Domain.Core.Models;

public class UserTarget : ValueObject
{
#region Constructors

    public UserTarget()
    {
    }

    public UserTarget(
        string   createBy,
        string   modifyBy,
        DateTime createDate,
        DateTime modifyDate
    )
    {
        CreateBy   = createBy;
        ModifyBy   = modifyBy;
        CreateDate = createDate;
        ModifyDate = modifyDate;
    }

#endregion

#region Properties

    public string   CreateBy   { get; }
    public DateTime CreateDate { get; }
    public string   ModifyBy   { get; private set; }
    public DateTime ModifyDate { get; private set; }

#endregion

#region Methods

    public void UpdateUserTager(
        string   modifyBy,
        DateTime modifyDate)
    {
        ModifyBy   = modifyBy;
        ModifyDate = modifyDate;
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return CreateBy;
        yield return ModifyBy;
        yield return CreateDate;
        yield return ModifyDate;
    }

#endregion
}

public abstract class ValueObject
{
#region Methods

    protected static bool EqualOperator(ValueObject left, ValueObject right)
    {
        if (ReferenceEquals(left,    null) ^ ReferenceEquals(right, null)) return false;
        return ReferenceEquals(left, null) || left.Equals(right);
    }

    protected static bool NotEqualOperator(ValueObject left, ValueObject right)
    {
        return !EqualOperator(left, right);
    }

    public override bool Equals(object obj)
    {
        if (obj == null || obj.GetType() != GetType()) return false;
        var other       = (ValueObject)obj;
        var thisValues  = GetAtomicValues().GetEnumerator();
        var otherValues = other.GetAtomicValues().GetEnumerator();
        while (thisValues.MoveNext() && otherValues.MoveNext())
        {
            if (ReferenceEquals(thisValues.Current, null) ^ ReferenceEquals(otherValues.Current, null))
                return false;
            if (thisValues.Current != null && !thisValues.Current.Equals(otherValues.Current)) return false;
        }

        return !thisValues.MoveNext() && !otherValues.MoveNext();
    }

    public ValueObject GetCopy()
    {
        return MemberwiseClone() as ValueObject;
    }

    public override int GetHashCode()
    {
        return GetAtomicValues()
               .Select(x => x != null ? x.GetHashCode() : 0)
               .Aggregate((x, y) => x ^ y);
    }

    protected abstract IEnumerable<object> GetAtomicValues();

#endregion
}

public abstract class Entity<T>
{
#region Properties

    public T Id { get; protected set; }

#endregion

#region Methods

    public static bool operator ==(Entity<T> a, Entity<T> b)
    {
        if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
            return true;

        if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
            return false;

        return a.Equals(b);
    }

    public static bool operator !=(Entity<T> a, Entity<T> b)
    {
        return !(a == b);
    }

    public override bool Equals(object obj)
    {
        var compareTo = obj as Entity<T>;

        if (ReferenceEquals(this, compareTo)) return true;
        if (ReferenceEquals(null, compareTo)) return false;

        return Id.Equals(compareTo.Id);
    }

    public override int GetHashCode()
    {
        return GetType().GetHashCode() * 907 + Id.GetHashCode();
    }

    public override string ToString()
    {
        return GetType().Name + " [Id=" + Id + "]";
    }

#endregion
}

public abstract class EntityString : Entity<string>
{
#region Constructors

    public EntityString()
    {
        Id = Guid.NewGuid().ToString();
    }

#endregion
}

public abstract class EntityInt : Entity<int>
{
}

public abstract class Enumeration : IComparable
{
#region IComparable Members

    public int CompareTo(object other)
    {
        return Id.CompareTo(((Enumeration)other).Id);
    }

#endregion

#region Constructors

    protected Enumeration()
    {
    }

    protected Enumeration(int id, string name)
    {
        Id   = id;
        Name = name;
    }

#endregion

#region Properties

    public int    Id   { get; }
    public string Name { get; }

#endregion

#region Methods

    public static int AbsoluteDifference(Enumeration firstValue, Enumeration secondValue)
    {
        var absoluteDifference = Math.Abs(firstValue.Id - secondValue.Id);
        return absoluteDifference;
    }

    public static T FromDisplayName<T>(string displayName) where T : Enumeration, new()
    {
        var matchingItem = Parse<T, string>(displayName, "display name", item => item.Name == displayName);
        return matchingItem;
    }

    public static T FromValue<T>(int value) where T : Enumeration, new()
    {
        var matchingItem = Parse<T, int>(value, "value", item => item.Id == value);
        return matchingItem;
    }

    public static IEnumerable<T> GetAll<T>() where T : Enumeration, new()
    {
        var type   = typeof(T);
        var fields = type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);

        foreach (var info in fields)
        {
            var instance     = new T();
            var locatedValue = info.GetValue(instance) as T;

            if (locatedValue != null)
                yield return locatedValue;
        }
    }

    private static T Parse<T, K>(K value, string description, Func<T, bool> predicate) where T : Enumeration, new()
    {
        var matchingItem = GetAll<T>().FirstOrDefault(predicate);

        if (matchingItem == null)
            throw new InvalidOperationException($"'{value}' is not a valid {description} in {typeof(T)}");

        return matchingItem;
    }

    public override bool Equals(object obj)
    {
        var otherValue = obj as Enumeration;

        if (otherValue == null)
            return false;

        var typeMatches  = GetType().Equals(obj.GetType());
        var valueMatches = Id.Equals(otherValue.Id);

        return typeMatches && valueMatches;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    public override string ToString()
    {
        return Name;
    }

#endregion
}