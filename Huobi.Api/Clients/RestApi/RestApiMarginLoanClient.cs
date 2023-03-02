using Huobi.Api.Models.RestApi.Margin;

namespace Huobi.Api.Clients.RestApi;

public class RestApiMarginLoanClient : RestApiBaseClient
{
    // Margin Loan Endpoints
    private const string accountRepaymentEndpoint = "account/repayment";
    private const string dwTransferInMarginEndpoint = "dw/transfer-in/margin";
    private const string dwTransferOutMarginEndpoint = "dw/transfer-out/margin";
    private const string marginLoanInfoEndpoint = "margin/loan-info";
    private const string marginOrdersEndpoint = "margin/orders";
    private const string marginOrdersOrderIdRepayEndpoint = "margin/orders/{}/repay";
    private const string marginLoanOrdersEndpoint = "margin/loan-orders";
    private const string marginAccountsBalanceEndpoint = "margin/accounts/balance";
    private const string crossMarginTransferInEndpoint = "cross-margin/transfer-in";
    private const string crossMarginTransferOutEndpoint = "cross-margin/transfer-out";
    private const string crossMarginLoanInfoEndpoint = "cross-margin/loan-info";
    private const string crossMarginOrdersEndpoint = "cross-margin/orders";
    private const string crossMarginOrdersOrderIdRepayEndpoint = "cross-margin/orders/{}/repay";
    private const string crossMarginLoanOrdersEndpoint = "cross-margin/loan-orders";
    private const string crossMarginAccountsBalanceEndpoint = "cross-margin/accounts/balance";
    private const string accountRepaymentRecordsEndpoint = "account/repayment";

    internal RestApiMarginLoanClient(HuobiRestApiClient root) : base("Huobi Margin Loan RestApi", root)
    {
    }

    #region Linked Methods
    public async Task<RestCallResult<long>> GetUserIdAsync(CancellationToken ct = default)
        => await RootClient.Users.GetUserIdAsync(ct).ConfigureAwait(false);

    public async Task<RestCallResult<IEnumerable<Models.RestApi.Account.HuobiAccount>>> GetAccountsAsync(CancellationToken ct = default)
        => await RootClient.Wallet.GetAccountsAsync(ct).ConfigureAwait(false);
    #endregion

