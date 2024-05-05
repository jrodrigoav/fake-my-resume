using System.ComponentModel.DataAnnotations;

namespace FakeMyResume.DTOs;

public class WorkExperienceDTO
{
    [Required(ErrorMessage = "Begin date is required")]
    public DateTime DateBegin { get; init; }

    public DateTime? DateEnd { get; init; }

    [StringLength(250, ErrorMessage = "Company name cannot exceed 250 characters")]
    public string? CompanyName { get; init; }

    [StringLength(250, ErrorMessage = "Project name cannot exceed 250 characters")]
    public string? ProjectName { get; init; }

    [StringLength(250, ErrorMessage = "Role cannot exceed 250 characters")]
    public string? Role { get; init; }

    [StringLength(1500, ErrorMessage = "Description cannot exceed 1500 characters")]
    public string Description { get; init; } = string.Empty;

    [Required(ErrorMessage = "Technologies is required")]
    public string[] Technologies { get; set; } = [];
}
