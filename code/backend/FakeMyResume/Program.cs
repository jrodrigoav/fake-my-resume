var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddSpaStaticFiles(configure => configure.RootPath = "wwwroot");
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
}
app.Run();
