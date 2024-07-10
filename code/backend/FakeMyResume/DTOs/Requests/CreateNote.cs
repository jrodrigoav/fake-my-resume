using FakeMyResume.Models.Data;
using System.ComponentModel.DataAnnotations;

namespace FakeMyResume.DTOs.Requests
{
    public class CreateNote
    {
        [Required,StringLength(100)]
        public string Title { get; init; } = null!;
        
        [Required, StringLength(500)]
        public string Content { get; init; } = null!;

        internal Note ToEntity(string userId)
        {
            return new Note { UserId = userId, Title = Title, Content = Content, CreatedAt = DateTimeOffset.UtcNow };
        }
    }

    public class UpdateNote
    {
        [Required]
        public DateTimeOffset CreatedAt { get; init; }

        [Required, StringLength(100)]
        public string Title { get; init; } = null!;

        [Required, StringLength(500)]
        public string Content { get; init; } = null!;
        internal Note ToEntity(string userId)
        {
            return new Note { UserId = userId, Title = Title, Content = Content, CreatedAt = CreatedAt, LastUpdatedAt = DateTimeOffset.UtcNow };
        }
    }

    public class DeleteNote
    {
        [Required]
        public DateTimeOffset CreatedAt { get; init; }
    }
}
