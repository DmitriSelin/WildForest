using WildForest.Domain.Common.Models;

namespace WildForest.Domain.Weather.ValueObjects
{
    public sealed class ForecastDateTime : ValueObject
    {
        public DateOnly Date { get; }

        public TimeOnly Time { get; }

        private ForecastDateTime(DateOnly date, TimeOnly time)
        {
            Date = date;
            Time = time;
        }

        public static ForecastDateTime CreateForecastDateTime(DateOnly date, TimeOnly time)
        {
            return new(date, time);
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Date; 
            yield return Time;
        }
    }
}