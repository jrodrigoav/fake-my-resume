using FakeMyResume.Data;
using FakeMyResume.Data.Models;
using FakeMyResume.Services.Interfaces;
using System.Reflection;

namespace FakeMyResume.Services;

public class TagService : ITagService
{
    private readonly string _assetsPath;
    private readonly MakeMyResumeDb _context;
    public TagService(MakeMyResumeDb context) 
    {
        _context = context;
        var assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty;
        _assetsPath = Path.Combine(assemblyPath, "Assets");
    }
    public List<Tag> GetTags(string text)
    {
        var auxText = text.Replace(" ", string.Empty).ToLower();
        var tags = FindTags(auxText);
        return tags;
    }

    public List<Tag> FindTags(string text)
    {
        return _context.Tag.Where(x => x.TagName.Replace(" ", string.Empty).ToLower().Contains(text)).ToList();
    }
}
