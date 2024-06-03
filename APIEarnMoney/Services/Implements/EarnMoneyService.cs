using APIEarnMoney.Helpers;
using APIEarnMoney.Models.Databases;
using APIEarnMoney.Models.Entities;
using APIEarnMoney.Models.Requests;
using APIEarnMoney.Models.Responses;
using APIEarnMoney.Services.Interfaces;
using Dapper;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace APIEarnMoney.Services.Implements
{
    public class EarnMoneyService : IEarnMoneyService
    {
        private readonly IDbConnection dbConnection;
        private readonly ILogger<EarnMoneyService> logger;

        public EarnMoneyService(AppDbContext context, ILogger<EarnMoneyService> logger)
        {
            dbConnection = context.Database.GetDbConnection();
            this.logger = logger;
        }
       
        public async Task<IEnumerable<EarnMoneyUser>> GetAllUsers(int limit = EarnMoneyConstant.LimitGetUser)
        {
            try
            {
                return await dbConnection.QueryAsync<EarnMoneyUser>($"Select * from EarnMoneyUsers order by Created desc {(limit > 0 ? $"limit {limit}" : "" )}");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<EarnMoneyUser> GetUserByGoogleId(string googleId)
        {
            try
            {
                var r =  await dbConnection.QueryFirstOrDefaultAsync<EarnMoneyUser>($"Select * from EarnMoneyUsers where GoogleId='{googleId}'");
                return r!;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<EarnMoneyUser> GetUserByDeviceId(string deviceId)
        {
            try
            {
                var r = await dbConnection.QueryFirstOrDefaultAsync<EarnMoneyUser>($"Select * from EarnMoneyUsers where DeviceId='{deviceId}'");
                return r!;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<int> InsertUser(EarnMoneyUser user)
        {
            try
            {
                var google = await GetUserByGoogleId(user.GoogleId!);
                if (google != null) throw new Exception("GoogleId exist");
                var device = await GetUserByDeviceId(user.DeviceId!);
                if (device != null) throw new Exception("DeviceId exist");
                return await dbConnection.ExecuteAsync(SQLQueryHelper.Insert(user), user);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<int> UpdateBalance(string googleId, double balance)
        {
            try
            {
                return await dbConnection.ExecuteAsync($"Update EarnMoneyUsers set Balance={balance} where GoogleId='{googleId}'");
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<int> UpdateMission(string googleId, int mission)
        {
            try
            {
                //return await dbConnection.ExecuteAsync($"Update EarnMoneyUsers set MissionSuccess={mission} {(mission == 3 ? $" ,TimeMission='{DateTime.Now:yyyy-MM-dd HH:mm:ss}'" : "")} where GoogleId='{googleId}'");
                return await dbConnection.ExecuteAsync($"Update EarnMoneyUsers set MissionSuccess={mission} where GoogleId='{googleId}'");
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                throw;
            }
        }
        public async Task<int> UpdateIsWD(string googleId, bool isWD, string response = "", string noHp = "")
        {
            try
            {
                bool ii = false;
                if(!string.IsNullOrWhiteSpace(response) || !string.IsNullOrWhiteSpace(noHp))
                {
                    ii = true;
                }
                return await dbConnection.ExecuteAsync($"Update EarnMoneyUsers set IsWD='{Convert.ToInt32(isWD)}' {(isWD == true ? $",LastWD='{DateTime.Now:yyyy-MM-dd HH:mm:ss}'": "")} {(ii == true ? $", Response='{response}', NoHP='{noHp}'" : "")} where GoogleId='{googleId}'");
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                throw;
            }
        }

        private async Task<EarnMoneyGetListMissionResponse> GetAllMission(EarnMoneyUser user)
        {
            try
            {

                var result = await RestHelper.GetResponse<EarnMoneyGetListMissionResponse>(EarnMoneyRouter.GetListMission, user.Token!, new EarnMoneygetListMissionRequest(user));
                return result.Data!;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private async Task<EarnMoneyBaseResponse> StartTask(EarnMoneyUser user, string missionId)
        {
            try
            {
                var result = await RestHelper.GetResponse<EarnMoneyBaseResponse>(EarnMoneyRouter.MissionStepOne, user.Token!, new EarnMoneyStartMissionRequest(user, missionId));
                return result.Data!;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private async Task<EarnMoneyBaseResponse> StepTwo(EarnMoneyUser user, string missionId)
        {
            try
            {
                var result = await RestHelper.GetResponse<EarnMoneyBaseResponse>(EarnMoneyRouter.MissionStepTwo, user.Token!, new EarnMoneyStartMissionRequest(user, missionId, 1));
                return result.Data!;
            }
            catch (Exception)
            {

                throw;
            }
        }
        
        private async Task<EarnMoneyBaseResponse> StepThree(EarnMoneyUser user, string missionId)
        {
            try
            {
                var result = await RestHelper.GetResponse<EarnMoneyBaseResponse>(EarnMoneyRouter.MissionStepThree, user.Token!, new EarnMoneyStartMissionRequest(user, missionId));
                return result.Data!;
            }
            catch (Exception)
            {

                throw;
            }
        }
        private async Task<EarnMoneyBaseResponse> SubmitTask(EarnMoneyUser user, string missionId)
        {
            try
            {
                var result = await RestHelper.GetResponse<EarnMoneyBaseResponse>(EarnMoneyRouter.MissionStepFour, user.Token!, new EarnMoneyStartMissionRequest(user, missionId));
                return result.Data!;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private async Task<EarnMoneyCheckCompleteMissionResponse> CheckCompleteMission(EarnMoneyUser user)
        {
            try
            {
                var result = await RestHelper.GetResponse<EarnMoneyCheckCompleteMissionResponse>(EarnMoneyRouter.CheckCompleteMission, user.Token!, new EarnMoneyCheckCompleteMissionRequest(user));
                return result.Data!;
            }
            catch (Exception)
            {

                throw;
            }
        }
        private async Task<int> DoOneMission(EarnMoneyUser user, string missionId)
        {
            try
            {
                await StartTask(user, missionId);
                await Task.Delay(1000);
                await StepTwo(user, missionId);
                await Task.Delay(1000);
                await StepThree(user, missionId);
                await Task.Delay(1000);
                await SubmitTask(user, missionId);
                await Task.Delay(1000);
                var check = await CheckCompleteMission(user);
                if (check != null && check.Code == 200)
                {
                    await UpdateMission(user.GoogleId!, check.Data!.Complete);
                    return check.Data.Complete!;
                }
                return 0;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task RawDoAutoMission(string googleId)
        {
            try
            {
                var user = await GetUserByGoogleId(googleId);
                if (user == null) throw new Exception("User null");
                int success = 0;
                var listMissions = await GetAllMission(user);
                int currentMissions = 0;
                foreach (var mission in listMissions.Data!.OrderBy(e => !e.Description!.Contains("detik")).Select(e => e.MissionId! ))
                {
                    currentMissions++;
                    logger.LogInformation($"Memulai Misi => {currentMissions} of {listMissions.Data!.Count}");
                    try
                    {
                        success = await DoOneMission(user, mission);
                        if(success >= 3)
                        {
                            try
                            {
                                //_ = RestHelper.SendText($"Mission User : {user.GoogleId!} Selesai");
                            }
                            catch (Exception)
                            {
                            }
                            logger.LogInformation("$Success All Mission");
                            throw new Exception("Exit");
                        }
                        await Task.Delay(1000);
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message == "Exit") throw new Exception("Selesai");
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> AutoInsertNewUser(EarnMoneyUser user)
        {
            try
            {
                var newUser = await InsertUser(user);
                if(newUser == 1)
                {
                    //await RestHelper.SendText($"User : {user.GoogleId!} Created");
                    _ = RawDoAutoMission(user.GoogleId!);
                    return 1;
                }
                return 0;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private async Task<EarnMoneyBaseResponse> DoWithDrawOne(EarnMoneyUser user, string noHp)
        {
            try
            {
                try
                {
                    var result = await RestHelper.GetResponse<EarnMoneyBaseResponse>(EarnMoneyRouter.Withdraw, user.Token!, new EarnMoneyWithdrawRequest(user, noHp));
                    result.Data!.GoogleId = user.GoogleId!;
                    logger.LogInformation(result.Data!.ToStringx());
                    return result.Data!;
                }
                catch (Exception)
                {
                    return null!;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<EarnMoneyUser>> CheckReadyWD(int limit = EarnMoneyConstant.LimitWD)
        {
            try
            {
                var users = await GetAllUsers(EarnMoneyConstant.LimitGetUser);
                int current = 0;
                List<EarnMoneyUser> result = [];
                foreach (var user in users.Where(e => !e.IsWD))
                {
                    var r = await CheckCompleteMission(user);
                    logger.LogInformation(r.ToStringx());
                    if(r != null && r.Code == 200 && r.Data!.Complete >= 3)
                    {
                        current++;
                        result.Add(user);
                    }
                }

                return result.ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task DoAutoWithDraw(string noHp, int limit = EarnMoneyConstant.LimitWD)
        {
            try
            {
                var users = await CheckReadyWD(limit);
                if (users.Count < EarnMoneyConstant.MinimWD) throw new Exception($"User not in limit, user : {users.Count}");
                List<Task<EarnMoneyBaseResponse>> listTask = new();
                foreach(var u in users)
                {
                    listTask.Add(DoWithDrawOne(u, noHp));
                }

                var result = await Task.WhenAll(listTask);
                foreach(var r in result)
                {
                    try
                    {
                        //await RestHelper.SendText($"Withdraw User => {r.GoogleId!}\nNomor => {noHp}\nStatus => {r.Code}");
                    }
                    catch (Exception)
                    {
                    }
                    try
                    {
                        bool t = r.Code == 200 ? true : false;
                        await UpdateIsWD(r.GoogleId!, t, r.Message!, noHp!);
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                throw;
            }
        }

        private async Task<int> DeleteUser(string googleId)
        {
            try
            {
                return await dbConnection.ExecuteAsync($"Delete from EarnMoneyUsers where GoogleId='{googleId}'");
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task RefreshUserWD(int limit = EarnMoneyConstant.LimitGetUser)
        {
            try
            {
                var users = await GetAllUsers(limit);
                foreach(var u in users)
                {
                    try
                    {
                        var r = await CheckCompleteMission(u);
                        await UpdateMission(u.GoogleId!, r.Data!.Complete);
                        if(r.Data.Condistion == 0)
                        {
                            await UpdateIsWD(r.GoogleId!, true);
                        }
                    }
                    catch (Exception)
                    {

                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
