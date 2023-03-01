namespace Huobi.Api.Models.RestApi.Spot.Trading;

/// <summary>
/// Conditional order cancelation result
/// </summary>
public class HuobiConditionalOrderCancelResult
{
    /// <summary>
    /// Orders accepted for cancelation
    /// </summary>
    public IEnumerable<string> Accepted { get; set; } = Array.Empty<string>();

    /// <summary>
    /// Orders rejected for cancelation
    /// </summary>
    public IEnumerable<string> Rejected { get; set; } = Array.Empty<string>();
}
