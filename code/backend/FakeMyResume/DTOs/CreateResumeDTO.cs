using System.ComponentModel.DataAnnotations;

namespace FakeMyResume.DTOs;

public class CreateResumeDTO
{
    [Required(ErrorMessage = "FullName is required")]
    [StringLength(250, ErrorMessage = "FullName cannot exceed 250 characters")]
    public string FullName { get; init; } = null!;

    [Required(ErrorMessage = "CurrentRole is required")]
    public string CurrentRole { get; init; } = null!;

    [Required(ErrorMessage = "Description is required")]
    [StringLength(1500, ErrorMessage = "Description cannot exceed 1500 characters")]
    public string Description { get; init; } = null!;

    public List<string> Certifications { get; init; } = [];

    [Required(ErrorMessage = "WorkExperience is required")]
    public WorkExperienceDTO[] WorkExperience { get; init; }=[];

    public EducationDTO[] Education { get; init; } = [];
}
