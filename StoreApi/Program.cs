using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.EntityFrameworkCore;
using StoreApi.Database;
using StoreApi.Hangfire.FakeApiSync;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
// TODO: Move to sql DB
builder.Services.AddDbContext<StoreDbContext>(options => options.UseInMemoryDatabase("StoreDB"));
// TODO: Move to sql DB
builder.Services.AddHangfire(options => options.UseMemoryStorage());
builder.Services.AddHttpClient();
builder.Services.AddHangfireServer();
builder.Services.AddHostedService<BackgroundWorker>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
