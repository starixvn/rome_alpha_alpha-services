namespace Alpha.Services.Sample
{
    using System;
    using Alpha.Services.Client.Services;
    using Newtonsoft.Json;
    using RestSharp;

    internal class Program
    {
        private static void Main(string[] args)
        {
            var restClient = new RestClient("http://localhost:57847/api");

            Console.WriteLine("-------------------- Report Service --------------------");

            var reportService = new ReportServiceImpl(restClient);
            GetOutstanding(reportService);
            GetBalanceInfo(reportService);
            GetWinLoss(reportService);
            GetOutstandingAsync(reportService);
            GetBalanceInfoAsync(reportService);
            GetWinLossAsync(reportService);

            Console.WriteLine("-------------------- Message Service --------------------");

            var messageService = new MessageServiceImpl(restClient);
            GetMessages(messageService);
            GetMessagesAsync(messageService);
        }

        #region Report

        private static void GetOutstanding(ReportService service)
        {
            var outstanding = service.GetOutstanding(1176086);

            Console.WriteLine($"Request: GetOutstanding, response: {JsonConvert.SerializeObject(outstanding)}");
        }

        private static void GetBalanceInfo(ReportService service)
        {
            var balanceInfo = service.GetBalanceInfo(1176086);

            Console.WriteLine($"Request: GetBalanceInfo, response: {JsonConvert.SerializeObject(balanceInfo)}");
        }

        private static void GetWinLoss(ReportService service)
        {
            var winLoss = service.GetWinLoss(1176086, DateTime.Now.AddDays(-1).Date);

            Console.WriteLine($"Request: GetWinLoss, response: {JsonConvert.SerializeObject(winLoss)}");
        }

        private static void GetOutstandingAsync(ReportService service)
        {
            var outstanding = service.GetOutstandingAsync(1176086).Result;

            Console.WriteLine($"Request: GetOutstandingAsync, response: {JsonConvert.SerializeObject(outstanding)}");
        }

        private static void GetBalanceInfoAsync(ReportService service)
        {
            var balanceInfo = service.GetBalanceInfoAsync(1176086).Result;

            Console.WriteLine($"Request: GetBalanceInfoAsync, response: {JsonConvert.SerializeObject(balanceInfo)}");
        }

        private static void GetWinLossAsync(ReportService service)
        {
            var winLoss = service.GetWinLossAsync(1176086, DateTime.Now.AddDays(-1).Date).Result;

            Console.WriteLine($"Request: GetWinLossAsync, response: {JsonConvert.SerializeObject(winLoss)}");
        }

        #endregion Report

        #region Message

        private static void GetMessages(MessageService service)
        {
            var messages = service.GetMessages(1176086, 1, "en-US", DateTime.Now.AddDays(-1), DateTime.Now);

            Console.WriteLine($"Request: GetMessages, response: {JsonConvert.SerializeObject(messages)}");
        }

        private static void GetMessagesAsync(MessageService service)
        {
            var messages = service.GetMessagesAsync(1176086, 1, "en-US", DateTime.Now.AddDays(-1), DateTime.Now).Result;

            Console.WriteLine($"Request: GetMessagesAsync, response: {JsonConvert.SerializeObject(messages)}");
        }

        #endregion Message
    }
}