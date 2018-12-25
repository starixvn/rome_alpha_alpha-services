namespace Alpha.Services.Sample
{
    using System;
    using System.Collections.Generic;
    using Alpha.Services.Client.Models;
    using Alpha.Services.Client.Services;
    using Newtonsoft.Json;

    internal static class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("-------------------- Report Service --------------------");

            var reportService = new ReportServiceImpl();
            GetOutstandings(reportService, 50);
            GetBalanceInfo(reportService);
            GetWinLoss(reportService);
            GetOutstandingsAsync(reportService, 50);
            GetBalanceInfoAsync(reportService);
            GetWinLossAsync(reportService);

            Console.WriteLine("-------------------- Message Service --------------------");

            var messageService = new MessageServiceImpl();
            GetMessages(messageService);
            GetMessagesAsync(messageService);

            Console.WriteLine("-------------------- Announcement Service --------------------");

            var announcementService = new AnnouncementServiceImpl();
            GetAnnouncements(announcementService);

            Console.WriteLine("-------------------- Customer Service --------------------");

            var customerService = new CustomerServiceImpl();
            IsAccountClosed(customerService);
            GetCustomer(customerService);
        }

        #region Report

        private static string FormatJson(object data)
            => JsonConvert.SerializeObject(data, Formatting.Indented);

        private static void GetOutstandings(ReportService service, int top)
        {
            var outstanding = service.GetOutstandings(1176086, 2, top);

            Console.WriteLine($"Request: GetOutstanding, response: {FormatJson(outstanding)}");
        }

        private static void GetBalanceInfo(ReportService service)
        {
            var balanceInfo = service.GetBalanceInfo(1176086);

            Console.WriteLine($"Request: GetBalanceInfo, response: {FormatJson(balanceInfo)}");
        }

        private static void GetWinLoss(ReportService service)
        {
            var winLoss = service.GetWinLoss(1176086, 2, DateTime.Now.AddDays(-1).Date);

            Console.WriteLine($"Request: GetWinLoss, response: {FormatJson(winLoss)}");
        }

        private static void GetOutstandingsAsync(ReportService service, int top)
        {
            var outstanding = service.GetOutstandingsAsync(1176086, 2, top).Result;

            Console.WriteLine($"Request: GetOutstandingAsync, response: {FormatJson(outstanding)}");
        }

        private static void GetBalanceInfoAsync(ReportService service)
        {
            var balanceInfo = service.GetBalanceInfoAsync(1176086).Result;

            Console.WriteLine($"Request: GetBalanceInfoAsync, response: {FormatJson(balanceInfo)}");
        }

        private static void GetWinLossAsync(ReportService service)
        {
            var winLoss = service.GetWinLossAsync(1176086, 2, DateTime.Now.AddDays(-1).Date).Result;

            Console.WriteLine($"Request: GetWinLossAsync, response: {FormatJson(winLoss)}");
        }

        #endregion Report

        #region Message

        private static void GetMessages(MessageService service)
        {
            var customers = new List<Customer>
            {
                new Customer { CustId=1176086, Language = "en-US" }
            };
            var messages = service.GetMessages(2, DateTime.Now.AddDays(-5), DateTime.Now, customers);

            Console.WriteLine($"Request: GetMessages, response: {FormatJson(messages)}");
        }

        private static void GetMessagesAsync(MessageService service)
        {
            var customers = new List<Customer>
            {
                new Customer { CustId = 1176086, Language = "en-US"}
            };
            var messages = service.GetMessagesAsync(0, DateTime.Now.AddDays(-5), DateTime.Now, customers).Result;

            Console.WriteLine($"Request: GetMessagesAsync, response: {FormatJson(messages)}");
        }

        #endregion Message

        #region Announcement

        private static void GetAnnouncements(AnnouncementService service)
        {
            var customers = new List<Customer>
            {
                new Customer { CustId = 1176086, Language = "en-US"}
            };
            var announcements = service.GetAnnouncements(DateTime.Now.AddDays(-3), DateTime.Now, customers);

            Console.WriteLine($"Request: GetAnnouncements, response: {FormatJson(announcements)}");
        }

        #endregion Announcement

        #region Customer

        private static void IsAccountClosed(CustomerService service)
        {
            var isClosed = service.IsAccountClosed(1176086, 0).Result;

            Console.WriteLine($"Request: IsAccountClosed, response: {isClosed}");
        }

        private static void GetCustomer(CustomerService service)
        {
            var customer = service.GetCustomer(1176086).Result;

            Console.WriteLine($"Request: GetCustomer, response: {FormatJson(customer)}");
        }

        #endregion Customer
    }
}