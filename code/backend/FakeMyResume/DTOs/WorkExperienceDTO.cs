using System.ComponentModel.DataAnnotations;

namespace FakeMyResume.DTOs;

public class WorkExperienceDTO
{
    [Required(ErrorMessage = "Begin date is required")]
    public DateTime DateBegin { get; set; }

    public DateTime? DateEnd { get; set; }

    [MaxLength(150, ErrorMessage = "Company name cannot exceed 150 characters")]
    public string? CompanyName { get; set; }

    [MaxLength(150, ErrorMessage = "Project name cannot exceed 150 characters")]
    public string? ProjectName { get; set; }

    [MaxLength(150, ErrorMessage = "Role cannot exceed 150 characters")]
    public string? Role { get; set; }

    [MaxLength(1000, ErrorMessage = "Description cannot exceed 1000 characters")]
    public string Description { get; set; } = string.Empty;

    [Required(ErrorMessage = "Technologies is required")]
    public List<string> Technologies { get; set; } = [];
}
