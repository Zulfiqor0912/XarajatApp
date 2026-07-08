using XarajatApp.Models;
using XarajatApp.Results;
using XarajatApp.ViewModels;

namespace XarajatApp.Repositories.Interface;

public interface IUserRepository
{
    public Task<bool> Register(RegisterViewModel registerViewModel);
    public Task<Result> Login(LoginViewModel loginViewModel);
    public Task<List<User>> GetAllUsers();
}
