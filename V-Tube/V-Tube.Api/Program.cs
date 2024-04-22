using V_Tube.Api;
using V_Tube.Api.Middlewares;
using V_Tube.Application.DI_Container;
using V_Tube.Infrastructure.DI_Container;
using V_Tube.Persisitence.DI_Container;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApiServices(builder.Configuration)
    .AddPersisitenceServices(builder.Configuration)
    .AddInfraStructureServices(builder.Configuration).
    AddApplicationServices();
var app = builder.Build();
app.UseExceptionHandler(_=>{ });
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
