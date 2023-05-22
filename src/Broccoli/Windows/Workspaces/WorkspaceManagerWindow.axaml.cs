using System;
using System.Threading.Tasks;
using Avalonia.Controls;
using Broccoli.Messages;
using Broccoli.ViewModels.Workspaces;
using Broccoli.Windows.Dialogs;
using Models.Domain;
using ReactiveUI;

namespace Broccoli.Windows.Workspaces;

public partial class WorkspaceManagerWindow : Window
{
    private readonly IMessageBus _messageBus = null!;
    private readonly WorkspaceManagerViewModel _vm = null!;
    private readonly WorkspaceCrudViewModel _crudVm = null!;

    public WorkspaceManagerWindow()
    {
        InitializeComponent();
    }

    public WorkspaceManagerWindow(IMessageBus messageBus, 
        WorkspaceManagerViewModel vm,
        WorkspaceCrudViewModel crudVm) : this()
    {
        _messageBus = messageBus;

        _vm = vm;
        DataContext = vm;
        
        _crudVm = crudVm;
        
        SubscribeToMessageBus();
    }

    protected override async void OnOpened(EventArgs e)
    {
        base.OnOpened(e);

        await Task.Run(async () =>
        {
            await _vm.LoadDataAsync();
        });
    }

    private void SubscribeToMessageBus()
    {
        _messageBus
            .Listen<OpenCrudWindowMessage<Workspace>>()
            .Subscribe(OnOpenCrudWindowMessage);
        _messageBus
            .Listen<RefreshWindowMessage>()
            .Subscribe(OnRefreshWindowMessage);
        _messageBus
            .Listen<OpenMessageBoxMessage<WorkspaceManagerViewModel>>()
            .Subscribe(OnOpenMessageBoxMessage);
    }

    private void OnOpenCrudWindowMessage(OpenCrudWindowMessage<Workspace> message)
    {
        var crudWindow = new WorkspaceCrudWindow(message?.Data?.Id, _messageBus, _crudVm);
        crudWindow.ShowDialog(this);
    }

    private async void OnRefreshWindowMessage(RefreshWindowMessage message)
    {
        await Task.Run(async () =>
        {
            await _vm.LoadDataAsync();
        });
    }

    private async void OnOpenMessageBoxMessage(OpenMessageBoxMessage<WorkspaceManagerViewModel> message)
    {
        var msgWindow = new MessageBoxWindow();
        var result = await msgWindow.ShowDialog<MessageBoxDialogResponse>(this);
    }
}