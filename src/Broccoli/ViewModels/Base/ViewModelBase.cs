using Avalonia.Controls;
using ReactiveUI;

namespace Broccoli.ViewModels.Base;

public abstract class ViewModelBase : ReactiveObject
{
    private bool _loadingState;

    protected bool LoadingState
    {
        get => _loadingState;
        set => this.RaiseAndSetIfChanged(ref _loadingState, value);
    }
}
