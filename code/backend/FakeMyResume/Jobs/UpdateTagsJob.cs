using FakeMyResume.Services;
using Quartz;

namespace FakeMyResume.Jobs;

internal class UpdateTagsJob(ImporterService importer, ILogger<UpdateTagsJob> logger) : IJob
{
    private readonly ILogger _logger = logger;
    private readonly ImporterService _importer = importer;

    async Task IJob.Execute(IJobExecutionContext context)
    {
        _logger.LogInformation("Update tags job started");
        await _importer.ImportAsync();
        _logger.LogInformation("Update tags job ended");
    }
}
