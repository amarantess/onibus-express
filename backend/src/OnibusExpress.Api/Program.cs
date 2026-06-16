var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthorization();
app.MapGet("/", () => Results.Ok(new
{
    Service = "OnibusExpress.Api",
    Status = "Healthy",
    UtcNow = DateTime.UtcNow
}));
app.MapControllers();

app.Run();
