namespace Huobi.Api.Models.RestApi.Account;

public class HuobiAccountPointBalance
{
    [JsonProperty("accountId")]
    public long AccountId { get; set; }

    [JsonProperty("acctBalance")]
    public decimal AccountBalance { get; set; }

    public string AccountStatus { get; set; }

    [JsonProperty("groupIds")]
    public IEnumerable<HuobiPointGroup> Groups { get; set; }
}

public class HuobiPointGroup
{
    public long GroupId { get; set; }

    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime ExpiryDate { get; set; }

    [JsonProperty("remainAmt")]
    public decimal RemainingQuantity { get; set; }
}
