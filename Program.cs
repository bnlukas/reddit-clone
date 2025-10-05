using Microsoft.AspNetCore.Mvc; 
using System;
using System.Net.Mime; 


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

app.MapGet("/api/threads", () => Thread); 
app.MapGet

