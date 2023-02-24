namespace Huobi.Api.Clients.RestApi;

public class RestApiSpotClient
{
    public RestApiSpotPublicClient Public { get; }
    public RestApiSpotTradingClient Trading { get; }

    internal RestApiSpotClient(HuobiRestApiClient root)
    {
        this.Public = new RestApiSpotPublicClient(root);
        this.Trading = new RestApiSpotTradingClient(root);
    }
}