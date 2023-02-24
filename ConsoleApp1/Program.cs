using Huobi.Api.Models.RestApi.Common;

namespace ConsoleApp1
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var api = new Huobi.Api.HuobiRestApiClient(new Huobi.Api.HuobiRestApiClientOptions
            {
                SignPublicRequests = true,
                RawResponse = true,
                ApiCredentials = new ApiSharp.Authentication.ApiCredentials("ff9ca6ff-8438d94b-bgrveg5tmn-d2c4a", "1a332bc4-f7ce811c-f85de26f-19db1")
            });

            //var system_01 = await api.System.GetSystemStatusAsync();
            //var spot_01 = await api.Spot.MasterData.GetMarketStatusAsync();
            //var spot_02 = await api.Spot.MasterData.GetSymbolsAsync();
            //var spot_03 = await api.Spot.MasterData.GetServerTimeAsync();
            //var spot_04 = await api.Spot.MasterData.GetCurrenciesAsync();
            //var spot_05 = await api.Spot.MasterData.GetCurrencySettingsAsync();
            //var spot_06 = await api.Spot.MasterData.GetCurrencySettingsAsync(DateTime.Now);
            //var spot_07 = await api.Spot.MasterData.GetSymbolSettingsAsync();
            //var spot_08 = await api.Spot.MasterData.GetMarketSymbolSettingsAsync();
            //var spot_09 = await api.Spot.MasterData.GetBlockchainsAsync();
            //var spot_10 = await api.Spot.MasterData.GetCurrencyChainsAsync();
            //var spot_201 = await api.Spot.MarketData.GetKlinesAsync("btcusdt", Huobi.Api.Enums.KlineInterval.OneHour, 100);
            //var spot_202 = await api.Spot.MarketData.GetTickerAsync("btcusdt");
            //var spot_202 = await api.Spot.MarketData.GetTickersAsync();
            //var spot_202 = await api.Spot.MarketData.GetOrderBookAsync("btcusdt");
            //var spot_203 = await api.Spot.MarketData.GetLastTradeAsync("btcusdt");
            //var spot_204 = await api.Spot.MarketData.GetTradesAsync("btcusdt", 100);
            //var spot_205 = await api.Spot.Public.GetTradesAsync("btcusdt", 100);
            //var spot_206 = await api.Spot.Public.GetMarketSummaryAsync("btcusdt");
            //var users_01 = await api.Users.GetUserIdAsync();
            //var users_02 = await api.Users.GetSubUsersAsync();
            //var wallet_01 = await api.Wallet.GetDepositAddressAsync("usdt");
            var wallet_01 = await api.Wallet.GetAccountsAsync();
            var trading_02 = await api.Spot.Trading.GetOpenOrdersAsync();
            /** /
            var trading_01 = await api.Spot.Trading.PlaceOrderAsync(new HuobiOrderRequest
            {
                AccountId = 6836888,
                Symbol = "xrpusdt",
                Side = Huobi.Api.Enums.SpotOrderSide.Buy,
                Type = Huobi.Api.Enums.SpotOrderType.Limit,
                Quantity = 100m,
                Price = 0.37500m,
                Source = Huobi.Api.Enums.OrderSource.Spot,
            });
            /**/
            /*
            var trading_01 = await api.Spot.Trading.PlaceOrderAsync(new HuobiOrderRequest
            {
                AccountId = 6836888,
                Symbol = "adausdt",
                Side = Huobi.Api.Enums.SpotOrderSide.Buy,
                Type = Huobi.Api.Enums.SpotOrderType.Limit,
                Quantity = 100m,
                Price = 0.365200m,
                Source = Huobi.Api.Enums.OrderSource.Spot,
            });
            */

            var y = 0;
            var z = 0;
        }
    }
}