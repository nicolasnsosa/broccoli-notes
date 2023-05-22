using Infrastructure.Functions;

namespace Domain.Services.Base;

public interface IServiceSaveBase<TEntity, in TKey> where TEntity : class
{
    public Task<FunctionResponse<TEntity>> SaveAsync(TEntity entity);
    public Task<FunctionResponse> DeleteAsync(TKey key);
}
