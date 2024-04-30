using Quartz;
using Quartz.AspNetCore;
using System.Configuration;

namespace FakeMyResume.Jobs;

public static class JobsDependencies
{
    public static void AddJobs(this IServiceCollection services)
    {
        services.AddQuartz(options =>
        {
            var udpdateTagsJobKey = JobKey.Create(nameof(UpdateTagsJob));
            options.AddJob<UpdateTagsJob>(udpdateTagsJobKey, (builder) =>
                {
                    builder.UsingJobData("nextPage", 1);
                })
                .AddTrigger(trigger => trigger.WithIdentity("DailyTrigger").WithCronSchedule("0 0 3,5,7 * * ?").ForJob(udpdateTagsJobKey));
        });

        // ASP.NET Core hosting
        services.AddQuartzServer(options =>
        {
            // when shutting down we want jobs to complete gracefully
            options.WaitForJobsToComplete = true;
        });
    }
}
