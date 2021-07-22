namespace Alpha.Services.Client.Models
{
    public class AccountStatus
    {
        public bool IsActive { get; set; }

        public byte Locked { get; set; }

        public bool IsLocked => Locked != 0;
    }
}
