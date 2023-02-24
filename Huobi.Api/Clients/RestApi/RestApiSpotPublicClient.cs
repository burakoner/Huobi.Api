using Huobi.Api.Models.RestApi.Reference;
using Huobi.Api.Models.RestApi.Spot;

namespace Huobi.Api.Clients.RestApi;

public class RestApiSpotPublicClient : RestApiBaseClient
{
    // Reference Endpoints
    private const string settingsCommonSymbolsEndpoint = "settings/common/symbols";
    private const string settingsCommonCurrenciesEndpoint = "settings/common/currencies";
    private const string settingsCommonCurrencysEndpoint = "settings/common/currencys";
    private const string settingsCommonMarketSymbolsEndpoint = "settings/common/market-symbols";
    private const string settingsCommonChainsEndpoint = "settings/common/chains";
    private const string referenceCurrenciesEndpoint = "reference/currencies";
    private const string commonTimestampEndpoint = "common/timestamp";

    // Market Data Endpoints
    private const string marketHistoryKlineEndpoint = "market/history/kline";
    private const string marketDetailMergedEndpoint = "market/detail/merged";
    private const string marketTickersEndpoint = "market/tickers";
    private const string marketDepthEndpoint = "market/depth";
    private const string marketTradeEndpoint = "market/trade";
    private const string marketHistoryTradeEndpoint = "market/history/trade";
    private const string marketDetailEndpoint = "market/detail";

    internal RestApiSpotPublicClient(HuobiRestApiClient root) : base("Huobi Spot Public RestApi", root)
    {
    }

    public async Task<RestCallResult<DateTime>> GetServerTimeAsync(CancellationToken ct = default)
    {
        var result = await SendHuobiRequest<string>(GetUrl(v1, commonTimestampEndpoint), HttpMethod.Get, ct, ignoreRatelimit: true).ConfigureAwait(false);
        if (!result) return result.AsError<DateTime>(result.Error!);
        return result.As(JsonConvert.DeserializeObject<DateTime>(result.Data, new DateTimeConverter()));
    }

    #region Assets
    public async Task<RestCallResult<IEnumerable<HuobiAsset>>> GetAssetsAsync(CancellationToken ct = default)
    {
        return await SendHuobiRequest<IEnumerable<HuobiAsset>>(GetUrl(v2, settingsCommonCurrenciesEndpoint), HttpMethod.Get, ct).ConfigureAwait(false);
    }

    public async Task<RestCallResult<IEnumerable<HuobiAssetSettings>>> GetAssetsSettingsAsync(DateTime? time = null, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("ts", time?.ConvertToMilliseconds());

        return await SendHuobiRequest<IEnumerable<HuobiAssetSettings>>(GetUrl(v1, settingsCommonCurrencysEndpoint), HttpMethod.Get, ct, queryParameters: parameters).ConfigureAwait(false);
    }

    public async Task<RestCallResult<IEnumerable<HuobiAssetChain>>> GetAssetsChainsAsync(IEnumerable<string> currencies = null, bool authorizedUser = true, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("currency", currencies.Join(","));
        parameters.AddOptionalParameter("authorizedUser", authorizedUser);

        return await SendHuobiRequest<IEnumerable<HuobiAssetChain>>(GetUrl(v2, referenceCurrenciesEndpoint), HttpMethod.Get, ct, queryParameters: parameters).ConfigureAwait(false);
    }
    #endregion

    #region Symbols
    public async Task<RestCallResult<IEnumerable<HuobiSymbol>>> GetSymbolsAsync(CancellationToken ct = default)
    {
        return await SendHuobiRequest<IEnumerable<HuobiSymbol>>(GetUrl(v2, settingsCommonSymbolsEndpoint), HttpMethod.Get, ct).ConfigureAwait(false);
    }

    public async Task<RestCallResult<IEnumerable<HuobiSymbolSettings>>> GetSymbolsSettingsAsync(DateTime? time = null, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("ts", time?.ConvertToMilliseconds());

        return await SendHuobiRequest<IEnumerable<HuobiSymbolSettings>>(GetUrl(v1, settingsCommonSymbolsEndpoint), HttpMethod.Get, ct, queryParameters: parameters).ConfigureAwait(false);
    }

    public async Task<RestCallResult<IEnumerable<HuobiSymbolDefinition>>> GetSymbolsDefinitionsAsync(IEnumerable<string> symbols = null, DateTime? time = null, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("symbols", symbols.Join(","));
        parameters.AddOptionalParameter("ts", time?.ConvertToMilliseconds());

        return await SendHuobiRequest<IEnumerable<HuobiSymbolDefinition>>(GetUrl(v1, settingsCommonMarketSymbolsEndpoint), HttpMethod.Get, ct, queryParameters: parameters).ConfigureAwait(false);
    }
    #endregion

