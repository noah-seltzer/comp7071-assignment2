using Microsoft.EntityFrameworkCore;
using Comp7071_A2.Areas.ManageCare.Data;
using Microsoft.Extensions.Options;
//using Comp7071_A2.Areas.ManageHumanResourcesAndPayroll.Data;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var secretConnection = builder.Configuration["Database:Connection"];

var defaultConnection = secretConnection ?? builder.Configuration.GetConnectionString("DefaultConnection");

Console.WriteLine(defaultConnection);

builder.Services.AddDbContext<CareManageMentDBContext>(options =>
    options.UseSqlServer(defaultConnection));


// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
