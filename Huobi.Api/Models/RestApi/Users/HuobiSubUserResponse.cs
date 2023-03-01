namespace Huobi.Api.Models.RestApi.Users;

public class HuobiSubUserResponse
{
    [JsonProperty("uid")]
    public long UserId { get; set; }

    [JsonProperty("userName")]
    public string Username { get; set; }

    [JsonProperty("note")]
    public string Note { get; set; }
}
