using System.Windows.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Project.Models;
using Project.Views.Pages.Employee;
using Wpf.Ui;
using Wpf.Ui.Controls;

namespace Project.ViewModels.Employee;

public partial class EmployeeMainViewModel : WindowBaseViewModel
{
    [ObservableProperty] private NavigationView _view;
    [ObservableProperty] private string _title = "EmployeeMainViewModel";
    [ObservableProperty] private Task _currentTask;

    private GroupListViewModel _groupListViewModel;
    private StudentListViewModel _studentListViewModel;
    
    private User _currentUser;
    
    public EmployeeMainViewModel(NavigationView view, User user)
    {
        _currentUser = user;
        _view = view;

        _view.SelectionChanged += (sender, args) =>
        {
            var item = sender.SelectedItem;

            if (item is null) return;
            
            if (item.TargetPageType == typeof(GroupList))
            {
                _groupListViewModel ??= new GroupListViewModel(this);
                sender.DataContext = _groupListViewModel;
            } 
            else if (item.TargetPageType == typeof(StudentList))
            {
                _studentListViewModel ??= new StudentListViewModel();
                sender.DataContext = _studentListViewModel;
            }
        };
    }
}