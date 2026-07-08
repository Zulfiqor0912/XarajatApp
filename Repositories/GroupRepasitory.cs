using XarajatApp.Models;
using XarajatApp.Repositories.Interface;
using XarajatApp.Results;
using XarajatApp.ViewModels;

namespace XarajatApp.Repositories;

public class GroupRepasitory : IGroupRepasitory
{
    public Task<Result> AddTeam(string teamName, string username, string password)
    {
        throw new NotImplementedException();
    }

    public Task<Result> CreateTeam(CreateGroupViewModel createGroup)
    {
        if (createGroup.GroupName != null && createGroup.GroupPassword != null)
        {
            
        }
        else
        {
            return new Result
            {
                Succed = false,
                Message = "Gruruh nomi yoki parol kiritilmagan"
            }
        }
    }

    public Task<List<Team>> GetAllTeam()
    {
        throw new NotImplementedException();
    }

    public Task<Team> GetTeamByName(string teamName)
    {
        throw new NotImplementedException();
    }
}
