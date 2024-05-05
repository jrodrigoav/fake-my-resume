using FakeMyResume.DTOs;
using FakeMyResume.Models;
using FakeMyResume.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FakeMyResume.Controllers;

[ApiController, Authorize, Route("api/resume")]
public class ResumeController(ResumeService resumeService, DocumentGenerationService documentGenerationService) : ControllerBase
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
        var accountId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var resume = resumeService.GetResume(id);

        if (resume == null)
        {
            return NotFound();
        }

        if (!resume.UserId.Equals(accountId))
        {
            return Unauthorized($"The requested resume belongs to another user.");
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
            return File(documentGenerationService.GenerateResumeInPDF(resume.ResumeData), "application/pdf");
        }
        catch (InvalidOperationException)
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

        //var newResume = resumeService.SaveResume(new Resume(), accountId);
        //var result = ResumeDTO.FromData(newResume);
        //return Created($"/api/resume/{newResume.Id}", result);
        return Ok();
    }

    [HttpPut("{id}")]
    public IActionResult UpdateResume(int id, [FromBody] UpdateResumeDTO resumeDTO)
    {        

        var updatedResume = resumeService.UpdateResume(id, new Resume());
        if (updatedResume == null)
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
