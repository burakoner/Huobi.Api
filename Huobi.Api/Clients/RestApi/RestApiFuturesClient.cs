using Huobi.Api.Models.RestApi.Futures;
using Huobi.Api.Models.RestApi.Spot;

namespace Huobi.Api.Clients.RestApi;

public class RestApiFuturesClient : RestApiBaseClient
{
    // Reference Data Endpoints
    private const string contractRiskInfoEndpoint = "contract_risk_info";
    private const string contractInsuranceFundEndpoint = "contract_insurance_fund";
    private const string contractAdjustfactorEndpoint = "contract_adjustfactor";
    private const string contractHisOpenInterestEndpoint = "contract_his_open_interest";
    private const string contractLadderMarginEndpoint = "contract_ladder_margin";
    private const string contractEliteAccountRatioEndpoint = "contract_elite_account_ratio";
    private const string contractElitePositionRatioEndpoint = "contract_elite_position_ratio";
    private const string contractLiquidationOrdersEndpoint = "contract_liquidation_orders";
    private const string contractSettlementRecordsEndpoint = "contract_settlement_records";
    private const string contractApiStateEndpoint = "contract_api_state";
    private const string contractEstimatedSettlementPriceEndpoint = "contract_estimated_settlement_price";
    private const string contractPriceLimitEndpoint = "contract_price_limit";
    private const string contractOpenInterestEndpoint = "contract_open_interest";
    private const string contractDeliveryPriceEndpoint = "contract_delivery_price";
    private const string contractContractInfoEndpoint = "contract_contract_info";
    private const string contractIndexEndpoint = "contract_index";
    private const string heartbeatEndpoint = "heartbeat";
    private const string timestampEndpoint = "timestamp";
    private const string statusEndpoint = "https://status-dm.huobigroup.com/api/v2/summary.json";

    // Market Data Endpoints
    private const string marketDepthEndpoint = "market/depth";
    private const string marketBboEndpoint = "market/bbo";
    private const string marketHistoryKlineEndpoint = "market/history/kline";
    private const string indexMarketHistoryMarkPriceKlineEndpoint = "index/market/history/mark_price_kline";
    private const string marketDetailMergedEndpoint = "market/detail/merged";
    private const string marketDetailBatchMergedEndpoint = "market/detail/batch_merged";
    private const string marketTradeEndpoint = "market/trade";
    private const string marketHistoryTradeEndpoint = "market/history/trade";
    private const string indexMarketHistoryIndexEndpoint = "index/market/history/index";
    private const string indexMarketHistoryBasisEndpoint = "index/market/history/basis";

    // Future Account
    private const string contractBalanceValuationEndpoint = "contract_balance_valuation";
    private const string contractAccountInfoEndpoint = "contract_account_info";
    private const string contractPositionInfoEndpoint = "contract_position_info";
    private const string contractSubAuthEndpoint = "contract_sub_auth";
    private const string contractSubAccountListEndpoint = "contract_sub_account_list";
    private const string contractSubAccountInfo_listEndpoint = "contract_sub_account_info_list";
    private const string contractSubAccountInfoEndpoint = "contract_sub_account_info";
    private const string contractSubPositionInfoEndpoint = "contract_sub_position_info";
    private const string contractFinancialRecordEndpoint = "contract_financial_record";
    private const string contractFinancialRecordExactEndpoint = "contract_financial_record_exact";
    private const string contractUserSettlementRecordsEndpoint = "contract_user_settlement_records";
    private const string contractOrderLimitEndpoint = "contract_order_limit";
    private const string contractFeeEndpoint = "contract_fee";
    private const string contractTransferLimitEndpoint = "contract_transfer_limit";
    private const string contractPositionLimitEndpoint = "contract_position_limit";
    private const string contractAccountPositionInfoEndpoint = "contract_account_position_info";
    private const string contractMasterSubTransferEndpoint = "contract_master_sub_transfer";
    private const string contractMasterSubTransferRecordEndpoint = "contract_master_sub_transfer_record";
    private const string contractApiTradingStatusEndpoint = "contract_api_trading_status";
    private const string contractAvailableLevelRateEndpoint = "contract_available_level_rate";

