namespace Huobi.Api.Models.RestApi.Users;

/// <summary>
/// Huobi sub-user account id and state
/// </summary>
public class HuobiSubUserAccountId
{
    /// <summary>
    /// The id of the account
    /// </summary>
    [JsonProperty("accountId")]
    public long Id { get; set; }

    /// <summary>
    /// The state of the account
    /// </summary>
    [JsonProperty("accountStatus"), JsonConverter(typeof(AccountStateConverter))]
    public AccountState State { get; set; }

    /// <summary>
    /// Account sub type (only valid for accountType=isolated-margin)
    /// </summary>
    public string SubType { get; set; }
}
