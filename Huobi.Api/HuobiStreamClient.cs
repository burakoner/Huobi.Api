namespace Huobi.Api;

public class HuobiStreamClient
{
    // Options
    public HuobiStreamClientOptions ClientOptions { get; }

    // Master Clients
    internal StreamApiBaseClient Base { get; }
    public StreamApiSpotClient Spot { get; }

    public HuobiStreamClient() : this(new HuobiStreamClientOptions())
    {
    }

    public HuobiStreamClient(HuobiStreamClientOptions options)
    {
        ClientOptions = options;

        Base = new StreamApiBaseClient(this);
        Spot = new StreamApiSpotClient(this);
    }

}
