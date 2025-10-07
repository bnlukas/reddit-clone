using Microsoft.AspNetCore.Mvc; 
using System;
using System.Net.Mime;
using Microsoft.AspNetCore.Http.HttpResults;
using RedditClone.Data;
using RedditClone.Models;
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
using (var scope = app.Services.CreateScope())
{
    var service = scope.ServiceProvider.GetService<DataService>();
    service?.SeedData();
}

app.UseCors(AllowCors);

//______ hent alle 
app.MapGet("/api/threads", (DataService service) => 
    service.GetAllThreads());

//______ Henter specifik thread 
app.MapGet("/api/threads/{id}", async (DataService service, int id) =>
{
    var thread = await service.GetThreads(id);
    return thread != null 
        ? Results.Ok(thread) : Results.NotFound();
}); 

//_____ opret ny thread 
app.MapPost("/api/threads", async (DataService service, ThreadsModel newThread) =>
{
    var createdThread = await service.CreateThread(newThread);
    return Results.Created($"/api/threads/{createdThread.ThreadsId}", createdThread);
});

//____ skriver en kommentar 
app.MapPost("/api/threads/{id}/comments", async (DataService service, int id, ThreadsCommentsModel newComment) =>
{
    var comment = await service.AddComment(id, newComment.Content, newComment.AuthorName);
    return Results.Ok(comment);
});


//_____ upvote
app.MapPost("/api/threads/{id}/upvote", async (DataService service, int id) =>
{
    var succes = await service.VoteThread(id, DataService.VoteType.Up);
    return succes ? Results.Ok() : Results.NotFound();
});

//_____ downvote
app.MapPost("/api/threads/{id}/downvote", async (DataService service, int id) =>
{
    var succes = await service.VoteThread(id, DataService.VoteType.Down);
    return succes ? Results.Ok() : Results.NotFound();
});

//_____ vote on comment 
app.MapPost("/api/threads/{id}/upvotecomments", async (DataService service, int commentId) =>
{
    var succes = await service.VoteComment(commentId, DataService.VoteType.Up);
    return succes ? Results.Ok() : Results.NotFound();
});

//_____ vote on comment 
app.MapPost("/api/threads/{id}/downvotecomments", async (DataService service, int commentId) =>
{
    var succes = await service.VoteComment(commentId, DataService.VoteType.Down);
    return succes ? Results.Ok() : Results.NotFound();
});

app.Run();
