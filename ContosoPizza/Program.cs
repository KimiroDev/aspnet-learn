using System.Diagnostics;
using ContosoPizza.Data;
using ContosoPizza.Services;

using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<PizzaContext>(options =>
    options.UseSqlite("Data Source=ContosoPizza.db"));

builder.Services.AddScoped<PizzaService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.MapPost("/deploy", async (HttpRequest request) =>
{
    var psi = new ProcessStartInfo
    {
        FileName = "/bin/bash",
        Arguments = "/home/student/deploy.sh",
        RedirectStandardOutput = true,
        RedirectStandardError = true,
        UseShellExecute = false
    }; 

    var p = Process.Start(psi);
    var output = await p.StandardOutput.ReadToEndAsync();
    var error  = await p.StandardError.ReadToEndAsync();

    await File.WriteAllTextAsync("/tmp/deploy.out", output);
    await File.WriteAllTextAsync("/tmp/deploy.err", error);

    return Results.Ok("Deploy triggered");
});

app.Run();
