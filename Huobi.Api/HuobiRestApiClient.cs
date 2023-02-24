using Huobi.Api.Models.RestApi;

namespace Huobi.Api;

public sealed class HuobiRestApiClient
{
    // Options
    internal HuobiRestApiClientOptions ClientOptions { get; }
    internal CultureInfo CI { get; } = CultureInfo.InvariantCulture;

    // Master Clients
    public RestApiSpotClient Spot { get; }
    public RestApiUsersClient Users { get; }
    public RestApiSystemClient System { get; }
    public RestApiWalletClient Wallet { get; }

    public RestApiSystemClient Common { get; }
    public RestApiSystemClient Market { get; }
    public RestApiSystemClient Margin { get; }
    public RestApiSystemClient CrossMargin { get; }
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
    }

    internal Error ParseErrorResponse(JToken error)
    {
        if (error["err-code"] == null || error["err-msg"] == null)
            return new ServerError(error.ToString());

        return new ServerError($"{(string)error["err-code"]!}, {(string)error["err-msg"]!}");
    }
}