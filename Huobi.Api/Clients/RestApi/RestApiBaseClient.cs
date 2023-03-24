namespace Huobi.Api.Clients.RestApi;

public abstract class RestApiBaseClient : RestApiClient
{
    // Api
    protected const string v0 = "";
    protected const string v1 = "1";
    protected const string v2 = "2";

    // Internal
    internal Log Log { get => this.log; }
    internal TimeSyncState TimeSyncState;

    // Root Client
    internal HuobiRestApiClient RootClient { get; }
    internal CultureInfo CI { get { return RootClient.CI; } }
    public new HuobiRestApiClientOptions ClientOptions { get { return RootClient.ClientOptions; } }

    internal RestApiBaseClient(string name, HuobiRestApiClient root) : base(name, root.ClientOptions)
    {
        RootClient = root;
        TimeSyncState = new(name);

        Thread.CurrentThread.CurrentCulture = CI;
        Thread.CurrentThread.CurrentUICulture = CI;

        RequestBodyFormat = RestRequestBodyFormat.Json;
        ArraySerialization = ArraySerialization.MultipleValues;
    }

    #region Override Methods
    protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
        => new HuobiAuthenticationProvider(credentials);

    protected override Error ParseErrorResponse(JToken error)
    {
        if (error["err-code"] == null || error["err-msg"] == null)
            return new ServerError(error.ToString());

        return new ServerError($"{(string)error["err-code"]!}, {(string)error["err-msg"]!}");
    }

    protected override Task<RestCallResult<DateTime>> GetServerTimestampAsync()
        => RootClient.Spot.Public.GetServerTimeAsync();

    protected override TimeSyncInfo GetTimeSyncInfo()
        => new(log, ClientOptions.AutoTimestamp, ClientOptions.TimestampRecalculationInterval, TimeSyncState);

    protected override TimeSpan GetTimeOffset()
        => TimeSyncState.TimeOffset;
    #endregion

    internal string ClientOrderId()
    {
        return ClientOptions.BrokerId + "-" + Guid.NewGuid().ToString();
    }

    internal string ClientOrderId(string clientOrderId)
    {
        clientOrderId.ValidateClientOrderId();
        return ClientOptions.BrokerId + "-" + (string.IsNullOrEmpty(clientOrderId) ? Guid.NewGuid().ToString() : clientOrderId);
    }

    #region Internal Methods
    internal Uri GetUrl(string version, string endpoint)
    {
        var address = ClientOptions.BaseAddress;
        if (!string.IsNullOrWhiteSpace(version)) address = address.AppendPath($"v{version}");
        if (!string.IsNullOrWhiteSpace(endpoint)) address = address.AppendPath(endpoint);

        return new Uri(address);
    }

    internal async Task<RestCallResult<T>> SendGenericRequest<T>(Uri uri, HttpMethod method, CancellationToken cancellationToken, bool signed = false, Dictionary<string, object> queryParameters = null, Dictionary<string, object> bodyParameters = null, Dictionary<string, string> headerParameters = null, ArraySerialization? serialization = null, JsonSerializer deserializer = null, bool ignoreRatelimit = false, int requestWeight = 1) where T : class
    {
        return await SendRequestAsync<T>(uri, method, cancellationToken, signed, queryParameters, bodyParameters, headerParameters, serialization, deserializer, ignoreRatelimit, requestWeight).ConfigureAwait(false);
    }

    internal async Task<RestCallResult<T>> SendHuobiRequest<T>( Uri uri, HttpMethod method, CancellationToken cancellationToken, bool signed = false, Dictionary<string, object> queryParameters = null, Dictionary<string, object> bodyParameters = null, Dictionary<string, string> headerParameters = null, ArraySerialization? serialization = null, JsonSerializer deserializer = null, bool ignoreRatelimit = false, int requestWeight = 1)
    {
        var result = await SendRequestAsync<HuobiApiResponse<T>>(uri, method, cancellationToken, signed, queryParameters, bodyParameters, headerParameters, serialization, deserializer, ignoreRatelimit, requestWeight).ConfigureAwait(false);

        if (!result || result.Data == null)
            return result.AsError<T>(result.Error!);

        if (!string.IsNullOrEmpty(result.Data.ErrorCode))
        {
            if (!result.Data.ErrorCode.IsNumeric() || Convert.ToInt32(result.Data.ErrorCode) != 200)
                return result.AsError<T>(new ServerError(result.Data.ErrorCode, result.Data.ErrorMessage));
        }

        return result.As(result.Data.Data);
    }
    #endregion

}