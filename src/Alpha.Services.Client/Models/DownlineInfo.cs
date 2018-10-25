namespace Alpha.Services.Client.Models
{
    public class DownlineInfo
    {
        public int CustId { get; set; }

        public bool IsSubAccount { get; set; }

        public bool IsActive { get; set; }

        public byte RoleId { get; set; }
    }
}