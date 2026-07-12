using System.Text.Json;
using XarajatApp.Models;
using XarajatApp.Repositories.Interface;
using XarajatApp.Results;
using XarajatApp.ViewModels;

namespace XarajatApp.Repositories;

public class ExpenditureRepository : IExpenditureRepository
{
    private static readonly string PathGM = Path.Combine(AppContext.BaseDirectory, "group_members.json");
    private static readonly string PathE = Path.Combine(AppContext.BaseDirectory, "expenditure.json");

    public async Task CreateExpenditure(Expenditure expenditure)
    {
        var expenditures = await GetAllExpenditure();
        expenditures.Add(expenditure);

        var json = JsonSerializer.Serialize<object>(expenditures);
        await File.WriteAllTextAsync(PathE, json);
    }

    public async Task<List<Expenditure>> GetAllExpenditure()
    {
        if (!File.Exists(PathE)) return new List<Expenditure>();

        var json = await File.ReadAllTextAsync(PathE);
        if (string.IsNullOrWhiteSpace(json)) return new List<Expenditure>();
        var expenditures = JsonSerializer.Deserialize<List<Expenditure>>(json) ?? new List<Expenditure>();
        return expenditures;

    }
    public async Task<List<Expenditure>> GetAllExpendituresByGroup(string groupname)
    {
        return (await GetAllExpenditure())
        .Where(g => g.TeamName == groupname)
        .ToList();
    }

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
