using XarajatApp.Models;
using XarajatApp.Results;
using XarajatApp.ViewModels;

namespace XarajatApp.Repositories.Interface;

public interface IGroupRepasitory
{
    public Task<Result> CreateGroup(CreateGroupViewModel createGroup);
    public Task<Result> AddTeam(string teamName, string username, string password);
    public Task<Group> GetTeamByName(string teamName);
    public Task<List<Group>> GetAllTeam();
    public Task<GetGroupsResult> ShowAllGroups();
}
