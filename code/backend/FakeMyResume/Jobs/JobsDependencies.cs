using Quartz;
using Quartz.AspNetCore;

namespace FakeMyResume.Jobs;

public static class JobsDependencies
{
    public static void AddJobs(this IServiceCollection services)
    {
        services.AddQuartz(options =>
        {
            var udpdateTagsJobKey = JobKey.Create(nameof(UpdateTagsJob));
        options
            .AddJob<UpdateTagsJob>(udpdateTagsJobKey)
            .AddTrigger(trigger => trigger.ForJob(udpdateTagsJobKey)
                .WithCronSchedule("0 0 0 * * ?"));
        });

        // ASP.NET Core hosting
        services.AddQuartzServer(options =>
        {
            // when shutting down we want jobs to complete gracefully
            options.WaitForJobsToComplete = true;
        });
    }
}
