namespace Huobi.Api.Models.RestApi.Users;

public class HuobiSubUserActivation
{
    [JsonProperty("subUid")]
    public long SubUserId { get; set; }

    [JsonProperty("accountType"), JsonConverter(typeof(AccountMarginModeConverter))]
    public AccountMarginMode MarginMode { get; set; }

    [JsonProperty("activation"), JsonConverter(typeof(AccountActivationConverter))]
    public AccountActivation Activation { get; set; }

    public bool Success { get => !ErrorCode.HasValue; }

    [JsonProperty("errCode")]
    public int? ErrorCode { get; set; }

    [JsonProperty("errMessage")]
    public string ErrorMessage { get; set; }
}