namespace Alpha.Services.Sample
{
    using System;
    using Alpha.Services.Client.Services;
    using Newtonsoft.Json;

    internal static class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("-------------------- Report Service --------------------");

            var reportService = new ReportServiceImpl();
            GetOutstanding(reportService);
            GetBalanceInfo(reportService);
            GetWinLoss(reportService);
            GetOutstandingAsync(reportService);
            GetBalanceInfoAsync(reportService);
            GetWinLossAsync(reportService);

            Console.WriteLine("-------------------- Message Service --------------------");

            var messageService = new MessageServiceImpl();
            GetMessages(messageService);
            GetMessagesAsync(messageService);

            Console.WriteLine("-------------------- Announcement Service --------------------");

            var announcementService = new AnnouncementServiceImpl();
            GetAnnouncements(announcementService);
        }

        #region Report

        private static string FormatJson(object data)
            => JsonConvert.SerializeObject(data, Formatting.Indented);

        private static void GetOutstanding(ReportService service)
        {
            var outstanding = service.GetOutstanding(1176086);

            Console.WriteLine($"Request: GetOutstanding, response: {FormatJson(outstanding)}");
        }

        private static void GetBalanceInfo(ReportService service)
        {
            var balanceInfo = service.GetBalanceInfo(1176086);

            Console.WriteLine($"Request: GetBalanceInfo, response: {FormatJson(balanceInfo)}");
        }

        private static void GetWinLoss(ReportService service)
        {
            var winLoss = service.GetWinLoss(1176086, DateTime.Now.AddDays(-1).Date);

            Console.WriteLine($"Request: GetWinLoss, response: {FormatJson(winLoss)}");
        }

        private static void GetOutstandingAsync(ReportService service)
        {
            var outstanding = service.GetOutstandingAsync(1176086).Result;

            Console.WriteLine($"Request: GetOutstandingAsync, response: {FormatJson(outstanding)}");
        }

        private static void GetBalanceInfoAsync(ReportService service)
        {
            var balanceInfo = service.GetBalanceInfoAsync(1176086).Result;

            Console.WriteLine($"Request: GetBalanceInfoAsync, response: {FormatJson(balanceInfo)}");
        }

        private static void GetWinLossAsync(ReportService service)
        {
            var winLoss = service.GetWinLossAsync(1176086, DateTime.Now.AddDays(-1).Date).Result;

            Console.WriteLine($"Request: GetWinLossAsync, response: {FormatJson(winLoss)}");
        }

        #endregion Report

        #region Message

        private static void GetMessages(MessageService service)
        {
            var messages = service.GetMessages(1176086, 1, "en-US", DateTime.Now.AddDays(-1), DateTime.Now);

            Console.WriteLine($"Request: GetMessages, response: {FormatJson(messages)}");
        }

        private static void GetMessagesAsync(MessageService service)
        {
            var messages = service.GetMessagesAsync(1176086, 1, "en-US", DateTime.Now.AddDays(-1), DateTime.Now).Result;

            Console.WriteLine($"Request: GetMessagesAsync, response: {FormatJson(messages)}");
        }

        #endregion Message

        #region Announcement

        private static void GetAnnouncements(AnnouncementService service)
        {
            var announcements = service.GetAnnouncements(1176086, "en-US", DateTime.Now.AddDays(-3), DateTime.Now);

            Console.WriteLine($"Request: GetAnnouncements, response: {FormatJson(announcements)}");
        }

        #endregion Announcement
    }
}