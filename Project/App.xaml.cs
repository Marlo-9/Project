using System.Configuration;
using System.Data;
using System.Windows;
using Project.Models;
using Project.ViewModels;
using Project.Views.Pages;
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
        
        using (DataBaseContext db = new())
        {
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
        }

        AuthenticationWindow window = new()
        {
            DataContext = new AuthenticationViewModel(new LoginPage())
            {
                Login = "Marlo",
                Password = "1234"
            }
        };
        
        window.Show();
    }
}