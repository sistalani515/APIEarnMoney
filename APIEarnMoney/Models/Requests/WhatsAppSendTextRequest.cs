using Newtonsoft.Json;

namespace APIEarnMoney.Models.Requests
{
    public class WhatsAppSendTextRequest
    {
        public WhatsAppSendTextRequest(string? phone, string? message)
        {
            Phone = phone;
            Message = message;
        }

        [JsonProperty("phone")]
        public string? Phone { get; set; }
        [JsonProperty("message")]
        public string? Message { get; set; }
    }
}
