namespace Huobi.Api.Models.RestApi.Spot;

/// <summary>
/// Result of Cancel Orders by Criteria
/// </summary>
public class HuobiBatchActionResult
{
    /// <summary>
    /// The number of cancel request sent successfully
    /// </summary>
    [JsonProperty("success-count")]
    public long SuccessCount { get; set; }

    /// <summary>
    /// The number of cancel request failed
    /// </summary>
    [JsonProperty("failed-count")]
    public long FailedCount { get; set; }

    /// <summary>
    /// the next order id that can be canceled
    /// </summary>
    [JsonProperty("next-id")]
    public long NextId { get; set; }
}