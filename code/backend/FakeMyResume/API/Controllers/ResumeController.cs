using AutoMapper;
using FakeMyResume.Data.Models;
using FakeMyResume.DTOs;
using FakeMyResume.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json;

namespace FakeMyResume.API.Controllers;

[ApiController, Authorize, Route("api/resume")]
public class ResumeController(IMapper mapper, IResumeService resumeService, IDocumentGenerationService documentGenerationService) : ControllerBase
{
    [HttpGet]
    public IActionResult GetAccountResumes()
    {
        var accountId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (accountId == null)
            return new UnauthorizedResult();

        var resumes = resumeService.GetResumes(accountId).Select(ResumeDTO.FromData);
        return Ok(resumes);
    }

    [HttpGet("{id}")]
    public IActionResult GetResume(int id)
    {
        var resume = resumeService.GetResume(id);

        if (resume == null)
        {
            return NotFound();
        }

        var result = ResumeDTO.FromData(resume);
        return Ok(result);
    }

    [HttpGet("{id}/pdf")]
    public IActionResult GetResumePDF(int id)
    {
        try
        {
            var resume = resumeService.GetResume(id);
            var resumeData = JsonSerializer.Deserialize<Resume>(resume.JsonData) ?? throw new NullReferenceException("Failed to deserialize resume data.");
            return File(documentGenerationService.GenerateResumeInPDF(resumeData), "application/pdf");
        } catch(InvalidOperationException)
        {
            return NotFound();
        }
    }

    [HttpPost]
    public IActionResult SaveResume(CreateResumeDTO resumeDTO)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var accountId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (accountId == null)
            return new UnauthorizedResult();
        var resume = mapper.Map<Resume>(resumeDTO);

        var newResume = resumeService.SaveResume(resume, accountId);
        var result = ResumeDTO.FromData(newResume);
        return Created($"/api/resume/{newResume.Id}", result);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateResume(int id, [FromBody] UpdateResumeDTO resumeDTO)
    {
        var resume = mapper.Map<Resume>(resumeDTO);

        var updatedResume = resumeService.UpdateResume(id, resume);
        if(updatedResume == null)
        {
            return NotFound();
        }

        var result = ResumeDTO.FromData(updatedResume);
        return Ok(result);
    }

    [HttpGet("dummy")]
    public IActionResult GetDummy()
    {
        return File(documentGenerationService.GenerateResumeInPDF(new Resume()), "application/pdf");
    }
}
