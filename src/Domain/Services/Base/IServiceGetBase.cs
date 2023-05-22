using System.Linq.Expressions;
using Infrastructure.Functions;

namespace Domain.Services.Base;

public interface IServiceGetBase<TEntity, TKey> where TEntity : class 
{
    public Task<FunctionResponse<TEntity>> GetAsync(TKey key);
    public Task<FunctionResponse<IEnumerable<TEntity>>> GetAllAsync();
    public Task<FunctionResponse<IEnumerable<TEntity>>> QueryAsync(Expression<Func<TEntity, bool>> predicate);
}