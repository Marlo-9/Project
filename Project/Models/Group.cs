using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Project.Models;

public partial class Group : ObservableObject
{
    [ObservableProperty] private int _id;
    [ObservableProperty] private string _name;
    [ObservableProperty] private ObservableCollection<Student>? _students;
    
    public Group() {}
}