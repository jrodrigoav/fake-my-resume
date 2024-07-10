using System.ComponentModel.DataAnnotations;

namespace FakeMyResume.Models.Data
{
    public class Note
    {
        public string UserId { get; set; } = null!;
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? LastUpdatedAt { get; set; }        
        
        [StringLength(100)]
        public string Title { get; set; } = null!;

        [StringLength(500)]
        public string Content { get; set; } = null!;
    }
}
