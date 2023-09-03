using Huobi.Api.Models.RestApi.Spot;
using Huobi.Api.Models.StreamApi;
using Huobi.Api.Models.StreamApi.Spot;

namespace Huobi.Api.Clients.StreamApi;

public class StreamApiSpotClient : StreamApiBaseClient
{
    // Private
    private readonly string _baseAddressPublic;
    private readonly string _baseAddressPrivate;
    private readonly string _baseAddressMBPFeed;

    internal StreamApiSpotClient(HuobiStreamClient root) : base(root)
    {
        _baseAddressPublic = ClientOptions.SpotStreamOptions.PublicAddress;
        _baseAddressPrivate = ClientOptions.SpotStreamOptions.PrivateAddress;
        _baseAddressMBPFeed = ClientOptions.SpotStreamOptions.MBPFeedAddress;
    }

    protected override Task<CallResult<bool>> AuthenticateAsync(WebSocketConnection connection)
        => SpotAuthenticateAsync(connection);

    public async Task<CallResult<IEnumerable<HuobiKline>>> GetKlinesAsync(string symbol, KlineInterval period)
    {
        symbol = symbol.ValidateSpotSymbol();
        var request = new HuobiSocketRequest(NextId().ToString(CultureInfo.InvariantCulture), $"market.{symbol}.kline.{JsonConvert.SerializeObject(period, new KlineIntervalConverter(false))}");
        var result = await QueryAsync<HuobiSocketResponse<IEnumerable<HuobiKline>>>(_baseAddressPublic, request, false).ConfigureAwait(false);
        return result ? result.As(result.Data.Data) : result.AsError<IEnumerable<HuobiKline>>(result.Error!);
    }

    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToKlineUpdatesAsync(string symbol, KlineInterval period, Action<WebSocketDataEvent<HuobiKline>> onData, CancellationToken ct = default)
    {
        symbol = symbol.ValidateSpotSymbol();
        var request = new HuobiSubscribeRequest(NextId().ToString(CultureInfo.InvariantCulture), $"market.{symbol}.kline.{JsonConvert.SerializeObject(period, new KlineIntervalConverter(false))}");
        var internalHandler = new Action<WebSocketDataEvent<HuobiDataEvent<HuobiKline>>>(data => onData(data.As(data.Data.Data, symbol)));
        return await SubscribeAsync(_baseAddressPublic, request, null, false, internalHandler, ct).ConfigureAwait(false);
    }

    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToTickerUpdatesAsync(Action<WebSocketDataEvent<HuobiSymbolDatas>> onData, CancellationToken ct = default)
    {
        var request = new HuobiSubscribeRequest(NextId().ToString(CultureInfo.InvariantCulture), "market.tickers");
        var internalHandler = new Action<WebSocketDataEvent<HuobiDataEvent<IEnumerable<HuobiSymbolTicker>>>>(data =>
        {
            var result = new HuobiSymbolDatas { Timestamp = data.Timestamp, Ticks = data.Data.Data };
            onData(data.As(result));
        });
        return await SubscribeAsync(_baseAddressPublic, request, null, false, internalHandler, ct).ConfigureAwait(false);
    }

    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToTickerUpdatesAsync(string symbol, Action<WebSocketDataEvent<HuobiSymbolTicker>> onData, CancellationToken ct = default)
    {
        var request = new HuobiSubscribeRequest(NextId().ToString(CultureInfo.InvariantCulture), $"market.{symbol}.ticker");
        var internalHandler = new Action<WebSocketDataEvent<HuobiDataEvent<HuobiSymbolTicker>>>(data =>
        {
            data.Data.Data.Symbol = symbol;
            onData(data.As(data.Data.Data));
        });
        return await SubscribeAsync(_baseAddressPublic, request, null, false, internalHandler, ct).ConfigureAwait(false);
    }

