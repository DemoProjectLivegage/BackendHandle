
using System.Text;
using Microsoft.EntityFrameworkCore;
using Persistence;
using FluentValidation.AspNetCore;
using MediatR;
using Application.Borrower;
using FluentValidation;
using Application.DataStructures;
using Domain;

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
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<LoanTypes>();
builder.Services.AddValidatorsFromAssemblyContaining<Benificiary>();

builder.Services.AddCors(opt=>{

    opt.AddPolicy("CorsPolicy",ploicy=>{

        ploicy.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:3000");

    });

});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("CorsPolicy");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;

try
{
    var context = services.GetRequiredService<DatabaseContext>();
    await context.Database.MigrateAsync();
}
catch (Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error");
}
app.Run();