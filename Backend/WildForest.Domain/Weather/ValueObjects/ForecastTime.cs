﻿using WildForest.Domain.Common.Models;

namespace WildForest.Domain.Weather.ValueObjects
{
    /// <summary>
    /// Value in UTC
    /// </summary>
    public sealed class ForecastTime : ValueObject
    {
        public TimeOnly Value { get; }

        private ForecastTime(TimeOnly value)
            => Value = value;

        public static ForecastTime Create(TimeOnly value)
        {
            return new(value);
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}