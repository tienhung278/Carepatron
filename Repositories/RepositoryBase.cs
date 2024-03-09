using System.Linq.Expressions;
using api.Data;
using api.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories;

public class RepositoryBase<T> : IRepositoryBase<T> where T : class
{
    private readonly DataContext _dataContext;

    public RepositoryBase(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<IReadOnlyList<T>> FindAllAsync()
    {
        return await _dataContext.Set<T>().ToListAsync();
    }

    public async Task<IReadOnlyList<T>> FindAsync(Expression<Func<T, bool>> predicate)
    {
        return await _dataContext.Set<T>().Where(predicate).ToListAsync();
    }

    public async Task<IReadOnlyList<T>> FindAsync(Expression<Func<T, bool>> predicate = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeString = null,
        bool disableTracking = true)
    {
        IQueryable<T> query = _dataContext.Set<T>();
        
        if (disableTracking)
        {
            query = query.AsNoTracking();
        }

        if (!string.IsNullOrWhiteSpace(includeString) != null)
        {
            query = query.Include(includeString);
        }

        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        if (orderBy != null)
        {
            return await orderBy(query).ToListAsync();
        }

        return await query.ToListAsync();
    }

    public async Task<IReadOnlyList<T>> FindAsync(Expression<Func<T, bool>> predicate = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, List<Expression<Func<T, object>>> includes = null,
        bool disableTracking = true)
    {
        IQueryable<T> query = _dataContext.Set<T>();
        
        if (disableTracking)
        {
            query = query.AsNoTracking();
        }

        if (includes.Count > 0)
        {
            query = includes.Aggregate(query, (query, include) => query.Include(include));
        }

        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        if (orderBy != null)
        {
            return await orderBy(query).ToListAsync();
        }

        return await query.ToListAsync();
    }

    public async Task<T> FindByIdAsync(Guid id)
    {
        return await _dataContext.Set<T>().FindAsync(id);
    }

    public async Task<T> CreateAsync(T entity)
    {
        _dataContext.Set<T>().Add(entity);
        return entity;
    }

    public async Task UpdateAsync(T entity)
    {
        _dataContext.Entry(entity).State = EntityState.Modified;
    }

    public async Task DeleteAsync(T entity)
    {
        _dataContext.Set<T>().Remove(entity);
    }
}