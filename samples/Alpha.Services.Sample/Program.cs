namespace Alpha.Services.Sample
{
    using System;
    using Alpha.Services.Client;
    using Newtonsoft.Json;
    using RestSharp;

    internal class Program
    {
        private static void Main(string[] args)
        {
            var restClient = new RestClient("http://localhost:57847/api");
            var alphaServicesClient = new AlphaServicesClient(restClient);

            Console.WriteLine("-------------------- Sync --------------------");

            GetOutstanding(alphaServicesClient);
            GetBalanceInfo(alphaServicesClient);
            GetWinLoss(alphaServicesClient);
            GetMessages(alphaServicesClient);

            Console.WriteLine("-------------------- Async --------------------");

            GetOutstandingAsync(alphaServicesClient);
            GetBalanceInfoAsync(alphaServicesClient);
            GetWinLossAsync(alphaServicesClient);
            GetMessagesAsync(alphaServicesClient);
        }

        #region Sync

        private static void GetOutstanding(IAlphaServicesClient client)
        {
            var outstanding = client.GetOutstanding(1176086);

            Console.WriteLine($"Request: GetOutstanding, response: {JsonConvert.SerializeObject(outstanding)}");
        }

        private static void GetBalanceInfo(IAlphaServicesClient client)
        {
            var balanceInfo = client.GetBalanceInfo(1176086);

            Console.WriteLine($"Request: GetBalanceInfo, response: {JsonConvert.SerializeObject(balanceInfo)}");
        }

        private static void GetWinLoss(IAlphaServicesClient client)
        {
            var winLoss = client.GetWinLoss(1176086, DateTime.Now.AddDays(-1).Date);

            Console.WriteLine($"Request: GetWinLoss, response: {JsonConvert.SerializeObject(winLoss)}");
        }

        private static void GetMessages(IAlphaServicesClient client)
        {
            var messages = client.GetMessages(1176086, 1, "en-US", DateTime.Now.AddDays(-1), DateTime.Now);

            Console.WriteLine($"Request: GetMessages, response: {JsonConvert.SerializeObject(messages)}");
        }

        #endregion Sync

        #region Async

        private static void GetOutstandingAsync(IAlphaServicesClient client)
        {
            var outstanding = client.GetOutstandingAsync(1176086).Result;

            Console.WriteLine($"Request: GetOutstandingAsync, response: {JsonConvert.SerializeObject(outstanding)}");
        }

        private static void GetBalanceInfoAsync(IAlphaServicesClient client)
        {
            var balanceInfo = client.GetBalanceInfoAsync(1176086).Result;

            Console.WriteLine($"Request: GetBalanceInfoAsync, response: {JsonConvert.SerializeObject(balanceInfo)}");
        }

        private static void GetWinLossAsync(IAlphaServicesClient client)
        {
            var winLoss = client.GetWinLossAsync(1176086, DateTime.Now.AddDays(-1).Date).Result;

            Console.WriteLine($"Request: GetWinLossAsync, response: {JsonConvert.SerializeObject(winLoss)}");
        }

        private static void GetMessagesAsync(IAlphaServicesClient client)
        {
            var messages = client.GetMessagesAsync(1176086, 1, "en-US", DateTime.Now.AddDays(-1), DateTime.Now).Result;

            Console.WriteLine($"Request: GetMessagesAsync, response: {JsonConvert.SerializeObject(messages)}");
        }

        #endregion Async
    }
}