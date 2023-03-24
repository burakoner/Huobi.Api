// .Attributes;

namespace Huobi.Api.Enums
{
    /// <summary>
    /// Business type
    /// </summary>
    public enum BusinessType
    {
        /// <summary>
        /// Futures
        /// </summary>
        [Map("futures")]
        Futures,
        /// <summary>
        /// Swap
        /// </summary>
        [Map("swap")]
        Swap
    }
}
