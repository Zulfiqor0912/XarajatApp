using XarajatApp.Models;
using XarajatApp.Repositories.Interface;
using XarajatApp.Results;
using XarajatApp.Services.Interfaces;
using XarajatApp.ViewModels;

namespace XarajatApp.Services;

public class ExpenditureService(
    IExpenditureRepository expenditureRepository,
    IGroupRepository groupRepository,
    IUserRepository userRepository
    ) : IExpenditureService
{
    public async Task<Result> CreateExpenditure(CreateExpenditureViewModel createExpenditureViewModel)
    {
        if (createExpenditureViewModel != null)
        {
            var user = await userRepository.GetUserByUsername(createExpenditureViewModel.Username);
            var gm = await expenditureRepository.GetAllGroupMembersByGroupname(createExpenditureViewModel.TeamName);
            if (user != null)
            {
                if (gm.FirstOrDefault(u => u.Username == createExpenditureViewModel.Username) != null)
                {
                    await expenditureRepository.CreateExpenditure( new Expenditure
                    {
                        Username = createExpenditureViewModel.Username,
                        Fullname = createExpenditureViewModel.Fullname,
                        Amount = createExpenditureViewModel.Amount,
                        Description = createExpenditureViewModel.Description,
                        TeamName = createExpenditureViewModel.TeamName
                    });
                    return new Result
                    {
                        Succed = true,
                        Message = "Xarajat saqlandi"
                    };
                }
                else
                {
                    return new Result
                    {
                        Succed = false,
                        Message = "Bu foydalanuvchi guruh azosi emas!"
                    };
                }
            }
            else
            {
                return new Result
                {
                    Succed = false,
                    Message = "Bunday foydalanuvchi topilmadi"
                };
            }
            //var expenditure = new Expenditure
            //{
            //    Id = Guid.NewGuid(),
            //    Username = createExpenditureViewModel.Username,
            //    Fullname = "",
            //}
        }
        else
        {
            return new Result
            {
                Succed = false,
                Message = "Ma'lumot kelmayapti!"
            };
        }
    }

    public async Task<CalculateExpendituresResult> GetAllCalculateExpenditures(string groupname)
    {
        var expenditures = await expenditureRepository.GetAllExpendituresByGroup(groupname);
        if (expenditures.Count != 0)
        {
            var userExpenditures = expenditures
                .GroupBy(u => u.Username)
                .Select(u => new
                {
                    Username = u.Key,
                    FullName = u.First().Fullname,
                    TotalAmount = u.Sum(y => y.Amount)
                })
                .ToList();
            decimal userCount = userExpenditures.Count();

            decimal _amount = userExpenditures.Sum(x => x.TotalAmount);
            decimal avaregaCost = _amount / userCount;

            List<CalculateExpendituresViewModel> calculateExpenditures = new List<CalculateExpendituresViewModel>();
            calculateExpenditures = userExpenditures
                .Select(x => new CalculateExpendituresViewModel
                {
                    Username = x.Username,
                    Fullname = x.FullName,
                    TotalCost = x.TotalAmount,
                    ToGetMoney = x.TotalAmount >= avaregaCost ? (x.TotalAmount - avaregaCost) : 0,
                    ToGiveMoney = x.TotalAmount <= avaregaCost ? (x.TotalAmount - avaregaCost) : 0,
                    TotalCostTeamMoney = _amount
                })
                .ToList();

            return new CalculateExpendituresResult
            {
                Succed = true,
                Message = "Hisoblandi",
                AllCalculateExpenditures = calculateExpenditures
            };

        }
        else
        {
            return new CalculateExpendituresResult
            {
                Succed = false,
                Message = "Hech kim xarajat qilmagan"
            };
        }
    }

    public async Task<GetAllExpendituresByGroupResult> GetAllExpenditures(string groupname)
    {
        var expendituresByGoup = await expenditureRepository.GetAllExpendituresByGroup(groupname);
        if (expendituresByGoup != null)
        {
            var viewModel = new List<GetAllExpendituresViewModel>();
            foreach (var item in expendituresByGoup)
            {
                viewModel.Add(new GetAllExpendituresViewModel
                {
                    Username = item.Username,
                    Fullname = item.Fullname,
                    Amount = item.Amount,
                    Description = item.Description,
                    CreatedDate = item.CreatedDate
                });
            }
            return new GetAllExpendituresByGroupResult
            {
                Succed = true,
                Message = "Hali hech kim xarajat qilmagan",
                GetAllExpenditures = viewModel
            };
        }
        else
        {
            return new GetAllExpendituresByGroupResult
            {
                Succed = false,
                Message = "Hali hech kim xarajat qilmagan"
            };
        }
    }

    public async Task<GetAllGroupMembersResult> GetAllGroupMembersByGroupname(string groupname)
    {
        var members = await expenditureRepository.GetAllGroupMembersByGroupname(groupname);
        List<GroupMemberViewModel> groupMembers = new List<GroupMemberViewModel>();
        foreach (var item in members)
        {
            groupMembers.Add(new GroupMemberViewModel
            {
                Username = item.Username
            });
        }

        return new GetAllGroupMembersResult
        {
            Succed = true,
            Message = "Guruhning barcha a'zolar",
            GroupMembersViewModel = groupMembers
        };
    }
}
