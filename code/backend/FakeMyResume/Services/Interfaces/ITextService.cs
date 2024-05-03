namespace FakeMyResume.Services.Interfaces;

public interface ITextService
{
    public Task<string?> GetSuggestions(string text);
}
