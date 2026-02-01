namespace BuildingBlocks.Infrastructure.UnitOfWork;
public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken ct = default);
}