using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MaintenanceService.Domain.Entities;
using MaintenanceService.Domain.Interfaces;
using MaintenanceService.Infrastructure.Persistence;

namespace MaintenanceService.Infrastructure.Repositories;

public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
{
    protected readonly MaintenanceDbContext _context;

    public GenericRepository(MaintenanceDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(TEntity entity)
    {
        await _context.Set<TEntity>().AddAsync(entity);
    }

    public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _context.Set<TEntity>().Where(predicate).ToListAsync();
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await _context.Set<TEntity>().ToListAsync();
    }

    public async Task<TEntity?> GetByIdAsync(Guid id)
    {
        return await _context.Set<TEntity>().FindAsync(id);
    }

    public void Remove(TEntity entity)
    {
        _context.Set<TEntity>().Remove(entity);
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Update(TEntity entity)
    {
        _context.Set<TEntity>().Update(entity);
    }
}
