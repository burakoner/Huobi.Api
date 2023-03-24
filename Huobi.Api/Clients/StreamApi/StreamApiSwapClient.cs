namespace Huobi.Api.Clients.StreamApi;

public class StreamApiSwapClient : StreamApiBaseClient
{
    // Private
    private readonly string _baseAddressMarketData;
    private readonly string _baseAddressNotification;
    private readonly string _baseAddressIndex;
    private readonly string _baseAddressSystemStatus;

    internal StreamApiSwapClient(HuobiStreamClient root) : base(root)
    {
        _baseAddressMarketData = ClientOptions.SwapStreamOptions.MarketDataAddress;
        _baseAddressNotification = ClientOptions.SwapStreamOptions.OrderPushAddress;
        _baseAddressIndex = ClientOptions.SwapStreamOptions.KlineBasisDataAddress;
        _baseAddressSystemStatus = ClientOptions.SwapStreamOptions.SystemStatusAddress;
    }

    protected override Task<CallResult<bool>> AuthenticateAsync(StreamConnection connection)
        => FuturesAuthenticateAsync(connection);






}