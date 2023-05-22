using System;
using System.Threading.Tasks;
using Avalonia.Controls;
using Broccoli.Messages;
using Broccoli.ViewModels.Workspaces;
using Models.Domain;
using ReactiveUI;

namespace Broccoli.Windows.Workspaces;

public partial class WorkspaceCrudWindow : Window
{
    private string _id;

    private readonly IMessageBus _messageBus = null!;
    private readonly WorkspaceCrudViewModel _vm = null!;

    public WorkspaceCrudWindow()
    {
        InitializeComponent();
    }

    public WorkspaceCrudWindow(string id, 
        IMessageBus messageBus, 
        WorkspaceCrudViewModel vm) : this()
    {
        _id = id;
        
        _messageBus = messageBus;

        _vm = vm;
        DataContext = vm;

        SubscribeToMessageBus();
    }

    protected override async void OnOpened(EventArgs e)
    {
        base.OnOpened(e);

        await Task.Run(async () =>
        {
            await _vm.LoadDataAsync(_id);
        });
    }

    private void SubscribeToMessageBus()
    {
        _messageBus
            .Listen<CloseWindowMessage<WorkspaceCrudViewModel>>()
            .Subscribe(OnCloseWindowMessage);
    }

    private void OnCloseWindowMessage(CloseWindowMessage<WorkspaceCrudViewModel> message)
    {
        Close();
    }
}