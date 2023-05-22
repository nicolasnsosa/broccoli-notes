using System;
using System.Threading.Tasks;
using Broccoli.Messages;
using Broccoli.ViewModels.Base;
using Domain.Services;
using Models.Domain;
using ReactiveUI;

namespace Broccoli.ViewModels.Workspaces;

public class WorkspaceCrudViewModel : ViewModelBase
{
    private string _id = null!;
    private DateTime? _updatedDate = null;

    public string Id
    {
        get => _id;
        set => this.RaiseAndSetIfChanged(ref _id, value);
    }

    private readonly IMessageBus _messageBus = null!;
    private readonly WorkspaceService _workspaceService = null!;

    private bool _isIdVisible;

    public bool IsIdVisible
    {
        get => _isIdVisible;
        set => this.RaiseAndSetIfChanged(ref _isIdVisible, value);
    }

    private string _name = null!;

    public string Name
    {
        get => _name;
        set => this.RaiseAndSetIfChanged(ref _name, value);
    }

    private string? _description = null!;

    public string? Description
    {
        get => _description;
        set => this.RaiseAndSetIfChanged(ref _description, value);
    }

    public WorkspaceCrudViewModel()
    {
    }

    public WorkspaceCrudViewModel(IMessageBus messageBus, WorkspaceService service)
    {
        _messageBus = messageBus;
        _workspaceService = service;
    }

    public async Task LoadDataAsync(string id)
    {
        Id = id;

        if (string.IsNullOrEmpty(Id))
        {
            IsIdVisible = false;

            Name = string.Empty;
            Description = string.Empty;
            return;
        }

        IsIdVisible = true;

        LoadingState = true;

        var fr = await _workspaceService.GetAsync(Id);
        if (fr.Status && fr.Data != null)
        {
            _updatedDate = fr.Data.UpdatedDate;

            Name = fr.Data.Name;
            Description = fr.Data.Description;
        }

        LoadingState = false;
    }

    public void OnCancel()
    {
        _messageBus.SendMessage(new CloseWindowMessage<WorkspaceCrudViewModel>());
    }

    public async Task OnSaveAsync()
    {
        LoadingState = true;

        var workspaceEntity = new Workspace()
        {
            Id = Id,
            Name = Name,
            Description = Description,
            UpdatedDate = _updatedDate,
        };

        var fr = await _workspaceService.SaveAsync(workspaceEntity);

        _messageBus.SendMessage(new RefreshWindowMessage());
        _messageBus.SendMessage(new CloseWindowMessage<WorkspaceCrudViewModel>());

        LoadingState = false;
    }
}