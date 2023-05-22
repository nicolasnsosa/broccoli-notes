using System.Data;
using Infrastructure.Functions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.Database;

public class RepositoryManager
{
    private readonly MyDbContext _context;

    public RepositoryManager(MyDbContext context)
    {
        _context = context;
    }

    public bool EnsureDatabaseCreated()
    {
        return _context.Database.EnsureCreated();
    }

    public async Task<FunctionResponse> TransactionalSaveAsync(Func<Task<FunctionResponse>> operation)
    {
        var fr = new FunctionResponse();
        IDbContextTransaction? transaction = null;

        try
        {
            transaction = await _context.Database.BeginTransactionAsync();
            fr = await operation();

            if (fr.Status)
                await transaction.CommitAsync();
        }
        catch (Exception ex)
        {
            if (transaction != null)
                await transaction.RollbackAsync();

            fr.AddError(ex);
        }

        return fr;
    }
}