using XarajatApp.Models;
using XarajatApp.Results;
using XarajatApp.ViewModels;

namespace XarajatApp.Services.Interfaces;

public interface IExpenditureService
{
    public Task<GetAllGroupMembersResult> GetAllGroupMembersByGroupname(string groupname);
    public Task<Result> CreateExpenditure(CreateExpenditureViewModel createExpenditureViewModel);
    public Task<GetAllExpendituresByGroupResult> GetAllExpenditures(string groupname);
    public Task<CalculateExpendituresResult> GetAllCalculateExpenditures(string groupname);
}
