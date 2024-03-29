﻿namespace Huobi.Api.Authentication;

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

    public override void AuthenticateTcpSocketApi()
    {
        throw new NotImplementedException();
    }

    public override void AuthenticateWebSocketApi()
    {
        throw new NotImplementedException();
    }

    public HuobiAuthenticationRequest GetWebsocketAuthentication(Uri uri)
    {
        var parameters = new Dictionary<string, object>();
        parameters.Add("accessKey", Credentials.Key!.GetString());
        parameters.Add("signatureMethod", "HmacSHA256");
        parameters.Add("signatureVersion", 2.1);
        parameters.Add("timestamp", DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture));

        var sortedParameters = parameters.OrderBy(kv => Encoding.UTF8.GetBytes(WebUtility.UrlEncode(kv.Key)!), new ByteOrderComparer());
        var paramString = uri.SetParameters(sortedParameters, ArraySerialization.Array).Query.Replace("?", "");
        paramString = new Regex(@"%[a-f0-9]{2}").Replace(paramString, m => m.Value.ToUpperInvariant()).Replace("%2C", ".");
        var signData = $"GET\n{uri.Host}\n{uri.AbsolutePath}\n{paramString}";
        var signature = SignHMACSHA256(signData, SignatureOutputType.Base64);

        return new HuobiAuthenticationRequest(Credentials.Key!.GetString(), (string)parameters["timestamp"], signature);
    }

    public HuobiAuthenticationRequest2 GetWebsocketAuthentication2(Uri uri)
    {
        var parameters = new Dictionary<string, object>();
        parameters.Add("AccessKeyId", Credentials.Key!.GetString());
        parameters.Add("SignatureMethod", "HmacSHA256");
        parameters.Add("SignatureVersion", 2);
        parameters.Add("Timestamp", DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture));

        var sortedParameters = parameters.OrderBy(kv => Encoding.UTF8.GetBytes(WebUtility.UrlEncode(kv.Key)!), new ByteOrderComparer());
        var paramString = uri.SetParameters(sortedParameters, ArraySerialization.Array).Query.Replace("?", "");
        paramString = new Regex(@"%[a-f0-9]{2}").Replace(paramString, m => m.Value.ToUpperInvariant()).Replace("%2C", ".");
        var signData = $"GET\n{uri.Host}\n{uri.AbsolutePath}\n{paramString}";
        var signature = SignHMACSHA256(signData, SignatureOutputType.Base64);

        return new HuobiAuthenticationRequest2(Credentials.Key!.GetString(), (string)parameters["Timestamp"], signature);
    }
}
