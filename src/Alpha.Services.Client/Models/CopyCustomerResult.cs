namespace Alpha.Services.Client.Models
{
    public class CopyCustomerResult : Result
    {
        public int CustId { get; set; }

        public string CustName { get; set; }

        public string Password { get; set; }

        public int CustLevel { get; set; }

        public string SourceUsername { get; set; }

        public decimal Credit { get; set; }

        public decimal MaxCredit { get; set; }
    }
}