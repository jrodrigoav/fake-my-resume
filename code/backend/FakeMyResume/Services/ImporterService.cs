using FakeMyResume.Data;
using FakeMyResume.Models;
using Microsoft.Extensions.Options;
using System.Net;

namespace FakeMyResume.Services;

internal class ImporterService
{
    private readonly string _key;
    private readonly ILogger<ImporterService> _logger;
    private readonly HttpClient _client;
    private readonly TagsDbContext _context;

    public ImporterService(HttpClient client, ILogger<ImporterService> logger, IOptionsMonitor<StackExchangeSettings> optionsMonitor, TagsDbContext context)
    {
        _client = client;
        _client.BaseAddress = optionsMonitor.CurrentValue.Url;
        _client.DefaultRequestHeaders.Add("Accept", "application/json");
        _client.Timeout = TimeSpan.FromSeconds(90);
        _key = optionsMonitor.CurrentValue.Key;
        _logger = logger;
        _context = context;
    }

    public async Task<int> ImportAsync(int page, int maxPages)
    {
        var hasmore = false;
        var pageSize = 100;
        var lastPage = page + maxPages;
        do
        {
            var tagsResponse = await GetTagsPage(page, pageSize);
            var validTags = tagsResponse.Items.Where(c => c.Count > 300);
            var validTagNames = validTags.Select(vt => vt.Name);
            var existingTagNames = _context.Tags.Where(t => validTagNames.Contains(t.Name)).Select(t => t.Name).ToList();
            foreach (var validTag in validTags)
            {
                if (existingTagNames.Contains(validTag.Name))
                {
                    _context.Tags.Update(validTag);
                }
                else
                {
                    _context.Tags.Add(validTag);
                }
            }

            // Ends if the page filtered at least one element or no elements are found
            hasmore = validTags.Count() == pageSize && tagsResponse.HasMore;
            page++;

            if (tagsResponse.QuotaRemaining < 10)
            {
                _logger.LogWarning("Quota dangerously low");
                break;
            }
        } while (page < lastPage && hasmore);
        await _context.SaveChangesAsync();
        return hasmore ? page : 1;
    }

    private async Task<StackExchangeTagsResponse> GetTagsPage(int page, int pageSize)
    {
        StackExchangeTagsResponse? tagsResponse = null;
        try
        {
            tagsResponse = await _client.GetFromJsonAsync<StackExchangeTagsResponse>($"tags?pagesize={pageSize}&order=desc&page={page}&site=stackoverflow&key={_key}");
        }
        catch (WebException httpRequestException)
        {
            _logger.LogError(httpRequestException, "WebException");
        }
        ArgumentNullException.ThrowIfNull(tagsResponse);
        return tagsResponse;
    }
}
