using APIEarnMoney.Models.Entities;

namespace APIEarnMoney.Models.Requests
{
    public class EarnMoneyStartMissionRequest : EarnMoneyBaseRequest
    {
        public string? MissionId { get; set; }
        public int Urut { get; set; } = 0;
        public EarnMoneyStartMissionRequest(EarnMoneyUser user,string missionId,  int urut = 0)
        {
            GoogleId = user.GoogleId!;
            DeviceId = user.DeviceId!;
            Token  = user.Token!;
            Urut = urut;
            MissionId = missionId;
        }
        public override string ToString()
        {
            return $"no={MissionId}" +
                    $"{(Urut != 0 ? $"&step={Urut}" : "")}" +
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
