namespace ME.SuperHero.Domain.Interfaces
{
    public interface IUow : IDisposable
    {
        Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default);
    }

}
