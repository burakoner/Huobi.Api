namespace Huobi.Api.Converters;

internal class OrderStatusConverter : BaseConverter<OrderStatus>
{
    public OrderStatusConverter() : this(true) { }
    public OrderStatusConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OrderStatus, string>> Mapping => new List<KeyValuePair<OrderStatus, string>>
    {
        new KeyValuePair<OrderStatus, string>(OrderStatus.PreSubmitted, "pre-submitted"),
        new KeyValuePair<OrderStatus, string>(OrderStatus.Submitted, "submitted"),
        new KeyValuePair<OrderStatus, string>(OrderStatus.PartiallyFilled, "partial-filled"),
        new KeyValuePair<OrderStatus, string>(OrderStatus.PartiallyCanceled, "partial-canceled"),
        new KeyValuePair<OrderStatus, string>(OrderStatus.Filled, "filled"),
        new KeyValuePair<OrderStatus, string>(OrderStatus.Canceled, "canceled"),
        new KeyValuePair<OrderStatus, string>(OrderStatus.Rejected, "rejected"),
        new KeyValuePair<OrderStatus, string>(OrderStatus.Created, "created")
    };
}
