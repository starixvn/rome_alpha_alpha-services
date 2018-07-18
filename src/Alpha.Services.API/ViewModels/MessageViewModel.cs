namespace Alpha.Services.API.ViewModels
{
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