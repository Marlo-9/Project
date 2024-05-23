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

        MainWindow window = new()
        {
            DataContext = new MainViewModel()
        };
        
        window.Show();
    }
}