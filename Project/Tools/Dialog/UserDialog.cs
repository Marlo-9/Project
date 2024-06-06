using System.Windows;
using System.Windows.Controls;
using Project.Tools.Enums;
using Wpf.Ui;
using Wpf.Ui.Controls;
using Wpf.Ui.Extensions;

namespace Project.Tools.Dialog;

public static class UserDialog
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

    public static async Task<ContentDialogResult> ShowGroupEditDialog(object content, DialogType type, string groupName = "")
    {
        ContentDialog dialog = new(ContentPresenter);

        var editTitle = !string.IsNullOrEmpty(groupName)
            ? $"Редактирование группы: {groupName}"
            : "Редактирование группы";
        var title = type == DialogType.Edit ? editTitle : "Создание группу";
        
        dialog.Title = title;
        dialog.Content = content;
        dialog.CloseButtonText = "Отмена";
        dialog.PrimaryButtonText = type == DialogType.Edit ? "Сохранить" : "Создать";
        dialog.HorizontalContentAlignment = HorizontalAlignment.Stretch;
        dialog.DialogWidth = 500;
        dialog.DialogMaxWidth = 500;
        dialog.DialogHeight = 200;
        dialog.DialogMaxHeight = 260;
        dialog.VerticalContentAlignment = VerticalAlignment.Center;

        return await dialog.ShowAsync();
    }
}