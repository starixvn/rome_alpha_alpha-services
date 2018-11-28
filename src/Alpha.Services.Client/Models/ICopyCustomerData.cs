namespace Alpha.Services.Client.Models
{
    public interface ICopyCustomerData
    {
        int ActorId { get; }

        byte ActorUserLevel { get;  }

        string UserName { get; }

        decimal Credit { get; }

        decimal MaxCredit { get; }

        int SourceCustId { get; }

        string Language { get; }

        string IP { get; }
    }
}
