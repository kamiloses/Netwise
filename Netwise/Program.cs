using Netwise.Dto;
using Netwise.Middlewares;
using Netwise.Services;
using Netwise.Services.Impl;

namespace Netwise;
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddScoped<IFileStorageService, FileStorageService>(); 
        builder.Services.AddScoped<ICatFactService, CatFactServiceImpl>();
        builder.Services.AddHttpClient<IHttpClientWrapper, HttpClientWrapper>();
        builder.Services.Configure<FilePathSettings>(builder.Configuration.GetSection("FileLocation"));
        builder.Services.AddControllers();
        var app = builder.Build();
        app.UseMiddleware<ExceptionHandlerMiddleware>();
        app.MapControllers();


        app.Run();

    }
}