using XarajatApp.ViewModels;

namespace XarajatApp.Results;

public class GetAllGroupMembersResult
{
    public bool Succed { get; set; }
    public string Message { get; set; } = String.Empty;
    public List<GroupMemberViewModel> GroupMembersViewModel { get; set; }
}
