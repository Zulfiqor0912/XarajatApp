using XarajatApp.ViewModels;

namespace XarajatApp.Results;

public class GetAllExpendituresByGroupResult
{
    public bool Succed { get; set; }
    public string Message { get; set; } = string.Empty;
    public List<GetAllExpendituresViewModel> GetAllExpenditures { get; set; }

}
