using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using Dapr;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Dapr will send serialized event object vs. being raw CloudEvent
app.UseCloudEvents();

// needed for Dapr pub/sub routing
app.MapSubscribeHandler();

// Dapr subscription in [Topic] routes orders topic to this route
app.MapPost("/process", [Topic("pubsub", "sltest")] (TestMesage message) => {
    Console.WriteLine("Subscriber received : " + message);
    return Results.Ok(message);
});

app.Run();


public record TestMesage(string Message, int Counter);