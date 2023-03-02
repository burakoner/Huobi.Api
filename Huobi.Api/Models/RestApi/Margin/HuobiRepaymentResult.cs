namespace Huobi.Api.Models.RestApi.Margin;

/// <summary>
/// Repayment result
/// </summary>
public class HuobiRepaymentResult
{
    /// <summary>
    /// Repayment id
    /// </summary>
    public string RepayId { get; set; } = string.Empty;
    /// <summary>
    /// Repay time
    /// </summary>
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime RepayTime { get; set; }
}
