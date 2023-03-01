using Huobi.Api.Models.RestApi.Account;

namespace Huobi.Api.Clients.RestApi;

public class RestApiAccountClient : RestApiBaseClient
{
    // Account Endpoints
    private const string accountAccountsEndpoint = "account/accounts";
    private const string accountAccountsAccountIdBalanceEndpoint = "account/accounts/{}/balance";
    private const string accountValuationEndpoint = "account/valuation";
    private const string accountAssetValuationEndpoint = "account/asset-valuation";
    private const string accountTransferEndpoint = "account/transfer";
    private const string accountHistoryEndpoint = "account/history";
    private const string accountLedgerEndpoint = "account/ledger";
    private const string futuresTransferEndpoint = "futures/transfer";
    private const string pointAccountEndpoint = "point/account";
    private const string pointTransferEndpoint = "point/transfer";

    internal RestApiAccountClient(HuobiRestApiClient root) : base("Huobi Wallet RestApi", root)
    {
    }

    #region Linked Methods
    public async Task<RestCallResult<long>> GetUserIdAsync(CancellationToken ct = default)
        => await RootClient.Users.GetUserIdAsync(ct).ConfigureAwait(false);
    #endregion

    public async Task<RestCallResult<IEnumerable<HuobiAccount>>> GetAccountsAsync(CancellationToken ct = default)
    {
        return await SendHuobiRequest<IEnumerable<HuobiAccount>>(GetUrl(v1, accountAccountsEndpoint), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
    }

    public async Task<RestCallResult<IEnumerable<HuobiAccountBalance>>> GetBalancesAsync(long accountId, CancellationToken ct = default)
    {
        var endpoint = accountAccountsAccountIdBalanceEndpoint.FillPathParameters(accountId.ToString(CI));
        var result = await SendHuobiRequest<HuobiAccountBalances>(GetUrl(v1, endpoint), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        if (!result) return result.AsError<IEnumerable<HuobiAccountBalance>>(result.Error!);

        return result.As(result.Data.Data);
    }

    public async Task<RestCallResult<HuobiAccountValuation>> GetAccountValuationAsync(AccountType? accountType=null, string valuationCurrency = "BTC", CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>()
        {
            { "valuationCurrency", valuationCurrency },
        };
        parameters.AddOptionalParameter("accountType", JsonConvert.SerializeObject(accountType, new AccountTypeConverter(false)));

        return await SendHuobiRequest<HuobiAccountValuation>(GetUrl(v2, accountValuationEndpoint), HttpMethod.Get, ct, signed: true, queryParameters: parameters).ConfigureAwait(false);
    }

    public async Task<RestCallResult<HuobiAccountAssetValuation>> GetAssetValuationAsync(AccountType accountType, string valuationCurrency = "BTC", long? subUserId = null, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>()
        {
            { "accountType", JsonConvert.SerializeObject(accountType, new AccountTypeConverter(false))},
            { "valuationCurrency", valuationCurrency },
        };
        parameters.AddOptionalParameter("subUid", subUserId);

        return await SendHuobiRequest<HuobiAccountAssetValuation>(GetUrl(v2, accountAssetValuationEndpoint), HttpMethod.Get, ct, signed: true, queryParameters: parameters).ConfigureAwait(false);
    }

    public async Task<RestCallResult<HuobiAccountTransaction>> TransferAssetAsync(
    long fromUserId, AccountType fromAccountType, long fromAccountId,
    long toUserId, AccountType toAccountType, long toAccountId, 
    string asset, decimal quantity, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>()
        {
            { "from-account-id", fromAccountId.ToString(CI)},
            { "from-user", fromUserId.ToString(CI)},
            { "from-account-type", JsonConvert.SerializeObject(fromAccountType, new AccountTypeConverter(false))},

            { "to-account-id", toAccountId.ToString(CI)},
            { "to-user", toUserId.ToString(CI)},
            { "to-account-type", JsonConvert.SerializeObject(toAccountType, new AccountTypeConverter(false))},

            { "currency", asset },
            { "amount", quantity.ToString(CI) },
        };

        return await SendHuobiRequest<HuobiAccountTransaction>(GetUrl(v1, accountTransferEndpoint), HttpMethod.Post, ct, signed: true, bodyParameters: parameters).ConfigureAwait(false);
    }

    public async Task<RestCallResult<IEnumerable<HuobiAccountHistory>>> GetAccountHistoryAsync(long accountId, string asset = null, IEnumerable<TransactionType> transactionTypes = null, DateTime? startTime = null, DateTime? endTime = null, SortingType? sort = null, int? size = null, CancellationToken ct = default)
    {
        size?.ValidateIntBetween(nameof(size), 1, 500);

        var transactionTypeConverter = new TransactionTypeConverter(false);
        var parameters = new Dictionary<string, object>
        {
            { "account-id", accountId }
        };
        parameters.AddOptionalParameter("currency", asset);
        parameters.AddOptionalParameter("transact-types", transactionTypes == null ? null : string.Join(",", transactionTypes.Select(s => JsonConvert.SerializeObject(s, transactionTypeConverter))));
        parameters.AddOptionalParameter("start-time", startTime.ConvertToMilliseconds());
        parameters.AddOptionalParameter("end-time", endTime.ConvertToMilliseconds());
        parameters.AddOptionalParameter("sort", sort == null ? null : JsonConvert.SerializeObject(sort, new SortingTypeConverter(false)));
        parameters.AddOptionalParameter("size", size);

        return await SendHuobiRequest<IEnumerable<HuobiAccountHistory>>(GetUrl(v1, accountHistoryEndpoint), HttpMethod.Get, ct, signed: true, queryParameters: parameters).ConfigureAwait(false);
    }

    public async Task<RestCallResult<IEnumerable<HuobiAccountLedger>>> GetAccountLedgerAsync(long accountId, string asset = null, IEnumerable<TransactionType> transactionTypes = null, DateTime? startTime = null, DateTime? endTime = null, SortingType? sort = null, int? size = null, long? fromId = null, CancellationToken ct = default)
    {
        size?.ValidateIntBetween(nameof(size), 1, 500);

        var transactionTypeConverter = new TransactionTypeConverter(false);
        var parameters = new Dictionary<string, object>
        {
            { "accountId", accountId }
        };
        parameters.AddOptionalParameter("currency", asset);
        parameters.AddOptionalParameter("transactTypes", transactionTypes == null ? null : string.Join(",", transactionTypes.Select(s => JsonConvert.SerializeObject(s, transactionTypeConverter))));
        parameters.AddOptionalParameter("startTime", startTime.ConvertToMilliseconds());
        parameters.AddOptionalParameter("endTime", endTime.ConvertToMilliseconds());
        parameters.AddOptionalParameter("sort", sort == null ? null : JsonConvert.SerializeObject(sort, new SortingTypeConverter(false)));
        parameters.AddOptionalParameter("limit", size);
        parameters.AddOptionalParameter("fromId", fromId?.ToString(CI));

        return await SendHuobiRequest<IEnumerable<HuobiAccountLedger>>(GetUrl(v2, accountLedgerEndpoint), HttpMethod.Get, ct, signed: true, queryParameters: parameters).ConfigureAwait(false);
    }

    public async Task<RestCallResult<long>> FuturesTransferAsync(string asset, decimal quantity, FuturesTransferType type, CancellationToken ct = default)
    {
        asset.ValidateNotNull(nameof(asset));
        var parameters = new Dictionary<string, object>
        {
            { "currency", asset },
            { "amount", quantity },
            { "type", JsonConvert.SerializeObject(type, new FuturesTransferTypeConverter(false)) }
        };

        return await SendHuobiRequest<long>(GetUrl(v1, futuresTransferEndpoint), HttpMethod.Post, ct, signed: true, bodyParameters: parameters).ConfigureAwait(false);
    }

    public async Task<RestCallResult<HuobiAccountPointBalance>> GetPointBalancesAsync(long? subUserId = null, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("subUid", subUserId);

        return await SendHuobiRequest<HuobiAccountPointBalance>(GetUrl(v2, pointAccountEndpoint), HttpMethod.Get, ct, signed: true, queryParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// <b>Point Transfer</b><br/>
    /// Via this endpoint, parent user should be able to transfer points between parent user and sub user, sub user should be able to transfer point to parent user. Both ‘termless’ and ‘terminable’ points are transferrable.<br/>
    /// Via this endpoint, user could only transfer ‘termless’ and ‘terminable’ points instead of any other cryptocurrencies.<br/>
    /// Parent user could transfer point between parent user and sub user in two ways.<br/>
    /// Sub user could only transfer point from sub user to parent user.<br/>
    /// Before parent user trying to transfer the terminable points back from sub user's account, parent user should query the sub user's point balance first in order to get the corresponding groupId.<br/>
    /// <br/>
    /// API Key Permission：Trade<br/>
    /// Rate Limit: 2times/s<br/>
    /// Callable by sub user<br/>
    /// </summary>
    /// <param name="fromUserId">Transferer’s UID</param>
    /// <param name="toUserId">Transferee’s UID</param>
    /// <param name="groupId">Group ID</param>
    /// <param name="quantity">Transfer amount (precision: maximum 8 decimal places)</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<HuobiAccountTransaction>> TransferPointsAsync(long fromUserId, long toUserId, long groupId, decimal quantity, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>()
        {
            { "fromUid", fromUserId.ToString(CI) },
            { "toUserId", toUserId.ToString(CI) },
            { "groupId", groupId },
            { "amount", quantity.ToString(CI) },
        };

        return await SendHuobiRequest<HuobiAccountTransaction>(GetUrl(v2, pointTransferEndpoint), HttpMethod.Post, ct, signed: true, bodyParameters: parameters).ConfigureAwait(false);
    }

}