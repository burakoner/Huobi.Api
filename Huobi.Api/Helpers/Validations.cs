namespace Huobi.Api.Helpers;

internal static class Validations
{
    public static void ValidateClientOrderId(this string clientOrderId)
    {
        if (string.IsNullOrEmpty(clientOrderId)) return;

        if (clientOrderId.Length > 53)
            throw new ArgumentException("ClientOrderId can be up to 53 characters");
    }

    public static string ValidateSpotSymbol(this string symbol)
    {
        if (string.IsNullOrEmpty(symbol))
            throw new ArgumentException("Symbol is not provided");

        symbol = symbol.ToLower(CultureInfo.InvariantCulture);
        if (!Regex.IsMatch(symbol, "^(([a-z]|[0-9]){4,})$"))
            throw new ArgumentException($"{symbol} is not a valid Huobi symbol. Should be [QuoteAsset][BaseAsset], e.g. ethbtc");

        return symbol;
    }
}
