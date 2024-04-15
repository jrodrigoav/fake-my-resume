using AutoMapper;
using FakeMyResume.Data.Models;
using FakeMyResume.DTOs;

namespace FakeMyResume.API.Configuration;

public class MapperConfigurationProfile : Profile
{
    public MapperConfigurationProfile()
    {
        //Models to DTO
        CreateMap<Resume, ResumeDTO>();
        CreateMap<Education, EducationDTO>();
        CreateMap<WorkExperience, WorkExperienceDTO>();
        CreateMap<Resume, CreateResumeDTO>();


        //DTO to Models
        CreateMap<ResumeDTO, Resume>();
        CreateMap<EducationDTO, Education>();
        CreateMap<WorkExperienceDTO, WorkExperience>();
        CreateMap<CreateResumeDTO, Resume>();

    }
}
