using System.Windows.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using Project.Models;

namespace Project.ViewModels;

public abstract partial class PageBaseViewModel : ObservableObject
{
    [ObservableProperty] private string _pageTitle = "";
    [ObservableProperty] private object _page;
    [ObservableProperty] private User? _user;
    
    protected MainViewModel _mainViewModel;

    partial void OnPageChanged(object value)
    {
        PageTitle = ((Page)Page).Title;
        ((Page)Page).DataContext = this;
    }

    protected PageBaseViewModel(MainViewModel mainViewModel, object startPage)
    {
        _mainViewModel = mainViewModel;
        Page = startPage;
        ((Page)Page).DataContext = this;
    }
}