    #region Blockchains
    public async Task<RestCallResult<IEnumerable<HuobiBlockchainInfo>>> GetBlockchainsAsync(IEnumerable<string> symbols = null, DateTime? time = null, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("symbols", symbols.Join(","));
        parameters.AddOptionalParameter("ts", time?.ConvertToMilliseconds());

        return await SendHuobiRequest<IEnumerable<HuobiBlockchainInfo>>(GetUrl(v1, settingsCommonChainsEndpoint), HttpMethod.Get, ct, queryParameters: parameters).ConfigureAwait(false);
    }
    #endregion

    public async Task<RestCallResult<IEnumerable<HuobiSpotKline>>> GetKlinesAsync(string symbol, KlineInterval period, int? limit = null, CancellationToken ct = default)
    {
        symbol = symbol.ValidateSpotSymbol();
        limit?.ValidateIntBetween(nameof(limit), 0, 2000);

        var parameters = new Dictionary<string, object>
            {
                { "symbol", symbol },
                { "period", JsonConvert.SerializeObject(period, new KlineIntervalConverter(false)) },
            };
        parameters.AddOptionalParameter("size", limit);

        return await SendHuobiRequest<IEnumerable<HuobiSpotKline>>(GetUrl(v0, marketHistoryKlineEndpoint), HttpMethod.Get, ct, queryParameters: parameters).ConfigureAwait(false);
    }

    public async Task<RestCallResult<HuobiSpotAggregatedTicker>> GetTickerAsync(string symbol, CancellationToken ct = default)
    {
        symbol = symbol.ValidateSpotSymbol();
        var parameters = new Dictionary<string, object>
        {
            { "symbol", symbol }
        };

        return await SendHuobiRequest<HuobiSpotAggregatedTicker>(GetUrl(v0, marketDetailMergedEndpoint), HttpMethod.Get, ct, queryParameters: parameters).ConfigureAwait(false);
    }

    public async Task<RestCallResult<IEnumerable<HuobiSpotTicker>>> GetTickersAsync(CancellationToken ct = default)
    {
        return await SendHuobiRequest<IEnumerable<HuobiSpotTicker>>(GetUrl(v0, marketTickersEndpoint), HttpMethod.Get, ct).ConfigureAwait(false);
    }

    public async Task<RestCallResult<HuobiSpotOrderBook>> GetOrderBookAsync(string symbol, int depth = 20, int merge = 0 ,CancellationToken ct = default)
    {
        symbol = symbol.ValidateSpotSymbol();
        merge.ValidateIntValues(nameof(merge), 0, 1, 2, 3, 4, 5);
        depth.ValidateIntValues(nameof(depth), 5, 10, 20);

        var parameters = new Dictionary<string, object>
        {
            { "symbol", symbol },
            { "depth", depth },
            { "type", "step" + merge }
        };

        return await SendHuobiRequest<HuobiSpotOrderBook>(GetUrl(v0, marketDepthEndpoint), HttpMethod.Get, ct, queryParameters: parameters).ConfigureAwait(false);
    }

    public async Task<RestCallResult<HuobiSpotTrade>> GetLastTradeAsync(string symbol, CancellationToken ct = default)
    {
        symbol = symbol.ValidateSpotSymbol();
        var parameters = new Dictionary<string, object>
            {
                { "symbol", symbol }
            };

        var result = await SendHuobiRequest<HuobiApiResponse<IEnumerable<HuobiSpotTrade>>>(GetUrl(v0, marketTradeEndpoint), HttpMethod.Get, ct, queryParameters: parameters).ConfigureAwait(false);
        if (!result) return result.AsError<HuobiSpotTrade>(result.Error!);

        return result.As(result.Data.Data.FirstOrDefault());
    }

    public async Task<RestCallResult<IEnumerable<HuobiSpotTrade>>> GetTradesAsync(string symbol, int limit = 100, CancellationToken ct = default)
    {
        symbol = symbol.ValidateSpotSymbol();
        limit.ValidateIntBetween(nameof(limit), 1, 2000);

        var parameters = new Dictionary<string, object>
        {
            { "symbol", symbol },
        };
        parameters.AddOptionalParameter("size", limit);

        var result = await SendHuobiRequest<IEnumerable<HuobiApiResponse<IEnumerable<HuobiSpotTrade>>>>(GetUrl(v0, marketHistoryTradeEndpoint), HttpMethod.Get, ct, queryParameters: parameters).ConfigureAwait(false);
        if (!result) return result.AsError<IEnumerable<HuobiSpotTrade>>(result.Error!);

        var list = new List<HuobiSpotTrade>();
        foreach(var container in result.Data)
        {
            foreach(var item in container.Data)
            {
                list.Add(item);
            }
        }
        list = list.OrderByDescending(x=>x.TradeId).Take(limit).ToList();

        return result.As(list.AsEnumerable());
    }

    public async Task<RestCallResult<HuobiSpotMarketSummary>> GetMarketSummaryAsync(string symbol, CancellationToken ct = default)
    {
        symbol = symbol.ValidateSpotSymbol();
        var parameters = new Dictionary<string, object>
        {
            { "symbol", symbol }
        };

        return await SendHuobiRequest<HuobiSpotMarketSummary>(GetUrl(v0, marketDetailEndpoint), HttpMethod.Get, ct, queryParameters: parameters).ConfigureAwait(false);
    }

}