    // Future Trade
    private const string contractOrderEndpoint = "contract_order";
    private const string contractBatchorderEndpoint = "contract_batchorder";
    private const string contractCancelEndpoint = "contract_cancel";
    private const string contractCancelallEndpoint = "contract_cancelall";
    private const string contractSwitchLeverRateEndpoint = "contract_switch_lever_rate";
    private const string lightningClosePositionEndpoint = "lightning_close_position";
    private const string contractOrderInfoEndpoint = "contract_order_info";
    private const string contractOrderDetailEndpoint = "contract_order_detail";
    private const string contractOpenordersEndpoint = "contract_openorders";
    private const string contractHisordersEndpoint = "contract_hisorders";
    private const string contractHisordersExactEndpoint = "contract_hisorders_exact";
    private const string contractMatchresultsEndpoint = "contract_matchresults";
    private const string contractMatchresultsExactEndpoint = "contract_matchresults_exact";

    // Contract Strategy Order
    private const string contractTriggerOrderEndpoint = "contract_trigger_order";
    private const string contractTriggerCancelEndpoint = "contract_trigger_cancel";
    private const string contractTriggerCancelallEndpoint = "contract_trigger_cancelall";
    private const string contractTriggerOpenordersEndpoint = "contract_trigger_openorders";
    private const string contractTriggerHisordersEndpoint = "contract_trigger_hisorders";
    private const string contractTpslOrderEndpoint = "contract_tpsl_order";
    private const string contractTpslCancelEndpoint = "contract_tpsl_cancel";
    private const string contractTpslCancelallEndpoint = "contract_tpsl_cancelall";
    private const string contractTpslOpenordersEndpoint = "contract_tpsl_openorders";
    private const string contractTpslHisordersEndpoint = "contract_tpsl_hisorders";
    private const string contractRelationTpslOrderEndpoint = "contract_relation_tpsl_order";
    private const string contractTrackOrderEndpoint = "contract_track_order";
    private const string contractTrackCancelEndpoint = "contract_track_cancel";
    private const string contractTrackCancelallEndpoint = "contract_track_cancelall";
    private const string contractTrackOpenordersEndpoint = "contract_track_openorders";
    private const string contractTrackHisordersEndpoint = "contract_track_hisorders";

    // Future Transferring
    private const string futuresTransferEndpoint = "futures/transfer";

    internal RestApiFuturesClient(HuobiRestApiClient root) : base("Huobi Futures RestApi", root)
    {
    }

    private Uri GetUrl(string version, string endpoint)
    {
        var address = ClientOptions.FuturesRestApiBaseAddress;
        if (!string.IsNullOrWhiteSpace(version)) address = address.AppendPath($"v{version}");
        if (!string.IsNullOrWhiteSpace(endpoint)) address = address.AppendPath(endpoint);

        return new Uri(address);
    }

    public async Task<RestCallResult<HuobiFuturesOrderBook>> GetOrderBookAsync(string symbol, int merge = 0, CancellationToken ct = default)
    {
        merge.ValidateIntValues(nameof(merge), 0, 1, 2, 3, 4, 5);

        var parameters = new Dictionary<string, object>
        {
            { "symbol", symbol },
            { "type", "step" + merge }
        };

        return await SendHuobiRequest<HuobiFuturesOrderBook>(GetUrl(v0, marketDepthEndpoint), HttpMethod.Get, ct, queryParameters: parameters).ConfigureAwait(false);
    }

