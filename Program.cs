using dsw_3p_pr01.Data;
using dsw_3p_pr01.Repositories;
using dsw_3p_pr01.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddControllers();

builder.Services.AddDbContext<AppDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerConnection")));

var jwt = builder.Configuration.GetSection("Jwt");
var key = Convert.FromBase64String(
    jwt["Key"] ?? throw new InvalidOperationException("Falta configurar Jwt:Key"));

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = jwt["Issuer"],
            ValidateAudience = true,
            ValidAudience = jwt["Audience"],
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
    });

// configuración de CORS para permitir solicitudes desde el frontend
builder.Services.AddCors(options =>
{

    options.AddPolicy("QuestLogFrontend", policy =>
    {
        policy.WithOrigins(
                "http://localhost:5500",
                "http://127.0.0.1:5500"
            )
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});
builder.Services.AddAuthorization();

builder.Services.AddScoped<IMissionRepository, MissionSqlRepository>();
builder.Services.AddScoped<IMissionService, MissionService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();                       // genera /openapi/v1.json
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "Missions API v1");
    });
}

app.UseHttpsRedirection();
app.UseCors("QuestLogFrontend");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
