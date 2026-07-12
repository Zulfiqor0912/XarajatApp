using XarajatApp.ViewModels;

namespace XarajatApp.Results;

public class CalculateExpendituresResult
{
    public bool Succed { get; set; }
    public string Message { get; set; } = string.Empty;
    public List<CalculateExpendituresViewModel> AllCalculateExpenditures { get; set; }
}
