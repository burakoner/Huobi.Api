using Huobi.Api.Models.StreamApi;

namespace Huobi.Api.Clients.StreamApi;

public abstract class StreamApiBaseClient : StreamApiClient
{
    // Internal
    internal Log Log { get => this.log; }
    internal HuobiStreamClient RootClient { get; }

    // Options
    public new HuobiStreamClientOptions ClientOptions { get { return (HuobiStreamClientOptions)base.ClientOptions; } }

    internal StreamApiBaseClient(HuobiStreamClient root) : base("Huobi Stream", root.ClientOptions)
    {
        RootClient = root;
        KeepAliveInterval = TimeSpan.Zero;

        SetDataInterpreter(DecompressData, null);
        AddGenericHandler("PingV1", PingHandlerV1);
        AddGenericHandler("PingV2", PingHandlerV2);
        AddGenericHandler("PingV3", PingHandlerV3);
    }

    #region Override Methods
    protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
        => new HuobiAuthenticationProvider(credentials);

    protected override bool HandleQueryResponse<T>(StreamConnection connection, object request, JToken data, out CallResult<T> callResult)
    {
        callResult = new CallResult<T>(default(T)!);
        var v1Data = (data["data"] != null || data["tick"] != null) && data["rep"] != null;
        var v1Error = data["status"] != null && data["status"]!.ToString() == "error";
        var isV1QueryResponse = v1Data || v1Error;
        if (isV1QueryResponse)
        {
            var hRequest = (HuobiSocketRequest)request;
            var id = data["id"];
            if (id == null)
                return false;

            if (id.ToString() != hRequest.Id)
                return false;

            if (v1Error)
            {
                var error = new ServerError(data["err-msg"]!.ToString());
                callResult = new CallResult<T>(error);
                return true;
            }

            var desResult = Deserialize<T>(data);
            if (!desResult)
            {
                log.Write(LogLevel.Warning, $"Socket {connection.Id} Failed to deserialize data: {desResult.Error}. Data: {data}");
                callResult = new CallResult<T>(desResult.Error!);
                return true;
            }

            callResult = new CallResult<T>(desResult.Data);
            return true;
        }

        var action = data["action"]?.ToString();
        var isV2Response = action == "req";
        if (isV2Response)
        {
            var hRequest = (HuobiAuthenticatedSubscribeRequest)request;
            var channel = data["ch"]?.ToString();
            if (channel != hRequest.Channel)
                return false;

            var desResult = Deserialize<T>(data);
            if (!desResult)
            {
                log.Write(LogLevel.Warning, $"Socket {connection.Id} Failed to deserialize data: {desResult.Error}. Data: {data}");
                return false;
            }

            callResult = new CallResult<T>(desResult.Data);
            return true;
        }

        return false;
    }

