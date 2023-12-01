using Microsoft.AspNetCore.Cors.Infrastructure;
using System.Text.Json.Serialization;
using sample_crm.Application;
using sample_crm.Data;
using sample_crm.API;
using System.IdentityModel.Tokens.Jwt;

// avoid default claim mapping (claim type email)
JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

// Data and Application layers
builder.Services
    .AddDataLayer(builder.Configuration)
    .AddApplicationLayer(builder.Configuration);

builder.Services.AddJwt(builder.Configuration);

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
