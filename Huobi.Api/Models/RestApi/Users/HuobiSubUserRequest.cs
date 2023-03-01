namespace Huobi.Api.Models.RestApi.Users;

public class HuobiSubUserRequest
{
    [JsonProperty("userName")]
    public string Username { get; set; }

    [JsonProperty("note")]
    public string Note { get; set; }
}
