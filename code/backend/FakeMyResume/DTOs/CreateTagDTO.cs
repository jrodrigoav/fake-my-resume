using System.ComponentModel.DataAnnotations;

namespace FakeMyResume.DTOs;

public class CreateTagDTO
{
    [Required]
    public required string Name { get; set; }
}
