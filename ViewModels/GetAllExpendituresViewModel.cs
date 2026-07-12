namespace XarajatApp.ViewModels;

public class GetAllExpendituresViewModel
{
    public string Username { get; set; } = string.Empty;
    public string Fullname { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string Description { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; } = DateTime.Now;
}