    protected override bool HandleSubscriptionResponse(StreamConnection connection, StreamSubscription subscription, object request, JToken message, out CallResult<object> callResult)
    {
        callResult = null;
        var status = message["status"]?.ToString();
        var isError = status == "error";
        if (isError)
        {
            if (request is HuobiSubscribeRequest hRequest)
            {
                var subResponse = Deserialize<HuobiSubscribeResponse>(message);
                if (!subResponse)
                {
                    Log.Write(LogLevel.Warning, $"Socket {connection.Id} Subscription failed: " + subResponse.Error);
                    return false;
                }

                var id = subResponse.Data.Id;
                if (id != hRequest.Id)
                    return false; // Not for this request

                Log.Write(LogLevel.Warning, $"Socket {connection.Id} Subscription failed: " + subResponse.Data.ErrorMessage);
                callResult = new CallResult<object>(new ServerError($"{subResponse.Data.ErrorCode}, {subResponse.Data.ErrorMessage}"));
                return true;
            }

            if (request is HuobiAuthenticatedSubscribeRequest haRequest)
            {
                var subResponse = Deserialize<HuobiAuthSubscribeResponse>(message);
                if (!subResponse)
                {
                    Log.Write(LogLevel.Warning, $"Socket {connection.Id} Subscription failed: " + subResponse.Error);
                    callResult = new CallResult<object>(subResponse.Error!);
                    return false;
                }

                var id = subResponse.Data.Channel;
                if (id != haRequest.Channel)
                    return false; // Not for this request

                Log.Write(LogLevel.Warning, $"Socket {connection.Id} Subscription failed: " + subResponse.Data.Code);
                callResult = new CallResult<object>(new ServerError(subResponse.Data.Code, "Failed to subscribe"));
                return true;
            }
        }

        var v1Sub = message["subbed"] != null;
        if (v1Sub)
        {
            var subResponse = Deserialize<HuobiSubscribeResponse>(message);
            if (!subResponse)
            {
                Log.Write(LogLevel.Warning, $"Socket {connection.Id} Subscription failed: " + subResponse.Error);
                return false;
            }

            var hRequest = (HuobiSubscribeRequest)request;
            if (subResponse.Data.Id != hRequest.Id)
                return false;

            if (!subResponse.Data.IsSuccessful)
            {
                Log.Write(LogLevel.Warning, $"Socket {connection.Id} Subscription failed: " + subResponse.Data.ErrorMessage);
                callResult = new CallResult<object>(new ServerError($"{subResponse.Data.ErrorCode}, {subResponse.Data.ErrorMessage}"));
                return true;
            }

            Log.Write(LogLevel.Debug, $"Socket {connection.Id} Subscription completed");
            callResult = new CallResult<object>(subResponse.Data);
            return true;
        }

        var action = message["action"]?.ToString();
        var v2Sub = action == "sub";
        if (v2Sub)
        {
            var subResponse = Deserialize<HuobiAuthSubscribeResponse>(message);
            if (!subResponse)
            {
                Log.Write(LogLevel.Warning, $"Socket {connection.Id} Subscription failed: " + subResponse.Error);
                callResult = new CallResult<object>(subResponse.Error!);
                return false;
            }

            var hRequest = (HuobiAuthenticatedSubscribeRequest)request;
            if (subResponse.Data.Channel != hRequest.Channel)
                return false;

            if (!subResponse.Data.IsSuccessful)
            {
                Log.Write(LogLevel.Warning, $"Socket {connection.Id} Subscription failed: " + subResponse.Data.Message);
                callResult = new CallResult<object>(new ServerError(subResponse.Data.Code, subResponse.Data.Message));
                return true;
            }

            Log.Write(LogLevel.Debug, $"Socket {connection.Id} Subscription completed");
            callResult = new CallResult<object>(subResponse.Data);
            return true;
        }

        var operation = message["op"]?.ToString();
        var usdtMarginSub = operation == "sub";
        if (usdtMarginSub)
        {
            var subResponse = Deserialize<HuobiSocketResponse2>(message);
            if (!subResponse)
            {
                Log.Write(LogLevel.Warning, $"Socket {connection.Id} Subscription failed: " + subResponse.Error);
                callResult = new CallResult<object>(subResponse.Error!);
                return false;
            }

            var hRequest = (HuobiSocketRequest2)request;
            if (subResponse.Data.Topic != hRequest.Topic)
                return false;

            if (!subResponse.Data.IsSuccessful)
            {
                Log.Write(LogLevel.Warning, $"Socket {connection.Id} Subscription failed: " + subResponse.Data.ErrorMessage);
                callResult = new CallResult<object>(new ServerError(subResponse.Data.ErrorCode + " - " + subResponse.Data.ErrorMessage));
                return true;
            }

            Log.Write(LogLevel.Debug, $"Socket {connection.Id} Subscription completed");
            callResult = new CallResult<object>(subResponse.Data);
            return true;
        }

        return false;
    }

    protected override bool MessageMatchesHandler(StreamConnection connection, JToken message, object request)
    {
        if (request is HuobiSubscribeRequest hRequest)
            return hRequest.Topic == message["ch"]?.ToString();

        if (request is HuobiAuthenticatedSubscribeRequest haRequest)
            return haRequest.Channel == message["ch"]?.ToString();

        if (request is HuobiSocketRequest2 hRequest2)
        {
            if (hRequest2.Topic == message["topic"]?.ToString())
                return true;

            if (hRequest2.Topic.Contains("*") && hRequest2.Topic.Split('.')[0] == message["topic"]?.ToString().Split('.')[0])
                return true;
        }

        return false;
    }

