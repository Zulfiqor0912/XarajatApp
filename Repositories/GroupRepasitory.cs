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
    private static readonly string PathGM = Path.Combine(AppContext.BaseDirectory, "group_members.json");

    private List<Group> groups;
    private List<GroupMember> groupMembers;

    //public GroupRepasitory()
    //{
        
    //}
    public async Task<Result> AddTeam(string teamName, string username, string password)
    {
        var team = await GetTeamByName(teamName);
        if (team.Name != null)
        {
            var hasher = new PasswordHasher<object>();
            var result = hasher.VerifyHashedPassword(null, team.PasswordHash, password);

            if (result == PasswordVerificationResult.Success)
            {
                var users = await 
                return new Result
                {
                    Succed = true,
                    Message = $"->{teamName}<-guruhiga yangi foydalanuvchi >{username}< qo'shildi"
                };
            }
            else
            {
                return new Result
                {
                    Succed = false,
                    Message = $"Parol xato"
                };
            }
        }
        else
        {
            return new Result
            {
                Succed = false,
                Message = "Bunday guruh mavjud emas!"
            };
        }
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
                };
            }
            
        }
        else
        {
            return new Result
            {
                Succed = false,
                Message = "Gruruh nomi yoki parol kiritilmagan"
            };
        }
    }

    public async Task<List<Group>> GetAllTeam()
    {
        if (!File.Exists(PathG)) return new List<Group>();

        string json = await File.ReadAllTextAsync(PathG);

        if (string.IsNullOrWhiteSpace(json)) return new List<Group>();

        groups = JsonSerializer.Deserialize<List<Group>>(json) ?? new List<Group>();
        return groups;
    }

    public async Task<Group> GetTeamByName(string teamName)
    {
        groups = await GetAllTeam();
        var group = groups.FirstOrDefault(g => g.Name == teamName);
        return group!;
    }

    public async Task<GetGroupsResult> ShowAllGroups()
    {
        groups = await GetAllTeam();
        if (groups != null)
        {
            List<GetAllGroupsViewModel> allGroups = new List<GetAllGroupsViewModel>();
            foreach (var item in groups)
            {
                allGroups.Add(new GetAllGroupsViewModel
                {
                    Name = item.Name,
                });
            }
            return new GetGroupsResult
            {
                Succed = true,
                Message = "Barcha guruhlar",
                GroupsViewModel = allGroups
            };
        }
        else
        {
            return new GetGroupsResult
            {
                Succed = false,
                Message = "Guruh mavjud emas!"
            };
        }
    }

    public async Task<List<GroupMember>> GetAllMembers()
    {
        if (!File.Exists(PathGM)) return new List<GroupMember>();

        var json = await File.ReadAllTextAsync(PathGM);
        if (string.IsNullOrWhiteSpace(json))
            return new List<GroupMember>();

        groupMembers = JsonSerializer.Deserialize<List<GroupMember>>(json) ?? new List<GroupMember>();
        return groupMembers;
    }
}
