using Avalonia.Controls;

namespace Broccoli.Windows;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        WindowState = WindowState.Maximized;
        InitializeComponent();
    }
}