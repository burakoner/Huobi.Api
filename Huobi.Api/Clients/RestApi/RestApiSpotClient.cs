using Huobi.Api.Models.RestApi;
using Huobi.Api.Models.RestApi.Spot;

namespace Huobi.Api.Clients.RestApi;

public class RestApiSpotClient : RestApiBaseClient
{
    // Reference Endpoints
    private const string settingsCommonSymbolsEndpoint = "settings/common/symbols";
    private const string settingsCommonCurrenciesEndpoint = "settings/common/currencies";
    private const string settingsCommonCurrencysEndpoint = "settings/common/currencys";
    private const string settingsCommonMarketSymbolsEndpoint = "settings/common/market-symbols";
    private const string settingsCommonChainsEndpoint = "settings/common/chains";
    private const string referenceCurrenciesEndpoint = "reference/currencies";
    private const string commonTimestampEndpoint = "common/timestamp";
    private const string marketStatusEndpoint = "market-status";
    private const string statusEndpoint = "https://status.huobigroup.com/api/v2/summary.json";

    // Market Data Endpoints
    private const string marketHistoryKlineEndpoint = "market/history/kline";
    private const string marketDetailMergedEndpoint = "market/detail/merged";
    private const string marketTickersEndpoint = "market/tickers";
    private const string marketDepthEndpoint = "market/depth";
    private const string marketTradeEndpoint = "market/trade";
    private const string marketHistoryTradeEndpoint = "market/history/trade";
    private const string marketDetailEndpoint = "market/detail";

    // Trading Endpoints
    private const string orderOrdersPlaceEndpoint = "order/orders/place";
    private const string orderBatchOrdersEndpoint = "order/batch-orders";
    private const string orderOrdersOrderIdSubmitCancelEndpoint = "order/orders/{}/submitcancel";
    private const string orderOrdersSubmitCancelClientOrderEndpoint = "order/orders/submitCancelClientOrder";
    private const string openOrdersEndpoint = "order/openOrders";
    private const string orderOrdersBatchCancelOpenOrdersEndpoint = "order/orders/batchCancelOpenOrders";
    private const string orderOrdersBatchCancelEndpoint = "order/orders/batchcancel";
    private const string algoOrdersCancelAllAfterEndpoint = "algo-orders/cancel-all-after";
    private const string orderOrdersEndpoint = "order/orders";
    private const string orderOrderOrderIdEndpoint = "order/orders/{}";
    private const string orderOrdersGetClientOrderEndpoint = "order/orders/getClientOrder";
    private const string orderOrdersOrderIdMatchResultsEndpoint = "order/orders/{}/matchresults";
    private const string orderHistoryEndpoint = "order/history";
    private const string orderMatchResultsEndpoint = "order/matchresults";
    private const string referenceTransactFeeRateEndpoint = "reference/transact-fee-rate";

    // Conditional Order Endpoints
    private const string algoOrdersEndpoint = "algo-orders";
    private const string algoOrdersCancellationEndpoint = "algo-orders/cancellation";
    private const string algoOrdersOpeningEndpoint = "algo-orders/opening";
    private const string algoOrdersHistoryEndpoint = "algo-orders/history";
    private const string algoOrdersSpecificEndpoint = "algo-orders/specific";

    internal RestApiSpotClient(HuobiRestApiClient root) : base("Huobi Spot RestApi", root)
    {
    }

    private Uri GetUrl(string version, string endpoint)
    {
        var address = ClientOptions.SpotRestApiBaseAddress;
        if (!string.IsNullOrWhiteSpace(version)) address = address.AppendPath($"v{version}");
        if (!string.IsNullOrWhiteSpace(endpoint)) address = address.AppendPath(endpoint);

        return new Uri(address);
    }

    #region Linked Methods
    public async Task<RestCallResult<long>> GetUserIdAsync(CancellationToken ct = default)
        => await RootClient.Users.GetUserIdAsync(ct).ConfigureAwait(false);

    public async Task<RestCallResult<IEnumerable<Models.RestApi.Wallet.HuobiAccount>>> GetAccountsAsync(CancellationToken ct = default)
        => await RootClient.Wallet.GetAccountsAsync(ct).ConfigureAwait(false);
    #endregion