    protected override bool MessageMatchesHandler(StreamConnection connection, JToken message, string identifier)
    {
        if (message.Type != JTokenType.Object)
            return false;

        if (identifier == "PingV1" && message["ping"] != null)
            return true;

        if (identifier == "PingV2" && message["action"]?.ToString() == "ping")
            return true;

        if (identifier == "PingV3" && message["op"]?.ToString() == "ping")
            return true;

        return false;
    }

    protected override abstract Task<CallResult<bool>> AuthenticateAsync(StreamConnection connection);

    protected async Task<CallResult<bool>> SpotAuthenticateAsync(StreamConnection connection)
    {
        if (AuthenticationProvider == null)
            return new CallResult<bool>(new NoApiCredentialsError());

        var result = new CallResult<bool>(new ServerError("No response from server"));
        await connection.SendAndWaitAsync(((HuobiAuthenticationProvider)AuthenticationProvider).GetWebsocketAuthentication(connection.ConnectionUri), ClientOptions.ResponseTimeout, data =>
        {
            if (data["ch"]?.ToString() != "auth")
                return false;

            var authResponse = Deserialize<HuobiAuthSubscribeResponse>(data);
            if (!authResponse)
            {
                Log.Write(LogLevel.Warning, $"Socket {connection.Id} Authorization failed: " + authResponse.Error);
                result = new CallResult<bool>(authResponse.Error!);
                return true;
            }
            if (!authResponse.Data.IsSuccessful)
            {
                Log.Write(LogLevel.Warning, $"Socket {connection.Id} Authorization failed: " + authResponse.Data.Message);
                result = new CallResult<bool>(new ServerError(authResponse.Data.Code, authResponse.Data.Message));
                return true;
            }

            Log.Write(LogLevel.Debug, $"Socket {connection.Id} Authorization completed");
            result = new CallResult<bool>(true);
            return true;
        }).ConfigureAwait(false);

        return result;
    }

    protected async Task<CallResult<bool>> FuturesAuthenticateAsync(StreamConnection connection)
    {
        if (AuthenticationProvider == null)
            return new CallResult<bool>(new NoApiCredentialsError());

        var result = new CallResult<bool>(new ServerError("No response from server"));
        await connection.SendAndWaitAsync(((HuobiAuthenticationProvider)AuthenticationProvider).GetWebsocketAuthentication2(connection.ConnectionUri), ClientOptions.ResponseTimeout, data =>
        {
            if (data["op"]?.ToString() != "auth")
                return false;

            var authResponse = Deserialize<HuobiAuthResponse>(data);
            if (!authResponse)
            {
                Log.Write(LogLevel.Warning, $"Socket {connection.Id} Authorization failed: " + authResponse.Error);
                result = new CallResult<bool>(authResponse.Error!);
                return true;
            }
            if (!authResponse.Data.IsSuccessful)
            {
                Log.Write(LogLevel.Warning, $"Socket {connection.Id} Authorization failed: " + authResponse.Data.Message);
                result = new CallResult<bool>(new ServerError(authResponse.Data.Code, authResponse.Data.Message));
                return true;
            }

            Log.Write(LogLevel.Debug, $"Socket {connection.Id} Authorization completed");
            result = new CallResult<bool>(true);
            return true;
        }).ConfigureAwait(false);

        return result;
    }

    protected async Task<CallResult<bool>> SwapAuthenticateAsync(StreamConnection connection)
    {
        if (AuthenticationProvider == null)
            return new CallResult<bool>(new NoApiCredentialsError());

        var result = new CallResult<bool>(new ServerError("No response from server"));
        await connection.SendAndWaitAsync(((HuobiAuthenticationProvider)AuthenticationProvider).GetWebsocketAuthentication2(connection.ConnectionUri), ClientOptions.ResponseTimeout, data =>
        {
            if (data["op"]?.ToString() != "auth")
                return false;

            var authResponse = Deserialize<HuobiAuthResponse>(data);
            if (!authResponse)
            {
                Log.Write(LogLevel.Warning, $"Socket {connection.Id} Authorization failed: " + authResponse.Error);
                result = new CallResult<bool>(authResponse.Error!);
                return true;
            }
            if (!authResponse.Data.IsSuccessful)
            {
                Log.Write(LogLevel.Warning, $"Socket {connection.Id} Authorization failed: " + authResponse.Data.Message);
                result = new CallResult<bool>(new ServerError(authResponse.Data.Code, authResponse.Data.Message));
                return true;
            }

            Log.Write(LogLevel.Debug, $"Socket {connection.Id} Authorization completed");
            result = new CallResult<bool>(true);
            return true;
        }).ConfigureAwait(false);

        return result;
    }

