using System.Linq.Expressions;
using FerozLeisureHub.Application;
using FerozLeisureHub.Insfrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FerozLeisureHub.Insfrastructure;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly ApplicationDbContext _dbContext;
    internal DbSet<T> dbset;
    public Repository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        dbset = _dbContext.Set<T>();

    }
    public void Add(T entity)
    {
        dbset.Add(entity);
    }

    public bool Any(Expression<Func<T, bool>>? filter)
    {
        return dbset.Any(filter);
    }

    public T Get(Expression<Func<T, bool>>? filter, string? includeProperties = null)
    {
        IQueryable<T> query = dbset;

        // Apply filter if provided
        if (filter != null)
        {
            query = query.Where(filter);
        }

        // Include related properties if provided
        if (!string.IsNullOrEmpty(includeProperties))
        {
            foreach (var includeProperty in includeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
        }

        return query.FirstOrDefault();

    }

    public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, string? includeProperties = null)
    {
        IQueryable<T> query = dbset;

        // Apply filter if provided
        if (filter != null)
        {
            query = query.Where(filter);
        }

        // Include related properties if provided
        if (!string.IsNullOrEmpty(includeProperties))
        {
            foreach (var includeProperty in includeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
        }

        return query.ToList();

    }

    public void Remove(T entity)
    {
        dbset.Remove(entity);
    }
}
