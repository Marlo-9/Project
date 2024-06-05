using System.Windows.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using Project.Models;

namespace Project.ViewModels;

public abstract partial class PageBaseViewModel : ObservableObject
{
    [ObservableProperty] private Page? _page;
    [ObservableProperty] private User? _user;
    
    partial void OnPageChanged(Page? value)
    {
        Page!.DataContext = this;
    }

    protected PageBaseViewModel()
    {
        
    }
    
    protected PageBaseViewModel(Page startPage)
    {
        Page = startPage;
        Page.DataContext = this;
    }
}