using Microsoft.Extensions.Options;
using Netwise.Dto;

namespace Netwise.Services.Impl;

public class FileStorageService : IFileStorageService
{
    private readonly ILogger<FileStorageService> _logger;

    private readonly string _filePath;
    
    
    public FileStorageService(ILogger<FileStorageService> logger, IOptions<FilePathSettings> options)
    {
        _logger = logger;
        _filePath = options.Value.FilePath;
    }


    public async Task  SaveToFile(string fact)
    {

        if (!File.Exists(_filePath))
        {
            _logger.LogInformation($"Creating file {_filePath}");
            
            await File.WriteAllTextAsync(_filePath, fact + Environment.NewLine);
            
           _logger.LogInformation($"Appended fact to file {_filePath}");
        }
        else
        {
            await File.AppendAllTextAsync(_filePath, fact + Environment.NewLine);
            
            _logger.LogInformation($"Appended to existing file {_filePath}");
        }
    }
}