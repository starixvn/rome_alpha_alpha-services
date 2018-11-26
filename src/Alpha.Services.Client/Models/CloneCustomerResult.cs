namespace Alpha.Services.Client.Models
{
    public class CloneCustomerResult
    {
        public int CustId { get; set; }

        public string CustName { get; set; }

        public string Password { get; set; }

        public int Code { get; set; }

        public string Message { get; set; }

        public bool IsSuccess { get; set; }
    }
}
