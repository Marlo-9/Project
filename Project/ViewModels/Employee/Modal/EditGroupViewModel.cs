using System.Windows;
using System.Windows.Forms;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Project.Models;
using Project.Tools.Enums;
using Project.Views.Windows.Modal;
using Wpf.Ui.Controls;

namespace Project.ViewModels.Employee.Modal;

public partial class EditGroupViewModel : ObservableObject
{
    [ObservableProperty] private string _title;
    [ObservableProperty] private Group _group;
    [ObservableProperty] private FluentWindow _editWindow;


    public EditGroupViewModel(string title, Group group, Window owner)
    {
        Title = title;
        Group = group;

        _editWindow = new EditGroupWindow()
        {
            DataContext = this,
            Owner = owner
        };
    }

    [RelayCommand]
    private void Save()
    {
        _editWindow.DialogResult = true;
    }

    [RelayCommand]
    private void Cancel()
    {
        _editWindow.DialogResult = false;
    }
}