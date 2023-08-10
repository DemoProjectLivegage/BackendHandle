
using System.Text;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Spire.Pdf.Exporting.XPS.Schema;
using Spire.Xls;
// using  Microsoft.Extensions.Configuration.IConfiguration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DatabaseContext>(opt=>{
    opt.UseMySql(builder.Configuration.GetConnectionString("DefaultSqlConnection"),new MySqlServerVersion(new Version(8,0,11)));
});
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

using var scope=app.Services.CreateScope();
var services=scope.ServiceProvider;

 try{
var context=services.GetRequiredService<DatabaseContext>();
    context.Database.Migrate();
}
 catch(Exception ex )
 {
var logger=services.GetRequiredService<ILogger<Program>>();
     logger.LogError(ex, "An error");
 }

Workbook workbook =new Workbook();
workbook.LoadFromFile("C:/Users/IMehta/Downloads/Loan_data.xlsx");
Worksheet sheet=workbook.Worksheets[0];

sheet.SaveToFile("ExceltoTxt.txt"," ,",Encoding.UTF8);
app.Run();
