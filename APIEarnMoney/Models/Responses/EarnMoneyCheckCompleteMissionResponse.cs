using Newtonsoft.Json;

namespace APIEarnMoney.Models.Responses
{
    public class EarnMoneyCheckCompleteMissionResponse : EarnMoneyBaseResponse
    {
        [JsonProperty("data")]
        public EarnMoneyCheckCompleteMissionResponseData? Data { get; set; } = new EarnMoneyCheckCompleteMissionResponseData();
    }
    public class EarnMoneyCheckCompleteMissionResponseData
    {
        [JsonProperty("completion_conditions")]
        public int Condistion { get; set; }
        [JsonProperty("success_conditions")]
        public int Complete { get; set; }
        [JsonProperty("money")]
        public double Money { get; set; }
    }
}
