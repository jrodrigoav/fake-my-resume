
namespace FakeMyResume.DTOs.Responses
{
    public class Note
    {
        public Note(Models.Data.Note entity)
        {
            CreatedAt = entity.CreatedAt;
            Title = entity.Title;
            Content = entity.Content;            
        }

        public DateTimeOffset CreatedAt { get; init; }
        public DateTimeOffset? LastUpdatedAt { get; init; }
        public string Title { get; init; } = null!;
        public string Content { get; init; } = null!;
    }
}
