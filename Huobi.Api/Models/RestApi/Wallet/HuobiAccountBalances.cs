namespace Huobi.Api.Models.RestApi.Wallet;

/// <summary>
/// Account and balance info
/// </summary>
public class HuobiAccountBalances : HuobiAccount
{
    /// <summary>
    /// The list of balances
    /// </summary>
    [JsonProperty("list")]
    public IEnumerable<HuobiAccountBalance> Data { get; set; } = Array.Empty<HuobiAccountBalance>();
}
