using FakeMyResume.Models;
using FakeMyResume.Services;
using FakeMyResume.Services.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using OpenAI.Extensions;
using Quartz;
using Quartz.Impl.AdoJobStore;
using System.Net;

const string AllowLocalhostCORSPolicy = "AllowLocalhostCORSPolicy";

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddSpaStaticFiles(configure => configure.RootPath = "wwwroot");

    builder.Services.AddDbContext<FakeMyResumeDbContext>(configure => configure.UseSqlite(builder.Configuration.GetConnectionString("FakeMyResumeDB")!));
    builder.Services.AddScoped<ResumeService>();
    builder.Services.AddScoped<DocumentGenerationService>();
    builder.Services.AddScoped<TagService>();
    builder.Services.AddScoped<UserService>();
    builder.Services.AddScoped<TextService>();

    builder.Services.AddQuartz(q =>
    {        
        q.UsePersistentStore(c =>
        {            
            c.UseNewtonsoftJsonSerializer();
            c.UseMicrosoftSQLite(builder.Configuration.GetConnectionString("FakeMyResumeDB")!);
        });
        var jobKey = new JobKey("TagImporterService");
        q.AddJob<ImporterService>(opts => opts.WithIdentity(jobKey).DisallowConcurrentExecution(true).RequestRecovery(false));
        q.AddTrigger(opts => opts.ForJob(jobKey).WithIdentity("TagImporterService-trigger").WithCronSchedule("0 0/5 * ? * *"));
    });
    
    builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
    
    builder.Services.AddHttpClient<ImporterService>().ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
    {
        AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
    });

    
    builder.Services.Configure<StackExchangeSettings>(builder.Configuration.GetSection(nameof(StackExchangeSettings)));


    builder.Services.AddCors(options =>
    {
        options.AddPolicy(
            name: AllowLocalhostCORSPolicy,
            policy =>
            {
                policy.AllowAnyMethod();
                policy.WithHeaders("Authorization", "Accept", "Referer", "User-Agent", "Content-Type");
                policy.WithOrigins("https://localhost:7121");
            });
    });
    builder.Services.AddMicrosoftIdentityWebApiAuthentication(builder.Configuration, "AzureAd");

    builder.Services.AddOpenAIService();

    builder.Services.AddControllers();

}
var app = builder.Build();
{
    app.UseDefaultFiles(new DefaultFilesOptions
    {
        DefaultFileNames = ["index.html"]
    });

    const string cacheMaxAge = "720";
    app.UseStaticFiles(new StaticFileOptions
    {
        OnPrepareResponse = ctx =>
        {
            ctx.Context.Response.Headers.Append(
                "Cache-Control", $"public, max-age={cacheMaxAge}");
        }
    });

    app.UseCors(AllowLocalhostCORSPolicy);
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();
    app.MapGet("/api", () => "FakeMyResume");
    app.UseSpa(configuration => configuration.Options.DefaultPage = new PathString("/index.html"));
}
app.Run();
