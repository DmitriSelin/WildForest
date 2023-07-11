﻿using WildForest.Domain.Common.Models;

namespace WildForest.Domain.Cities.ValueObjects;

public sealed class CityId : ValueObject
{
    public Guid Value { get; }

    private CityId(Guid value)
    {
        Value = value;
    }

    public static CityId Create()
    {
        return new(Guid.NewGuid());
    }

    public static CityId Create(Guid value)
    {
        return new(value);
    }

    public static CityId Parse(string cityId)
    {
        return Create(Guid.Parse(cityId));
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}