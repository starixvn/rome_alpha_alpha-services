using MessagePack;

namespace Alpha.Services.API.ViewModels
{
    [MessagePackObject]
    public class MessageViewModel
    {
        [Key(0)]
        public int Id { get; set; }

        [Key(1)]
        public string Content { get; set; }
    }
}