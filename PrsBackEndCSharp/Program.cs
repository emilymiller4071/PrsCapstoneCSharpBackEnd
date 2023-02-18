
using Microsoft.EntityFrameworkCore;
using PrsBackEndCSharp.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<PrsContext>(
    // lambda
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("PrsConnectionString"))
    );


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
app.UseCors("*");

app.UseAuthorization();

app.MapControllers();

app.Run();
