using APIEarnMoney.Models.Entities;
using System.ComponentModel;

namespace APIEarnMoney.Models.Requests
{
    public class EarnMoneyWithdrawRequest : EarnMoneyBaseRequest
    {
        public string? NoDana { get; set; }
        public EarnMoneyWithdrawRequest(EarnMoneyUser user, string noHp)
        {
            GoogleId = user.GoogleId;
            DeviceId = user.DeviceId;
            Token = user.Token;
            NoDana = noHp;
        }
        public override string ToString()
        {
            return  $"payee_documentType=" +
                    $"&payee_documentType2=" +
                    $"&payee_documentType3=" +
                    $"&payee_documentId=" +
                    $"&payee_documentId2=" +
                    $"&payee_documentId3=" +
                    $"&bank=DANA" +
                    $"&pay_account={NoDana}" +
                    $"&real_name=bdhdhd" +
                    $"&money=400.0" +
                    $"&jf=600" +
                    $"&id=187" +
                    $"&channel={Channel}" +
                    $"&version={Version}" +
                    $"&versionCode={VersionCode}" +
                    $"&deviceId={DeviceId}" +
                    $"&randomUUID=" +
                    $"&token={Token}" +
                    $"&is_vpn={IsVPN}" +
                    $"&is_developer={IsDeveloper}" +
                    $"&cell_mcc={CellMCC}" +
                    $"&gaid={GoogleId}";
        }
    }
}
