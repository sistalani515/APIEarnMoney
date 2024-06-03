using APIEarnMoney.Models.Entities;

namespace APIEarnMoney.Models.Requests
{
    public class EarnMoneyCheckCompleteMissionRequest : EarnMoneyBaseRequest
    {
        public EarnMoneyCheckCompleteMissionRequest(EarnMoneyUser user)
        {
            GoogleId = user.GoogleId;
            DeviceId = user.DeviceId;
            Token = user.Token;
        }

        public override string ToString()
        {
            return $"channel={Channel}" +
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
