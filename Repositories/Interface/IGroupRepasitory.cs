using XarajatApp.Models;
using XarajatApp.Results;
using XarajatApp.ViewModels;

namespace XarajatApp.Repositories.Interface;

public interface IGroupRepasitory
{
    public Task<Result> CreateGroup(CreateGroupViewModel createGroup);
    public Task<Result> AddTeam(JoinGroupViewModel joinGroupViewModel);
    public Task<Group> GetTeamByName(string teamName);
    public Task<List<Group>> GetAllTeam();
    public Task<GetGroupsResult> ShowAllGroups();
}
