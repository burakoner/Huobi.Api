namespace Huobi.Api.Clients.StreamApi;

public class StreamApiSpotClient
{
    // Channels
    private const string spotPingChannel = "spot.ping";

    // Internal
    internal HuobiStreamClient RootClient { get; }
    internal StreamApiBaseClient BaseClient { get; }
    internal HuobiStreamClientOptions ClientOptions { get; }
    private string BaseAddress { get => ClientOptions.BaseAddress; }

    internal StreamApiSpotClient(HuobiStreamClient root)
    {
        RootClient = root;
        BaseClient = root.Base;
        ClientOptions = root.ClientOptions;
    }

    public async Task UnsubscribeAsync(int subscriptionId)
        => await BaseClient.UnsubscribeAsync(subscriptionId).ConfigureAwait(false);

    public async Task UnsubscribeAsync(UpdateSubscription subscription)
        => await BaseClient.UnsubscribeAsync(subscription).ConfigureAwait(false);

    public async Task UnsubscribeAllAsync()
        => await BaseClient.UnsubscribeAllAsync().ConfigureAwait(false);

}