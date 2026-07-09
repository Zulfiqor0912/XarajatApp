using XarajatApp.ViewModels;

namespace XarajatApp.Results;

public class GetGroupsResult
{
    public bool Succed { get; set; }
    public string Message { get; set; } = String.Empty;
    public List<GetAllGroupsViewModel> GroupsViewModel { get; set; }
}
