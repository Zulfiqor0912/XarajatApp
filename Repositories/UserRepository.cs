using Microsoft.AspNetCore.Identity;
using System.Text.Json;
using XarajatApp.Models;
using XarajatApp.Repositories.Interface;
using XarajatApp.ViewModel;

namespace XarajatApp.Repositories;

public class UserRepository : IUserRepository
{
    private List<User> users { get; set; }

    private static readonly string Path = System.IO.Path.Combine(AppContext.BaseDirectory, "users.json");

    public UserRepository()
    {
        if (!File.Exists(Path))
            users = new List<User>();
    }
    public async Task<List<User>> GetAllUsers()
    {
        if (!File.Exists(Path)) return new List<User>();
        string json = await File.ReadAllTextAsync(Path);

        if (string.IsNullOrWhiteSpace(json))
            return new List<User>();
        users = JsonSerializer.Deserialize<List<User>>(json) ?? new List<User>();
        return users;
    }

    public Task<bool> Login(string username, string password)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Register(RegisterViewModel registerViewModel)
    {
        var hasher = new PasswordHasher<object>();
        string hash = hasher.HashPassword(null, registerViewModel.Password);

        var user = new User
        {
            Id = Guid.NewGuid(),
            Username = registerViewModel.Username,
            Fullname = registerViewModel.Fullname,
            PasswordHash = hash,
            CreatedDate = DateTime.Now
        };


    }
}
