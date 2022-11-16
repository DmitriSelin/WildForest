using WildForest.Application.Common.Interfaces.Services;

namespace WildForest.Infrastructure.Services
{
    public sealed class DateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}