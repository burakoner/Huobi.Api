namespace Huobi.Api.Authentication;

internal class HuobiAuthenticationProvider : AuthenticationProvider
{
    public HuobiAuthenticationProvider(ApiCredentials credentials) : base(credentials)
    {
    }

    public override void AuthenticateRestApi(RestApiClient apiClient, Uri uri, HttpMethod method, bool signed, ArraySerialization serialization, SortedDictionary<string, object> query, SortedDictionary<string, object> body, string bodyContent, SortedDictionary<string, string> headers)
    {
        if (!signed && !((HuobiRestApiClientOptions)apiClient.ClientOptions).SignPublicRequests) return;

        // These are always in the uri
        query.Add("AccessKeyId", Credentials.Key!.GetString());
        query.Add("SignatureMethod", "HmacSHA256");
        query.Add("SignatureVersion", 2);
        query.Add("Timestamp", GetTimestamp(apiClient).ToString("yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture));

        var absolutePath = uri.AbsolutePath;
        if (absolutePath.StartsWith("/api"))
        {
            // Russian api has /api prefix which shouldn't be part of the signature
            absolutePath = absolutePath.Substring(4);
        }

        var sortedParameters = query.OrderBy(kv => Encoding.UTF8.GetBytes(WebUtility.UrlEncode(kv.Key)!), new ByteOrderComparer());
        var paramString = uri.SetParameters(sortedParameters, serialization).Query.Replace("?", "");
        paramString = new Regex(@"%[a-f0-9]{2}").Replace(paramString, m => m.Value.ToUpperInvariant());
        query.Add("Signature", SignHMACSHA256($"{method}\n{uri.Host}\n{absolutePath}\n{paramString}", SignatureOutputType.Base64));
    }

    public override void AuthenticateSocketApi()
    {
        throw new NotImplementedException();
    }

    public override void AuthenticateStreamApi()
    {
        throw new NotImplementedException();
    }

    public void AuthenticateStreamRequest(GateStreamRequest request)
    {
        var eventName = JsonConvert.SerializeObject(request.Event, new StreamRequestEventConverter(false));
        var signatureBody = $"channel={request.Channel}&event={eventName}&time={request.Timestamp}";
        var signature = SignHMACSHA512(signatureBody).ToLower();
        request.Auth = new StreamRequestAuth
        {
            Method = "api_key",
            ApiKey = Credentials.Key!.GetString(),
            Signature = signature
        };
    }
}
