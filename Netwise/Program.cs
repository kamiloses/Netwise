using Netwise.Middlewares;
using Netwise.Services;
using Netwise.Services.Impl;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddScoped<ICatFactService, CatFactServiceImpl>(); //todo sprawdz czy scope czy inny
        builder.Services.AddHttpClient<IHttpClientWrapper, HttpClientWrapper>();
        builder.Services.AddControllers();
        var app = builder.Build();
        app.UseMiddleware<ExceptionHandlerMiddleware>();
        app.MapControllers();


        app.Run();

    }
}