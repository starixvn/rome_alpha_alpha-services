namespace Alpha.Services.Tests.ModelsForTesting
{
    using Alpha.Services.Models;

    public class UserLevelForTesting : UserLevel
    {
        public static readonly new UserLevelForTesting Agent = new UserLevelForTesting(1, "Super");

        public UserLevelForTesting()
        {
        }

        private UserLevelForTesting(byte value, string displayName)
        {
        }
    }
}