namespace Alpha.Services.Client.Models
{
    using MessagePack;

    [MessagePackObject]
    public class Message
    {
        [Key(0)]
        public int Id { get; set; }

        [Key(1)]
        public string Content { get; set; }
    }
}