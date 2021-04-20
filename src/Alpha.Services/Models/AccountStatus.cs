namespace Alpha.Services.Models
{
    public class AccountStatus
    {
        public AccountStatus(bool isActive, byte locked)
        {
            IsActive = isActive;
            Locked = locked;
        }

        public bool IsActive { get; }

        public byte Locked { get; }
    }
}
