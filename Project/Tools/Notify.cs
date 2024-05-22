using Project.Tools.Enums;
using Wpf.Ui.Controls;

namespace Project.Tools;

public static class Notify
{
    private static SnackbarPresenter? _snackbarPresenter = null;

    public static void SetGlobalSnackbarPresenter(SnackbarPresenter snackbarPresenter)
    {
        _snackbarPresenter = snackbarPresenter;
    }

    public static bool HasGlobalSnackbarPresenter()
    {
        return _snackbarPresenter != null;
    }
    
    public static void ShowNotification(object content, string title = "",
        NotificationAppearance appearance = 0, TimeSpan? timeout = null)
    {
        if (!HasGlobalSnackbarPresenter()) throw new Exception("Don`t set global SnackbarPresenter");

        var snackbar = new Snackbar(_snackbarPresenter)
        {
            Title = title,
            Content = content,
            Timeout = timeout ?? TimeSpan.FromSeconds(5)
        };

        switch (appearance)
        {
            case NotificationAppearance.Success:
            {
                snackbar.Appearance = ControlAppearance.Success;
                snackbar.Icon = new SymbolIcon(SymbolRegular.CheckmarkCircle24);
                break;
            }
            case NotificationAppearance.Error:
            {
                snackbar.Appearance = ControlAppearance.Danger;
                snackbar.Icon = new SymbolIcon(SymbolRegular.ErrorCircle24);
                break;
            }
            case NotificationAppearance.Warning:
            {
                snackbar.Appearance = ControlAppearance.Caution;
                snackbar.Icon = new SymbolIcon(SymbolRegular.Warning28);
                break;
            }
            case NotificationAppearance.Info:
            {
                snackbar.Appearance = ControlAppearance.Info;
                snackbar.Icon = new SymbolIcon(SymbolRegular.Info28);
                break;
            }
        }

        snackbar.ShowAsync();
    }

    public static void ShowSuccess(object content, string title = "Успех")
    {
        ShowNotification(content, title, NotificationAppearance.Success);
    }

    public static void ShowError(object content, string title = "Ошибка")
    {
        ShowNotification(content, title, NotificationAppearance.Error);
    }

    public static void ShowWarning(object content, string title = "Внимание")
    {
        ShowNotification(content, title, NotificationAppearance.Warning);
    }

    public static void ShowInfo(object content, string title = "")
    {
        ShowNotification(content, title, NotificationAppearance.Info);
    }
}