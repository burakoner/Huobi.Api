using Huobi.Api.Models.RestApi.Spot.Trading;
using Huobi.Api.Models.StreamApi.Futures;
using Huobi.Api.Models.StreamApi.Spot;

namespace Huobi.Api.Clients.StreamApi;

public class StreamApiSpotClient : StreamApiBaseClient
{
    // Private
    private readonly string _baseAddressPublic;
    private readonly string _baseAddressPrivate;
    private readonly string _baseAddressOrderBookFeed;

    internal StreamApiSpotClient(HuobiStreamClient root) : base(root)
    {
        _baseAddressPublic = ClientOptions.SpotStreamOptions.PublicAddress;
        _baseAddressPrivate = ClientOptions.SpotStreamOptions.PrivateAddress;
        _baseAddressOrderBookFeed = ClientOptions.SpotStreamOptions.OrderBookFeedAddress;
    }

    protected override Task<CallResult<bool>> AuthenticateAsync(StreamConnection connection)
        => SpotAuthenticateAsync(connection);

    public async Task<CallResult<IEnumerable<HuobiKline>>> GetKlinesAsync(string symbol, KlineInterval period)
    {
        symbol = symbol.ValidateSpotSymbol();
        var request = new HuobiSocketRequest(NextId().ToString(CultureInfo.InvariantCulture), $"market.{symbol}.kline.{JsonConvert.SerializeObject(period, new KlineIntervalConverter(false))}");
        var result = await QueryAsync<HuobiSocketResponse<IEnumerable<HuobiKline>>>(request, false).ConfigureAwait(false);
        return result ? result.As(result.Data.Data) : result.AsError<IEnumerable<HuobiKline>>(result.Error!);
    }

    public async Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(string symbol, KlineInterval period, Action<StreamDataEvent<HuobiKline>> onData, CancellationToken ct = default)
    {
        symbol = symbol.ValidateSpotSymbol();
        var request = new HuobiSubscribeRequest(NextId().ToString(CultureInfo.InvariantCulture), $"market.{symbol}.kline.{JsonConvert.SerializeObject(period, new KlineIntervalConverter(false))}");
        var internalHandler = new Action<StreamDataEvent<HuobiDataEvent<HuobiKline>>>(data => onData(data.As(data.Data.Data, symbol)));
        return await SubscribeAsync(_baseAddressPublic, request, null, false, internalHandler, ct).ConfigureAwait(false);
    }

    public async Task<CallResult<HuobiOrderBook>> GetOrderBookWithMergeStepAsync(string symbol, int mergeStep)
    {
        symbol = symbol.ValidateSpotSymbol();
        mergeStep.ValidateIntBetween(nameof(mergeStep), 0, 5);

        var request = new HuobiSocketRequest(NextId().ToString(CultureInfo.InvariantCulture), $"market.{symbol}.depth.step{mergeStep}");
        var result = await QueryAsync<HuobiSocketResponse<HuobiOrderBook>>(request, false).ConfigureAwait(false);
        if (!result)
            return new CallResult<HuobiOrderBook>(result.Error!);

        result.Data.Data.Timestamp = result.Data.Timestamp;
        return new CallResult<HuobiOrderBook>(result.Data.Data);
    }

