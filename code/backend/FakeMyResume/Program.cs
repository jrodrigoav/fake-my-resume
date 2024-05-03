using FakeMyResume.Services.Interfaces;
using FakeMyResume.Services;
using FakeMyResume.API.Configuration;
using FakeMyResume.Data;
using Microsoft.Identity.Web;
using FakeMyResume.Models;
using FakeMyResume.Jobs;
using System.Net;
using Quartz;
using OpenAI.Extensions;

const string AllowLocalhostCORSPolicy = "AllowLocalhostCORSPolicy";

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddSpaStaticFiles(configure => configure.RootPath = "wwwroot");
    builder.Services.AddSwaggerGen();

    builder.Services.AddAutoMapper(typeof(MapperConfigurationProfile));

    builder.Services.AddScoped<IResumeService, ResumeService>();
    builder.Services.AddScoped<IDocumentGenerationService, DocumentGenerationService>();
    builder.Services.AddScoped<ITagService, TagService>();
    builder.Services.AddScoped<IUserService, UserService>();
    builder.Services.AddScoped<ITextService, TextService>();

    // Jobs
    builder.Services.Configure<QuartzOptions>(builder.Configuration.GetSection(nameof(QuartzOptions)));
    builder.Services.Configure<QuartzOptions>(options =>
    {
        options.Scheduling.IgnoreDuplicates = true;
        options.Scheduling.OverWriteExistingData = true;
    });

    builder.Services.AddScoped<ImporterService>();
    builder.Services.AddHttpClient<ImporterService>().ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
    {
        AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
    });

    builder.Configuration.AddUserSecrets<StackExchangeSettings>();
    builder.Services.Configure<StackExchangeSettings>(builder.Configuration.GetSection(nameof(StackExchangeSettings)));
    builder.Services.AddJobs();

    var connectionString = builder.Configuration.GetConnectionString("MyResume");
    builder.Services.AddSqlServer<MakeMyResumeDb>(connectionString);
    var tagsConnectionString = builder.Configuration.GetConnectionString("Tags");
    builder.Services.AddSqlite<TagsDbContext>(tagsConnectionString);
    builder.Services.AddCors(options =>
    {
        options.AddPolicy(
            name: AllowLocalhostCORSPolicy,
            policy =>
            {
                policy.AllowAnyMethod();
                policy.WithHeaders("Authorization", "Accept", "Referer", "User-Agent", "Content-Type");
                policy.WithOrigins("http://localhost:4200");
            });
    });
    builder.Services.AddMicrosoftIdentityWebApiAuthentication(builder.Configuration, "AzureAd");

    builder.Services.AddOpenAIService();

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();

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
    app.MapGet("/api", () => "FakeMyResume");
    app.UseSpa(configuration => configuration.Options.DefaultPage = new PathString("/index.html"));

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseCors(AllowLocalhostCORSPolicy);
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();
}
app.Run();
