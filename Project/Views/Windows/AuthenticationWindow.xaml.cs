using System.Windows;
using Advanced.Tools;
using Wpf.Ui.Controls;

namespace Project.Views.Windows;

public partial class AuthenticationWindow : FluentWindow
{
    public AuthenticationWindow()
    {
        InitializeComponent();
        Notify.SetGlobalSnackbarPresenter(AuthenticationSnackbarPresenter);
    }
}