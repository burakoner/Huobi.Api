namespace Huobi.Api;

public class HuobiStreamClientOptions : StreamApiClientOptions
{
    // Stream Api Addresses
    public string StreamMarketAddress { get; set; }
    public string StreamMarketMBPAddress { get; set; }
    public string StreamAccountOrderAddress { get; set; }

    public HuobiStreamClientOptions()
    {
        // Base Address
        this.BaseAddress = HuobiApiAddresses.Default.StreamMarketAddress;

        // Stream Api Addresses
        this.StreamMarketAddress = HuobiApiAddresses.Default.StreamMarketAddress;
        this.StreamMarketMBPAddress = HuobiApiAddresses.Default.StreamMarketMBPAddress;
        this.StreamAccountOrderAddress = HuobiApiAddresses.Default.StreamAccountOrderAddress;
    }
}