    public async Task<CallResult<HuobiIncementalOrderBook>> GetOrderBookAsync(string symbol, int levels)
    {
        symbol = symbol.ValidateSpotSymbol();
        levels.ValidateIntValues(nameof(levels), 5, 20, 150, 400);

        var request = new HuobiSocketRequest(NextId().ToString(CultureInfo.InvariantCulture), $"market.{symbol}.mbp.{levels}");
        var result = await QueryAsync<HuobiSocketResponse<HuobiIncementalOrderBook>>(_baseAddressOrderBookFeed, request, false).ConfigureAwait(false);
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

    public async Task<CallResult<UpdateSubscription>> SubscribeToPartialOrderBookUpdates1SecondAsync(string symbol, int mergeStep, Action<StreamDataEvent<HuobiOrderBook>> onData, CancellationToken ct = default)
    {
        symbol = symbol.ValidateSpotSymbol();
        mergeStep.ValidateIntBetween(nameof(mergeStep), 0, 5);

        var internalHandler = new Action<StreamDataEvent<HuobiDataEvent<HuobiOrderBook>>>(data =>
        {
            data.Data.Timestamp = data.Timestamp;
            onData(data.As(data.Data.Data, symbol));
        });

        var request = new HuobiSubscribeRequest(NextId().ToString(CultureInfo.InvariantCulture), $"market.{symbol}.depth.step{mergeStep}");
        return await SubscribeAsync(request, null, false, internalHandler, ct).ConfigureAwait(false);
    }

    public async Task<CallResult<UpdateSubscription>> SubscribeToPartialOrderBookUpdates100MilisecondAsync(string symbol, int levels, Action<StreamDataEvent<HuobiOrderBook>> onData, CancellationToken ct = default)
    {
        symbol = symbol.ValidateSpotSymbol();
        levels.ValidateIntValues(nameof(levels), 5, 10, 20);

        var internalHandler = new Action<StreamDataEvent<HuobiDataEvent<HuobiOrderBook>>>(data =>
        {
            data.Data.Timestamp = data.Timestamp;
            onData(data.As(data.Data.Data, symbol));
        });

        var request = new HuobiSubscribeRequest(NextId().ToString(CultureInfo.InvariantCulture), $"market.{symbol}.mbp.refresh.{levels}");
        return await SubscribeAsync(request, null, false, internalHandler, ct).ConfigureAwait(false);
    }

    public async Task<CallResult<UpdateSubscription>> SubscribeToOrderBookChangeUpdatesAsync(string symbol, int levels, Action<StreamDataEvent<HuobiIncementalOrderBook>> onData, CancellationToken ct = default)
    {
        symbol = symbol.ValidateSpotSymbol();
        levels.ValidateIntValues(nameof(levels), 5, 20, 150, 400);

        var internalHandler = new Action<StreamDataEvent<HuobiDataEvent<HuobiIncementalOrderBook>>>(data =>
        {
            data.Data.Timestamp = data.Timestamp;
            onData(data.As(data.Data.Data, symbol));
        });

        var request = new HuobiSubscribeRequest(NextId().ToString(CultureInfo.InvariantCulture), $"market.{symbol}.mbp.{levels}");
        return await SubscribeAsync(_baseAddressOrderBookFeed, request, null, false, internalHandler, ct).ConfigureAwait(false);
    }


    public async Task<CallResult<IEnumerable<HuobiSymbolTradeDetails>>> GetTradeHistoryAsync(string symbol)
    {
        symbol = symbol.ValidateSpotSymbol();
        var request = new HuobiSocketRequest(NextId().ToString(CultureInfo.InvariantCulture), $"market.{symbol}.trade.detail");
        var result = await QueryAsync<HuobiSocketResponse<IEnumerable<HuobiSymbolTradeDetails>>>(request, false).ConfigureAwait(false);
        return result ? result.As(result.Data.Data) : result.AsError<IEnumerable<HuobiSymbolTradeDetails>>(result.Error!);
    }

    public async Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string symbol, Action<StreamDataEvent<HuobiSymbolTrade>> onData, CancellationToken ct = default)
    {
        symbol = symbol.ValidateSpotSymbol();
        var request = new HuobiSubscribeRequest(NextId().ToString(CultureInfo.InvariantCulture), $"market.{symbol}.trade.detail");
        var internalHandler = new Action<StreamDataEvent<HuobiDataEvent<HuobiSymbolTrade>>>(data => onData(data.As(data.Data.Data, symbol)));
        return await SubscribeAsync(_baseAddressPublic, request, null, false, internalHandler, ct).ConfigureAwait(false);
    }

    public async Task<CallResult<HuobiSymbolDetails>> GetSymbolDetailsAsync(string symbol)
    {
        symbol = symbol.ValidateSpotSymbol();
        var request = new HuobiSocketRequest(NextId().ToString(CultureInfo.InvariantCulture), $"market.{symbol}.detail");
        var result = await QueryAsync<HuobiSocketResponse<HuobiSymbolDetails>>(request, false).ConfigureAwait(false);
        if (!result)
            return result.AsError<HuobiSymbolDetails>(result.Error!);

        result.Data.Data.Timestamp = result.Data.Timestamp;
        return result.As(result.Data.Data);
    }

    public async Task<CallResult<UpdateSubscription>> SubscribeToSymbolDetailUpdatesAsync(string symbol, Action<StreamDataEvent<HuobiSymbolDetails>> onData, CancellationToken ct = default)
    {
        symbol = symbol.ValidateSpotSymbol();
        var request = new HuobiSubscribeRequest(NextId().ToString(CultureInfo.InvariantCulture), $"market.{symbol}.detail");
        var internalHandler = new Action<StreamDataEvent<HuobiDataEvent<HuobiSymbolDetails>>>(data =>
        {
            data.Data.Timestamp = data.Timestamp;
            onData(data.As(data.Data.Data, symbol));
        });
        return await SubscribeAsync(_baseAddressPublic, request, null, false, internalHandler, ct).ConfigureAwait(false);
    }

