// This file sets up the ASP.NET Core web application, enabling Razor Pages and serving static files.
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// This line registers Razor Pages services, allowing the application to process .cshtml files.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
// In development environment, use the developer exception page for detailed errors.
if (!app.Environment.IsDevelopment())
{
    // In production, use a more user-friendly error page.
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// Redirects HTTP requests to HTTPS for security.
app.UseHttpsRedirection();
// Enables serving static files (HTML, CSS, JavaScript, images) from the wwwroot folder.
app.UseStaticFiles();

// Configures routing for the application.
app.UseRouting();

// Enables authorization middleware (if you were to implement authentication/authorization).
app.UseAuthorization();

// Maps Razor Pages to their respective routes.
app.MapRazorPages();

// Starts the application.
app.Run();

