using CommunityToolkit.Mvvm.ComponentModel;

namespace Project.Models;

public partial class Visit : ObservableObject
{
    [ObservableProperty] private int _id;
    [ObservableProperty] private int _studentId;
    [ObservableProperty] private Student _student;
    [ObservableProperty] private DateOnly _start;
    [ObservableProperty] private DateOnly? _end;
    
    public Visit() {}
}