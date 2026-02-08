using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using URLShorteningService;
using URLShorteningService.Models;
using URLShorteningService.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<URLShortenerDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("dbConnection")
    )
);
builder.Services.AddScoped<IUrlShortenerService, UrlShortenerService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontendPolicy", policy =>
    {
        policy
            .WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();
app.UseCors("FrontendPolicy");



app.MapGet("/getOriginalUrl/{shortUrl}", async (string shortUrl, IUrlShortenerService service, HttpContext httpContext) =>
{
    var OriginalUrl = await service.ReturnUrlEntity(shortUrl, httpContext);
    if (OriginalUrl == null)
        return Results.NotFound();

    return Results.Ok(OriginalUrl);
});

app.MapGet("{shortUrl}", async (string shortUrl, IUrlShortenerService service, HttpContext httpContext) =>
{
    var OriginalUrl = await service.ReturnUrlEntity(shortUrl, httpContext);
    if (OriginalUrl == null)
        return Results.NotFound();

    return Results.Redirect(OriginalUrl);
});

app.MapPost("/getUser", async ([FromBody]LoginDTO loginDto, IUserService service) =>
{
    var user = await service.GetUserByEmail(loginDto.Email);
    if (user == null)
        return Results.NotFound();

    return Results.Ok(user);
});
app.MapGet("/GetUrlEntities", async ([FromHeader(Name = "X-Api-Key")] string apiKey, IUrlShortenerService service) =>
{
    return await service.ReturnUrlEntities(apiKey);
});

app.MapGet("/GetUrlEntityClicks/{id:int}", async (int id, IUrlShortenerService service) =>
{
    return await service.ReturnUrlEntityClicks(id);
}); 

app.MapPost("/api/urls", async (
    UpsertUrlEntityDTO dto,
    [FromHeader(Name = "X-Api-Key")] string apiKey, IUrlShortenerService service, HttpContext httpContext) =>
{
    var response = await service.SaveUrlEntity(dto, apiKey);
    return Results.Ok(response);
});

app.MapPut("/api/urls/{id:int}", async (
    UpsertUrlEntityDTO dto,
    [FromHeader(Name = "X-Api-Key")] string apiKey, int id, IUrlShortenerService service, HttpContext httpContext) =>
{
    var response = await service.UpdateUrlEntity(id, dto, apiKey);
    return Results.Ok(response);
});

app.MapGet("/", () => "Hello World!");

app.Run();
