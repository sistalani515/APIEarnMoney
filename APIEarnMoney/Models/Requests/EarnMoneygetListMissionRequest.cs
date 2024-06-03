using APIEarnMoney.Models.Entities;

namespace APIEarnMoney.Models.Requests
{
    public class EarnMoneygetListMissionRequest : EarnMoneyBaseRequest
    {

        public int Page { get; set; } = 1;
        public int Limit { get; set; } = 100;
        public int GameFlag { get; set; } = 0;
        public EarnMoneygetListMissionRequest(EarnMoneyUser user)
        {
            GoogleId = user.GoogleId;
            DeviceId = user.DeviceId;
            Token = user.Token;
        }
        public override string ToString()
        {
            return $"page={Page}" +
                   $"&limit={Limit}" +
                   $"&game_flag={GameFlag}" +
                   $"&channel={Channel}" +
                   $"&version={Version}" +
                   $"&versionCode={VersionCode}" +
                   $"&deviceId={DeviceId}" +
                   $"&randomUUID={RandomUUID}" +
                   $"&token={Token}" +
                   $"&is_vpn={IsVPN}" +
                   $"&is_developer={IsDeveloper}" +
                   $"&cell_mcc={CellMCC}" +
                   $"&gaid={GoogleId}";
        }
    }
}
