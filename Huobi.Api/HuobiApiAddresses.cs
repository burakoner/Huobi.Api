namespace Huobi.Api;

public class HuobiApiAddresses
{
    // Rest Api
    public string RestApiAddress { get; set; }
    // public string StatusRestApiAddress { get; set; }

    // Stream Api
    public string StreamMarketAddress { get; set; }
    public string StreamMarketMBPAddress { get; set; }
    public string StreamAccountOrderAddress { get; set; }

    public static HuobiApiAddresses Default = new()
    {
        // Rest Api
        RestApiAddress = "https://api.huobi.pro",
        // StatusRestApiAddress = "https://status.huobigroup.com",

        // Stream Api
        StreamMarketAddress = "wss://api.huobi.pro/ws",
        StreamMarketMBPAddress = "wss://api.huobi.pro/feed",
        StreamAccountOrderAddress = "wss://api.huobi.pro/ws/v2",
    };

    public static HuobiApiAddresses AWS = new()
    {
        // Rest Api
        RestApiAddress = "https://api-aws.huobi.pro",
        // StatusRestApiAddress = "https://status.huobigroup.com",

        // Stream Api
        StreamMarketAddress = "wss://api-aws.huobi.pro/ws",
        StreamMarketMBPAddress = "wss://api-aws.huobi.pro/feed",
        StreamAccountOrderAddress = "wss://api-aws.huobi.pro/ws/v2",
    };

    public static HuobiApiAddresses TestNet = new()
    {
        // Rest Api
        RestApiAddress = "",
        // StatusRestApiAddress = "",

        // Stream Api
        StreamMarketAddress = "",
        StreamMarketMBPAddress = "",
        StreamAccountOrderAddress = "",
    };
}
