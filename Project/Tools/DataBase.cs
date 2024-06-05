using System.Collections.ObjectModel;
using Advanced.Tools;
using Microsoft.EntityFrameworkCore;
using Project.Models;

namespace Project.Tools;

public static class DataBase
{
    public static bool TryRemoveEntity<T>(T entity)
    {
        if (entity == null)
        {
            Notify.ShowError("Не удалось удалить нулевую запись, обратитесь к системному администратору", "Ошибка работы с базой данных");
            return false;
        }
        
        try
        {
            using DataBaseContext db = new();
            db.Remove(entity);
            db.SaveChanges();
            return true;
        }
        catch (Exception e)
        {
            Notify.ShowError("Не удалось удалить запись, обратитесь к системному администратору", "Ошибка работы с базой данных");
            return false;
        }
    }
    
    public static async Task<bool> TryRemoveEntityAsync<T>(T entity)
    {
        if (entity == null)
        {
            Notify.ShowError("Не удалось удалить нулевую запись, обратитесь к системному администратору", "Ошибка работы с базой данных");
            return false;
        }
        
        try
        {
            await using DataBaseContext db = new();
            db.Remove(entity);
            await db.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            Notify.ShowError("Не удалось удалить запись, обратитесь к системному администратору", "Ошибка работы с базой данных");
            return false;
        }
    }

    public static async Task<ObservableCollection<T>> LoadEntityAsync<T>()
    {
        await using DataBaseContext db = new();
        
        if (!await db.Database.CanConnectAsync())
        {
            Notify.ShowError("Не удаётся подключится к базе данных", "Ошибка соединения");
            return [];
        }
        
        if (typeof(T) == typeof(Group))
        {
            
            return new ObservableCollection<T>((IEnumerable<T>)db.Groups.ToList());
        }
        if (typeof(T) == typeof(Student))
        {
            return new ObservableCollection<T>((IEnumerable<T>)db.Students.ToList());
        }
        if (typeof(T) == typeof(User))
        {
            return new ObservableCollection<T>((IEnumerable<T>)db.Users.ToList());
        }
        if (typeof(T) == typeof(Visit))
        {
            return new ObservableCollection<T>((IEnumerable<T>)db.Visits.ToList());
        }

        return [];
    }

    public static async Task<bool> TryUpdateEntityAsync<T>(T entity)
    {
        if (entity == null)
        {
            Notify.ShowError("Не удалось обновить нулевую запись, обратитесь к системному администратору", "Ошибка работы с базой данных");
            return false;
        }
        
        try
        {
            await using DataBaseContext db = new();
            db.Update(entity);
            await db.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            Notify.ShowError("Не удалось обновить запись, обратитесь к системному администратору", "Ошибка работы с базой данных");
            return false;
        }
    }

    public static async Task<bool> TryAddEntity<T>(T entity)
    {
        if (entity == null)
        {
            Notify.ShowError("Не удалось добавить нулевую запись, обратитесь к системному администратору", "Ошибка работы с базой данных");
            return false;
        }
        
        try
        {
            await using DataBaseContext db = new();
            await db.AddAsync(entity);
            await db.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            Notify.ShowError("Не удалось добавить запись, обратитесь к системному администратору", "Ошибка работы с базой данных");
            return false;
        }
    }
}