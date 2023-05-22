using System.Linq.Expressions;
using Domain.Services.Base;
using Infrastructure.Database;
using Infrastructure.Functions;
using Models.Domain;

namespace Domain.Services;

public class WorkspaceService : IServiceGetBase<Workspace, string>, IServiceSaveBase<Workspace, string>
{
    private readonly Repository<Workspace> _workspaceRepository;

    public WorkspaceService(Repository<Workspace> workspaceRepository)
    {
        _workspaceRepository = workspaceRepository;
    }

    public async Task<FunctionResponse<Workspace>> GetAsync(string key)
    {
        var fr = new FunctionResponse<Workspace>();

        try
        {
            fr = await _workspaceRepository.GetAsync(x => x.Id == key);
        }
        catch (Exception ex)
        {
            fr.AddError(ex);
        }

        return fr;
    }

    public async Task<FunctionResponse<IEnumerable<Workspace>>> GetAllAsync()
    {
        var fr = new FunctionResponse<IEnumerable<Workspace>>();

        try
        {
            fr = await _workspaceRepository.GetAllAsync();
        }
        catch (Exception ex)
        {
            fr.AddError(ex);
        }

        return fr;
    }

    public async Task<FunctionResponse<IEnumerable<Workspace>>> QueryAsync(Expression<Func<Workspace, bool>> predicate)
    {
        var fr = new FunctionResponse<IEnumerable<Workspace>>();

        try
        {
            fr = await _workspaceRepository.QueryAsync(predicate);
        }
        catch (Exception ex)
        {
            fr.AddError(ex);
        }

        return fr;
    }

    public async Task<FunctionResponse<Workspace>> SaveAsync(Workspace entity)
    {
        var fr = new FunctionResponse<Workspace>();

        try
        {
            if (string.IsNullOrEmpty(entity.Id))
            {
                entity.Id = Guid.NewGuid().ToString();

                var insertFr = await _workspaceRepository.AddAsync(entity);
                if (!insertFr.Status)
                {
                    fr.AddErrors(insertFr.Errors);
                    return fr;
                }
            }
            else
            {
                var getFr = await GetAsync(entity.Id);
                if (!getFr.Status || getFr.Data == null)
                {
                    // NOT_FOUND
                    return fr;
                }

                var entityDb = getFr.Data;
                if(entityDb.UpdatedDate != entity.UpdatedDate)
                {
                    // OLD_RECORD
                    return fr;
                }

                var updateFr = await _workspaceRepository.UpdateAsync(entity);
                if (!updateFr.Status)
                {
                    fr.AddErrors(updateFr.Errors);
                    return fr;
                }
            }

            fr.Data = entity;
        }
        catch (Exception ex)
        {
            fr.AddError(ex);
        }

        return fr;
    }

    public async Task<FunctionResponse> DeleteAsync(string key)
    {
        var fr = new FunctionResponse();

        try
        {
            var getFr = await GetAsync(key);
            if (!getFr.Status || getFr.Data == null)
            {
                // NOT_FOUND
                return fr;
            }

            fr = await _workspaceRepository.DeleteAsync(getFr.Data);
        }
        catch (Exception ex)
        {
            fr.AddError(ex);
        }

        return fr;
    }
}