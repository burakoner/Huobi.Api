namespace Huobi.Api.Models.RestApi.Users;

public class HuobiSubUser
{
    [JsonProperty("uid")]
    public long SubUserId { get; set; }

    [JsonProperty("subUid")]
    private long SubUserIdAlias { set => SubUserId = value; get => SubUserId; }

    [JsonProperty("userState"), JsonConverter(typeof(SubUserStatusConverter))]
    public SubUserStatus UserState { get; set; }
}
