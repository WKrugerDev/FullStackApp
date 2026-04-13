// Configure application services and middleware pipeline
var builder = WebApplication.CreateBuilder(args);

// Add CORS policy to allow requests from any origin (front-end Blazor app)
// In production this should be restricted to specific origins
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

// Add response caching service to reduce server load on repeated requests
builder.Services.AddResponseCaching();

var app = builder.Build();

// Apply CORS middleware before any endpoint handling
app.UseCors("AllowAll");

// Enable response caching middleware
app.UseResponseCaching();

// Minimal API endpoint returning product list with nested category objects
// In production this would be replaced by a database call via a service/repository layer
// CacheOutput() caches the response to minimize repeated processing
app.MapGet("/api/productlist", () =>
{
    return new[]
    {
        new
        {
            Id = 1,
            Name = "Laptop",
            Price = 1200.50,
            Stock = 25,
            Category = new { Id = 101, Name = "Electronics" }
        },
        new
        {
            Id = 2,
            Name = "Headphones",
            Price = 50.00,
            Stock = 100,
            Category = new { Id = 102, Name = "Accessories" }
        }
    };
}).CacheOutput();

app.Run();