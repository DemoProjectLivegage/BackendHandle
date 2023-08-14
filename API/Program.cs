
using System.Text;
using Microsoft.EntityFrameworkCore;
using Persistence;

using Spire.Xls;
// using  Microsoft.Extensions.Configuration.IConfiguration;
using Domain;

using MediatR;
using Application.Borrower;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DatabaseContext>(opt =>
{
    opt.UseMySql(builder.Configuration.GetConnectionString("DefaultSqlConnection"), new MySqlServerVersion(new Version(8, 0, 11)));
});
//builder.Services.AddScoped<ICSVService , CSVService>();
builder.Services.AddMediatR(typeof(List.Handler));
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

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;

try
{
    var context = services.GetRequiredService<DatabaseContext>();
    await context.Database.MigrateAsync();
    await Seed.SeedData(context);
    var borrower = context.BorrowersDetails;
    var logger = services.GetRequiredService<ILogger<Program>>();
    foreach (var ids in borrower)
    {
        int pk = ids.BorrowerId;

        logger.LogInformation($"Primary key value: {pk}");
    }
}
catch (Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error");
}

app.Run();


