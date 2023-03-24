namespace Huobi.Api;

public class HuobiStreamClient
{
    // Options
    public HuobiStreamClientOptions ClientOptions { get; }

    // Master Clients
    public StreamApiFuturesClient Futures { get; }
    public StreamApiSpotClient Spot { get; }

    public HuobiStreamClient() : this(new HuobiStreamClientOptions())
    {
    }

    public HuobiStreamClient(HuobiStreamClientOptions options)
    {
        ClientOptions = options;

        Futures = new StreamApiFuturesClient(this);
        Spot = new StreamApiSpotClient(this);
    }

}
