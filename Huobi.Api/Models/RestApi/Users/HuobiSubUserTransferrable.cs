namespace Huobi.Api.Models.RestApi.Users;

public class HuobiSubUserTransferrable
{
    [JsonProperty("subUid")]
    public long SubUserId { get; set; }

    [JsonProperty("accountType")]
    public string AccountType { get; set; }

    [JsonProperty("transferrable")]
    public bool Transferrable { get; set; }

    public bool Success { get => !ErrorCode.HasValue; }

    [JsonProperty("errCode")]
    public int? ErrorCode { get; set; }

    [JsonProperty("errMessage")]
    public string ErrorMessage { get; set; }
}