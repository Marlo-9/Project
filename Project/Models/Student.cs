using CommunityToolkit.Mvvm.ComponentModel;

namespace Project.Models;

public partial class Student : ObservableObject
{
    [ObservableProperty] private int _id;
    [ObservableProperty] private int? _groupId;
    [ObservableProperty] private Group? _group;
    [ObservableProperty] private string _name = "";
    [ObservableProperty] private string _surname = "";
    [ObservableProperty] private string? _patronymic;
    
    public Student() {}
}