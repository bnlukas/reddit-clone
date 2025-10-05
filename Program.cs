using Microsoft.AspNetCore.Mvc; 
using System;
using System.Net.Mime;
using Microsoft.AspNetCore.Http.HttpResults;


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

var app = builder.Build();
app.UseCors(AllowCors);

//______ hent alle 
app.MapGet("/api/threads", () => Thread);

//______ Henter specifik opgave med validering 
app.MapGet("/api/threads/{id}", (int id) =>
{
    if (id < 0)
        return BadRequest();

    if (id >= Thread.Count)
        return Results.NotFound();

    return Results.Ok(Thread[id]);
});

//______ Tilføjer ny threadscontent 
app.MapPost("/api/tasks/{id}", (int id, ThreadsContent newThreadsContent) =>
{
    ThreadsContent.Add(id, newThreadsContent);
    return Results.Created($"/api/threads/{ThreadsContent.Count - 1}", newThreadsContent);
}); 

//______ opdatere en thread
app.MapPut("/api/threads/{id}", (int id, xxxxx opdateret) =>
{
    if (id < 0 || id >= ThreadsContent.Count)
        return Results.NotFound(new { error = "ID not found" });

    ThreadsContent[id] = opdateret;
    return Results.Ok(opdateret);
}); 


