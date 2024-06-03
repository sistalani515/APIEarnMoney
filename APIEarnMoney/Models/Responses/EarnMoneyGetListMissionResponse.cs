using Newtonsoft.Json;

namespace APIEarnMoney.Models.Responses
{
    public class EarnMoneyGetListMissionResponse : EarnMoneyBaseResponse
    {
        [JsonProperty("data")]
        public List<EarnMoneyGetListMissionResponseData>? Data { get; set; } = new List<EarnMoneyGetListMissionResponseData>();
    }
    public class EarnMoneyGetListMissionResponseData
    {
        [JsonProperty("no")]
        public string? MissionId { get; set; }
        [JsonProperty("type")]
        public int Type { get; set; }
        [JsonProperty("status")]
        public int Status { get; set; }
        [JsonProperty("app_name")]
        public string? Name { get; set; }
        [JsonProperty("name")]
        public string? Description { get; set; }
    }
}
