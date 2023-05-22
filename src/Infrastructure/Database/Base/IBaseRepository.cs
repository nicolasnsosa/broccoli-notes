using System.Linq.Expressions;
using Infrastructure.Functions;

namespace Infrastructure.Database.Base;

public interface IBaseRepository<TEntity> where TEntity : class
{
    Task<FunctionResponse<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate);
    Task<FunctionResponse<IEnumerable<TEntity>>> GetAllAsync();
    Task<FunctionResponse<IEnumerable<TEntity>>> QueryAsync(Expression<Func<TEntity, bool>> predicate);
    Task<FunctionResponse> AddAsync(TEntity entity);
    Task<FunctionResponse> UpdateAsync(TEntity entity);
    Task<FunctionResponse> DeleteAsync(TEntity entity);
}