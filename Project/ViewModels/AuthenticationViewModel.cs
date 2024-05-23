using Advanced.Tools;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using Project.Models;
using Project.Tools.Enums;
using Project.Views.Pages;
using Project.Views.Pages.Client;
using Wpf.Ui;
using Wpf.Ui.Controls;

namespace Project.ViewModels;

public partial class AuthenticationViewModel(MainViewModel mainViewModel, object startPage) : PageBaseViewModel(mainViewModel, startPage)
{
    [ObservableProperty] private string _login = "";
    [ObservableProperty] private string _password = "";
    [ObservableProperty] private string _passwordConfirm = "";
    [ObservableProperty] private string _phone = "";
    [ObservableProperty] private string _name = "";
    [ObservableProperty] private string _surname = "";
    [ObservableProperty] private string _patronymic = "";

    [RelayCommand]
    private void OpenLoginPage()
    {
        Page = new LoginPage();
    }

    [RelayCommand]
    private void OpenRegistrationPage()
    {
        Page = new RegistrationPage();
    }
    
    [RelayCommand]
    private async Task UserLogin()
    {
        if (string.IsNullOrWhiteSpace(Login) || string.IsNullOrWhiteSpace(Password))
        {
            Notify.ShowWarning("Заполнены не все поля", "Внимание");
            return;
        }

        List<User> users;

        _mainViewModel.IsLoading = true;
        using (DataBaseContext db = new())
        {
            if (await db.Database.CanConnectAsync())
            {
                await db.Users.LoadAsync();
                users = db.Users.Local.ToList();
            }
            else
            {
                Notify.ShowError("Не удаётся подключится к базе данных", "Ошибка соединения");
                
                _mainViewModel.IsLoading = false;
                return;
            }
        }
        
        User? user = users.Find(u => u.Login.Equals(Login) && u.Password.Equals(Password));
            
        if (user != null)
        {
            Notify.ShowSuccess($"Вы вошли как {user.Name} {user.Surname} {user.Patronymic}", "Успех");

            if (user.Role == UserRole.User)
            {
                _mainViewModel.CurrentViewModel = new UserMainPageViewModel(_mainViewModel, new MainPage())
                {
                    User = user
                };
            }
            else
            {
                _mainViewModel.CurrentViewModel = new EmployeeMainPageViewModel(_mainViewModel, new Views.Pages.Employee.MainPage())
                {
                    User = user
                };
            }
        }
        else
        {
            Notify.ShowError("Не верный логин и/или пароль", "Ошибка");
        }
        
        _mainViewModel.IsLoading = false;
    }

    [RelayCommand]
    private async Task UserRegistration()
    {
        if (string.IsNullOrWhiteSpace(Login) || string.IsNullOrWhiteSpace(Password)
            || string.IsNullOrWhiteSpace(PasswordConfirm)|| string.IsNullOrWhiteSpace(Name)
            || string.IsNullOrWhiteSpace(Surname)|| string.IsNullOrWhiteSpace(Phone))
        {
            Notify.ShowWarning("Заполнены не все поля", "Внимание");
            return;
        }

        if (!Login.IsLength(3, 20))
        {
            Notify.ShowWarning("Минимальная длинна логина 3, максимальна 20", "Внимание");
            return;
        }

        if (!Password.IsLength(6, 40))
        {
            Notify.ShowWarning("Минимальная длинна пароля 6, максимальна 40", "Внимание");
            return;
        }

        if (!Phone.IsCorrectPhone())
        {
            Notify.ShowWarning("Не корректный номер телефона", "Внимание");
            return;
        }

        if (!Password.Equals(PasswordConfirm))
        {
            Notify.ShowWarning("Пароли не совпадают", "Внимание");
            return;
        }
            
        _mainViewModel.IsLoading = true;
        using (DataBaseContext db = new())
        {
            if (await db.Database.CanConnectAsync())
            {
                await db.Users.LoadAsync();
            }
            else
            {
                Notify.ShowError("Не удаётся подключится к базе данных", "Ошибка соединения");
                _mainViewModel.IsLoading = false;
                return;
            }

            List<User> users = db.Users.Local.ToList();

            if (users.Any(u => u.Login.Equals(Login)))
            {
                Notify.ShowWarning("Пользователь с таким логином уже существует", "Внимание");
                return;
            }

            User user = new()
            {
                Login = Login,
                Name = Name,
                Password = Password,
                Surname = Surname,
                Patronymic = Patronymic,
                Phone = Phone,
            };

            db.Users.Add(user);
            await db.SaveChangesAsync();
            
            Notify.ShowSuccess($"Вы зарегистрировались как {user.Name} {user.Surname} {user.Patronymic}", "Успех");
            
            _mainViewModel.CurrentViewModel = new UserMainPageViewModel(_mainViewModel, new MainPage())
            {
                User = user
            };
        }
    }
}