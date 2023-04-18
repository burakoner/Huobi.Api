namespace Huobi.Api.Models.RestApi.Wallet;

public class HuobiAccountAssetValuation
{
    public decimal Balance { get; set; }

    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime timestamp { get; set; }
}