    public async Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(Action<StreamDataEvent<HuobiSymbolDatas>> onData, CancellationToken ct = default)
    {
        var request = new HuobiSubscribeRequest(NextId().ToString(CultureInfo.InvariantCulture), "market.tickers");
        var internalHandler = new Action<StreamDataEvent<HuobiDataEvent<IEnumerable<HuobiSymbolTicker>>>>(data =>
        {
            var result = new HuobiSymbolDatas { Timestamp = data.Timestamp, Ticks = data.Data.Data };
            onData(data.As(result));
        });
        return await SubscribeAsync(_baseAddressPublic, request, null, false, internalHandler, ct).ConfigureAwait(false);
    }

    public async Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(string symbol, Action<StreamDataEvent<HuobiSymbolTicker>> onData, CancellationToken ct = default)
    {
        var request = new HuobiSubscribeRequest(NextId().ToString(CultureInfo.InvariantCulture), $"market.{symbol}.ticker");
        var internalHandler = new Action<StreamDataEvent<HuobiDataEvent<HuobiSymbolTicker>>>(data =>
        {
            data.Data.Data.Symbol = symbol;
            onData(data.As(data.Data.Data));
        });
        return await SubscribeAsync(_baseAddressPublic, request, null, false, internalHandler, ct).ConfigureAwait(false);
    }

    public async Task<CallResult<UpdateSubscription>> SubscribeToBestOfferUpdatesAsync(string symbol, Action<StreamDataEvent<HuobiBestOffer>> onData, CancellationToken ct = default)
    {
        var request = new HuobiSubscribeRequest(NextId().ToString(CultureInfo.InvariantCulture), $"market.{symbol}.bbo");
        var internalHandler = new Action<StreamDataEvent<HuobiDataEvent<HuobiBestOffer>>>(data =>
        {
            onData(data.As(data.Data.Data, symbol));
        });
        return await SubscribeAsync(_baseAddressPublic, request, null, false, internalHandler, ct).ConfigureAwait(false);
    }

    public async Task<CallResult<UpdateSubscription>> SubscribeToOrderUpdatesAsync(
        string symbol = null,
        Action<StreamDataEvent<HuobiSubmittedOrderUpdate>> onOrderSubmitted = null,
        Action<StreamDataEvent<HuobiMatchedOrderUpdate>> onOrderMatched = null,
        Action<StreamDataEvent<HuobiCanceledOrderUpdate>> onOrderCancelation = null,
        Action<StreamDataEvent<HuobiTriggerFailureOrderUpdate>> onConditionalOrderTriggerFailure = null,
        Action<StreamDataEvent<HuobiOrderUpdate>> onConditionalOrderCanceled = null,
        CancellationToken ct = default)
    {
        symbol = symbol?.ValidateSpotSymbol();
        var request = new HuobiAuthenticatedSubscribeRequest($"orders#{symbol ?? "*"}");
        var internalHandler = new Action<StreamDataEvent<JToken>>(data =>
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

    public async Task<CallResult<UpdateSubscription>> SubscribeToAccountUpdatesAsync(Action<StreamDataEvent<HuobiAccountUpdate>> onAccountUpdate, int? updateMode = null, CancellationToken ct = default)
    {
        if (updateMode != null && (updateMode > 2 || updateMode < 0))
            throw new ArgumentException("UpdateMode should be either 0, 1 or 2");

        var request = new HuobiAuthenticatedSubscribeRequest("accounts.update#" + (updateMode ?? 1));
        var internalHandler = new Action<StreamDataEvent<JToken>>(data =>
        {
            DeserializeAndInvoke(data, onAccountUpdate);
        });
        return await SubscribeAsync(_baseAddressPrivate, request, null, true, internalHandler, ct).ConfigureAwait(false);
    }

    public async Task<CallResult<UpdateSubscription>> SubscribeToOrderDetailsUpdatesAsync(string symbol = null, Action<StreamDataEvent<HuobiTradeUpdate>> onOrderMatch = null, Action<StreamDataEvent<HuobiOrderCancelationUpdate>> onOrderCancel = null, CancellationToken ct = default)
    {
        var request = new HuobiAuthenticatedSubscribeRequest($"trade.clearing#{symbol ?? "*"}#1");
        var internalHandler = new Action<StreamDataEvent<JToken>>(data =>
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