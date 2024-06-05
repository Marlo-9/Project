using CommunityToolkit.Mvvm.ComponentModel;
using Project.Tools.Enums;

namespace Project.Models;

public partial class User : ObservableObject
{
    [ObservableProperty] private int _id;
    [ObservableProperty] private string _login = "";
    [ObservableProperty] private string _password = "";
    [ObservableProperty] private string _name = "";
    [ObservableProperty] private string _surname = "";
    [ObservableProperty] private string? _patronymic;
    [ObservableProperty] private string? _phone;
    [ObservableProperty] private UserRole _role;
    [ObservableProperty] private bool _isCanLogin;

    public User()
    {
        IsCanLogin = false;
        Role = UserRole.User;
    }
}