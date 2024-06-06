using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Advanced.Tools;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using Project.Models;
using Project.Tools;
using Project.Tools.Dialog;
using Project.Tools.Enums;
using Project.ViewModels.Employee.Modal;
using Wpf.Ui;
using Wpf.Ui.Controls;
using Message = System.Windows.Forms.Message;
using TextBox = Wpf.Ui.Controls.TextBox;

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
        DialogField field = new("Название", "Name", group.Name);

        if (await UserDialog.ShowGroupEditDialog(field.GetContent(), DialogType.Edit, group.Name) == ContentDialogResult.Primary)
        {
            _parentDataContext.IsLoading = true;

            group.Name = field.GetText();
            await DataBase.TryUpdateEntityAsync(group);

            _parentDataContext.IsLoading = false;

            await LoadGroups();
        }
    }

    [RelayCommand]
    private async Task CreateGroup()
    {
        DialogField field = new("Название", "Name");

        if (await UserDialog.ShowGroupEditDialog(field.GetContent(), DialogType.Create) == ContentDialogResult.Primary)
        {
            _parentDataContext.IsLoading = true;

            await DataBase.TryUpdateEntityAsync(new Group()
            {
                Name = field.GetText()
            });

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