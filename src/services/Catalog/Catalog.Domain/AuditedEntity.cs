using NodaTime;

namespace Catalog.Domain
{
    public abstract class AuditedEntity : Entity, IAuditedEntity
    {
        protected IClock _clock;

        protected AuditedEntity(IClock clock)
        {
            ArgumentNullException.ThrowIfNull(clock, nameof(clock));

            CreatedDate = clock.GetCurrentInstant().ToDateTimeUtc();
            _clock = clock;
        }

        public DateTime? CreatedDate { get; protected set; }

        public DateTime? LastModifiedDate { get; protected set; }
    }
}