    public async Task<RestCallResult<IEnumerable<HuobiFuturesBestOffer>>> GetBestOrdersAsync(string symbol, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "symbol", symbol },
        };

        return await SendHuobiRequest<IEnumerable<HuobiFuturesBestOffer>>(GetUrl(v0, marketBboEndpoint), HttpMethod.Get, ct, queryParameters: parameters).ConfigureAwait(false);
    }

    public async Task<RestCallResult<IEnumerable<HuobiKline>>> GetKlinesAsync(string symbol, KlineInterval period, int? limit = null, DateTime? from = null, DateTime? to = null, CancellationToken ct = default)
    {
        limit?.ValidateIntBetween(nameof(limit), 1, 2000);
        var parameters = new Dictionary<string, object>
            {
                { "symbol", symbol },
                { "period", JsonConvert.SerializeObject(period, new KlineIntervalConverter(false)) },
            };
        parameters.AddOptionalParameter("size", limit);
        parameters.AddOptionalParameter("from", from.ConvertToSeconds());
        parameters.AddOptionalParameter("to", to.ConvertToSeconds());

        return await SendHuobiRequest<IEnumerable<HuobiKline>>(GetUrl(v0, marketHistoryKlineEndpoint), HttpMethod.Get, ct, queryParameters: parameters).ConfigureAwait(false);
    }

    public async Task<RestCallResult<IEnumerable<HuobiKline>>> GetMarkPriceKlinesAsync(string symbol, KlineInterval period, int limit = 2000, CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 2000);
        var parameters = new Dictionary<string, object>
            {
                { "symbol", symbol },
                { "period", JsonConvert.SerializeObject(period, new KlineIntervalConverter(false)) },
            };
        parameters.AddOptionalParameter("size", limit);

        return await SendHuobiRequest<IEnumerable<HuobiKline>>(GetUrl(v0, indexMarketHistoryMarkPriceKlineEndpoint), HttpMethod.Get, ct, queryParameters: parameters).ConfigureAwait(false);
    }

    public async Task<RestCallResult<HuobiMarketData>> GetMarketDataAsync(string symbol, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>()
            {
                { "symbol", symbol }
            };
        return await SendHuobiRequest<HuobiMarketData>(GetUrl(v0, marketDetailMergedEndpoint), HttpMethod.Get, ct, queryParameters: parameters).ConfigureAwait(false);
    }

    public async Task<RestCallResult<IEnumerable<HuobiMarketData>>> GetMarketDatasAsync(string symbol=null, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("symbol", symbol);

        return await SendHuobiRequest<IEnumerable<HuobiMarketData>>(GetUrl(v2, marketDetailBatchMergedEndpoint), HttpMethod.Get, ct, queryParameters: parameters).ConfigureAwait(false);
    }

    public async Task<RestCallResult<HuobiFuturesLastTrade>> GetLastTradesAsync(string symbol = null, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("symbol", symbol);
        
        var result = await SendHuobiRequest<HuobiFuturesLastTradeWrapper>(GetUrl(v0, marketTradeEndpoint), HttpMethod.Get, ct, queryParameters: parameters).ConfigureAwait(false);
        return result.As(result.Data?.Data?.First()!);
    }

    public async Task<RestCallResult<IEnumerable<HuobiFuturesTrade>>> GetRecentTradesAsync(string symbol, int limit, CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 2000);
        var parameters = new Dictionary<string, object>()
            {
                { "symbol", symbol },
                { "size", limit }
            };
        var result = await SendHuobiRequest<IEnumerable<HuobiFuturesTradeWrapper>>(GetUrl(v0, marketHistoryTradeEndpoint), HttpMethod.Get, ct, queryParameters: parameters).ConfigureAwait(false);
        return result.As<IEnumerable<HuobiFuturesTrade>>(result.Data?.SelectMany(d => d.Data)!);
    }

    public async Task<RestCallResult<IEnumerable<HuobiKline>>> GetIndexPriceKlinesAsync(string symbol, KlineInterval period, int limit = 2000, CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 2000);
        var parameters = new Dictionary<string, object>
            {
                { "symbol", symbol },
                { "period", JsonConvert.SerializeObject(period, new KlineIntervalConverter(false)) },
            };
        parameters.AddOptionalParameter("size", limit);

        return await SendHuobiRequest<IEnumerable<HuobiKline>>(GetUrl(v0, indexMarketHistoryIndexEndpoint), HttpMethod.Get, ct, queryParameters: parameters).ConfigureAwait(false);
    }

    public async Task<RestCallResult<IEnumerable<HuobiFuturesBasisData>>> GetBasisDataAsync(string contractCode, KlineInterval interval, int limit, string basisPriceType = null, CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 2000);
        var parameters = new Dictionary<string, object>()
            {
                { "contract_code", contractCode },
                { "period", EnumConverter.GetString(interval) },
                { "size", limit }
            };
        parameters.AddOptionalParameter("basis_price_type", basisPriceType);

        return await SendHuobiRequest<IEnumerable<HuobiFuturesBasisData>>(GetUrl(v0, indexMarketHistoryBasisEndpoint), HttpMethod.Get, ct, queryParameters: parameters).ConfigureAwait(false);
    }
}