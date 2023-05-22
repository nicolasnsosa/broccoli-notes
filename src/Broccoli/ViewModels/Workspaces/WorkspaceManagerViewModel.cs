using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Broccoli.Messages;
using Broccoli.ViewModels.Base;
using Broccoli.Windows.Workspaces;
using Domain.Repositories;
using Domain.Services;
using Models.Domain;
using ReactiveUI;

namespace Broccoli.ViewModels.Workspaces;

public class WorkspaceManagerViewModel : ViewModelBase
{
    private readonly IMessageBus _messageBus = null!;
    private readonly WorkspaceService _workspaceService = null!;

    private IEnumerable<Workspace>? _workspaces;

    public IEnumerable<Workspace>? Workspaces
    {
        get => _workspaces;
        set => this.RaiseAndSetIfChanged(ref _workspaces, value);
    }

    public WorkspaceManagerViewModel()
    {
    }

    public WorkspaceManagerViewModel(IMessageBus messageBus, WorkspaceService service)
    {
        _messageBus = messageBus;
        _workspaceService = service;
    }

    public async Task LoadDataAsync()
    {
        LoadingState = true;

        var fr = await _workspaceService.GetAllAsync();
        if (fr.Status)
            Workspaces = fr.Data;

        LoadingState = false;
    }

    public void OnCreateWorkspace()
    {
        _messageBus.SendMessage(new OpenCrudWindowMessage<Workspace>());
    }

    public void OnEditWorkspace(string id)
    {
        _messageBus.SendMessage(new OpenCrudWindowMessage<Workspace>
        {
            Data = new Workspace() { Id = id }
        });
    }

    public void OnDeleteWorkspace(string id)
    {
        _messageBus.SendMessage(new OpenMessageBoxMessage<WorkspaceManagerViewModel>()
        {
            
        });
    }
}