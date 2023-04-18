namespace Huobi.Api;

public class HuobiRestApiClient
{
    // Options
    internal HuobiRestApiClientOptions ClientOptions { get; }
    internal CultureInfo CI { get; } = CultureInfo.InvariantCulture;

    // Master Clients
    public RestApiSpotClient Spot { get; }
    public RestApiMarginClient Margin { get; }
    public RestApiUsersClient Users { get; }
    public RestApiWalletClient Wallet { get; }
    public RestApiFuturesClient Futures { get; }
    //public RestApiSwapClient Swap { get; }

    public HuobiRestApiClient() : this(new HuobiRestApiClientOptions())
    {
    }

    public HuobiRestApiClient(HuobiRestApiClientOptions options)
    {
        ClientOptions = options;

        Spot = new RestApiSpotClient(this);
        Margin = new RestApiMarginClient(this);
        Users = new RestApiUsersClient(this);
        Wallet = new RestApiWalletClient(this);
        Futures = new RestApiFuturesClient(this);
    }

}