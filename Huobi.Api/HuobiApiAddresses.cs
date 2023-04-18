namespace Huobi.Api;

public class HuobiApiAddresses
{
    // Rest Api
    public string SpotRestApiAddress { get; set; }
    public string FuturesRestApiAddress { get; set; }
    public string SwapRestApiAddress { get; set; }

    // Spot Streams
    public string StreamSpotPublicAddress { get; set; }
    public string StreamSpotMBPFeedAddress { get; set; }
    public string StreamSpotPrivateAddress { get; set; }

    // Futures Streams
    public string StreamFuturesMarketDataAddress { get; set; }
    public string StreamFuturesOrderPushAddress { get; set; }
    public string StreamFuturesKlineBasisDataAddress { get; set; }
    public string StreamFuturesSystemStatusAddress { get; set; }

    // Swap Streams
    public string StreamSwapMarketDataAddress { get; set; }
    public string StreamSwapOrderPushAddress { get; set; }
    public string StreamSwapKlineBasisDataAddress { get; set; }
    public string StreamSwapSystemStatusAddress { get; set; }

    public static HuobiApiAddresses Default = new()
    {
        // Rest Api
        SpotRestApiAddress = "https://api.huobi.pro",
        FuturesRestApiAddress = "https://api.hbdm.com",
        SwapRestApiAddress = "https://api.hbdm.com",

        // Spot Streams
        StreamSpotPublicAddress = "wss://api.huobi.pro/ws",
        StreamSpotMBPFeedAddress = "wss://api.huobi.pro/feed",
        StreamSpotPrivateAddress = "wss://api.huobi.pro/ws/v2",

        // Futures Streams
        StreamFuturesMarketDataAddress = "wss://api.hbdm.com/ws",
        StreamFuturesOrderPushAddress = "wss://api.hbdm.com/notification",
        StreamFuturesKlineBasisDataAddress = "wss://api.hbdm.com/ws_index",
        StreamFuturesSystemStatusAddress = "wss://api.hbdm.com/center-notification",

        // Swap Streams
        StreamSwapMarketDataAddress = "wss://api.hbdm.com/swap-ws",
        StreamSwapOrderPushAddress = "wss://api.hbdm.com/swap-notification",
        StreamSwapKlineBasisDataAddress = "wss://api.hbdm.com/ws_index",
        StreamSwapSystemStatusAddress = "wss://api.hbdm.com/center-notification",
    };

    public static HuobiApiAddresses Debug = new()
    {
        // Rest Api
        SpotRestApiAddress = "https://api.huobi.pro",
        FuturesRestApiAddress = "https://api.btcgateway.pro",
        SwapRestApiAddress = "https://api.btcgateway.pro",

        // Spot Streams
        StreamSpotPublicAddress = "wss://api.huobi.pro/ws",
        StreamSpotMBPFeedAddress = "wss://api.huobi.pro/feed",
        StreamSpotPrivateAddress = "wss://api.huobi.pro/ws/v2",

        // Futures Streams
        StreamFuturesMarketDataAddress = "wss://api.btcgateway.pro/ws",
        StreamFuturesOrderPushAddress = "wss://api.btcgateway.pro/notification",
        StreamFuturesKlineBasisDataAddress = "wss://api.btcgateway.pro/ws_index",
        StreamFuturesSystemStatusAddress = "wss://api.btcgateway.pro/center-notification",

        // Swap Streams
        StreamSwapMarketDataAddress = "wss://api.btcgateway.pro/swap-ws",
        StreamSwapOrderPushAddress = "wss://api.btcgateway.pro/swap-notification",
        StreamSwapKlineBasisDataAddress = "wss://api.btcgateway.pro/ws_index",
        StreamSwapSystemStatusAddress = "wss://api.btcgateway.pro/center-notification",
    };

    public static HuobiApiAddresses AWS = new()
    {
        // Rest Api
        SpotRestApiAddress = "https://api-aws.huobi.pro",
        FuturesRestApiAddress = "https://api.hbdm.vn",
        SwapRestApiAddress = "https://api.hbdm.vn",

        // Spot Streams
        StreamSpotPublicAddress = "wss://api-aws.huobi.pro/ws",
        StreamSpotMBPFeedAddress = "wss://api-aws.huobi.pro/feed",
        StreamSpotPrivateAddress = "wss://api-aws.huobi.pro/ws/v2",

        // Futures Streams
        StreamFuturesMarketDataAddress = "wss://api.hbdm.vn/ws",
        StreamFuturesOrderPushAddress = "wss://api.hbdm.vn/notification",
        StreamFuturesKlineBasisDataAddress = "wss://api.hbdm.vn/ws_index",
        StreamFuturesSystemStatusAddress = "wss://api.hbdm.vn/center-notification",

        // Swap Streams
        StreamSwapMarketDataAddress = "wss://api.hbdm.vn/swap-ws",
        StreamSwapOrderPushAddress = "wss://api.hbdm.vn/swap-notification",
        StreamSwapKlineBasisDataAddress = "wss://api.hbdm.vn/ws_index",
        StreamSwapSystemStatusAddress = "wss://api.hbdm.vn/center-notification",
    };

    public static HuobiApiAddresses TestNet = new()
    {
        // Rest Api
        SpotRestApiAddress = "",
        FuturesRestApiAddress = "",
        SwapRestApiAddress = "",

        // Spot Streams
        StreamSpotPublicAddress = "",
        StreamSpotMBPFeedAddress = "",
        StreamSpotPrivateAddress = "",

        // Futures Streams
        StreamFuturesMarketDataAddress = "",
        StreamFuturesOrderPushAddress = "",
        StreamFuturesKlineBasisDataAddress = "",
        StreamFuturesSystemStatusAddress = "",

        // Swap Streams
        StreamSwapMarketDataAddress = "",
        StreamSwapOrderPushAddress = "",
        StreamSwapKlineBasisDataAddress = "",
        StreamSwapSystemStatusAddress = "",
    };
}
