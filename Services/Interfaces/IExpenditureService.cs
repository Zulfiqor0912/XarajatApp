using XarajatApp.Models;
using XarajatApp.Results;

namespace XarajatApp.Services.Interfaces;

public interface IExpenditureService
{
    public Task<GetAllGroupMembersResult> GetAllGroupMembersByGroupname(string groupname);
}
