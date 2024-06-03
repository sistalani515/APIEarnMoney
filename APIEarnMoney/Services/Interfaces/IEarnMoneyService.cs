using APIEarnMoney.Models.Entities;

namespace APIEarnMoney.Services.Interfaces
{
    public interface IEarnMoneyService
    {
        Task<IEnumerable<EarnMoneyUser>> GetAllUsers(int limit = 10);
        Task<int> InsertUser(EarnMoneyUser user);
        Task<int> UpdateBalance(string googleId, double balance);
        Task<int> UpdateMission(string googleId, int mission);
        Task<int> UpdateIsWD(string googleId, bool isWD, string response = "", string noHp = "");
        Task RawDoAutoMission(string googleId);
        Task<int> AutoInsertNewUser(EarnMoneyUser user);
        Task DoAutoWithDraw(string noHp, int limit = 10);
        Task RefreshUserWD(int limit = 100);
    }
}
