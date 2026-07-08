using XarajatApp.Models;
using XarajatApp.ViewModel;

namespace XarajatApp.Repositories.Interface;

public interface IUserRepository
{
    public Task<bool> Register(RegisterViewModel registerViewModel);
    public Task<bool> Login(string username, string password);
    public Task<List<User>> GetAllUsers();
}
