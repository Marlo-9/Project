using System.Windows;
using System.Windows.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using Project.Models;

namespace Project.ViewModels;

public partial class WindowBaseViewModel : ObservableObject
{
    [ObservableProperty] private bool _isLoading = false;

    public WindowBaseViewModel()
    {
    }
}