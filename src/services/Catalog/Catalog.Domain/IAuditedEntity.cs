namespace Catalog.Domain
{
    public interface IAuditedEntity
    {
        DateTime? CreatedDate { get; }
        DateTime? LastModifiedDate {  get; }
    }
}
