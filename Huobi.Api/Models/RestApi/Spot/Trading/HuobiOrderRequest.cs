namespace Huobi.Api.Models.RestApi.Spot.Trading;

public class HuobiOrderRequest
{
    public long AccountId { get; set; }
    public string Symbol { get; set; }
    public SpotOrderSide Side { get; set; }
    public SpotOrderType Type { get; set; }
    public decimal Quantity { get; set; }
    public decimal? Price { get; set; }
    public decimal? StopPrice { get; set; }
    public StopPriceOperator? StopPriceOperator { get; set; }
    // public string ClientOrderId { get; set; }
    public OrderSource Source { get; set; }
    public bool PreventSelfMatch { get; set; }
}

internal class HuobiOrderRequestString
{
    [JsonProperty("account-id")]
    public string AccountId { get; set; }

    [JsonProperty("symbol")]
    public string Symbol { get; set; }

    [JsonProperty("type")]
    public string Type { get; set; }

    [JsonProperty("amount")]
    public string Quantity { get; set; }

    [JsonProperty("price", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string Price { get; set; }

    [JsonProperty("stop-price", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string StopPrice { get; set; }

    [JsonProperty("operator", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string StopPriceOperator { get; set; }

    [JsonProperty("client-order-id", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string ClientOrderId { get; set; }

    [JsonProperty("source", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string Source { get; set; }

    [JsonProperty("self-match-prevent", NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore)]
    public int PreventSelfMatch { get; set; }

    public HuobiOrderRequestString() { }
    public HuobiOrderRequestString(HuobiOrderRequest request)
    {
        AccountId = request.AccountId.ToString();
        Symbol = request.Symbol;
        Type = JsonConvert.SerializeObject(request.Side, new SpotOrderSideConverter(false)) + "-" + JsonConvert.SerializeObject(request.Type, new OrderTypeConverter(false));
        Quantity = request.Quantity.ToString(CultureInfo.InvariantCulture);
        Price = request.Price?.ToString(CultureInfo.InvariantCulture);
        StopPrice = request.StopPrice?.ToString(CultureInfo.InvariantCulture);
        StopPriceOperator = request.StopPriceOperator == null ? null : JsonConvert.SerializeObject(request.StopPriceOperator, new StopPriceOperatorConverter(false));
        // ClientOrderId = request.ClientOrderId;
        Source = JsonConvert.SerializeObject(request.Source, new OrderSourceConverter(false)); ;
        PreventSelfMatch = request.PreventSelfMatch ? 1 : 0;
    }

    public static IEnumerable<HuobiOrderRequestString> ImportList(IEnumerable<HuobiOrderRequest> requests)
    {
        var list = new List<HuobiOrderRequestString>();
        foreach (var request in requests) list.Add(new HuobiOrderRequestString(request));

        return list;
    }
}