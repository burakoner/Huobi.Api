using Huobi.Api.Models.RestApi.Spot.Public;

namespace Huobi.Api.Clients.RestApi;

public class RestApiSystemClient : RestApiBaseClient
{
    // Endpoints
    private const string marketStatusEndpoint = "market-status";

    internal RestApiSystemClient(HuobiRestApiClient root) : base("Huobi System RestApi", root)
    {
    }

    public async Task<RestCallResult<HuobiSystemSummary>> GetSystemSummaryAsync(CancellationToken ct = default)
    {
        return await SendGenericRequest<HuobiSystemSummary>(new Uri("https://status.huobigroup.com/api/v2/summary.json"), HttpMethod.Get, ct, ignoreRatelimit: true).ConfigureAwait(false);
    }

    public async Task<RestCallResult<HuobiSystemStatus>> GetSystemStatusAsync(CancellationToken ct = default)
    {
        return await SendHuobiRequest<HuobiSystemStatus>(GetUrl(v2, marketStatusEndpoint), HttpMethod.Get, ct).ConfigureAwait(false);
    }

}