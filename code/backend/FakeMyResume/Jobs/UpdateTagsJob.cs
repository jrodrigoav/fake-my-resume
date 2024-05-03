using FakeMyResume.Services;
using Quartz;

namespace FakeMyResume.Jobs;

[PersistJobDataAfterExecution]
internal class UpdateTagsJob(ImporterService importer, ILogger<UpdateTagsJob> logger) : IJob
{
    private static readonly int PagesPerRun = 50;

    async Task IJob.Execute(IJobExecutionContext context)
    {
        var startPage = (int)(long)context.JobDetail.JobDataMap.Get("nextPage");
        logger.LogInformation("Update tags job started for pages the next {PagesPerRun} starting at {startPage}", PagesPerRun, startPage);
        var nextPage = await importer.ImportAsync(startPage, PagesPerRun);
        context.JobDetail.JobDataMap["nextPage"] = nextPage;
        var nextRun = context.Trigger.GetNextFireTimeUtc();
        logger.LogInformation("Update tags job ended. Next run will start at page {nextPage}, the next run will be at {nextRun} (UTC)", nextPage, nextRun);
    }
}