    #region MBP
    public async Task<CallResult<HuobiIncementalOrderBook>> GetOrderBookAsync(string symbol, int levels)
    {
        symbol = symbol.ValidateSpotSymbol();
        levels.ValidateIntValues(nameof(levels), 5, 20, 150, 400);

        var request = new HuobiSocketRequest(NextId().ToString(CultureInfo.InvariantCulture), $"market.{symbol}.mbp.{levels}");
        var result = await QueryAsync<HuobiSocketResponse<HuobiIncementalOrderBook>>(_baseAddressMBPFeed, request, false).ConfigureAwait(false);
        if (!result)
            return new CallResult<HuobiIncementalOrderBook>(result.Error!);

        if (result.Data.Data == null)
        {
            var info = "No data received when requesting order book. " +
                "Levels 5/20 are only supported for a subset of symbols, see https://huobiapi.github.io/docs/spot/v1/en/#market-by-price-incremental-update. Use 150 level instead.";
            Log.Write(LogLevel.Debug, info);
            return new CallResult<HuobiIncementalOrderBook>(new ServerError(info));
        }

        return new CallResult<HuobiIncementalOrderBook>(result.Data.Data);
    }

    // 100 milliseconds
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToPartialOrderBookUpdatesAsync(string symbol, int levels, Action<WebSocketDataEvent<HuobiOrderBook>> onData, CancellationToken ct = default)
    {
        symbol = symbol.ValidateSpotSymbol();
        levels.ValidateIntValues(nameof(levels), 5, 10, 20);

        var internalHandler = new Action<WebSocketDataEvent<HuobiDataEvent<HuobiOrderBook>>>(data =>
        {
            data.Data.Timestamp = data.Timestamp;
            onData(data.As(data.Data.Data, symbol));
        });

        var request = new HuobiSubscribeRequest(NextId().ToString(CultureInfo.InvariantCulture), $"market.{symbol}.mbp.refresh.{levels}");
        return await SubscribeAsync(_baseAddressMBPFeed, request, null, false, internalHandler, ct).ConfigureAwait(false);
    }

    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToOrderBookUpdatesAsync(string symbol, int levels, Action<WebSocketDataEvent<HuobiIncementalOrderBook>> onData, CancellationToken ct = default)
    {
        symbol = symbol.ValidateSpotSymbol();
        levels.ValidateIntValues(nameof(levels), 5, 20, 150, 400);

        var internalHandler = new Action<WebSocketDataEvent<HuobiDataEvent<HuobiIncementalOrderBook>>>(data =>
        {
            data.Data.Timestamp = data.Timestamp;
            onData(data.As(data.Data.Data, symbol));
        });

        var request = new HuobiSubscribeRequest(NextId().ToString(CultureInfo.InvariantCulture), $"market.{symbol}.mbp.{levels}");
        return await SubscribeAsync(_baseAddressMBPFeed, request, null, false, internalHandler, ct).ConfigureAwait(false);
    }
    #endregion

    public async Task<CallResult<HuobiOrderBook>> GetMergedOrderBookAsync(string symbol, int mergeStep)
    {
        symbol = symbol.ValidateSpotSymbol();
        mergeStep.ValidateIntBetween(nameof(mergeStep), 0, 5);

        var request = new HuobiSocketRequest(NextId().ToString(CultureInfo.InvariantCulture), $"market.{symbol}.depth.step{mergeStep}");
        var result = await QueryAsync<HuobiSocketResponse<HuobiOrderBook>>(_baseAddressPublic, request, false).ConfigureAwait(false);
        if (!result)
            return new CallResult<HuobiOrderBook>(result.Error!);

        result.Data.Data.Timestamp = result.Data.Timestamp;
        return new CallResult<HuobiOrderBook>(result.Data.Data);
    }

