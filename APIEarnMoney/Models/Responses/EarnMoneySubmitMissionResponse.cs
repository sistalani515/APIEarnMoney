using Newtonsoft.Json;

namespace APIEarnMoney.Models.Responses
{
    public class EarnMoneySubmitMissionResponse
    {
        [JsonProperty("data")]
        public EarnMoneySubmitMissionResponseData? Data { get; set; } = new EarnMoneySubmitMissionResponseData();
    }
    public class EarnMoneySubmitMissionResponseData
    {
        [JsonProperty("is_auto_success")]
        public int Status { get; set; }
    }
}
