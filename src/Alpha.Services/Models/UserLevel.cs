namespace Alpha.Services.Models
{
    using System;
    using Alpha.Services.Enumerations;

    [Serializable]
    public class UserLevel : Enumeration
    {
        public static readonly UserLevel Super = new UserLevel(4, "Super");

        public static readonly UserLevel Master = new UserLevel(3, "Master");

        public static readonly UserLevel Agent = new UserLevel(2, "Agent");

        public static readonly UserLevel Member = new UserLevel(1, "Member");

        public UserLevel()
        {
        }

        private UserLevel(byte value, string displayName)
            : base(value, displayName)
        {
        }

        public bool IsAgentLevel() => this == Agent;

        public bool IsMasterLevel() => this == Master;

        public bool IsSuperLevel() => this == Super;

        public bool IsMemberLevel() => this == Member;

        public UserLevel GetDirectDownLineLevel() => FromValue<UserLevel>((byte)(Value - 1));
    }
}