using System.Configuration;
using System.Data;
using System.Windows;
using Project.Models;
using Project.ViewModels;
using Project.Views.Windows;

namespace Project;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        /*using (DataBaseContext db = new())
        {
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            db.Users.Add(new User()
            {
                Login = "login1",
                Password = "pass1",
                Name = "Имя1",
                Surname = "Фамилия1",
                Patronymic = "Отчество1",
                Phone = "87006005040"
            });
            db.SaveChanges();
        }*/

        MainWindow window = new()
        {
            DataContext = new MainViewModel()
        };
        
        window.Show();
    }
}