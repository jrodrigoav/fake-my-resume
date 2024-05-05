using System.ComponentModel.DataAnnotations;

namespace FakeMyResume.DTOs;

public class EducationDTO
{
    [StringLength(250)]
    public string? Degree { get; set; }

    [StringLength(250)]
    public string? Major { get; set; }

    [StringLength(250)]
    public string? UniversityName { get; set; }

    [Range(1980,2025)]
    public int? YearOfCompletion { get; set; }
    
    [StringLength(250)]
    public string? Country { get; set; }
    
    [StringLength(250)]
    public string? State { get; set; }
}
