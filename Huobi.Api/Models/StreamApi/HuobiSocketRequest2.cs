using Newtonsoft.Json;

namespace Huobi.Api.Models.StreamApi
{
    internal class HuobiSocketRequest2
    {
        [JsonProperty("op")]
        public string Operation { get; set; }
        [JsonProperty("cid")]
        public string ClientId { get; set; }
        [JsonProperty("topic")]
        public string Topic { get; set; }

        public HuobiSocketRequest2(string operation, string clientId, string topic)
        {
            Operation = operation;
            ClientId = clientId;
            Topic = topic;
        }
    }
}
