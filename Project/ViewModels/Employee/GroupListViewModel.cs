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
        StackPanel panel = new();
        TextBox textBox = new()
        {
            Name = "NameBox",
            Text = group.Name
        };
        Label label = new()
        {
            Content = "Название"
        };

        label.SetBinding(Label.TargetProperty, new Binding()
        {
            ElementName = textBox.Name
        });

        panel.Children.Add(label);
        panel.Children.Add(textBox);

        ContentDialog dialog = new(Tools.Message.ContentPresenter);
        
        dialog.SetCurrentValue(ContentDialog.TitleProperty, "Редактирование группы: " + group.Name);
        dialog.SetCurrentValue(ContentDialog.ContentProperty, panel);
        dialog.SetCurrentValue(ContentDialog.CloseButtonTextProperty, "Отмена");
        dialog.SetCurrentValue(ContentDialog.PrimaryButtonTextProperty, "Сохранить");
        dialog.SetCurrentValue(ContentDialog.MinWidthProperty, (double)420);
        dialog.SetCurrentValue(ContentDialog.DialogWidthProperty, (double)420);
        dialog.SetCurrentValue(ContentDialog.DialogHeightProperty, (double)250);

        ContentDialogResult result = await dialog.ShowAsync();

        if (result == ContentDialogResult.Primary)
        {
            _parentDataContext.IsLoading = true;

            group.Name = textBox.Text;
            await DataBase.TryUpdateEntityAsync(group);

            _parentDataContext.IsLoading = false;

            await LoadGroups();
        }
    }

    [RelayCommand]
    private async Task CreateGroup()
    {
        StackPanel panel = new();
        TextBox textBox = new()
        {
            Name = "NameBox"
        };
        Label label = new()
        {
            Content = "Название"
        };

        label.SetBinding(Label.TargetProperty, new Binding()
        {
            ElementName = textBox.Name
        });

        panel.Children.Add(label);
        panel.Children.Add(textBox);

        ContentDialog dialog = new(Tools.Message.ContentPresenter);
        
        dialog.SetCurrentValue(ContentDialog.TitleProperty, "Создание группы");
        dialog.SetCurrentValue(ContentDialog.ContentProperty, panel);
        dialog.SetCurrentValue(ContentDialog.CloseButtonTextProperty, "Отмена");
        dialog.SetCurrentValue(ContentDialog.PrimaryButtonTextProperty, "Создать");
        dialog.SetCurrentValue(ContentDialog.MinWidthProperty, (double)420);
        dialog.SetCurrentValue(ContentDialog.DialogWidthProperty, (double)420);
        dialog.SetCurrentValue(ContentDialog.DialogMaxHeightProperty, (double)250);

        ContentDialogResult result = await dialog.ShowAsync();

        if (result == ContentDialogResult.Primary)
        {
            _parentDataContext.IsLoading = true;

            await DataBase.TryUpdateEntityAsync(new Group()
            {
                Name = textBox.Text
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