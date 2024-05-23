using Advanced.Tools;
using Wpf.Ui.Controls;

namespace Project.Views.Windows;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : FluentWindow
{
    public MainWindow()
    {
        InitializeComponent();
        Notify.SetGlobalSnackbarPresenter(MainSnackbarPresenter);
    }
}