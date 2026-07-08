using XarajatApp.Models;
using XarajatApp.Results;
using XarajatApp.ViewModels;

namespace XarajatApp.Repositories.Interface;

public interface IGroupRepasitory
{
    public Task<Result> CreateTeam(CreateGroupViewModel createGroup);
    public Task<Result> AddTeam(string teamName, string username, string password);
    public Task<Team> GetTeamByName(string teamName);
    public Task<List<Team>> GetAllTeam();
}