    public async Task<RestCallResult<DateTime>> GetServerTimeAsync(CancellationToken ct = default)
    {
        var result = await SendHuobiRequest<string>(GetUrl(v1, commonTimestampEndpoint), HttpMethod.Get, ct, ignoreRatelimit: true).ConfigureAwait(false);
        if (!result) return result.AsError<DateTime>(result.Error!);
        return result.As(JsonConvert.DeserializeObject<DateTime>(result.Data, new DateTimeConverter()));
    }

    #region Reference Data
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

    public async Task<RestCallResult<HuobiSystemSummary>> GetSystemSummaryAsync(CancellationToken ct = default)
    {
        return await SendGenericRequest<HuobiSystemSummary>(new Uri(statusEndpoint), HttpMethod.Get, ct, ignoreRatelimit: true).ConfigureAwait(false);
    }

    public async Task<RestCallResult<HuobiSystemStatus>> GetSystemStatusAsync(CancellationToken ct = default)
    {
        return await SendHuobiRequest<HuobiSystemStatus>(GetUrl(v2, marketStatusEndpoint), HttpMethod.Get, ct).ConfigureAwait(false);
    }

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
    
    public async Task<RestCallResult<IEnumerable<HuobiBlockchainInfo>>> GetBlockchainsAsync(IEnumerable<string> symbols = null, DateTime? time = null, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("symbols", symbols.Join(","));
        parameters.AddOptionalParameter("ts", time?.ConvertToMilliseconds());

        return await SendHuobiRequest<IEnumerable<HuobiBlockchainInfo>>(GetUrl(v1, settingsCommonChainsEndpoint), HttpMethod.Get, ct, queryParameters: parameters).ConfigureAwait(false);
    }
    #endregion

