using Advanced.Tools;
using Project.Tools.Dialog;
using Wpf.Ui.Controls;

namespace Project.Views.Windows;

public partial class EmployeeWindow : FluentWindow
{
    public EmployeeWindow()
    {
        InitializeComponent();
        Notify.SetGlobalSnackbarPresenter(AuthenticationSnackbarPresenter);
        UserDialog.SetGlobalContentPresenter(AuthenticationContentDialog);
    }
}