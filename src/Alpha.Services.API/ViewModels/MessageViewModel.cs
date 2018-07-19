namespace Alpha.Services.API.ViewModels
{
    using MessagePack;

    [MessagePackObject(keyAsPropertyName: true)]
    public class MessageViewModel
    {
        public MessageViewModel(int id, string content)
        {
            Id = id;
            Content = content;
        }

        public int Id { get; }

        public string Content { get; }
    }
}