    // 1 Second
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToMergedPartialOrderBookUpdatesAsync(string symbol, int mergeStep, Action<WebSocketDataEvent<HuobiOrderBook>> onData, CancellationToken ct = default)
    {
        symbol = symbol.ValidateSpotSymbol();
        mergeStep.ValidateIntBetween(nameof(mergeStep), 0, 5);

        var internalHandler = new Action<WebSocketDataEvent<HuobiDataEvent<HuobiOrderBook>>>(data =>
        {
            data.Data.Timestamp = data.Timestamp;
            onData(data.As(data.Data.Data, symbol));
        });

        var request = new HuobiSubscribeRequest(NextId().ToString(CultureInfo.InvariantCulture), $"market.{symbol}.depth.step{mergeStep}");
        return await SubscribeAsync(_baseAddressPublic, request, null, false, internalHandler, ct).ConfigureAwait(false);
    }

    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToBestOfferUpdatesAsync(string symbol, Action<WebSocketDataEvent<HuobiBestOffer>> onData, CancellationToken ct = default)
    {
        var request = new HuobiSubscribeRequest(NextId().ToString(CultureInfo.InvariantCulture), $"market.{symbol}.bbo");
        var internalHandler = new Action<WebSocketDataEvent<HuobiDataEvent<HuobiBestOffer>>>(data =>
        {
            onData(data.As(data.Data.Data, symbol));
        });
        return await SubscribeAsync(_baseAddressPublic, request, null, false, internalHandler, ct).ConfigureAwait(false);
    }

    public async Task<CallResult<IEnumerable<HuobiSymbolTradeDetails>>> GetTradeHistoryAsync(string symbol)
    {
        symbol = symbol.ValidateSpotSymbol();
        var request = new HuobiSocketRequest(NextId().ToString(CultureInfo.InvariantCulture), $"market.{symbol}.trade.detail");
        var result = await QueryAsync<HuobiSocketResponse<IEnumerable<HuobiSymbolTradeDetails>>>(_baseAddressPublic, request, false).ConfigureAwait(false);
        return result ? result.As(result.Data.Data) : result.AsError<IEnumerable<HuobiSymbolTradeDetails>>(result.Error!);
    }

    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToTradeUpdatesAsync(string symbol, Action<WebSocketDataEvent<HuobiSymbolTrade>> onData, CancellationToken ct = default)
    {
        symbol = symbol.ValidateSpotSymbol();
        var request = new HuobiSubscribeRequest(NextId().ToString(CultureInfo.InvariantCulture), $"market.{symbol}.trade.detail");
        var internalHandler = new Action<WebSocketDataEvent<HuobiDataEvent<HuobiSymbolTrade>>>(data => onData(data.As(data.Data.Data, symbol)));
        return await SubscribeAsync(_baseAddressPublic, request, null, false, internalHandler, ct).ConfigureAwait(false);
    }

