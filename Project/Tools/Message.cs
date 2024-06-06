using System.Windows.Controls;
using Wpf.Ui;
using Wpf.Ui.Controls;
using Wpf.Ui.Extensions;
using TextBox = Wpf.Ui.Controls.TextBox;

namespace Project.Tools;

public static class Message
{
    private static ContentDialogService? _contentDialogService = new ContentDialogService();
    public static ContentPresenter? _contentPresenter { get; private set; }

    public static void SetGlobalContentContentPresenter(ContentPresenter contentPresenter)
    {
        _contentPresenter = contentPresenter;
        _contentDialogService.SetDialogHost(contentPresenter);
    }

    public static async Task<ContentDialogResult> Show(SimpleContentDialogCreateOptions options)
    {
        return await _contentDialogService.ShowSimpleDialogAsync(options);
    }
}