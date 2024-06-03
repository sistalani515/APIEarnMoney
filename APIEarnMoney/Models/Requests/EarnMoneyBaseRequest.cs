using APIEarnMoney.Helpers;

namespace APIEarnMoney.Models.Requests
{
    public class EarnMoneyBaseRequest
    {
        public string? Channel { get; set; } = EarnMoneyConstant.Channel;
        public string? Version { get; set; } = EarnMoneyConstant.Version;
        public string? VersionCode { get; set; } = EarnMoneyConstant.VersionCode;
        public string? IsVPN { get; set; } = EarnMoneyConstant.IsVPN;
        public string? IsDeveloper { get; set; } = EarnMoneyConstant.IsDeveloper;
        public string? CellMCC { get; set; } = EarnMoneyConstant.CellMCC;
        public string? GoogleId { get; set; }
        public string? DeviceId { get; set; }
        public string? Token { get; set; }
        public string? RandomUUID { get; set; }
    }
}
