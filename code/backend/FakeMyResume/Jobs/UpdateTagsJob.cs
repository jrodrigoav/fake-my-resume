using FakeMyResume.Services;
using Quartz;

namespace FakeMyResume.Jobs;

[PersistJobDataAfterExecution]
internal class UpdateTagsJob(ImporterService importer, ILogger<UpdateTagsJob> logger) : IJob
{
    private readonly ILogger _logger = logger;
    private readonly ImporterService _importer = importer;
    private static readonly int PagesPerRun = 50;

    async Task IJob.Execute(IJobExecutionContext context)
    {
        var startPage = (int)(long)context.JobDetail.JobDataMap.Get("nextPage");
        _logger.LogInformation("Update tags job started for pages the next {PagesPerRun} starting at {startPage}", PagesPerRun, startPage);
        var nextPage = await _importer.ImportAsync(startPage, PagesPerRun);
        context.JobDetail.JobDataMap["nextPage"] = nextPage;
        _logger.LogInformation("Update tags job ended. Next run will start at page {nextPage}", nextPage);
    }
}
