namespace Huobi.Api;

public class HuobiRestApiClient
{
    // Options
    internal HuobiRestApiClientOptions ClientOptions { get; }
    internal CultureInfo CI { get; } = CultureInfo.InvariantCulture;

    // Master Clients
    public RestApiSpotClient Spot { get; }
    public RestApiUsersClient Users { get; }
    public RestApiSystemClient System { get; }
    public RestApiWalletClient Wallet { get; }
    public RestApiAccountClient Account { get; }
    public RestApiMarginLoanClient MarginLoan { get; }

    //public RestApiSystemClient Common { get; }
    //public RestApiSystemClient Market { get; }
    //public RestApiSystemClient Margin { get; }
    //public RestApiSystemClient CrossMargin { get; }
    //public RestApiSpotClient Wallet { get; }
    //public RestApiSpotClient Futures { get; }
    //public RestApiSpotClient Swaps { get; }

    public HuobiRestApiClient() : this(new HuobiRestApiClientOptions())
    {
    }

    public HuobiRestApiClient(HuobiRestApiClientOptions options)
    {
        ClientOptions = options;

        Spot = new RestApiSpotClient(this);
        Users = new RestApiUsersClient(this);
        System = new RestApiSystemClient(this);
        Wallet = new RestApiWalletClient(this);
        Account = new RestApiAccountClient(this);
        MarginLoan = new RestApiMarginLoanClient(this);
    }

}