    #region Market Data
    public async Task<RestCallResult<IEnumerable<HuobiKline>>> GetKlinesAsync(string symbol, KlineInterval period, int? limit = null, CancellationToken ct = default)
    {
        symbol = symbol.ValidateSpotSymbol();
        limit?.ValidateIntBetween(nameof(limit), 0, 2000);

        var parameters = new Dictionary<string, object>
            {
                { "symbol", symbol },
                { "period", JsonConvert.SerializeObject(period, new KlineIntervalConverter(false)) },
            };
        parameters.AddOptionalParameter("size", limit);

        return await SendHuobiRequest<IEnumerable<HuobiKline>>(GetUrl(v0, marketHistoryKlineEndpoint), HttpMethod.Get, ct, queryParameters: parameters).ConfigureAwait(false);
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

    public async Task<RestCallResult<HuobiOrderBook>> GetOrderBookAsync(string symbol, int depth = 20, int merge = 0 ,CancellationToken ct = default)
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

        return await SendHuobiRequest<HuobiOrderBook>(GetUrl(v0, marketDepthEndpoint), HttpMethod.Get, ct, queryParameters: parameters).ConfigureAwait(false);
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
    #endregion

    #region Trading Methods
    public async Task<RestCallResult<long>> PlaceOrderAsync(long accountId, string symbol, SpotOrderSide side, SpotOrderType type, decimal quantity, decimal? price = null, /*string clientOrderId = null,*/ decimal? stopPrice = null, StopPriceOperator? stopPriceOperator = null, OrderSource source = OrderSource.Spot, bool preventSelfMatch = false, CancellationToken ct = default)
    {
        symbol = symbol.ValidateSpotSymbol();
        var brokerClientOrderId = ClientOrderId();

        if (type == SpotOrderType.StopLimit)
            throw new ArgumentException("Stop limit orders not supported by API");

        var orderType =
            JsonConvert.SerializeObject(side, new SpotOrderSideConverter(false)) + "-" +
            JsonConvert.SerializeObject(type, new OrderTypeConverter(false));

        var parameters = new Dictionary<string, object>
        {
            { "account-id", accountId },
            { "symbol", symbol },
            { "type", orderType },
            { "amount", quantity },
            { "source", JsonConvert.SerializeObject(source, new OrderSourceConverter(false)) },
            { "self-match-prevent", preventSelfMatch ? 1 : 0 },
        };

        parameters.AddOptionalParameter("price", price);
        parameters.AddOptionalParameter("client-order-id", brokerClientOrderId);
        parameters.AddOptionalParameter("stop-price", stopPrice);
        parameters.AddOptionalParameter("operator", stopPriceOperator == null ? null : JsonConvert.SerializeObject(stopPriceOperator, new StopPriceOperatorConverter(false)));

        // If precision of the symbol = 1 (eg has to use whole amounts, 1,2,3 etc) Huobi doesn't except the .0 postfix (1.0) for amount
        // Issue at the Huobi side
        if (quantity % 1 == 0)
            parameters["amount"] = quantity.ToString(CI);

        return await SendHuobiRequest<long>(GetUrl(v1, orderOrdersPlaceEndpoint), HttpMethod.Post, ct, true, bodyParameters: parameters).ConfigureAwait(false);
    }

    public async Task<RestCallResult<long>> PlaceOrderAsync(HuobiOrderRequest request, CancellationToken ct = default)
    {
        var brokerRequest = new HuobiOrderRequestString(request);
        brokerRequest.Symbol = brokerRequest.Symbol.ValidateSpotSymbol();
        brokerRequest.ClientOrderId = ClientOrderId();

        var parameters = new Dictionary<string, object>
        {
            { ClientOptions.RequestBodyParameterKey, brokerRequest },
        };

        return await SendHuobiRequest<long>(GetUrl(v1, orderOrdersPlaceEndpoint), HttpMethod.Post, ct, true, bodyParameters: parameters).ConfigureAwait(false);
    }

    public async Task<RestCallResult<IEnumerable<HuobiOrderResponse>>> PlaceBatchOrdersAsync(IEnumerable<HuobiOrderRequest> requests, CancellationToken ct = default)
    {
        var brokerRequests = HuobiOrderRequestString.ImportList(requests);
        foreach (var brokerRequest in brokerRequests)
        {
            brokerRequest.Symbol = brokerRequest.Symbol.ValidateSpotSymbol();
            brokerRequest.ClientOrderId = ClientOrderId();
        }

        var parameters = new Dictionary<string, object>
        {
            { ClientOptions.RequestBodyParameterKey, brokerRequests },
        };

        return await SendHuobiRequest<IEnumerable<HuobiOrderResponse>>(GetUrl(v1, orderBatchOrdersEndpoint), HttpMethod.Post, ct, true, bodyParameters: parameters).ConfigureAwait(false);
    }

    public async Task<RestCallResult<long>> CancelOrderAsync(long orderId, CancellationToken ct = default)
    {
        var endpoint = orderOrdersOrderIdSubmitCancelEndpoint.FillPathParameters(orderId.ToString(CI));
        return await SendHuobiRequest<long>(GetUrl(v1, endpoint), HttpMethod.Post, ct, signed: true).ConfigureAwait(false);
    }

    public async Task<RestCallResult<long>> CancelOrderAsync(string clientOrderId, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "client-order-id", clientOrderId }
        };

