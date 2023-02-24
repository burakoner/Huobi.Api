namespace Huobi.Api.Models.RestApi.Users;

/// <summary>
/// Huobi user info
/// </summary>
public class HuobiUser
{
    /// <summary>
    /// The id of the user
    /// </summary>
    [JsonProperty("uid")]
    public long UserId { get; set; }

    /// <summary>
    /// The state of the user
    /// </summary>
    [JsonProperty("userState"), JsonConverter(typeof(UserStateConverter))]
    public UserState Status { get; set; }
}
