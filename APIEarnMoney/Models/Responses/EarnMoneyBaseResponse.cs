using Newtonsoft.Json;

namespace APIEarnMoney.Models.Responses
{
    public class EarnMoneyBaseResponse
    {
        [JsonProperty("code")]
        public int Code {  get; set; }
        [JsonProperty("count")]
        public int Count {  get; set; }
        [JsonProperty("msg")]
        public string? Message { get; set; }
        public string? GoogleId { get; set; }
    }
}
