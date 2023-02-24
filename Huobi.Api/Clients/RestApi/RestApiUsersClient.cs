using Huobi.Api.Models.RestApi.Users;

namespace Huobi.Api.Clients.RestApi;

public class RestApiUsersClient : RestApiBaseClient
{
    // Sub-Account Endpoints
    private const string userUidEndpoint = "user/uid";
    private const string subUserUserListEndpoint = "sub-user/user-list";
    private const string subUserUserStateEndpoint = "sub-user/user-state";
    private const string subUserAccountListEndpoint = "sub-user/account-list";

    internal RestApiUsersClient(HuobiRestApiClient root) : base("Huobi SubUsers RestApi", root)
    {
    }

    public async Task<RestCallResult<long>> GetUserIdAsync(CancellationToken ct = default)
    {
        return await SendHuobiRequest<long>(GetUrl(v2, userUidEndpoint), HttpMethod.Get, ct, signed: true).ConfigureAwait(false);
    }

    public async Task<RestCallResult<IEnumerable<HuobiUser>>> GetSubUsersAsync(long? fromId = null, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("fromId", fromId);

        return await SendHuobiRequest<IEnumerable<HuobiUser>>(GetUrl(v2, subUserUserListEndpoint), HttpMethod.Get, ct, signed: true, queryParameters: parameters).ConfigureAwait(false);
    }

    public async Task<RestCallResult<HuobiUser>> GetSubUserStatusAsync(long subUserId, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>()
        {
            { "subUid", subUserId}
        };

        return await SendHuobiRequest<HuobiUser>(GetUrl(v2, subUserUserStateEndpoint), HttpMethod.Get, ct, signed: true, queryParameters: parameters).ConfigureAwait(false);
    }

    public async Task<RestCallResult<HuobiSubUserAccounts>> GetSubUserAccountsAsync(long subUserId, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>()
        {
            { "subUid", subUserId.ToString(CI)}
        };

        return await SendHuobiRequest<HuobiSubUserAccounts>(GetUrl(v2, subUserAccountListEndpoint), HttpMethod.Get, ct, signed: true, queryParameters: parameters).ConfigureAwait(false);
    }
}
