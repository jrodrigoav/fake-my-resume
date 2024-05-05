using FakeMyResume.Models;
using FakeMyResume.Models.StackExchange;
using FakeMyResume.Services.Data;
using Microsoft.Extensions.Options;
using Quartz;
using System.Net;

namespace FakeMyResume.Services;

internal class ImporterService : IJob
{
    private readonly StackExchangeSettings _settings;
    private readonly ILogger<ImporterService> _logger;
    private readonly HttpClient _client;
    private readonly FakeMyResumeDbContext _context;

    public ImporterService(HttpClient client, ILogger<ImporterService> logger, IOptionsMonitor<StackExchangeSettings> optionsMonitor, FakeMyResumeDbContext context)
    {
        _client = client;
        _settings = optionsMonitor.CurrentValue;
        _client.BaseAddress = _settings.Url;
        _client.DefaultRequestHeaders.Remove("Accept");
        _client.DefaultRequestHeaders.Add("Accept", "application/json");
        _client.Timeout = TimeSpan.FromSeconds(90);
        _logger = logger;
        _context = context;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        var lastPage = _context.AppConfigs.Find(1);
        if (lastPage != null)
        {
            var source_tags = await GetTagsPageAsync(lastPage.Page ?? 1);
            await _context.UpsertTagsFromSourceAsync(source_tags);
            if (source_tags.Any(t => t.Count <= 300))
            {
                lastPage.Page = 1;
            }
            else
            {
                lastPage.Page += 1;
            }
            lastPage.LastUpdated = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }
    }

    private async Task<Tag[]> GetTagsPageAsync(int page)
    {
        var response = await _client.GetFromJsonAsync<TagsResponse>($"tags?pagesize=100&order=desc&page={page}&site=stackoverflow&key={_settings.Key}");
        return response?.Items ?? [];
    }
}
