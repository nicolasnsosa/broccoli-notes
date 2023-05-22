using System.Linq.Expressions;
using Infrastructure.Database.Base;
using Infrastructure.Functions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database;

public class Repository<TEntity> : IBaseRepository<TEntity> where TEntity : class
{
    private readonly MyDbContext _context;

    public Repository(MyDbContext context)
    {
        _context = context;
    }

    public async Task<FunctionResponse<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate)
    {
        var fr = new FunctionResponse<TEntity>();

        try
        {
            fr.Data = await _context
                .Set<TEntity>()
                .AsNoTracking()
                .SingleOrDefaultAsync(predicate);
        }
        catch (Exception ex)
        {
            fr.AddError(ex);
        }
        
        return fr;
    }

    public async Task<FunctionResponse<IEnumerable<TEntity>>> GetAllAsync()
    {
        var fr = new FunctionResponse<IEnumerable<TEntity>>();

        try
        {
            fr.Data = await _context
                .Set<TEntity>()
                .AsNoTracking()
                .ToListAsync();
        }
        catch (Exception ex)
        {
            fr.AddError(ex);
        }
        
        return fr;
    }

    public async Task<FunctionResponse<IEnumerable<TEntity>>> QueryAsync(Expression<Func<TEntity, bool>> predicate)
    {
        var fr = new FunctionResponse<IEnumerable<TEntity>>();

        try
        {
            fr.Data = await _context.Set<TEntity>()
                .AsNoTracking()
                .Where(predicate)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            fr.AddError(ex);
        }

        return fr;
    }

    public async Task<FunctionResponse> AddAsync(TEntity entity)
    {
        var fr = new FunctionResponse();

        try
        {
            await _context.Set<TEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            fr.AddError(ex);
        }

        return fr;
    }

    public async Task<FunctionResponse> UpdateAsync(TEntity entity)
    {
        var fr = new FunctionResponse();

        try
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            fr.AddError(ex);
        }

        return fr;
    }

    public async Task<FunctionResponse> DeleteAsync(TEntity entity)
    {
        var fr = new FunctionResponse();

        try
        {
            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            fr.AddError(ex);
        }

        return fr;
    }
}