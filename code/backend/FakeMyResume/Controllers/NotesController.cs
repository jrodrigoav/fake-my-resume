using FakeMyResume.DTOs.Requests;
using FakeMyResume.DTOs.Responses;
using FakeMyResume.Services.Data;
using Microsoft.AspNetCore.Mvc;

namespace FakeMyResume.Controllers
{
    [Route("api/notes"), ApiController]
    public class NotesController : ControllerBase
    {
        private readonly FakeMyResumeDbContext _dbContext;
        private readonly ILogger<NotesController> _logger;

        public NotesController(FakeMyResumeDbContext dbContext, ILogger<NotesController> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<DTOs.Responses.Note[]> GetNotes()
        {
            var userId = User.Identity?.Name ?? null;
            if (userId == null) { return NotFound(); }
            return Ok(_dbContext.Notes.Where(n => n.UserId == userId).Select(r => new DTOs.Responses.Note(r)).ToArray());
        }

        [HttpPost]
        public async Task<ActionResult<DTOs.Responses.Note>> CreateNote([FromBody] CreateNote createNote)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            DTOs.Responses.Note? response = null;
            if (User.Identity?.Name != null)
            {
                var entity = createNote.ToEntity(User.Identity.Name);
                await _dbContext.Notes.AddAsync(entity);
                await _dbContext.SaveChangesAsync();
                response = new DTOs.Responses.Note(entity);
            }
            return CreatedAtAction("CreateNote", response);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateNote([FromBody] UpdateNote updateNote)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            if (User.Identity?.Name != null)
            {
                var entity = _dbContext.Notes.Find(User.Identity.Name, updateNote.CreatedAt);
                if (entity != null)
                {
                    entity.LastUpdatedAt = DateTimeOffset.UtcNow;
                    entity.Title = updateNote.Title;
                    entity.Content = updateNote.Content;
                    await _dbContext.SaveChangesAsync();
                }
            }
            return NoContent();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteNote([FromBody] DeleteNote deleteNote)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            if (User.Identity?.Name != null)
            {
                var entity = _dbContext.Notes.Find(User.Identity.Name, deleteNote.CreatedAt);
                if (entity != null)
                {
                    _dbContext.Notes.Remove(entity);
                    await _dbContext.SaveChangesAsync();
                }
            }
            return NoContent();
        }
    }
}
