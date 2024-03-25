using FluentValidation;
using MakeMyResume.API.Configuration;
using MakeMyResume.Data;
using MakeMyResume.DTOs.FluentValidations;
using MakeMyResume.Services;
using MakeMyResume.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddValidatorsFromAssemblyContaining<ResumeDTOValidator>();

builder.Services.AddAutoMapper(typeof(MapperConfigurationProfile));

//Dependency injection
builder.Services.AddScoped<IResumeService, ResumeService>();
builder.Services.AddScoped<IDocumentGenerationService, DocumentGenerationService>();
builder.Services.AddScoped<ITagService, TagService>();
builder.Services.AddScoped<IAditionalSkillsService, AditionalSkillsService>();
builder.Services.AddScoped<IUploadService, UploadService>();

//DbContext
var connectionString = builder.Configuration.GetConnectionString("MyResume");
builder.Services.AddSqlServer<MakeMyResumeDb>(connectionString);

var blobStorage = builder.Configuration.GetConnectionString("AzureStorageSettings:ConnectionString");
var containerName = builder.Configuration.GetConnectionString("AzureStorageSettings:ContainerName");

const string? AllowLocalhostCORSPolicy = "AllowLocalhostCORSPolicy";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: AllowLocalhostCORSPolicy,
                      policy =>
                      {
                          policy.WithHeaders("Authorization", "Accept", "Referer", "User-Agent", "Content-Type");
                          policy.WithOrigins("http://localhost:4200");
                      });
});
builder.Services.AddMicrosoftIdentityWebApiAuthentication(builder.Configuration, "AzureAd");
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Migrate latest database changes during startup
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider
        .GetRequiredService<MakeMyResumeDb>();

}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(AllowLocalhostCORSPolicy);
app.MapControllers();
app.MapGet("/", () => "Make my Resume");

app.Run();

