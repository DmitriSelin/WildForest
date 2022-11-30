using WildForest.Domain.Common.Models;
using WildForest.Domain.Marks.ValueObjects;
using WildForest.Domain.User.ValueObjects;

namespace WildForest.Domain.Marks.Entities
{
    public class Mark : Entity<MarkId>
    {
        public MarkDetails MarkDetails { get; set; }

        public UserId UserId { get; } = null!;

        public virtual User.Entities.User User { get; set; } = null!;

        public Mark(MarkId id, MarkDetails markDetails) : base(id)
        {
            MarkDetails = markDetails;
        }
    }
}