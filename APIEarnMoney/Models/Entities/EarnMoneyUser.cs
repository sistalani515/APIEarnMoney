using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace APIEarnMoney.Models.Entities
{
    public class EarnMoneyUser
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string? GoogleId { get; set; }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string? DeviceId { get; set; }
        public string? Token { get; set; }
        public double Balance { get; set; }
        public int MissionSuccess { get; set; }
        public bool IsWD {  get; set; }
        public string? Response {  get; set; }
        public string? NoHP {  get; set; }
        public DateTime? TimeMission { get; set; }
        public DateTime? LastWD { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public static EarnMoneyUser ParseFromRaw(string raw)
        {
            NameValueCollection parsedData = HttpUtility.ParseQueryString(raw);
            return new EarnMoneyUser
            {
                DeviceId = parsedData["deviceId"],
                GoogleId = parsedData["gaid"],
                Token = parsedData["token"]
            };
        }
    }
}
