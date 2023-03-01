using Huobi.Api.Models.RestApi.Wallet;

namespace Huobi.Api.Clients.RestApi;

public class RestApiWalletClient : RestApiBaseClient
{
    // Wallet Endpoints
    private const string accountDepositAddressEndpoint = "account/deposit/address";
    private const string accountWithdrawQuotaEndpoint = "account/withdraw/quota";
    private const string accountWithdrawAddressEndpoint = "account/withdraw/address";
    private const string dwWithdrawApiCreateEndpoint = "dw/withdraw/api/create";
    private const string queryWithdrawClientOrderIdEndpoint = "query/withdraw/client-order-id";
    private const string dwWithdrawVirtualWithdrawIdCancelEndpoint = "dw/withdraw-virtual/{}/cancel";
    private const string queryDepositWithdrawEndpoint = "query/deposit-withdraw";

    internal RestApiWalletClient(HuobiRestApiClient root) : base("Huobi Wallet RestApi", root)
    {
    }

    #region Linked Methods
    public async Task<RestCallResult<long>> GetUserIdAsync(CancellationToken ct = default)
        => await RootClient.Users.GetUserIdAsync(ct).ConfigureAwait(false);

    public async Task<RestCallResult<IEnumerable<Models.RestApi.Account.HuobiAccount>>> GetAccountsAsync(CancellationToken ct = default)
        => await RootClient.Wallet.GetAccountsAsync(ct).ConfigureAwait(false);
    #endregion

    public async Task<RestCallResult<IEnumerable<HuobiWalletAddress>>> GetDepositAddressAsync(string asset, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>() { { "currency", asset } };
        return await SendHuobiRequest<IEnumerable<HuobiWalletAddress>>(GetUrl(v2, accountDepositAddressEndpoint), HttpMethod.Get, ct, signed: true, queryParameters: parameters).ConfigureAwait(false);
    }

    public async Task<RestCallResult<HuobiWithdrawalQuota>> GetWithdrawalQuotaAsync(string asset, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>() { { "currency", asset } };
        return await SendHuobiRequest<HuobiWithdrawalQuota>(GetUrl(v2, accountWithdrawQuotaEndpoint), HttpMethod.Get, ct, signed: true, queryParameters: parameters).ConfigureAwait(false);
    }

    public async Task<RestCallResult<IEnumerable<HuobiWithdrawalAddress>>> GetWithdrawalAddressesAsync(string asset, string network = null, string note = null, int limit = 100, long? fromId = null, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "currency", asset },
            { "limit", limit },
        };
        parameters.AddOptionalParameter("chain", network);
        parameters.AddOptionalParameter("note", note);
        parameters.AddOptionalParameter("fromId", fromId);

        return await SendHuobiRequest<IEnumerable<HuobiWithdrawalAddress>>(GetUrl(v2, accountWithdrawAddressEndpoint), HttpMethod.Get, ct, signed: true, queryParameters: parameters).ConfigureAwait(false);
    }

    public async Task<RestCallResult<HuobiWithdrawalResponse>> WithdrawAsync(string address, string asset, decimal quantity, decimal fee, string network = null, string addressTag = null, string clientOrderId = null, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "address", address },
            { "currency", asset },
            { "amount", quantity },
            { "fee", fee },
        };
        parameters.AddOptionalParameter("chain", network);
        parameters.AddOptionalParameter("addr-tag", addressTag);
        parameters.AddOptionalParameter("client-order-id", clientOrderId);

        return await SendGenericRequest<HuobiWithdrawalResponse>(GetUrl(v1, dwWithdrawApiCreateEndpoint), HttpMethod.Post, ct, signed: true, bodyParameters: parameters).ConfigureAwait(false);
    }

    public async Task<RestCallResult<HuobiWithdrawal>> GetWithdrawalAsync(string clientOrderId, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "clientOrderId", clientOrderId },
        };

        return await SendGenericRequest<HuobiWithdrawal>(GetUrl(v1, queryWithdrawClientOrderIdEndpoint), HttpMethod.Get, ct, signed: true, queryParameters: parameters).ConfigureAwait(false);
    }

    public async Task<RestCallResult<long>> CancelWithdrawalAsync(long withdrawalId, CancellationToken ct = default)
    {
        var endpoint = dwWithdrawVirtualWithdrawIdCancelEndpoint.FillPathParameters(withdrawalId.ToString(CI));
        return await SendHuobiRequest<long>(GetUrl(v1, endpoint), HttpMethod.Post, ct, signed: true).ConfigureAwait(false);
    }

    public async Task<RestCallResult<IEnumerable<HuobiWalletTransaction>>> GetTransactionsAsync(WalletTransactionType type, string asset, long? fromId = null, int limit = 100, FilterDirection? direction = null, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "type", JsonConvert.SerializeObject(type, new WalletTransactionTypeConverter(false)) },
        };
        parameters.AddOptionalParameter("currency", asset);
        parameters.AddOptionalParameter("from", fromId);
        parameters.AddOptionalParameter("size", limit);
        parameters.AddOptionalParameter("direct", direction == null ? null : JsonConvert.SerializeObject(direction, new FilterDirectionConverter(false)));

        return await SendGenericRequest<IEnumerable<HuobiWalletTransaction>>(GetUrl(v1, queryDepositWithdrawEndpoint), HttpMethod.Get, ct, signed: true, queryParameters: parameters).ConfigureAwait(false);
    }

}