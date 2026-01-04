using ME.SuperHero.Domain.Interfaces;
using ME.SuperHero.Infra.Data.Context;
using System;

public class Uow : IUow
{
    private readonly AppDbContext _context;


    public Uow(
        AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken) > 0;
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}

