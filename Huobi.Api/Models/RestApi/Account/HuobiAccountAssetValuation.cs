namespace Huobi.Api.Models.RestApi.Account;

public class HuobiAccountAssetValuation
{
    public decimal Balance { get; set; }

    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime timestamp { get; set; }
}