namespace Huobi.Api.Models.RestApi.Wallet;

public class HuobiWithdrawalQuota
{
    [JsonProperty("currency")]
    public string Asset { get; set; }

    [JsonProperty("chains")]
    public IEnumerable<HuobiWithdrawalChainQuota> Chains { get; set; }
}

public class HuobiWithdrawalChainQuota
{
    /// <summary>
    /// Block chain name
    /// </summary>
    public string Chain { get; set; }

    /// <summary>
    /// Maximum withdraw amount in a day
    /// </summary>
    public decimal WithdrawQuotaPerDay { get; set; }

    /// <summary>
    /// Remaining withdraw quota in the day
    /// </summary>
    public decimal RemainWithdrawQuotaPerDay { get; set; }

    /// <summary>
    /// Maximum withdraw amount in a year
    /// </summary>
    public decimal WithdrawQuotaPerYear { get; set; }

    /// <summary>
    /// Remaining withdraw quota in the year
    /// </summary>
    public decimal RemainWithdrawQuotaPerYear { get; set; }

    /// <summary>
    /// Maximum withdraw amount in total
    /// </summary>
    public decimal WithdrawQuotaTotal { get; set; }

    /// <summary>
    /// Remaining withdraw quota in total
    /// </summary>
    public decimal RemainWithdrawQuotaTotal { get; set; }
}