    protected override async Task<bool> UnsubscribeAsync(StreamConnection connection, StreamSubscription subscription)
    {
        var result = false;
        if (subscription.Request is HuobiSubscribeRequest hRequest)
        {
            var unsubId = NextId().ToString();
            var unsub = new HuobiUnsubscribeRequest(unsubId, hRequest.Topic);

            await connection.SendAndWaitAsync(unsub, ClientOptions.ResponseTimeout, data =>
            {
                if (data.Type != JTokenType.Object)
                    return false;

                var id = data["id"]?.ToString();
                if (id == unsubId)
                {
                    result = data["status"]?.ToString() == "ok";
                    return true;
                }

                return false;
            }).ConfigureAwait(false);
            return result;
        }

        if (subscription.Request is HuobiAuthenticatedSubscribeRequest haRequest)
        {
            var unsub = new Dictionary<string, object>()
                {
                    { "action", "unsub" },
                    { "ch", haRequest.Channel },
                };

            await connection.SendAndWaitAsync(unsub, ClientOptions.ResponseTimeout, data =>
            {
                if (data.Type != JTokenType.Object)
                    return false;

                if (data["action"]?.ToString() == "unsub" && data["ch"]?.ToString() == haRequest.Channel)
                {
                    result = data["code"]?.Value<int>() == 200;
                    return true;
                }

                return false;
            }).ConfigureAwait(false);
            return result;
        }

        if (subscription.Request is HuobiSocketRequest2 hRequest2)
        {
            var unsub = new
            {
                op = "unsub",
                topic = hRequest2.Topic,
                cid = NextId().ToString()
            };
            await connection.SendAndWaitAsync(unsub, ClientOptions.ResponseTimeout, data =>
            {
                if (data.Type != JTokenType.Object)
                    return false;

                if (data["op"]?.ToString() == "unsub" && data["cid"]?.ToString() == unsub.cid)
                {
                    result = data["err-code"]?.Value<int>() == 0;
                    return true;
                }

                return false;
            }).ConfigureAwait(false);
            return result;
        }

        throw new InvalidOperationException("Unknown request type");
    }
    #endregion

    #region Internal Methods
    internal void DeserializeAndInvoke<T>(StreamDataEvent<JToken> data, Action<StreamDataEvent<T>> action, string symbol = null)
    {
        var obj = Deserialize<T>(data.Data["data"]!);
        if (!obj)
        {
            Log.Write(LogLevel.Error, $"Failed to deserialize {typeof(T).Name}: " + obj.Error);
            return;
        }
        action?.Invoke(data.As(obj.Data, symbol));
    }

    private static string DecompressData(byte[] byteData)
    {
        using var decompressedStream = new MemoryStream();
        using var compressedStream = new MemoryStream(byteData);
        using var deflateStream = new GZipStream(compressedStream, CompressionMode.Decompress);
        deflateStream.CopyTo(decompressedStream);
        decompressedStream.Position = 0;

        using var streamReader = new StreamReader(decompressedStream);
        return streamReader.ReadToEnd();
    }

    private void PingHandlerV1(StreamMessageEvent messageEvent)
    {
        var v1Ping = messageEvent.JsonData["ping"] != null;

        if (v1Ping)
            messageEvent.Connection.Send(new HuobiPingResponse(messageEvent.JsonData["ping"]!.Value<long>()));
    }

    private void PingHandlerV2(StreamMessageEvent messageEvent)
    {
        var v2Ping = messageEvent.JsonData["action"]?.ToString() == "ping";

        if (v2Ping)
            messageEvent.Connection.Send(new HuobiPingAuthResponse(messageEvent.JsonData["data"]!["ts"]!.Value<long>()));
    }

    private void PingHandlerV3(StreamMessageEvent messageEvent)
    {
        var v3Ping = messageEvent.JsonData["op"]?.ToString() == "ping";

        if (v3Ping)
        {
            messageEvent.Connection.Send(new
            {
                op = "pong",
                ts = messageEvent.JsonData["ts"]?.ToString()
            });
        }
    }
    #endregion

}