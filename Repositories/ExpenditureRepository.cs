using System.Text.Json;
using XarajatApp.Models;
using XarajatApp.Repositories.Interface;
using XarajatApp.Results;
using XarajatApp.ViewModels;

namespace XarajatApp.Repositories;

public class ExpenditureRepository : IExpenditureRepository
{
    private static readonly string PathGM = Path.Combine(AppContext.BaseDirectory, "group_members.json");
    public async Task<List<GroupMember>> GetAllGroupMembersByGroupname(string groupname)
    {
        if (!File.Exists(PathGM)) return new List<GroupMember>();

        string json = await File.ReadAllTextAsync(PathGM);
        if (string.IsNullOrWhiteSpace(json)) return new List<GroupMember>();

        var usergroup = JsonSerializer.Deserialize<List<GroupMember>>(json) ?? new List<GroupMember>();
        var groupmembers = usergroup
            .Where(g => g.Groupname == groupname)
            .DistinctBy(g => g.Username)
            .ToList();

        return groupmembers;
    }
}
