namespace BuildingBlocks.Domain.Abstractions;
public interface IAuditedEntity
{
    DateTime? CreatedDate { get; }
    DateTime? LastModifiedDate {  get; }
}