namespace Alpha.Services.Criteria
{
    using Fanex.Data;
    using Fanex.Data.Repository;

    public class InsertUserLogCommand : NonQueryCommand
    {
        public InsertUserLogCommand(
            int actorId,
            int custId,
            int actionId,
            string trackingValue,
            string trackingValue2,
            int duration)
        {
            ActorId = actorId;
            CustId = custId;
            ActionId = actionId;
            TrackingValue = trackingValue;
            TrackingValue2 = trackingValue2;
            Duration = duration;
        }

        [SpParam(SqlParamName = "UserId")]
        public int ActorId { get; }

        public int CustId { get; }

        public int ActionId { get; }

        public string TrackingValue { get; }

        public string TrackingValue2 { get; }

        public int Duration { get; }

        public override string GetSettingKey() => "Acc_WriteLog_UserLog_RunSP";

        public override bool IsValid()
            => ActorId > 0
            && CustId > 0
            && ActionId > 0
            && !string.IsNullOrWhiteSpace(TrackingValue);
    }
}