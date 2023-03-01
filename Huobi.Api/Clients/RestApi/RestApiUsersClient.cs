using Huobi.Api.Models.RestApi.Account;
using Huobi.Api.Models.RestApi.Users;
using System.Collections.Generic;

namespace Huobi.Api.Clients.RestApi;

public class RestApiUsersClient : RestApiBaseClient
{
    // Sub-Account Endpoints
    private const string userUidEndpoint = "user/uid";
    private const string subUserUserListEndpoint = "sub-user/user-list";
    private const string subUserCreationEndpoint = "sub-user/creation";
    private const string subUserManagementEndpoint = "sub-user/management";
    private const string subUserUserStateEndpoint = "sub-user/user-state";
    private const string subUserTradableMarketEndpoint = "sub-user/tradable-market";
    private const string subUserTransferabilityEndpoint = "sub-user/transferability";
    private const string subUserAccountListEndpoint = "sub-user/account-list";

    private const string userApiKeyEndpoint = "user/api-key";
    private const string subUserApiKeyGenerationEndpoint = "sub-user/api-key-generation";
    private const string subUserApiKeyModificationEndpoint = "sub-user/api-key-modification";
    private const string subUserApiKeyDeletionEndpoint = "sub-user/api-key-deletion";
    private const string subUserTransferEndpoint = "subuser/transfer"; // OK
    private const string subUserDepositAddressEndpoint = "sub-user/deposit-address";
    private const string subUserQueryDepositEndpoint = "sub-user/query-deposit";
    private const string subUserAggregateBalanceEndpoint = "subuser/aggregate-balance";
    private const string accountAccountsSubUidEndpoint = "account/accounts/{sub-uid}"; // OK

    internal RestApiUsersClient(HuobiRestApiClient root) : base("Huobi SubUsers RestApi", root)
    {
    }

    // TODO: API key query

    #region Get UID
    public async Task<RestCallResult<long>> GetUserIdAsync(CancellationToken ct = default)
    {
        return await SendHuobiRequest<long>(GetUrl(v2, userUidEndpoint), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
    }
    #endregion

    #region Sub user creation
    public async Task<RestCallResult<IEnumerable<HuobiSubUserResponse>>> CreateSubUserAccountAsync(IEnumerable<HuobiSubUserRequest> requests, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>()
        {
            { "userList", requests }
        };

        return await SendHuobiRequest<IEnumerable<HuobiSubUserResponse>>(GetUrl(v2, subUserCreationEndpoint), HttpMethod.Post, ct, signed: true, bodyParameters: parameters).ConfigureAwait(false);
    }
    #endregion

    #region Get Sub User's List
    public async Task<RestCallResult<IEnumerable<HuobiSubUser>>> GetSubUsersAsync(long? fromId = null, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("fromId", fromId);

        return await SendHuobiRequest<IEnumerable<HuobiSubUser>>(GetUrl(v2, subUserUserListEndpoint), HttpMethod.Get, ct, signed: true, queryParameters: parameters).ConfigureAwait(false);
    }
    #endregion

    #region Lock/Unlock Sub User
    public async Task<RestCallResult<HuobiSubUser>> SetSubUserStatusAsync(long subUserId, SubUserStatusAction action, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>()
        {
            { "subUid", subUserId },
            { "action", JsonConvert.SerializeObject(action, new SubUserStatusActionConverter(false)) },
        };

        return await SendHuobiRequest<HuobiSubUser>(GetUrl(v2, subUserManagementEndpoint), HttpMethod.Post, ct, signed: true, bodyParameters: parameters).ConfigureAwait(false);
    }
    #endregion

    #region Get Sub User's Status
    public async Task<RestCallResult<HuobiSubUser>> GetSubUserStatusAsync(long subUserId, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>()
        {
            { "subUid", subUserId },
        };

        return await SendHuobiRequest<HuobiSubUser>(GetUrl(v2, subUserUserStateEndpoint), HttpMethod.Get, ct, signed: true, queryParameters: parameters).ConfigureAwait(false);
    }
    #endregion

    #region Set Tradable Market for Sub Users
    public async Task<RestCallResult<IEnumerable<HuobiSubUserActivation>>> SetSubUserTradableMarketsAsync(List<long> subUserIds, AccountMarginMode marginMode, AccountActivation activation, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>()
        {
            { "subUids", string.Join(",", subUserIds) },
            { "accountType", JsonConvert.SerializeObject(marginMode, new AccountMarginModeConverter(false)) },
            { "activation", JsonConvert.SerializeObject(activation, new AccountActivationConverter(false)) },
        };

        return await SendHuobiRequest<IEnumerable<HuobiSubUserActivation>>(GetUrl(v2, subUserTradableMarketEndpoint), HttpMethod.Post, ct, signed: true, bodyParameters: parameters).ConfigureAwait(false);
    }
    #endregion

    #region Set Asset Transfer Permission for Sub Users
    public async Task<RestCallResult<IEnumerable<HuobiSubUserTransferrable>>> SetSubUserTransferPermissionAsync(List<long> subUserIds, bool transferrable, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>()
        {
            { "subUids", string.Join(",", subUserIds) },
            { "accountType", "spot" },
            { "transferrable", transferrable },
        };

        return await SendHuobiRequest<IEnumerable<HuobiSubUserTransferrable>>(GetUrl(v2, subUserTransferabilityEndpoint), HttpMethod.Post, ct, signed: true, bodyParameters: parameters).ConfigureAwait(false);
    }
    #endregion

    #region Get Sub User's Account List
    public async Task<RestCallResult<HuobiSubUserAccounts>> GetSubUserAccountsAsync(long subUserId, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>()
        {
            { "subUid", subUserId.ToString(CI)}
        };

        return await SendHuobiRequest<HuobiSubUserAccounts>(GetUrl(v2, subUserAccountListEndpoint), HttpMethod.Get, ct, signed: true, queryParameters: parameters).ConfigureAwait(false);
    }
    #endregion

    // TODO: Sub user API key creation
    // TODO: Sub user API key modification
    // TODO: Sub user API key deletion

    #region Transfer Asset between Parent and Sub Account
    public async Task<RestCallResult<long>> TransferWithSubAccountAsync(long subAccountId, string asset, decimal quantity, TransferType transferType, CancellationToken ct = default)
    {
        asset.ValidateNotNull(nameof(asset));
        var parameters = new Dictionary<string, object>
            {
                { "sub-uid", subAccountId },
                { "currency", asset },
                { "amount", quantity },
                { "type", JsonConvert.SerializeObject(transferType, new TransferTypeConverter(false)) }
            };

        return await SendHuobiRequest<long>(GetUrl(v1, subUserTransferEndpoint), HttpMethod.Post, ct, signed: true, bodyParameters: parameters).ConfigureAwait(false);
    }
    #endregion

    // TODO: Query Deposit Address of Sub User
    // TODO: Query Deposit History of Sub User
    // TODO: Get the Aggregated Balance of all Sub-users

    #region Get Account Balance of a Sub-User
    public async Task<RestCallResult<IEnumerable<HuobiAccountBalance>>> GetSubAccountBalancesAsync(long subAccountId, CancellationToken ct = default)
    {
        var endpoint = accountAccountsSubUidEndpoint.FillPathParameters(subAccountId.ToString(CultureInfo.InvariantCulture));
        var result = await SendHuobiRequest<IEnumerable<HuobiAccountBalances>>(GetUrl(v1, endpoint), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
        if (!result) return result.AsError<IEnumerable<HuobiAccountBalance>>(result.Error!);

        return result.As(result.Data.First().Data);
    }
    #endregion

}
