namespace Huobi.Api.Models.RestApi.Wallet;

public class HuobiWithdrawalResponse
{
    [JsonProperty("currency")]
    public string Asset { get; set; }

    public string Address { get; set; }

    [JsonProperty("amount")]
    public decimal Quantity { get; set; }

    public decimal Fee { get; set; }
    
}