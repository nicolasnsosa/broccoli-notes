using Infrastructure.Database;
using Models.Domain;

namespace Domain.Repositories;

public class RepositoryManager
{
    private Infrastructure.Database.RepositoryManager _repositoryManager;

    private Repository<Workspace> _workspaceRepository;

    public RepositoryManager(Infrastructure.Database.RepositoryManager repositoryManager,
        Repository<Workspace> workspaceRepository)
    {
        _repositoryManager = repositoryManager;
        _workspaceRepository = workspaceRepository;
    }

    public async Task EnsureDatabaseCreated()
    {
        var databaseFirstRun = _repositoryManager.EnsureDatabaseCreated();
        if (databaseFirstRun)
        {
            // for (var i = 0; i <= 10; i++)
            // {
            //     await _workspaceRepository.AddAsync(new Workspace()
            //     {
            //         Id = Guid.NewGuid().ToString(),
            //         Name = $"Workspace {i}",
            //         Description = $"Workspace {i}",
            //     });
            // }
        }
    }
}