using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.ModelBuilder;
using SampleWebApplication1.Data;
using SampleWebApplication1.Models;
using SampleWebApplication1.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add DbContext with InMemory database
builder.Services.AddDbContext<TodoContext>(options =>
    options.UseInMemoryDatabase("TodoDb"));

// Configure OData
var modelBuilder = new ODataConventionModelBuilder();
modelBuilder.EntitySet<Todo>("Todos");

builder.Services.AddControllers()
    .AddOData(options => options
        .Select()
        .Filter()
        .OrderBy()
        .Expand()
        .Count()
        .SetMaxTop(100)
        .AddRouteComponents("odata", modelBuilder.GetEdmModel()));

builder.Services.Configure<AzureAIFoundryOptions>(
    builder.Configuration.GetSection(AzureAIFoundryOptions.SectionName));
builder.Services.AddHttpClient<IChatbotService, AzureFoundryChatService>();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Ensure database is created and seeded
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<TodoContext>();
    context.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// Only use HTTPS redirection when HTTPS port is properly configured
// In Docker, Kestrel handles this via environment variables
if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("ASPNETCORE_HTTPS_PORTS")))
{
    app.UseHttpsRedirection();
}

// Serve static files from wwwroot
app.UseDefaultFiles();
app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