    public async Task<CallResult<HuobiSymbolDetails>> GetSymbolDetailsAsync(string symbol)
    {
        symbol = symbol.ValidateSpotSymbol();
        var request = new HuobiSocketRequest(NextId().ToString(CultureInfo.InvariantCulture), $"market.{symbol}.detail");
        var result = await QueryAsync<HuobiSocketResponse<HuobiSymbolDetails>>(_baseAddressPublic, request, false).ConfigureAwait(false);
        if (!result)
            return result.AsError<HuobiSymbolDetails>(result.Error!);

        result.Data.Data.Timestamp = result.Data.Timestamp;
        return result.As(result.Data.Data);
    }

    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToSymbolDetailUpdatesAsync(string symbol, Action<WebSocketDataEvent<HuobiSymbolDetails>> onData, CancellationToken ct = default)
    {
        symbol = symbol.ValidateSpotSymbol();
        var request = new HuobiSubscribeRequest(NextId().ToString(CultureInfo.InvariantCulture), $"market.{symbol}.detail");
        var internalHandler = new Action<WebSocketDataEvent<HuobiDataEvent<HuobiSymbolDetails>>>(data =>
        {
            data.Data.Timestamp = data.Timestamp;
            onData(data.As(data.Data.Data, symbol));
        });
        return await SubscribeAsync(_baseAddressPublic, request, null, false, internalHandler, ct).ConfigureAwait(false);
    }

    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToOrderUpdatesAsync(
        string symbol = null,
        Action<WebSocketDataEvent<HuobiSubmittedOrderUpdate>> onOrderSubmitted = null,
        Action<WebSocketDataEvent<HuobiMatchedOrderUpdate>> onOrderMatched = null,
        Action<WebSocketDataEvent<HuobiCanceledOrderUpdate>> onOrderCancelation = null,
        Action<WebSocketDataEvent<HuobiTriggerFailureOrderUpdate>> onConditionalOrderTriggerFailure = null,
        Action<WebSocketDataEvent<HuobiOrderUpdate>> onConditionalOrderCanceled = null,
        CancellationToken ct = default)
    {
        symbol = symbol?.ValidateSpotSymbol();
        var request = new HuobiAuthenticatedSubscribeRequest($"orders#{symbol ?? "*"}");
        var internalHandler = new Action<WebSocketDataEvent<JToken>>(data =>
        {
            if (data.Data["data"] == null || data.Data["data"]!["eventType"] == null)
            {
                Log.Write(LogLevel.Warning, "Invalid order update data: " + data);
                return;
            }

            var eventType = data.Data["data"]!["eventType"]?.ToString();
            var symbol = data.Data["data"]!["symbol"]?.ToString();
            if (eventType == "trigger")
            {
                DeserializeAndInvoke(data, onConditionalOrderTriggerFailure, symbol);
            }
            else if (eventType == "deletion")
            {
                DeserializeAndInvoke(data, onConditionalOrderCanceled, symbol);
            }
            else if (eventType == "creation")
            {
                DeserializeAndInvoke(data, onOrderSubmitted, symbol);
            }
            else if (eventType == "trade")
            {
                DeserializeAndInvoke(data, onOrderMatched, symbol);
            }
            else if (eventType == "cancellation")
            {
                DeserializeAndInvoke(data, onOrderCancelation, symbol);
            }
            else
            {
                Log.Write(LogLevel.Warning, "Unknown order event type: " + eventType);
            }
        });
        return await SubscribeAsync(_baseAddressPrivate, request, null, true, internalHandler, ct).ConfigureAwait(false);
    }

    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToAccountUpdatesAsync(Action<WebSocketDataEvent<HuobiAccountUpdate>> onAccountUpdate, int? updateMode = null, CancellationToken ct = default)
    {
        if (updateMode != null && (updateMode > 2 || updateMode < 0))
            throw new ArgumentException("UpdateMode should be either 0, 1 or 2");

        var request = new HuobiAuthenticatedSubscribeRequest("accounts.update#" + (updateMode ?? 1));
        var internalHandler = new Action<WebSocketDataEvent<JToken>>(data =>
        {
            DeserializeAndInvoke(data, onAccountUpdate);
        });
        return await SubscribeAsync(_baseAddressPrivate, request, null, true, internalHandler, ct).ConfigureAwait(false);
    }

    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToOrderDetailsUpdatesAsync(string symbol = null, Action<WebSocketDataEvent<HuobiTradeUpdate>> onOrderMatch = null, Action<WebSocketDataEvent<HuobiOrderCancelationUpdate>> onOrderCancel = null, CancellationToken ct = default)
    {
        var request = new HuobiAuthenticatedSubscribeRequest($"trade.clearing#{symbol ?? "*"}#1");
        var internalHandler = new Action<WebSocketDataEvent<JToken>>(data =>
        {
            if (data.Data["data"] == null || data.Data["data"]!["eventType"] == null)
            {
                Log.Write(LogLevel.Warning, "Invalid order update data: " + data);
                return;
            }

            var eventType = data.Data["data"]!["eventType"]?.ToString();
            var symbol = data.Data["data"]!["symbol"]?.ToString();
            if (eventType == "trade")
            {
                DeserializeAndInvoke(data, onOrderMatch, symbol);
            }
            else if (eventType == "cancellation")
            {
                DeserializeAndInvoke(data, onOrderCancel, symbol);
            }
            else
            {
                Log.Write(LogLevel.Warning, "Unknown order details event type: " + eventType);
            }
        });
        return await SubscribeAsync(_baseAddressPrivate, request, null, true, internalHandler, ct).ConfigureAwait(false);
    }

}