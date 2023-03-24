// .Attributes;

namespace Huobi.Api.Enums
{
    /// <summary>
    /// Conditional order type
    /// </summary>
    public enum ConditionalOrderType
    {
        /// <summary>
        /// Limit order
        /// </summary>
        [Map("limit")]
        Limit,
        /// <summary>
        /// Market order
        /// </summary>
        [Map("market")]
        Market
    }
}
