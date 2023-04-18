namespace Huobi.Api;

public class HuobiStreamClientOptions : StreamApiClientOptions
{
    public HuobiFuturesStreamClientOptions FuturesStreamOptions { get; set; }
    public HuobiSpotStreamClientOptions SpotStreamOptions { get; set; }
    public HuobiSwapStreamClientOptions SwapStreamOptions { get; set; }

    public HuobiStreamClientOptions()
    {
        this.FuturesStreamOptions = new HuobiFuturesStreamClientOptions();
        this.SpotStreamOptions = new HuobiSpotStreamClientOptions();
        this.SwapStreamOptions = new HuobiSwapStreamClientOptions();
    }
}

public class HuobiSpotStreamClientOptions
{
    // Stream Api Addresses
    public string PublicAddress { get; set; }
    public string PrivateAddress { get; set; }
    public string MBPFeedAddress { get; set; }

    public HuobiSpotStreamClientOptions()
    {
        // Stream Api Addresses
        this.PublicAddress = HuobiApiAddresses.Default.StreamSpotPublicAddress;
        this.PrivateAddress = HuobiApiAddresses.Default.StreamSpotPrivateAddress;
        this.MBPFeedAddress = HuobiApiAddresses.Default.StreamSpotMBPFeedAddress;
    }
}

public class HuobiFuturesStreamClientOptions
{
    // Stream Api Addresses
    public string MarketDataAddress { get; set; }
    public string OrderPushAddress { get; set; }
    public string KlineBasisDataAddress { get; set; }
    public string SystemStatusAddress { get; set; }

    public HuobiFuturesStreamClientOptions()
    {
        // Stream Api Addresses
        this.MarketDataAddress = HuobiApiAddresses.Default.StreamFuturesMarketDataAddress;
        this.OrderPushAddress = HuobiApiAddresses.Default.StreamFuturesOrderPushAddress;
        this.KlineBasisDataAddress = HuobiApiAddresses.Default.StreamFuturesKlineBasisDataAddress;
        this.SystemStatusAddress = HuobiApiAddresses.Default.StreamFuturesSystemStatusAddress;
    }
}

public class HuobiSwapStreamClientOptions
{
    // Stream Api Addresses
    public string MarketDataAddress { get; set; }
    public string OrderPushAddress { get; set; }
    public string KlineBasisDataAddress { get; set; }
    public string SystemStatusAddress { get; set; }

    public HuobiSwapStreamClientOptions()
    {
        // Stream Api Addresses
        this.MarketDataAddress = HuobiApiAddresses.Default.StreamSwapMarketDataAddress;
        this.OrderPushAddress = HuobiApiAddresses.Default.StreamSwapOrderPushAddress;
        this.KlineBasisDataAddress = HuobiApiAddresses.Default.StreamSwapKlineBasisDataAddress;
        this.SystemStatusAddress = HuobiApiAddresses.Default.StreamSwapSystemStatusAddress;
    }
}