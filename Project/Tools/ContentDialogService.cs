using System.Windows.Controls;
using Wpf.Ui;
using Wpf.Ui.Controls;

namespace Project.Tools;

public class ContentDialogService : IContentDialogService
{
    private ContentPresenter? _dialogHost;

    public void SetContentPresenter(ContentPresenter contentPresenter)
    {
        SetDialogHost(contentPresenter);
    }

    public ContentPresenter? GetContentPresenter()
    {
        return GetDialogHost();
    }

    public void SetDialogHost(ContentPresenter dialogHost)
    {
        _dialogHost = dialogHost;
    }

    public ContentPresenter? GetDialogHost()
    {
        return _dialogHost;
    }

    public Task<ContentDialogResult> ShowAsync(ContentDialog dialog, CancellationToken cancellationToken)
    {
        if (_dialogHost == null)
        {
            throw new InvalidOperationException("The DialogHost was never set.");
        }

        if (dialog.DialogHost != null && _dialogHost != dialog.DialogHost)
        {
            throw new InvalidOperationException(
                "The DialogHost is not the same as the one that was previously set."
            );
        }

        dialog.DialogHost = _dialogHost;

        return dialog.ShowAsync(cancellationToken);
    }
}