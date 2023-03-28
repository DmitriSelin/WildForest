using WildForest.Domain.Common.Models;
using WildForest.Domain.Marks.ValueObjects;
using WildForest.Domain.Users.Entities;
using WildForest.Domain.Users.ValueObjects;
using WildForest.Domain.Weather.Entities;
using WildForest.Domain.Weather.ValueObjects;

namespace WildForest.Domain.Marks.Entities
{
    public sealed class Mark : Entity<MarkId>
    {
        public Rating Rating { get; } = null!;
        
        public Comment? Comment { get; }
        
        public UserId UserId { get; } = null!;

        public User User { get; } = null!;

        public WeatherId WeatherId { get; } = null!;

        public WeatherForecast WeatherForecast { get; } = null!;

        private Mark(
            MarkId id,
            Rating rating,
            Comment? comment,
            UserId userId,
            WeatherId weatherId) : base(id)
        {
            Rating = rating;
            Comment = comment;
            UserId = userId;
            WeatherId = weatherId;
        }
        
        public static Mark Create(Rating rating, Comment? comment, UserId userId, WeatherId weatherId)
        {
            return new(
                MarkId.Create(),
                rating,
                comment,
                userId,
                weatherId);
        }

        private Mark(MarkId id) : base(id) { }
    }
}