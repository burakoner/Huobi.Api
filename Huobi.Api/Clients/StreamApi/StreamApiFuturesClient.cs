using Huobi.Api.Models.RestApi.Spot;
using Huobi.Api.Models.StreamApi;
using Huobi.Api.Models.StreamApi.Futures;
using Huobi.Api.Models.StreamApi.Spot;

namespace Huobi.Api.Clients.StreamApi;

public class StreamApiFuturesClient : StreamApiBaseClient
{
    // Private
    private readonly string _baseAddressMarketData;
    private readonly string _baseAddressNotification;
    private readonly string _baseAddressIndex;
    private readonly string _baseAddressSystemStatus;

    internal StreamApiFuturesClient(HuobiStreamClient root) : base(root)
    {
        _baseAddressMarketData = ClientOptions.FuturesStreamOptions.MarketDataAddress;
        _baseAddressNotification = ClientOptions.FuturesStreamOptions.OrderPushAddress;
        _baseAddressIndex = ClientOptions.FuturesStreamOptions.KlineBasisDataAddress;
        _baseAddressSystemStatus = ClientOptions.FuturesStreamOptions.SystemStatusAddress;
    }

    protected override Task<CallResult<bool>> AuthenticateAsync(StreamConnection connection)
        => FuturesAuthenticateAsync(connection);

    public async Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(string contractCode, KlineInterval period, Action<StreamDataEvent<HuobiKline>> onData, CancellationToken ct = default)
    {
        var request = new HuobiSubscribeRequest(NextId().ToString(CultureInfo.InvariantCulture), $"market.{contractCode}.kline.{JsonConvert.SerializeObject(period, new KlineIntervalConverter(false))}");
        var internalHandler = new Action<StreamDataEvent<HuobiDataEvent<HuobiKline>>>(data => onData(data.As(data.Data.Data, contractCode)));
        return await SubscribeAsync(request, null, false, internalHandler, ct).ConfigureAwait(false);
    }

    public async Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(string contractCode, int mergeStep, Action<StreamDataEvent<HuobiUsdtMarginSwapIncementalOrderBook>> onData, CancellationToken ct = default)
    {
        var request = new HuobiSubscribeRequest(NextId().ToString(CultureInfo.InvariantCulture), $"market.{contractCode}.depth.step" + mergeStep);
        var internalHandler = new Action<StreamDataEvent<HuobiDataEvent<HuobiUsdtMarginSwapIncementalOrderBook>>>(data => onData(data.As(data.Data.Data, contractCode)));
        return await SubscribeAsync(request, null, false, internalHandler, ct).ConfigureAwait(false);
    }

    public async Task<CallResult<UpdateSubscription>> SubscribeToIncrementalOrderBookUpdatesAsync(string contractCode, bool snapshot, int limit, Action<StreamDataEvent<HuobiUsdtMarginSwapIncementalOrderBook>> onData, CancellationToken ct = default)
    {
        var request = new HuobiIncrementalOrderBookSubscribeRequest(
            NextId().ToString(CultureInfo.InvariantCulture),
            $"market.{contractCode}.depth.size_{limit}.high_freq",
            snapshot ? "snapshot" : "incremental");
        var internalHandler = new Action<StreamDataEvent<HuobiDataEvent<HuobiUsdtMarginSwapIncementalOrderBook>>>(data => onData(data.As(data.Data.Data, contractCode)));
        return await SubscribeAsync(request, null, false, internalHandler, ct).ConfigureAwait(false);
    }

    public async Task<CallResult<UpdateSubscription>> SubscribeToSymbolTickerUpdatesAsync(string contractCode, Action<StreamDataEvent<HuobiSymbolTickUpdate>> onData, CancellationToken ct = default)
    {
        var request = new HuobiSubscribeRequest(
            NextId().ToString(CultureInfo.InvariantCulture),
            $"market.{contractCode}.detail");
        var internalHandler = new Action<StreamDataEvent<HuobiDataEvent<HuobiSymbolTickUpdate>>>(data => onData(data.As(data.Data.Data, contractCode)));
        return await SubscribeAsync(request, null, false, internalHandler, ct).ConfigureAwait(false);
    }

