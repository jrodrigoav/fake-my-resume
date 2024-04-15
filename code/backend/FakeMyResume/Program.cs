using FakeMyResume.Services.Interfaces;
using FakeMyResume.Services;
using FakeMyResume.API.Configuration;
using FakeMyResume.DTOs.FluentValidations;
using FluentValidation;
using FakeMyResume.Data;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddSpaStaticFiles(configure => configure.RootPath = "wwwroot");
    builder.Services.AddSwaggerGen();

    builder.Services.AddValidatorsFromAssemblyContaining<ResumeDTOValidator>();
    builder.Services.AddAutoMapper(typeof(MapperConfigurationProfile));

    builder.Services.AddScoped<IResumeService, ResumeService>();
    builder.Services.AddScoped<IDocumentGenerationService, DocumentGenerationService>();
    builder.Services.AddScoped<ITagService, TagService>();
    builder.Services.AddScoped<IAditionalSkillsService, AditionalSkillsService>();

    var connectionString = builder.Configuration.GetConnectionString("MyResume");
    builder.Services.AddSqlServer<MakeMyResumeDb>(connectionString);

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();

}
var app = builder.Build();
{
    app.UseDefaultFiles(new DefaultFilesOptions
    {
        DefaultFileNames = new List<string> { "index.html" }
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
    app.UseSpa(configuration => configuration.Options.DefaultPage = new Microsoft.AspNetCore.Http.PathString("/index.html"));

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.MapControllers();
}
app.Run();
