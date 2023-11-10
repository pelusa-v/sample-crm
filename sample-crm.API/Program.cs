using Microsoft.AspNetCore.Cors.Infrastructure;
using sample_crm.Application;
using sample_crm.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Data and Application layers
builder.Services
    .AddDataLayer(builder.Configuration)
    .AddApplicationLayer(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// cors
app.UseCors(corsPolicyBuilder =>
    corsPolicyBuilder
        .AllowAnyOrigin()
        .AllowAnyHeader()
    );

app.UseAuthorization();

app.MapControllers();

app.Run();
