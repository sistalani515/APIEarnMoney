using Newtonsoft.Json;

namespace APIEarnMoney.Helpers
{
    public static class EarnMoneyHelper
    {
        public static string ToStringx(this object data)
        {
			try
			{
                if (data == null) return "{}";
                return JsonConvert.SerializeObject(data);
			}
			catch (Exception)
			{
                return "{}";
			}
        }
    }
}
