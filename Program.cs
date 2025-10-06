using Microsoft.AspNetCore.Mvc; 
using System;
using System.Net.Mime;
using Microsoft.AspNetCore.Http.HttpResults;
using RedditClone.Data;
using RedditClone.Service;


var builder = WebApplication.CreateBuilder(args);
var AllowCors = "_AllowCors";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: AllowCors, builder =>
    {
        builder.AllowAnyHeader()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.Services.AddScoped<DataService>();
builder.Services.AddDbContext<RedditContent>(); 

var app = builder.Build();
app.UseCors(AllowCors);

//______ hent alle 
app.MapGet("/api/threads", async (DataService service) => service.GetAllThreads());

//______ Henter specifik opgave med validering 
app.MapGet("/api/threads/{id}", async (DataService service) =>
{
    var thread = await service.GetThreads(threadsId);
    return thread != null ? 
        Results.Ok(thread) : Results.NotFound();
}); 


//______ Tilføjer ny threadscontent 
app.MapPost("/api/tasks/{id}", (DataService service) => service.CreateThread());

//______ opdatere en thread
app.MapPut("/api/threads/{id}", (int id, xxxxx opdateret) =>
{
    if (id < 0 || id >= ThreadsContent.Count)
        return Results.NotFound(new { error = "ID not found" });

    ThreadsContent[id] = opdateret;
    return Results.Ok(opdateret);
}); 


