using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Broccoli.Windows.Dialogs;

public partial class MessageBoxWindow : Window
{
    public MessageBoxWindow()
    {
        InitializeComponent();

        DataContext = new
        {
            Title = "Test",
            Message = "Message",
            Options = new[] { "1", "2", "3" }
        };
    }
}