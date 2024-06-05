using System.Windows;
using Advanced.Tools;
using Wpf.Ui.Controls;

namespace Project.Views.Windows;

public partial class EmployeeWindow : FluentWindow
{
    public EmployeeWindow()
    {
        InitializeComponent();
        Notify.SetGlobalSnackbarPresenter(AuthenticationSnackbarPresenter);
    }
}