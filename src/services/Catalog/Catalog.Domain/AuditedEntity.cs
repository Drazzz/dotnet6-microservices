namespace Catalog.Domain
{
    public abstract class AuditedEntity : Entity, IAuditedEntity
    {
        protected AuditedEntity() => CreatedDate = DateTime.UtcNow;

        public DateTime? CreatedDate { get; protected set; }

        public DateTime? LastModifiedDate { get; protected set; }
    }
}
