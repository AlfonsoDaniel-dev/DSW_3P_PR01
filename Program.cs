using dsw_3p_pr01.Repositories;
using dsw_3p_pr01.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddSingleton<IMissionRepository>(_ => new MissionJSONRepository("data/missions.json"));
builder.Services.AddScoped<IMissionService, MissionService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1.json", "Missions API v1");
    });
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

// ORM 
// dotnet add package Microsoft.EntityFrameworkCore.SqlServer
// dotnet add package Microsoft.EntityFrameworkCore.Design
// dotnet tool install --global dotnet-ef --version 10.0.*