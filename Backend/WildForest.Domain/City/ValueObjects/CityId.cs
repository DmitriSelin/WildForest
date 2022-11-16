﻿using WildForest.Domain.Common.Models;

namespace WildForest.Domain.City.ValueObjects
{
    public sealed class CityId : ValueObject
    {
        public Guid Value { get; }

        private CityId(Guid value)
        {
            Value = value;
        }

        public static CityId CreateCityId()
        {
            return new(Guid.NewGuid());
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}