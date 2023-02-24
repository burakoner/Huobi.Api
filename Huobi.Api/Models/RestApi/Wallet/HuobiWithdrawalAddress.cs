namespace Huobi.Api.Models.RestApi.Wallet;

public class HuobiWithdrawalAddress
{
    [JsonProperty("currency")]
    public string Asset { get; set; }

    public string Chain { get; set; }

    public string Note { get; set; }
    
    public string AddressTag { get; set; }
    
    public string Address { get; set; }
}