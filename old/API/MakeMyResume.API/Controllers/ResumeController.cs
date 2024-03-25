using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using MakeMyResume.Data.Models;
using MakeMyResume.DTOs;
using MakeMyResume.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MakeMyResume.API.Controllers
{
    [ApiController, Route("api/resume")]
    public class ResumeController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IValidator<CreateResumeDTO> _validator;
        private readonly IResumeService _resumeService;

        public ResumeController(IMapper mapper, IValidator<CreateResumeDTO> validator, IResumeService resumeService)
        {
            _mapper = mapper;
            _validator = validator;
            _resumeService = resumeService;
        }


        [HttpPost]
        public IActionResult SaveResume(CreateResumeDTO resumeDTO)
        {
            var validationResult = _validator.Validate(resumeDTO);
            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState);
                return BadRequest(ModelState);
            }
            var resume = _mapper.Map<Resume>(resumeDTO);

            _resumeService.SaveResume(resume);

            return Created($"/api/resume/{resume.Id}", resume);

        }

        [HttpGet("{id}")]
        public IActionResult GetResume(int id)
        {

            var resume = _resumeService.GetResume(id);

            if (resume == null)
            {
                return NotFound();
            }

            return Ok(resume);

        }

        [HttpPut]
        public IActionResult UpdateResume(ResumeDTO resumeDTO)
        {
            var validationResult = _validator.Validate(resumeDTO);
            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState);
                return BadRequest(ModelState);
            }
            var resume = _mapper.Map<Resume>(resumeDTO);

            var result = _resumeService.UpdateResume(resume);

            return result == null? NotFound() : Ok(resume);

        }

        [HttpGet("get-resume-PDF/{id}"), AllowAnonymous]
        public IActionResult GetResumePDF(int id)
        {
            var resume = _resumeService.GetResumePDF(id);

            if (resume == null)
            {
                return NotFound();
            }

            return File(resume, "application/pdf");
        }

        [HttpGet("dummy"), AllowAnonymous]
        public IActionResult GetDummy([FromServices] IDocumentGenerationService documentGenerationService)
        {
            return File(documentGenerationService.GenerateResumeInPDF(new Resume()), "application/pdf");
        }
    }
}
