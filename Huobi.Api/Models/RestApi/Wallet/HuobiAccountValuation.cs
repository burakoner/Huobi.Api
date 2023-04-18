namespace Huobi.Api.Models.RestApi.Wallet;

/// <summary>
/// Account valuation
/// </summary>
public class HuobiAccountValuation
{
    [JsonProperty("updated")]
    public HuobiAccountValuationUpdate Update { get; set; }

    /// <summary>
    /// Timestamp of the data
    /// </summary>
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime Timestamp { get; set; }

    public decimal TodayProfit { get; set; }
    public decimal TodayProfitRate { get; set; }
    public decimal TotalBalance { get; set; }
    public IEnumerable<ProfitAccountBalance> ProfitAccountBalanceList { get; set; }
}

public class HuobiAccountValuationUpdate
{
    public bool Success { get; set; }

    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }
}

public class ProfitAccountBalance
{
    public string DistributionType { get; set; }
    public decimal AccountBalance { get; set; }
    public decimal Balance { get; set; }
    public bool Success { get; set; }
}
