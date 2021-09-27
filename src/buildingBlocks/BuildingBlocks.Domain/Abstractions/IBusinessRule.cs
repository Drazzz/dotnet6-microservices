namespace BuildingBlocks.Domain.Abstractions;

public interface IBusinessRule
{
    string Message { get; }
    bool IsBroken();
}
