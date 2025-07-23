using Netwise.Middlewares;
using Netwise.Services;
using Netwise.Services.Impl;

var builder = WebApplication.CreateBuilder(args);
//builder.Services.AddScoped<CatFactService,CatFactServiceImpl>();//todo sprawdz czy scope czy inny
builder.Services.AddScoped<CatFactServiceImpl>();
builder.Services.AddHttpClient<CatFactService>();
builder.Services.AddControllers();
var app = builder.Build();
app.UseMiddleware<ExceptionHandlerMiddleware>();
app.MapControllers();


app.Run();