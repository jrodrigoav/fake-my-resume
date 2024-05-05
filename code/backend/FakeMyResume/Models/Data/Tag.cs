using System.ComponentModel.DataAnnotations;

namespace FakeMyResume.Models.Data
{

    public class Tag
    {

        [Key, StringLength(50)]
        public string Name { get; set; } = null!;
        public bool HasSynonyms { get; set; }
        public bool IsModeratorOnly { get; set; }
        public bool IsRequired { get; set; }
        public int Count { get; set; }

        public Tag()
        {

        }
        public Tag(StackExchange.Tag source)
        {
            Name = source.Name;
            HasSynonyms = source.HasSynonyms;
            IsModeratorOnly = source.IsModeratorOnly;
            IsRequired = source.IsRequired;
            Count = source.Count;
        }
    }

    public class AppConfig
    {
        [Key]
        public int Id { get; set; }
        public int? Page { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}