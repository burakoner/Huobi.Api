namespace Huobi.Api.Models.RestApi
{
    internal class HuobiApiResponse<T>
    {
        [JsonProperty("ok", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        private bool? OK { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("ts"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }

        [JsonProperty("ch", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Channel { get; set; } = string.Empty;

        [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        public T Data { get; set; } = default!;

        [JsonProperty("tick")]
        private T Tick { set => Data = value; get => Data; }

        [JsonProperty("ticks")]
        private T Ticks { set => Data = value; get => Data; }

        [JsonProperty("full")]
        public bool FullData { get; set; }

        [JsonProperty("err-code", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        internal string ErrorCode { get; set; }

        [JsonProperty("err_code")]
        private string ErrorCodeAlias01 { get => ErrorCode; set => ErrorCode = value; }

        [JsonProperty("code")]
        private string ErrorCodeAlias02 { get => ErrorCode; set => ErrorCode = value; }

        [JsonProperty("err-msg", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
        internal string ErrorMessage { get; set; }

        [JsonProperty("err_msg")]
        private string ErrorMessageAlias01 { get => ErrorMessage; set => ErrorMessage = value; }

        [JsonProperty("msg")]
        private string ErrorMessageAlias02 { get => ErrorMessage; set => ErrorMessage = value; }

        [JsonProperty("message")]
        private string ErrorMessageAlias03 { get => ErrorMessage; set => ErrorMessage = value; }
    }
}
