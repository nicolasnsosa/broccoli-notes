using Avalonia.Controls;

namespace Broccoli.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        WindowState = WindowState.Maximized;
        InitializeComponent();
    }
}