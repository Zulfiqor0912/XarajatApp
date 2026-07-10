using XarajatApp.Models;
using XarajatApp.Repositories.Interface;
using XarajatApp.Results;
using XarajatApp.Services.Interfaces;
using XarajatApp.ViewModels;

namespace XarajatApp.Services;

public class ExpenditureService(
    IExpenditureRepository expenditureRepository,
    IGroupRepository groupRepository
    ) : IExpenditureService
{
    public async Task<GetAllGroupMembersResult> GetAllGroupMembersByGroupname(string groupname)
    {
        var members = await expenditureRepository.GetAllGroupMembersByGroupname(groupname);
        List<GroupMemberViewModel> groupMembers = new List<GroupMemberViewModel>();
        foreach (var item in members)
        {
            groupMembers.Add(new GroupMemberViewModel
            {
                Username = item.Username
            });
        }

        return new GetAllGroupMembersResult
        {
            Succed = true,
            Message = "Guruhning barcha a'zolar",
            GroupMembersViewModel = groupMembers
        };
    }
}
