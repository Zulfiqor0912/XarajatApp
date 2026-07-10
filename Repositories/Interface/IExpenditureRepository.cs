using XarajatApp.Models;
using XarajatApp.Results;
using XarajatApp.ViewModels;

namespace XarajatApp.Repositories.Interface
{
    public interface IExpenditureRepository
    {
        public Task<List<GroupMember>> GetAllGroupMembersByGroupname(string groupname);
    }
}
