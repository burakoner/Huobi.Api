namespace Huobi.Api.Models.RestApi.Spot.Trading;

public class HuobiDeadManSwitch
{
    [JsonProperty("currentTime"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime CurrentTime { get; set; }

    [JsonProperty("triggerTime"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime TriggerTime { get; set; }
}