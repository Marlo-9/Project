using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using Project.Views.Pages;

namespace Project.ViewModels;

public partial class MainViewModel : ObservableObject
{
    [ObservableProperty] private object _currentViewModel;
    [ObservableProperty] private Visibility _progressBarVisibility = Visibility.Collapsed;
    [ObservableProperty] private bool _isLoading = false;

    partial void OnIsLoadingChanged(bool value)
    {
        ProgressBarVisibility = value ? Visibility.Visible : Visibility.Hidden;
    }

    public MainViewModel()
    {
        _currentViewModel = new AuthenticationViewModel(this, new LoginPage());
    }
}