        return await SendHuobiRequest<long>(GetUrl(v1, orderOrdersSubmitCancelClientOrderEndpoint), HttpMethod.Post, ct, signed: true, bodyParameters: parameters).ConfigureAwait(false);
    }

    public async Task<RestCallResult<IEnumerable<HuobiOpenOrder>>> GetOpenOrdersAsync(long? accountId = null, string symbol = null, SpotOrderSide? side = null, long? fromId = null, int? limit = null, FilterDirection? direction = null, CancellationToken ct = default)
    {
        symbol = symbol?.ValidateSpotSymbol();

        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("account-id", accountId);
        parameters.AddOptionalParameter("symbol", symbol);
        parameters.AddOptionalParameter("side", side == null ? null : JsonConvert.SerializeObject(side, new SpotOrderSideConverter(false)));
        parameters.AddOptionalParameter("from", fromId);
        parameters.AddOptionalParameter("size", limit);
        parameters.AddOptionalParameter("direct", direction == null ? null : JsonConvert.SerializeObject(direction, new FilterDirectionConverter(false)));

        return await SendHuobiRequest<IEnumerable<HuobiOpenOrder>>(GetUrl(v1, openOrdersEndpoint), HttpMethod.Get, ct, true, queryParameters: parameters, requestWeight: 2).ConfigureAwait(false);
    }

    public async Task<RestCallResult<HuobiBatchActionResult>> CancelOrdersAsync(long? accountId = null, IEnumerable<string> symbols = null, SpotOrderSide? side = null, int? limit = null, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("account-id", accountId?.ToString(CI));
        parameters.AddOptionalParameter("symbol", symbols == null ? null : string.Join(",", symbols));
        parameters.AddOptionalParameter("side", side == null ? null : JsonConvert.SerializeObject(side, new SpotOrderSideConverter(false)));
        parameters.AddOptionalParameter("size", limit);

        return await SendHuobiRequest<HuobiBatchActionResult>(GetUrl(v1, orderOrdersBatchCancelOpenOrdersEndpoint), HttpMethod.Post, ct, signed: true, bodyParameters: parameters).ConfigureAwait(false);
    }

    public async Task<RestCallResult<HuobiBatchCancelResult>> CancelOrdersAsync(IEnumerable<long> orderIds = null, IEnumerable<string> clientOrderIds = null, CancellationToken ct = default)
    {
        if (orderIds == null && clientOrderIds == null)
            throw new ArgumentException("Either orderIds or clientOrderIds should be provided");

        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("order-ids", orderIds?.Select(s => s.ToString(CI)));
        parameters.AddOptionalParameter("client-order-ids", clientOrderIds?.Select(s => s.ToString(CI)));

        return await SendHuobiRequest<HuobiBatchCancelResult>(GetUrl(v1, orderOrdersBatchCancelEndpoint), HttpMethod.Post, ct, true, bodyParameters: parameters, requestWeight: 2).ConfigureAwait(false);
    }

    public async Task<RestCallResult<HuobiDeadManSwitch>> CancelAllOrdersAsync(int timeout, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "timeout", timeout }
        };

        return await SendHuobiRequest<HuobiDeadManSwitch>(GetUrl(v2, algoOrdersCancelAllAfterEndpoint), HttpMethod.Post, ct, true, bodyParameters: parameters).ConfigureAwait(false);
    }

    public async Task<RestCallResult<HuobiOrder>> GetOrderAsync(long orderId, CancellationToken ct = default)
    {
        var endpoint = orderOrderOrderIdEndpoint.FillPathParameters(orderId.ToString(CI));
        return await SendHuobiRequest<HuobiOrder>(GetUrl(v1, endpoint), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
    }

    public async Task<RestCallResult<HuobiOrder>> GetOrderAsync(string clientOrderId, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "clientOrderId", clientOrderId }
        };

        return await SendHuobiRequest<HuobiOrder>(GetUrl(v1, orderOrdersGetClientOrderEndpoint), HttpMethod.Get, ct, true, queryParameters: parameters).ConfigureAwait(false);
    }

    public async Task<RestCallResult<IEnumerable<HuobiOrderTrade>>> GetOrderTradesAsync(long orderId, CancellationToken ct = default)
    {
        var endpoint = orderOrdersOrderIdMatchResultsEndpoint.FillPathParameters(orderId.ToString(CI));
        return await SendHuobiRequest<IEnumerable<HuobiOrderTrade>>(GetUrl(v1, endpoint), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
    }

    public async Task<RestCallResult<IEnumerable<HuobiOrder>>> GetOrdersHistoryAsync(string symbol, IEnumerable<OrderStatus> states = null, IEnumerable<SpotOrderType> types = null, DateTime? startTime = null, DateTime? endTime = null, long? fromId = null, FilterDirection? direction = null, int? limit = null, CancellationToken ct = default)
    {
        symbol = symbol.ValidateSpotSymbol();
        states ??= new OrderStatus[] { OrderStatus.Filled, OrderStatus.Canceled, OrderStatus.PartiallyCanceled };

        var stateConverter = new OrderStatusConverter(false);
        var typeConverter = new OrderTypeConverter(false);
        var parameters = new Dictionary<string, object>
        {
            { "states", string.Join(",", states.Select(s => JsonConvert.SerializeObject(s, stateConverter))) }
        };
        parameters.AddOptionalParameter("symbol", symbol);
        parameters.AddOptionalParameter("start-date", startTime.ConvertToMilliseconds());
        parameters.AddOptionalParameter("end-date", endTime.ConvertToMilliseconds());
        parameters.AddOptionalParameter("types", types == null ? null : string.Join(",", types.Select(s => JsonConvert.SerializeObject(s, typeConverter))));
        parameters.AddOptionalParameter("from", fromId);
        parameters.AddOptionalParameter("direct", direction == null ? null : JsonConvert.SerializeObject(direction, new FilterDirectionConverter(false)));
        parameters.AddOptionalParameter("size", limit);

        return await SendHuobiRequest<IEnumerable<HuobiOrder>>(GetUrl(v1, orderOrdersEndpoint), HttpMethod.Get, ct, true, queryParameters: parameters).ConfigureAwait(false);
    }

    public async Task<RestCallResult<IEnumerable<HuobiOrder>>> GetHistoricalOrdersAsync(string symbol = null, DateTime? startTime = null, DateTime? endTime = null, FilterDirection? direction = null, int? limit = null, CancellationToken ct = default)
    {
        symbol = symbol?.ValidateSpotSymbol();
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("symbol", symbol);
        parameters.AddOptionalParameter("start-time", startTime.ConvertToMilliseconds());
        parameters.AddOptionalParameter("end-time", endTime.ConvertToMilliseconds());
        parameters.AddOptionalParameter("direct", direction == null ? null : JsonConvert.SerializeObject(direction, new FilterDirectionConverter(false)));
        parameters.AddOptionalParameter("size", limit);

        return await SendHuobiRequest<IEnumerable<HuobiOrder>>(GetUrl(v1, orderHistoryEndpoint), HttpMethod.Get, ct, true, parameters).ConfigureAwait(false);
    }

    public async Task<RestCallResult<IEnumerable<HuobiOrderTrade>>> GetOrderMatchesAsync(IEnumerable<OrderStatus> states = null, string symbol = null, IEnumerable<SpotOrderType> types = null, DateTime? startTime = null, DateTime? endTime = null, long? fromId = null, FilterDirection? direction = null, int? limit = null, CancellationToken ct = default)
    {
        symbol = symbol?.ValidateSpotSymbol();
        var stateConverter = new OrderStatusConverter(false);
        var typeConverter = new OrderTypeConverter(false);
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("states", states == null ? null : string.Join(",", states.Select(s => JsonConvert.SerializeObject(s, stateConverter))));
        parameters.AddOptionalParameter("symbol", symbol);
        parameters.AddOptionalParameter("start-time", startTime.ConvertToMilliseconds());
        parameters.AddOptionalParameter("end-time", endTime.ConvertToMilliseconds());
        parameters.AddOptionalParameter("types", types == null ? null : string.Join(",", types.Select(s => JsonConvert.SerializeObject(s, typeConverter))));
        parameters.AddOptionalParameter("from", fromId);
        parameters.AddOptionalParameter("direct", direction == null ? null : JsonConvert.SerializeObject(direction, new FilterDirectionConverter(false)));
        parameters.AddOptionalParameter("size", limit);

        return await SendHuobiRequest<IEnumerable<HuobiOrderTrade>>(GetUrl(v1, orderMatchResultsEndpoint), HttpMethod.Get, ct, true, parameters).ConfigureAwait(false);
    }

    public async Task<RestCallResult<IEnumerable<HuobiFeeRate>>> GetCurrentFeeRatesAsync(IEnumerable<string> symbols, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("symbols", string.Join(",", symbols));

        return await SendHuobiRequest<IEnumerable<HuobiFeeRate>>(GetUrl(v2, referenceTransactFeeRateEndpoint), HttpMethod.Get, ct, true, parameters).ConfigureAwait(false);
    }
    #endregion

    #region Conditional Order Methods
    public async Task<RestCallResult<HuobiConditionalOrderResult>> PlaceConditionalOrderAsync(long accountId, string symbol, SpotOrderSide side, ConditionalOrderType type, decimal stopPrice, decimal? quantity = null, decimal? price = null, decimal? quoteQuantity = null, decimal? trailingRate = null, TimeInForce? timeInForce = null, /*string clientOrderId = null,*/ CancellationToken ct = default)
    {
        symbol = symbol.ValidateSpotSymbol();
        var brokerClientOrderId = ClientOrderId();

        var parameters = new Dictionary<string, object>
            {
                { "accountId", accountId },
                { "symbol", symbol },
                { "orderSide", EnumConverter.GetString(side) },
                { "orderType", EnumConverter.GetString(type) },
                { "clientOrderId", brokerClientOrderId },
                { "stopPrice", stopPrice.ToString(CI) },
            };

        parameters.AddOptionalParameter("orderPrice", price?.ToString(CI));
        parameters.AddOptionalParameter("orderSize", quantity?.ToString(CI));
        parameters.AddOptionalParameter("orderValue", quoteQuantity?.ToString(CI));
        parameters.AddOptionalParameter("timeInForce", EnumConverter.GetString(timeInForce));
        parameters.AddOptionalParameter("trailingRate", trailingRate?.ToString(CI));

        return await SendHuobiRequest<HuobiConditionalOrderResult>(GetUrl(v2, algoOrdersEndpoint), HttpMethod.Post, ct, signed: true, bodyParameters: parameters).ConfigureAwait(false);
    }

    public async Task<RestCallResult<HuobiConditionalOrderCancelResult>> CancelConditionalOrdersAsync(IEnumerable<string> clientOrderIds, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "clientOrderIds", clientOrderIds }
        };

        return await SendHuobiRequest<HuobiConditionalOrderCancelResult>(GetUrl(v2, algoOrdersCancellationEndpoint), HttpMethod.Post, ct, true, bodyParameters: parameters).ConfigureAwait(false);
    }

    public async Task<RestCallResult<IEnumerable<HuobiConditionalOrder>>> GetOpenConditionalOrdersAsync(long? accountId = null, string symbol = null, SpotOrderSide? side = null, ConditionalOrderType? type = null, string sort = null, int? limit = null, long? fromId = null, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("accountId", accountId);
        parameters.AddOptionalParameter("symbol", symbol);
        parameters.AddOptionalParameter("orderSide", EnumConverter.GetString(side));
        parameters.AddOptionalParameter("orderType", EnumConverter.GetString(type));
        parameters.AddOptionalParameter("sort", sort);
        parameters.AddOptionalParameter("limit", limit);
        parameters.AddOptionalParameter("fromId", fromId);

        return await SendHuobiRequest<IEnumerable<HuobiConditionalOrder>>(GetUrl(v2, algoOrdersOpeningEndpoint), HttpMethod.Get, ct, true, queryParameters: parameters).ConfigureAwait(false);
    }

    public async Task<RestCallResult<IEnumerable<HuobiConditionalOrder>>> GetClosedConditionalOrdersAsync(string symbol, ConditionalOrderStatus status, long? accountId = null, SpotOrderSide? side = null, ConditionalOrderType? type = null, DateTime? startTime = null, DateTime? endTime = null, string sort = null, int? limit = null, long? fromId = null, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>()
        {
            { "symbol", symbol },
            { "orderStatus", EnumConverter.GetString(status) }
        };
        parameters.AddOptionalParameter("accountId", accountId);
        parameters.AddOptionalParameter("orderSide", EnumConverter.GetString(side));
        parameters.AddOptionalParameter("orderType", EnumConverter.GetString(type));
        parameters.AddOptionalParameter("startTime", startTime.ConvertToMilliseconds());
        parameters.AddOptionalParameter("endTime", endTime.ConvertToMilliseconds());
        parameters.AddOptionalParameter("sort", sort);
        parameters.AddOptionalParameter("limit", limit);
        parameters.AddOptionalParameter("fromId", fromId);

        return await SendHuobiRequest<IEnumerable<HuobiConditionalOrder>>(GetUrl(v2, algoOrdersHistoryEndpoint), HttpMethod.Get, ct, true, queryParameters: parameters).ConfigureAwait(false);
    }

    public async Task<RestCallResult<HuobiConditionalOrder>> GetConditionalOrderAsync(string clientOrderId, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>()
        {
            { "clientOrderId", clientOrderId }
        };
        return await SendHuobiRequest<HuobiConditionalOrder>(GetUrl(v2, algoOrdersSpecificEndpoint), HttpMethod.Get, ct, true, queryParameters: parameters).ConfigureAwait(false);
    }
    #endregion

}