    public async Task<CallResult<UpdateSubscription>> SubscribeToBestOfferUpdatesAsync(string contractCode, Action<StreamDataEvent<HuobiBestOfferUpdate>> onData, CancellationToken ct = default)
    {
        var request = new HuobiSubscribeRequest(
            NextId().ToString(CultureInfo.InvariantCulture),
            $"market.{contractCode}.bbo");
        var internalHandler = new Action<StreamDataEvent<HuobiDataEvent<HuobiBestOfferUpdate>>>(data => onData(data.As(data.Data.Data, contractCode)));
        return await SubscribeAsync(request, null, false, internalHandler, ct).ConfigureAwait(false);
    }

    public async Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string contractCode, Action<StreamDataEvent<HuobiUsdtMarginSwapTradesUpdate>> onData, CancellationToken ct = default)
    {
        var request = new HuobiSubscribeRequest(
            NextId().ToString(CultureInfo.InvariantCulture),
            $"market.{contractCode}.trade.detail");
        var internalHandler = new Action<StreamDataEvent<HuobiDataEvent<HuobiUsdtMarginSwapTradesUpdate>>>(data => onData(data.As(data.Data.Data, contractCode)));
        return await SubscribeAsync(request, null, false, internalHandler, ct).ConfigureAwait(false);
    }

    public async Task<CallResult<UpdateSubscription>> SubscribeToIndexKlineUpdatesAsync(string contractCode, KlineInterval period, Action<StreamDataEvent<HuobiKline>> onData, CancellationToken ct = default)
    {
        var request = new HuobiSubscribeRequest(
            NextId().ToString(CultureInfo.InvariantCulture),
            $"market.{contractCode}.index.{JsonConvert.SerializeObject(period, new KlineIntervalConverter(false))}");
        var internalHandler = new Action<StreamDataEvent<HuobiDataEvent<HuobiKline>>>(data => onData(data.As(data.Data.Data, contractCode)));
        return await SubscribeAsync(_baseAddressIndex, request, null, false, internalHandler, ct).ConfigureAwait(false);
    }

    public async Task<CallResult<UpdateSubscription>> SubscribeToPremiumIndexKlineUpdatesAsync(string contractCode, KlineInterval period, Action<StreamDataEvent<HuobiKline>> onData, CancellationToken ct = default)
    {
        var request = new HuobiSubscribeRequest(
            NextId().ToString(CultureInfo.InvariantCulture),
            $"market.{contractCode}.premium_index.{JsonConvert.SerializeObject(period, new KlineIntervalConverter(false))}");
        var internalHandler = new Action<StreamDataEvent<HuobiDataEvent<HuobiKline>>>(data => onData(data.As(data.Data.Data, contractCode)));
        return await SubscribeAsync(_baseAddressIndex, request, null, false, internalHandler, ct).ConfigureAwait(false);
    }

    public async Task<CallResult<UpdateSubscription>> SubscribeToEstimatedFundingRateKlineUpdatesAsync(string contractCode, KlineInterval period, Action<StreamDataEvent<HuobiKline>> onData, CancellationToken ct = default)
    {
        var request = new HuobiSubscribeRequest(
            NextId().ToString(CultureInfo.InvariantCulture),
            $"market.{contractCode}.estimated_rate.{JsonConvert.SerializeObject(period, new KlineIntervalConverter(false))}");
        var internalHandler = new Action<StreamDataEvent<HuobiDataEvent<HuobiKline>>>(data => onData(data.As(data.Data.Data, contractCode)));
        return await SubscribeAsync(_baseAddressIndex, request, null, false, internalHandler, ct).ConfigureAwait(false);
    }

    public async Task<CallResult<UpdateSubscription>> SubscribeToBasisUpdatesAsync(string contractCode, KlineInterval period, string priceType, Action<StreamDataEvent<HuobiUsdtMarginSwapBasisUpdate>> onData, CancellationToken ct = default)
    {
        var request = new HuobiSubscribeRequest(
            NextId().ToString(CultureInfo.InvariantCulture),
            $"market.{contractCode}.basis.{JsonConvert.SerializeObject(period, new KlineIntervalConverter(false))}.{priceType}");
        var internalHandler = new Action<StreamDataEvent<HuobiDataEvent<HuobiUsdtMarginSwapBasisUpdate>>>(data => onData(data.As(data.Data.Data, contractCode)));
        return await SubscribeAsync(_baseAddressIndex, request, null, false, internalHandler, ct).ConfigureAwait(false);
    }

    public async Task<CallResult<UpdateSubscription>> SubscribeToMarkPriceKlineUpdatesAsync(string contractCode, KlineInterval period, Action<StreamDataEvent<HuobiKline>> onData, CancellationToken ct = default)
    {
        var request = new HuobiSubscribeRequest(
            NextId().ToString(CultureInfo.InvariantCulture),
            $"market.{contractCode}.mark_price.{JsonConvert.SerializeObject(period, new KlineIntervalConverter(false))}");
        var internalHandler = new Action<StreamDataEvent<HuobiDataEvent<HuobiKline>>>(data => onData(data.As(data.Data.Data, contractCode)));
        return await SubscribeAsync(_baseAddressIndex, request, null, false, internalHandler, ct).ConfigureAwait(false);
    }

    // WIP
    //public async Task<CallResult<UpdateSubscription>> SubscribeToIsolatedMarginOrderUpdatesAsync(Action<StreamDataEvent<HuobiIsolatedMarginOrder>> onData, CancellationToken ct = default)
    //{
    //    var request = new HuobiSocketRequest2(
    //        "sub",
    //        NextId().ToString(CultureInfo.InvariantCulture),
    //        $"orders.*");
    //    var internalHandler = new Action<StreamDataEvent<HuobiIsolatedMarginOrder>>(data => onData(data.As(data.Data, data.Data.ContractCode)));
    //    return await SubscribeAsync( _baseAddressAuthenticated, request, null, true, internalHandler, ct).ConfigureAwait(false);
    //}

    //public async Task<CallResult<UpdateSubscription>> SubscribeToIsolatedMarginOrderUpdatesAsync(string contractCode, Action<StreamDataEvent<HuobiIsolatedMarginOrder>> onData, CancellationToken ct = default)
    //{
    //    var request = new HuobiSocketRequest2(
    //        "sub",
    //        NextId().ToString(CultureInfo.InvariantCulture),
    //        $"orders.{contractCode}");
    //    var internalHandler = new Action<StreamDataEvent<HuobiIsolatedMarginOrder>>(data => onData(data.As(data.Data, contractCode)));
    //    return await SubscribeAsync( _baseAddressAuthenticated, request, null, true, internalHandler, ct).ConfigureAwait(false);
    //}

    //public async Task<CallResult<UpdateSubscription>> SubscribeToCrossMarginOrderUpdatesAsync(Action<StreamDataEvent<HuobiCrossMarginOrder>> onData, CancellationToken ct = default)
    //{
    //    var request = new HuobiSocketRequest2(
    //        "sub",
    //        NextId().ToString(CultureInfo.InvariantCulture),
    //        $"orders_cross.*");
    //    var internalHandler = new Action<StreamDataEvent<HuobiCrossMarginOrder>>(data => onData(data.As(data.Data, data.Data.ContractCode)));
    //    return await SubscribeAsync(_baseAddressAuthenticated, request, null, true, internalHandler, ct).ConfigureAwait(false);
    //}

    //public async Task<CallResult<UpdateSubscription>> SubscribeToCrossMarginOrderUpdatesAsync(string contractCode, Action<StreamDataEvent<HuobiCrossMarginOrder>> onData, CancellationToken ct = default)
    //{
    //    var request = new HuobiSocketRequest2(
    //        "sub",
    //        NextId().ToString(CultureInfo.InvariantCulture),
    //        $"orders_cross.{contractCode}");
    //    var internalHandler = new Action<StreamDataEvent<HuobiCrossMarginOrder>>(data => onData(data.As(data.Data, contractCode)));
    //    return await SubscribeAsync(_baseAddressAuthenticated, request, null, true, internalHandler, ct).ConfigureAwait(false);
    //}
}