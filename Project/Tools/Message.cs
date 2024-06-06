using System.Windows.Controls;
using Wpf.Ui;
using Wpf.Ui.Controls;
using Wpf.Ui.Extensions;

namespace Project.Tools;

public static class Message
{
    private static ContentDialogService? _contentDialogService;
    public static ContentPresenter? ContentPresenter { get; private set; }

    public static void SetGlobalContentPresenter(ContentPresenter contentPresenter)
    {
        ContentPresenter = contentPresenter;
        _contentDialogService ??= new ContentDialogService();
        _contentDialogService.SetDialogHost(contentPresenter);
    }

    public static bool HasGlobalContentPresenter()
    {
        return ContentPresenter == null;
    }

    public static async Task<ContentDialogResult> Show(SimpleContentDialogCreateOptions options)
    {
        if (!HasGlobalContentPresenter())
            throw new Exception("Don`t set global ContentPresenter");
        return await _contentDialogService!.ShowSimpleDialogAsync(options);
    }
}