using Microsoft.AspNetCore.Identity;
using System.Text.Json;
using XarajatApp.Models;
using XarajatApp.Repositories.Interface;
using XarajatApp.Results;
using XarajatApp.ViewModels;

namespace XarajatApp.Repositories;

public class GroupRepasitory : IGroupRepasitory
{
    private readonly string PathG = Path.Combine(AppContext.BaseDirectory, "group.json");
    private List<Group> groups;

    public GroupRepasitory()
    {
        
    }
    public Task<Result> AddTeam(string teamName, string username, string password)
    {
        throw new NotImplementedException();
    }

    public async Task<Result> CreateGroup(CreateGroupViewModel createGroup)
    {
        if (createGroup.GroupName != null && createGroup.GroupPassword != null)
        {
            groups = await GetAllTeam();

            var group = groups
                .FirstOrDefault(g => g.Name == createGroup.GroupName);

            if (group is null)
            {
                var hasher = new PasswordHasher<object>();
                var hash = hasher.HashPassword(null, createGroup.GroupPassword);

                group = new Group
                {
                    Id = Guid.NewGuid(),
                    Name = createGroup.GroupName,
                    PasswordHash = hash
                };
                groups.Add(group);
                var json = JsonSerializer.Serialize(groups);
                await File.WriteAllTextAsync(PathG, json);

                return new Result
                {
                    Succed = true,
                    Message = "Yangi guruh yaratildi"
                };

            }
            else 
            {
                return new Result
                {
                    Succed = false,
                    Message = "Bu nomdagi guruh mavjud"
                }
            }
            
        }
        else
        {
            return new Result
            {
                Succed = false,
                Message = "Gruruh nomi yoki parol kiritilmagan"
            }
        }
    }

    public async Task<List<Group>> GetAllTeam()
    {
        if (!File.Exists(PathG)) return new List<Group>();

        string json = await File.ReadAllTextAsync(Path);

        if (string.IsNullOrWhiteSpace(json)) return new List<Group>();

        groups = JsonSerializer.Deserialize<List<Group>>(json) ?? new List<Group>();
        return groups;
    }

    public Task<Group> GetTeamByName(string teamName)
    {
        throw new NotImplementedException();
    }
}
