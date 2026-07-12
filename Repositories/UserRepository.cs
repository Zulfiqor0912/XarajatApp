using Microsoft.AspNetCore.Identity;
using System.Text.Json;
using XarajatApp.Models;
using XarajatApp.Repositories.Interface;
using XarajatApp.Results;
using XarajatApp.ViewModels;

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

    public async Task<Result> Login(LoginViewModel loginViewModel)
    {
        if (loginViewModel.Username is null || loginViewModel.Password is null)
        {
            return new Result
            {
                Succed = false,
                Message = "Username yoki password kiritilmagan."
            };
        }
        else
        {
            users = await GetAllUsers();

            var user = users
                .FirstOrDefault(u => u.Username == loginViewModel.Username);

            if (user is null)
            {
                return new Result
                {
                    Succed = false,
                    Message = "Bunday foydalanuvchi hali royhatdan otmagan"
                };
            }
            else
            {
                var hasher = new PasswordHasher<object>();
                var result = hasher.VerifyHashedPassword(null, user.PasswordHash, loginViewModel.Password);

                if (result == PasswordVerificationResult.Success)
                {
                    return new Result
                    {
                        Succed = true,
                        Message = "Login "
                    };
                }
                else
                {
                    return new Result
                    {
                        Succed = false,
                        Message = "Parol noto'g'ri kiritilgan",
                        UserId = user.Id
                    };
                }
            }
        }
    }

    public async Task<bool> Register(RegisterViewModel registerViewModel)
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

        users = await GetAllUsers();

        if (users.Contains(user)) return false;
        else
        {
            users.Add(user);
            var json = JsonSerializer.Serialize(users);
            File.WriteAllText(Path, json);
            return true;
        }
    }

    public async Task<User> GetUserByUsername(string username)
    {
        users = await GetAllUsers();
        var t = users
            .FirstOrDefault(u => u.Username == username);
        return t!;
    }
}