    public async Task<RestCallResult<IEnumerable<HuobiRepaymentResult>>> RepayMarginLoanAsync(string accountId, string asset, decimal quantity, string transactionId = null, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "accountId", accountId },
            { "currency", asset },
            { "amount", quantity.ToString(CultureInfo.InvariantCulture) }
        };

        parameters.AddOptionalParameter("transactId", transactionId);
        return await SendHuobiRequest<IEnumerable<HuobiRepaymentResult>>(GetUrl(v2, accountRepaymentEndpoint), HttpMethod.Post, ct, signed: true, bodyParameters: parameters).ConfigureAwait(false);
    }

    public async Task<RestCallResult<long>> TransferSpotToIsolatedMarginAsync(string symbol, string asset, decimal quantity, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "symbol", symbol },
            { "currency", asset },
            { "amount", quantity.ToString(CultureInfo.InvariantCulture) }
        };

        return await SendHuobiRequest<long>(GetUrl(v1, dwTransferInMarginEndpoint), HttpMethod.Post, ct, signed: true, bodyParameters: parameters).ConfigureAwait(false);
    }

    public async Task<RestCallResult<long>> TransferIsolatedMarginToSpotAsync(string symbol, string asset, decimal quantity, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "symbol", symbol },
            { "currency", asset },
            { "amount", quantity.ToString(CultureInfo.InvariantCulture) }
        };

        return await SendHuobiRequest<long>(GetUrl(v1, dwTransferOutMarginEndpoint), HttpMethod.Post, ct, signed: true, bodyParameters: parameters).ConfigureAwait(false);
    }
    public async Task<RestCallResult<IEnumerable<HuobiLoanInfo>>> GetIsolatedLoanInterestRateAndQuotaAsync(IEnumerable<string>? symbols = null, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("symbols", symbols == null ? null : string.Join(",", symbols));

        return await SendHuobiRequest<IEnumerable<HuobiLoanInfo>>(GetUrl(v1, marginLoanInfoEndpoint), HttpMethod.Get, ct, signed: true, queryParameters: parameters).ConfigureAwait(false);
    }

    public async Task<RestCallResult<long>> RequestIsolatedMarginLoanAsync(string symbol, string asset, decimal quantity, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>()
        {
            { "symbol", symbol },
            { "currency", asset },
            { "amount", quantity.ToString(CultureInfo.InvariantCulture) },
        };

        return await SendHuobiRequest<long>(GetUrl(v1, marginOrdersEndpoint), HttpMethod.Post, ct, signed: true, bodyParameters: parameters).ConfigureAwait(false);
    }

    public async Task<RestCallResult<long>> RepayIsolatedMarginLoanAsync(string orderId, decimal quantity, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "amount", quantity.ToString(CultureInfo.InvariantCulture) }
        };

        var endpoint = marginOrdersOrderIdRepayEndpoint.FillPathParameters(orderId.ToString(CultureInfo.InvariantCulture));
        return await SendHuobiRequest<long>(GetUrl(v1, endpoint), HttpMethod.Post, ct, signed: true, bodyParameters: parameters).ConfigureAwait(false);
    }

    public async Task<RestCallResult<IEnumerable<HuobiMarginOrder>>> GetIsolatedMarginClosedOrdersAsync(
    string symbol,
    IEnumerable<MarginOrderStatus>? states = null,
    DateTime? startDate = null,
    DateTime? endDate = null,
    string from = null,
    FilterDirection? direction = null,
    int? limit = null,
    int? subUserId = null,
    CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "symbol", symbol }
        };

        parameters.AddOptionalParameter("states", states == null ? null : string.Join(",", states.Select(EnumConverter.GetString)));
        parameters.AddOptionalParameter("start-date", startDate == null ? null : startDate.Value.ToString("yyyy-mm-dd"));
        parameters.AddOptionalParameter("end-date", endDate == null ? null : endDate.Value.ToString("yyyy-mm-dd"));
        parameters.AddOptionalParameter("from", from);
        parameters.AddOptionalParameter("direct", EnumConverter.GetString(direction));
        parameters.AddOptionalParameter("size", limit);
        parameters.AddOptionalParameter("sub-uid", subUserId);

        return await SendHuobiRequest<IEnumerable<HuobiMarginOrder>>(GetUrl(v1, marginLoanOrdersEndpoint), HttpMethod.Get, ct, signed: true, queryParameters: parameters).ConfigureAwait(false);
    }

    public async Task<RestCallResult<IEnumerable<HuobiMarginBalances>>> GetIsolatedMarginBalanceAsync(string symbol, int? subUserId = null, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "symbol", symbol }
        };

        parameters.AddOptionalParameter("sub-uid", subUserId);

        return await SendHuobiRequest<IEnumerable<HuobiMarginBalances>>(GetUrl(v1, marginAccountsBalanceEndpoint), HttpMethod.Get, ct, signed: true, queryParameters: parameters).ConfigureAwait(false);
    }

    public async Task<RestCallResult<long>> TransferSpotToCrossMarginAsync(string asset, decimal quantity, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "currency", asset },
            { "amount", quantity },
        };

        return await SendHuobiRequest<long>(GetUrl(v1, crossMarginTransferInEndpoint), HttpMethod.Post, ct, signed: true, bodyParameters: parameters).ConfigureAwait(false);
    }

    public async Task<RestCallResult<long>> TransferCrossMarginToSpotAsync(string asset, decimal quantity, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "currency", asset },
            { "amount", quantity },
        };

        return await SendHuobiRequest<long>(GetUrl(v1, crossMarginTransferOutEndpoint), HttpMethod.Post, ct, signed: true, bodyParameters: parameters).ConfigureAwait(false);
    }

    public async Task<RestCallResult<IEnumerable<HuobiLoanInfoAsset>>> GetCrossLoanInterestRateAndQuotaAsync(CancellationToken ct = default)
    {
        return await SendHuobiRequest<IEnumerable<HuobiLoanInfoAsset>>(GetUrl(v1, crossMarginLoanInfoEndpoint), HttpMethod.Get, ct, true).ConfigureAwait(false);
    }

    public async Task<RestCallResult<long>> RequestCrossMarginLoanAsync(string asset, decimal quantity, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>()
        {
            { "currency", asset },
            { "amount", quantity.ToString(CultureInfo.InvariantCulture) },
        };

        return await SendHuobiRequest<long>(GetUrl(v1, crossMarginOrdersEndpoint), HttpMethod.Post, ct, signed: true, bodyParameters: parameters).ConfigureAwait(false);
    }

    public async Task<RestCallResult<object>> RepayCrossMarginLoanAsync(string orderId, decimal quantity, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "amount", quantity.ToString(CultureInfo.InvariantCulture) }
        };

        var endpoint = crossMarginOrdersOrderIdRepayEndpoint.FillPathParameters(orderId.ToString(CultureInfo.InvariantCulture));
        return await SendHuobiRequest<object>(GetUrl(v1, endpoint), HttpMethod.Post, ct, signed: true, bodyParameters: parameters).ConfigureAwait(false);
    }

    public async Task<RestCallResult<IEnumerable<HuobiMarginOrder>>> GetCrossMarginClosedOrdersAsync(
        string asset = null,
        MarginOrderStatus? state = null,
        DateTime? startDate = null,
        DateTime? endDate = null,
        string from = null,
        FilterDirection? direction = null,
        int? limit = null,
        int? subUserId = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("currency", asset);
        parameters.AddOptionalParameter("state", EnumConverter.GetString(state));
        parameters.AddOptionalParameter("start-date", startDate == null ? null : startDate.Value.ToString("yyyy-mm-dd"));
        parameters.AddOptionalParameter("end-date", endDate == null ? null : endDate.Value.ToString("yyyy-mm-dd"));
        parameters.AddOptionalParameter("from", from);
        parameters.AddOptionalParameter("direct", EnumConverter.GetString(direction));
        parameters.AddOptionalParameter("size", limit);
        parameters.AddOptionalParameter("sub-uid", subUserId);

        return await SendHuobiRequest<IEnumerable<HuobiMarginOrder>>(GetUrl(v1, crossMarginLoanOrdersEndpoint), HttpMethod.Get, ct, signed: true, queryParameters: parameters).ConfigureAwait(false);
    }

    public async Task<RestCallResult<HuobiMarginBalances>> GetCrossMarginBalanceAsync(int? subUserId = null, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("sub-uid", subUserId);

        return await SendHuobiRequest<HuobiMarginBalances>(GetUrl(v1, crossMarginAccountsBalanceEndpoint), HttpMethod.Get, ct, signed: true, queryParameters: parameters).ConfigureAwait(false);
    }

    public async Task<RestCallResult<IEnumerable<HuobiRepayment>>> GetRepaymentHistoryAsync(long? repayId = null, long? accountId = null, string asset = null, DateTime? startTime = null, DateTime? endTime = null, string sort = null, int? limit = null, long? fromId = null, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("repayId", repayId);
        parameters.AddOptionalParameter("accountId", accountId);
        parameters.AddOptionalParameter("currency", asset);
        parameters.AddOptionalParameter("startTime", startTime.ConvertToMilliseconds());
        parameters.AddOptionalParameter("endTime", endTime.ConvertToMilliseconds());
        parameters.AddOptionalParameter("sort", sort);
        parameters.AddOptionalParameter("limit", limit);
        parameters.AddOptionalParameter("fromId", fromId);

        return await SendHuobiRequest<IEnumerable<HuobiRepayment>>(GetUrl(v2, accountRepaymentRecordsEndpoint), HttpMethod.Get, ct, signed: true, queryParameters: parameters).ConfigureAwait(false);
    }
}