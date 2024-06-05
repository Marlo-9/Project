using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Advanced.Tools;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using Project.Models;
using Project.Tools;
using Project.ViewModels.Employee.Modal;

namespace Project.ViewModels.Employee;

public partial class GroupListViewModel : PageBaseViewModel
{
    [ObservableProperty] private ObservableCollection<Group> _groups;
    [ObservableProperty] private string _title = "Группы";
    [ObservableProperty] private Group _selectedGroup;
    
    private EmployeeMainViewModel _parentDataContext;

    public GroupListViewModel(EmployeeMainViewModel dataContext)
    {
        _parentDataContext = dataContext;
        LoadGroups();
    }

    private async Task LoadGroups()
    {
        _parentDataContext.IsLoading = true;

        Groups = await DataBase.LoadEntityAsync<Group>();
        
        _parentDataContext.IsLoading = false;
    }

    [RelayCommand]
    private async Task EditGroup(Group group)
    {
        EditGroupViewModel vm = new("Редактирование группы: " + group.Name, group, Application.Current.MainWindow!);

        vm.EditWindow.ShowDialog();
        
        if ((bool)vm.EditWindow.DialogResult!)
        {
            _parentDataContext.IsLoading = true;

            await DataBase.TryUpdateEntityAsync(vm.Group);
            
            _parentDataContext.IsLoading = false;
            
            await LoadGroups();
        }
    }

    [RelayCommand]
    private async Task CreateGroup()
    {
        EditGroupViewModel vm = new("Создание группы", new Group(), Application.Current.MainWindow!);

        vm.EditWindow.ShowDialog();
        
        if ((bool)vm.EditWindow.DialogResult!)
        {
            _parentDataContext.IsLoading = true;

            await DataBase.TryUpdateEntityAsync(vm.Group);
            
            _parentDataContext.IsLoading = false;
            
            await LoadGroups();
        }
    }

    [RelayCommand]
    private async Task DeleteGroup(Group group)
    {
        _parentDataContext.IsLoading = true;

        await DataBase.TryRemoveEntityAsync(group);

        _parentDataContext.IsLoading = false;

        await LoadGroups();
    }
}