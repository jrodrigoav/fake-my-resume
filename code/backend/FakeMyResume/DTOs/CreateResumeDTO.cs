using System.ComponentModel.DataAnnotations;

namespace FakeMyResume.DTOs;

public class CreateResumeDTO
{
    [Required(ErrorMessage = "FullName is required")]
    [MaxLength(150, ErrorMessage = "FullName cannot exceed 150 characters")]
    public string FullName { get; set; } = null!;

    [Required(ErrorMessage = "CurrentRole is required")]
    public string CurrentRole { get; set; } = null!;

    [Required(ErrorMessage = "Description is required")]
    [MaxLength(1000, ErrorMessage = "Description cannot exceed 1000 characters")]
    public string Description { get; set; } = null!;

    public List<string> Certifications { get; set; } = [];

    [Required(ErrorMessage = "WorkExperience is required")]
    public List<WorkExperienceDTO> WorkExperience { get; set; } = null!;

    public List<EducationDTO>? Education { get; set; }
}
