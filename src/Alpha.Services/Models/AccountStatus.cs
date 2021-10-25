namespace Alpha.Services.Models
{
    public class AccountStatus
    {
        public AccountStatus(bool isActive, byte locked, bool isProtectedMode)
        {
            IsActive = isActive;
            Locked = locked;
            IsProtectedMode = isProtectedMode;
        }

        public bool IsActive { get; }

        public byte Locked { get; }

        public bool IsProtectedMode { get; }
    }
}
